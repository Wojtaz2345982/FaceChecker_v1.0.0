from flask import Flask, request, jsonify
import face_recognition
import base64
from PIL import Image, ImageDraw
import io
import numpy as np
import pyodbc
import hashlib

unrecognized_faces_encodings = {}
unrecognized_faces_counters = {}
class_data = {}
unrecognized = {}

FACE_SIMILARITY_THRESHOLD = 0.6
DATABASE_CONNECTION_STRING = 'DRIVER={ODBC Driver 17 for SQL Server};Server=127.0.0.1;Database=master;Port=1433;UID=SA;PWD=LIUGgfds35DP9OAR;'

app = Flask(__name__)

'''
Endpoint /class -> tutaj wysyłane są zapytania zawierające EncodedName klasy.
Za jego pośrednictwem z bazy danych pobierane są zdjęcia uczniów należących do danej klasy
'''
@app.route('/class', methods=['POST'])
def get_faces():
    global unrecognized, unrecognized_faces_encodings,unrecognized_faces_counters
    unrecognized_faces_encodings = {}
    unrecognized_faces_counters = {}
    unrecognized = {}
    try:
        data = request.json
        klasa = data.get('klasa')
        
        unrecognized[klasa] = []                          #wyczyszczenie listy nierozpoznanych osób
        unrecognized_faces_encodings[klasa] = {}          #w danej klasie
        unrecognized_faces_counters[klasa] = {}
        
        with pyodbc.connect(DATABASE_CONNECTION_STRING) as data_base:
            cursor = data_base.cursor()
            query = "SELECT s.ImageUrl, s.StudentKey FROM Students s INNER JOIN Classes c ON s.ClassId = c.Id WHERE c.EncodedName = ?"                         #zapytanie do bazy danych -> pobranie zdjęć uczniów
            rows = cursor.execute(query, klasa).fetchall()

            current_student_keys = [row.StudentKey for row in rows]

            class_data[klasa] = {
                "known_faces_encodings": [],
                "known_faces_ids": []
            }

            for row in rows:
                if row.ImageUrl is None or row.StudentKey in class_data[klasa]["known_faces_ids"]:
                    continue
                photo_data = bytes(f"{row.ImageUrl}", "utf-8").decode("unicode_escape").encode("latin1")
                image = Image.open(io.BytesIO(photo_data[2:]))
                jpg_image = np.array(image)
                face_encodings = face_recognition.face_encodings(jpg_image)
                if face_encodings:
                    class_data[klasa]["known_faces_encodings"].append(face_encodings[0])
                    class_data[klasa]["known_faces_ids"].append(row.StudentKey)
                    
		#zwrócenie listy osób w klasie, których zdjęcia są w bazie danych
        return jsonify({"message": class_data[klasa]["known_faces_ids"]})

    except Exception as e:
        return jsonify({"Error": str(e)})


'''
Endpoint /recognize -> wysyłane są do niego zapytania zawierające encoded_name klasy oraz zdjęcia w
postaci ciągu bajtów. Odpowiada za rozpoznawanie uczniów oraz osób nienależących do danej klasy.
'''
@app.route('/recognize', methods=['POST'])
def recognize_faces():
    global unrecognized_faces_encodings, unrecognized_faces_counters
    try:
        data = request.json
        klasa = data.get('klasa', None)
        image_base64 = data['image'] 
        if not klasa or klasa not in class_data:
            return jsonify({'Error': 'Nieznana klasa lub klasa nie została podana'})

        known_faces_encodings = class_data[klasa]["known_faces_encodings"]
        known_faces_names = class_data[klasa]["known_faces_ids"]
        
        #wczytanie i preprocessing otrzymanego zdjęcia (klatki z kamery) 
        image_data = base64.b64decode(image_base64)
        image = Image.open(io.BytesIO(image_data))
        draw = ImageDraw.Draw(image)
        frame = np.array(image)

        if frame.shape[-1] == 4:  
            frame = frame[..., :3]

        face_locations = face_recognition.face_locations(frame)
        frame_encodings = face_recognition.face_encodings(frame, face_locations)
        recognized_list = []
        faces_with_boxes = False
        
        #pętla rozpoznająca każdą twarz na zdjęciu
        for index, face_encoding in enumerate(frame_encodings):
            matches = face_recognition.compare_faces(known_faces_encodings, face_encoding)
            face_distances = face_recognition.face_distance(known_faces_encodings, face_encoding)
            best_match_index = np.argmin(face_distances)
            
            #dopasowano twarz do członka klasy
            if matches[best_match_index]:
                name = known_faces_names[best_match_index]
                recognized_list.append(name)
                
            #znaleziono osobę nie należącą do klasy
            else:
                similar_face_found = False
                for saved_face_hash, saved_face_encoding in unrecognized_faces_encodings[klasa].items():
                    distance = face_recognition.face_distance([saved_face_encoding], face_encoding)
                    if distance < FACE_SIMILARITY_THRESHOLD:
                        similar_face_found = True
                        unrecognized_faces_counters[klasa][saved_face_hash] += 1
                        
                        #rysowanie obramowania wokół twarzy osoby spoza klasy
                        if unrecognized_faces_counters[klasa][saved_face_hash] == 2:
                            top, right, bottom, left = face_locations[index]
                            draw.rectangle(((left, top), (right, bottom)), outline="red", width=2)
                            faces_with_boxes = True
                        break
                #dodanie twarzy do listy nierozpoznanych osób        
                if not similar_face_found:
                    new_face_hash = hashlib.sha256(face_encoding.tobytes()).hexdigest()
                    unrecognized_faces_encodings[klasa][new_face_hash] = face_encoding
                    unrecognized_faces_counters[klasa][new_face_hash] = 1
                recognized_list.append("Nieznany")

        response_data = {'recognized_faces': recognized_list}
        
        #zamiana zdjęcia na ciąg bajtów
        if faces_with_boxes:
            buffered = io.BytesIO()
            image.save(buffered, format="JPEG")
            encoded_image_with_boxes = base64.b64encode(buffered.getvalue()).decode("utf-8")
            response_data['unrecognized_faces'] = encoded_image_with_boxes

        return jsonify(response_data)

    except Exception as e:
        return jsonify({'Error': str(e)})


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, threaded=True,
            ssl_context=('/home/mati7v2/fullchain.pem', '/home/mati7v2/privkey.pem'))

# API do rozpoznawania twarzy

Poniżej znajduje się plik README dla API rozpoznającego twarze, będącego częścią aplikacji przeznaczonej do weryfikacji obecności uczniów na zajęciach.Poniżej znajdziesz podstawowe informacje o konfiguracji i instrukcje użytkowania.

## Funkcje

- Odbiera zakodowane nazwy klas, aby pobrać zdjęcia uczniów z bazy danych.
- Rozpoznaje uczniów na podstawie dostarczonego obrazu za pomocą rozpoznawania twarzy.
- Oznacza twarze nierozpoznane i rozróżnia między osobami znanymi a nieznanymi.

## Wymagania wstępne

Zanim zaczniesz, upewnij się, że spełniasz następujące wymagania:

- Python w wersji 3.6 lub wyższej
- Framework webowy Flask
- Biblioteka face_recognition
- NumPy
- Pillow 
- PyODBC
- Sterownik ODBC dla SQL Server
- Serwer SQL z tabelami 'Students' i 'Classes'

## Instalacja

Aby zainstalować niezbędne biblioteki, wykonaj poniższe polecenia:

```bash
pip install Flask
pip install face_recognition
pip install numpy
pip install Pillow
pip install pyodbc
```

## Konfiguracja

Edytuj `DATABASE_CONNECTION_STRING`, aby wskazać instancję Twojego serwera SQL i dostarczyć odpowiednie dane uwierzytelniające.

```python
DATABASE_CONNECTION_STRING = 'DRIVER={ODBC Driver 17 for SQL Server};Server=TWÓJ_SERWER;Database=Twoja_Baza_Danych;Port=TWÓJ_PORT;UID=TWÓJ_UID;PWD=TWOJE_HASŁO;'
```

Ustaw `FACE_SIMILARITY_THRESHOLD` według potrzeb, aby dostosować czułość rozpoznawania twarzy.

## Uruchamianie Aplikacji

Aby uruchomić serwer, przejdź do katalogu zawierającego aplikację i uruchom:

```bash
python app.py
```

## Użycie

Aplikacja udostępnia dwa punkty końcowe:

### /class

Ten punkt końcowy akceptuje żądania POST z ładunkiem JSON zawierającym klucz 'klasa', który reprezentuje zakodowaną nazwę klasy.

```json
{
    "klasa": "zakodowana_nazwa_klasy"
}
```

Pobiera on obrazy uczniów należących do danej klasy z bazy danych i przygotowuje je do rozpoznania.

### /recognize

Ten punkt końcowy również akceptuje żądania POST z ładunkiem JSON zawierającym klucze 'klasa' i 'image', gdzie 'image' to zakodowany w base64 ciąg reprezentujący zdjęcie do przetworzenia.

```json
{
    "klasa": "zakodowana_nazwa_klasy",
    "image": "zakodowany_w_base64_obraz"
}
```

Odpowiada on za rozpoznawanie uczniów i wykrywanie osób nie należących do klasy.

## Konfiguracja SSL

Aby uruchomić aplikację z SSL, podaj ścieżkę do plików `fullchain.pem` i `privkey.pem` w argumencie `ssl_context` metody `app.run()`.

---

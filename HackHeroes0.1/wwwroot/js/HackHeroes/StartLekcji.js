let remainingTime = 5 * 60; // 5 minuty
const countdownElement2 = document.getElementById("countdown");

const interval = setInterval(function () {
    remainingTime--;
    countdownElement2.innerText = Math.floor(remainingTime / 60) + ":" + (remainingTime % 60).toString().padStart(2, '0');
    
    if (remainingTime <= 0) {
        clearInterval(interval);
       
    }
}, 1000);


//Zapytania do backendu


const encodedLesson1 = countdownElement2.getAttribute('data-encodedLesson');

function fetchAttendanceWithAjax() {
    $.ajax({
        url: `/FaceChecker/${encodedLesson1}/GetStudentAttendance`,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            updateViewWithAttendanceData(data);
            
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Wyst�pi� b��d:', textStatus, errorThrown);
        }
    });
}
function checkForUnrecognizedFaces() {
    $.ajax({
        url: '/FaceChecker/GetUnrecognizedFaces',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            // Je�eli odpowied� jest poprawna, ustaw �r�d�o obrazka i poka� modal
            if (data.unrecognizedFaceUrl && data.unrecognizedFaceUrl !== "") {
                
                $('#intruderImg').attr('src', data.unrecognizedFaceUrl);
                $('#intruderModal').modal('show');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            // Je�li serwer zwr�ci status 404 (NotFound), nie r�b nic.
            // W innym przypadku loguj b��d.
            if (jqXHR.status !== 404) {
                console.error('Error:', textStatus, errorThrown);
            }
        }
    });
}
           

function updateViewWithAttendanceData(data) {
    data.forEach(student => {
        const studentElement = document.querySelector(`li[data-key="${student.studentKey}"]`);
        if (studentElement) {
            if (student.wasLate) {
                studentElement.classList.remove('present');
                studentElement.classList.remove('absent');
                studentElement.classList.add('late');
            } else if (student.wasThere) {
                studentElement.classList.add('present');
                studentElement.classList.remove('absent');
                studentElement.classList.remove('late'); // Opcjonalnie, je�li chcesz, aby upewni� si�, �e klasa 'late' jest usuni�ta
            } else {
                studentElement.classList.remove('present');
                studentElement.classList.add('absent');
                studentElement.classList.remove('late');
            }
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    // Rozpocznij polling co sekund�
    const pollingInterval = setInterval(fetchAttendanceWithAjax, 1000);

    // Zatrzymaj polling po 300 sekundach
    setTimeout(() => {
        clearInterval(pollingInterval);
    }, 300 * 1000);

});


document.addEventListener("DOMContentLoaded", function () {
    // Rozpocznij polling co sekund�

    const unrecognizedFacesPollingInterval = setInterval(checkForUnrecognizedFaces, 1000);
    // Zatrzymaj polling po 300 sekundach

    setTimeout(() => {
        clearInterval(unrecognizedFacesPollingInterval);
    }, 300 * 1000);

});



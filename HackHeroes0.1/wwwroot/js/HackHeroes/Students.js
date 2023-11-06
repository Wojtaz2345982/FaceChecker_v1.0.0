$(document).ready(function () {

    const RenderStudents = (students, container) => {
        container.empty();
      


        for (const student of students) {

            let imageElement = student.imageUrl ? `<img src="${student.imageUrl}" class="w-100 mb-2 image-frame" alt="${student.firstName}'s image">` : '';


            container.append(
                ` <div class="card-div-theme card m-3" style="width: 22rem; border-radius: 15px;">
                    <div class="card-title text-theme" style="text-align: center; margin: 0px !important; margin-top: 10px !important;">Imi&eogon;: ${student.firstName}</div>
                    <hr class="dark--text-smaller" />
                    <div class="card-body">
                        ${imageElement}
                        <h5 class="card-title dark--text-smaller" style="font-size: 24px !important; font-weight: bold !important;">Nazwisko: ${student.lastName}</h5>
                        <h5 class="card-title dark--text-smaller" style="font-size: 24px !important; font-weight: bold !important;">Numer: ${student.number}</h5>
                        <hr class="dark--text-smaller" />
                        <div class="btn-group">
                            <button type="button" class="btn delete-student btn-primary card-del-btn" data-student-key="${student.studentKey}" data-bs-toggle="modal" data-bs-target="#deleteStudentModal">
                                  Usu&nacute;
                            </button>
                            <a class="btn btn-primary btn-theme card-btn-theme" href="/FaceChecker/${student.studentKey}/EditStudent">
                                Edytuj
                            </a>
                        </div>
                    </div>
                </div>`);
          
        }

      
        container.on('click', '.delete-student', function (event) {
            const studentKey = $(this).data('student-key');
            $('#deleteStudentModal').data('student-key', studentKey); // Przekazywanie studentKey do modalu
        });
    }

  


    const LoadStudents = () => {
        const container = $("#students")
        const classEncodedName = container.data("encodedName");
    
        $.ajax({
            url: `/FaceChecker/${classEncodedName}/Student`,
            type: 'get',
            success: function (data) {

                RenderStudents(data, container)
            },
            error: function () {
                
            }
        })
    }

    LoadStudents()

  

    $("#deleteStudentModal form").submit(function (event) {
        event.preventDefault();
        const studentKey = $('#deleteStudentModal').data('student-key');

        $.ajax({
            url: `/FaceChecker/${studentKey}/Student`,
            type: $(this).attr('method'),
            success: function (data) {

                toastr["success"]("Usuni&eogon;to ucznia")
                LoadStudents()

            },
            error: function () {

                toastr["error"]("Coœ posz³o nie tak...")
            }
        })

    });

    $("#addStudentModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
               
                toastr["success"]("Dodano ucznia")

                LoadStudents()
                
            },
            error: function () {
                
                toastr["error"]("Niepoprawne dane ucznia!")
            }
        })

    });
});
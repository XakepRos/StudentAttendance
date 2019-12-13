if (!window.webapp) {
    window.webapp = {};
}


window.webapp.student = (function ($) {
    var table;
    var frmDialogCreate = $("#ModalCreate");
    var frmDialogEdit = $("#ModalEdit");
    var viewtable = $("#studentGrid");
    var buttonCreat = $("#btnSubmit");
    var buttonEdit = $("#btnEdit");
    var reload = $('.btn_reload');

    var onDocumentReady = function () {
        initUI();
        initEvent();
    },
        initUI = function () {
        },

        initEvent = function () {
            $(".stdcreate").on('click', addEntity);
            viewtable.on('click', '.btn_edit', getEditEnity);
            viewtable.on('click', '.btn_delete', deleteEntity);
            $("#btnCreate").on('click', saveStudent);
            $('#btnModalEdit').on('click', updateStudent);
            $('#btnClose').on('click', closePopup);
            $('#btnCloseEdit').on('click', closePopupEdit);
          



        },

        calculatedAge = function (DOB) {
            debugger;
           var dobs= $("#DOB").val();
            //var today = new Date();
            //var birthDate = new Date(dateString);
            //var age = today.getFullYear() - birthDate.getFullYear();
            //var m = today.getMonth() - birthDate.getMonth();
            //if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            //    age--;
            //}
            //return age;

            //var dob = $('#DOB').val();
            //if (dob != '') {
            //    var today = new Date();
            //    var dayDiff = Math.ceil(today - dob) / (1000 * 60 * 60 * 24 * 365);
            //    var age = parseInt(dayDiff);
            //    $('#age').html(age + ' years old');
            //}

            dob = new Date(dobs);
            var today = new Date();
            var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
            $('#Age').val(age );
        }




    //function GetAge(dateString) {
    //    var today = new Date();
    //    var birthDate = new Date(dateString);
    //    var age = today.getFullYear() - birthDate.getFullYear();
    //    var m = today.getMonth() - birthDate.getMonth();
    //    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
    //        age--;
    //    }
    //    return age;
    //}
    //$(document).ready(function () {

    //    $("#dobfield").onblur(function () {
    //        var dob = $("#dobfield").val().trim();
    //        var calculatedDob = GetAge(dob);
    //        $("#age").text(calculatedDob);
    //    });
    //});


        closePopup = function (event) {
            event.preventDefault();
            $('.ModalCreatePopUp').removeClass('show');

        },
        closePopupEdit = function (event) {
            event.preventDefault();
            $('.ModalEditPopUp').removeClass('show');
        },
        addEntity = function (event) {
            debugger;
            event.preventDefault();
            var url = $(this).attr('data-href');
            $.get(url, function (data) {
                frmDialogEdit.find('div.modal-body').html('');
                frmDialogCreate.find('div.modal-body').html(data);
                frmDialogCreate.find('.heading ').html('Create Student');
                $('.ModalCreatePopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();
                $("#DOB").on('change', calculatedAge);
            });


        },
        getEditEnity = function (event) {
            var $d = $.Deferred();
            var id = $(this).parent().attr("data-id");
            var url = "/Student/EditInformation" + "?id=" + id;
            $.ajax(url).done(function (data) {
                $d.resolve(data);
                frmDialogCreate.find('div.modal-body').html();
                frmDialogEdit.find('div.modal-body').html(data);
                frmDialogEdit.find('.heading ').html('Edit Student');
                $('.ModalEditPopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();

            }).fail(function (data) {
                $d.reject(data);
            });
            return $d.promise();
        },
        updateStudent = function (event) {
            var model = {
                Id: '',
                StdName: '',
                CourseId: '',
                DOB: '',
                Address: '',
                Age: '',
                Mobile: '',
                Email: '',
                Gender: '',
                Food: '',
                Description: '' 
            }
            debugger;
            model.Id = $("#Id").val();
            model.StdName = $("#StdName").val();
            model.CourseId = $("#CourseId").val();
            model.DateOfBirth = $("#DOB").val();
            model.Address = $("#Address").val();
            model.Age = $("#Age").val();
            model.Mobile = $("#Mobile").val();
            model.Email = $("#Email").val();
            model.Gender = $("#Gender").val();
            model.Food = $("#Food").val();
            model.Description = $("#Description").val();
            console.log(model);

            debugger;

            var $form = frmDialogEdit.find('#EditStudentForm');

            event.preventDefault();
            $.ajax({
                url: '/Student/EditInformation',
                type: "Post",
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json"

            }).done(function (response) {
                if (response.success === true) {
                    $('.ModalEditPopUp').removeClass('show');
                    location.reload();
                    alert("success");
                }
                else if (response.success === false) {
                    alert("error");
                }
                else {
                    alert("error");
                }
            }).fail(function (response) {
                alert("Error occured while processing your request!!");
            });

        },
        saveStudent = function (event) {
            var $d = $.Deferred();
            var model = {
                Id: '',
                StdName: '',
                Course: '',
                DOB: '',
                Address: '',
                Age: '',
                Mobile: '',
                Email: '',
                Gender: '',
                Food: '',
                Description: ''                      
            }
            debugger;
            model.Id = $("#Id").val();
            model.StdName = $("#StdName").val();
            model.CourseId = $("#CourseId").val();
            model.DOB = $("#DOB").val();
            model.Address = $("#Address").val();
            model.Age = $("#Age").val();
            model.Mobile = $("#Mobile").val();
            model.Email = $("#Email").val();
            model.Gender = $('input[name=Gender]:checked').val();//$("#Gender").val();
            model.Food = $("#Food").val();
            model.Description = $("#Description").val();
            console.log(model);

            debugger;
            var $form = frmDialogCreate.find('#CreateStudentForm');

            event.preventDefault();
            $.ajax({
                url: '/Student/CreateInformation',
                type: "POST",
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json; charset=utf-8"
            }).done(function (response) {
                $d.resolve(response);
                if (response.success === true) {
                    $('.ModalCreatePopUp').removeClass('show');
                    $('body').addClass('scroll-hide');
                    location.reload();
                    alert("success");
                }
                else if (response.success === false) {
                    alert("error");
                }
                else {
                    alert("error");
                }

            }).fail(function (response) {
                $d.reject(response);
                alert("error");
            });


            return $d.promise();
        },
        deleteEntity = function (e) {
            var $d = $.Deferred();
            e.preventDefault();
            var id = $(this).parent().attr("data-id");
            if (confirm("Are you sure you want to delete this Student Details")) {
                $.ajax({
                    type: "POST",
                    url: "/Student/Delete",
                    dataType: "JSON",
                    data: { id: id }
                }).done(function (response) {
                    if (response.success === true) {
                        $d.resolve(response);
                        location.reload();
                        alert("success");
                    } else if (response.result === false) {
                        $d.resolve(response);
                        alert(response.message);
                    } else {
                        $d.resolve(response);
                        alert(response.message);
                    }
                }).fail(function (response) {
                    alert(response.message);
                });
            }
            return $d.promise;
        };



    


    return {
        onDocumentReady: onDocumentReady
    };
}(jQuery));

jQuery(webapp.student.onDocumentReady);


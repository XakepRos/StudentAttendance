if (!window.webapp) {
    window.webapp = {};
}


window.webapp.course = (function ($) {
    var table;
    var frmDialogCreate = $("#ModalCreate");
    var frmDialogEdit = $("#ModalEdit");
    var viewtable = $("#courseGrid");
    var buttonCreate = $("#btnSubmit");
    var buttonEdit = $("#btnEdit");
    var reload = $('.btn_reload');

    var onDocumentReady = function () {
        initUI();
        initEvent();
    },
        initUI = function () {
        },

        reloadData = function () {
            table.fnDraw();
        },

        initEvent = function () {
            $(".create").on('click', addEntity);
            viewtable.on('click', '.btn_edit', getEditEntity);
            viewtable.on('click', '.btn_delete', deleteEntity);
            $("#btnCreate").on('click', saveCourse);
            $('#btnModalEdit').on('click', updateCourse);
            $('#btnClose').on('click', closePopup);
            $('#btnCloseEdit').on('click', closePopupEdit);

            reload.on('click', reloadData);
        },

        closePopup = function (event) {
            event.preventDefault();
            $('.ModalCreatePopUp').removeClass('show');

        },

        closePopupEdit = function (event) {
            event.preventDefault();
            $('.ModalEditPopUp').removeClass('show');
        },

        addEntity = function (event) {
            event.preventDefault();
            var url = $(this).attr('data-href');
            debugger;
            $.get(url, function (data) {
                frmDialogEdit.find('div.modal-body').html('');
                frmDialogCreate.find('div.modal-body').html(data);
                frmDialogCreate.find('.heading ').html('Create Course');
                $('.ModalCreatePopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();
            });
        },

        getEditEntity = function (event) {
            var $d = $.Deferred();
            debugger;
            var id = $(this).parent().attr("data-id");
            var url = "/Course/EditCourse" + "?id=" + id;
            $.ajax(url).done(function (data) {
                $d.resolve(data);
                frmDialogCreate.find('div.modal-body').html();
                frmDialogEdit.find('div.modal-body').html(data);
                frmDialogEdit.find('.heading ').html('Edit Course');
                $('.ModalEditPopUp').addClass('show');
                $('body').addClass('scroll-hide');
                $('input:text:visible:first').focus();

            }).fail(function (data) {
                $d.reject(data);
            });
            return $d.promise();
        },

        updateCourse = function (event) {
            var model = {
                Id: '',
                CourseName: '',
            }
            model.Id = $("#Id").val();
            model.CourseName = $("#CourseName").val();
            var $form = frmDialogEdit.find('#EditCourseForm');

            event.preventDefault();
            $.ajax({
                url: '/Course/EditCourse',
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

        saveCourse = function (event) {
            var $d = $.Deferred();
            var model = {
                CourseName: ''
            };
            model.CourseName = $('#CreateCourseForm').find("#CourseName").val();
            debugger;
            var $form = frmDialogCreate.find('#CreateCourseForm');

            event.preventDefault();
            $.ajax({
                url: '/Course/CreateCourse',
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
            if (confirm("Are you sure you want to delete this course")) {
                $.ajax({
                    type: "POST",
                    url: "/Course/DeleteCourse",
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

jQuery(webapp.course.onDocumentReady);
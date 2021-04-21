function ShowDeleteConfirmation(url) {
    debugger
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0CC27E',
        cancelButtonColor: '#FF586B',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        confirmButtonClass: 'btn btn-success mr-5',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false
    }).then(function () {
        var codeToExecute = url;
        var tmpFunc = new Function(codeToExecute);
        tmpFunc();

    }, function (dismiss) {
        // dismiss can be 'overlay', 'cancel', 'close', 'esc', 'timer'
        if (dismiss === 'cancel') {
            swal(
                'Cancelled',
                'Your file is safe :)',
                'error'
            )
        }
    })
}


function PostDeleteAdminUser(Id) {

    var jsonData = {

        userId: Id

    };
    $.post("/Admin/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "User data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();
                        
                    })
                }

            }
            else
                swal(
                    'Error!',
                    "User is in use.",
                    'error',

                )
        });

}


function PostDeleteRegisteredUser(Id) {

    var jsonData = {

        userId: Id

    };
    $.post("/User/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "User data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "User is in use.",
                    'error',

                )
        });

}

function PostDeleteVideo(Id) {

    var jsonData = {

        videoId: Id

    };
    $.post("/Level/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "User data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.href = "/Home/Index"

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Video is in use.",
                    'error',

                )
        });

}


function PostDeleteQuote(Id) {

    var jsonData = {

        id: Id

    };
    $.post("/WeeklyUpdate/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "Quote data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Quote is in use.",
                    'error',

                )
        });

}
function PostDeleteDashboardQuote(Id) {

    var jsonData = {

        id: Id

    };
    $.post("/DashboardQuote/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "Quote data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Quote is in use.",
                    'error',

                )
        });

}

function PostDeleteModule(Id) {
    debugger
    var jsonData = {

        id: Id

    };
    $.post("/Level/DeleteModule",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "Module has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Quote is in use.",
                    'error',

                )
        });

}

function PostDeleteVideo(Id) {

    var jsonData = {

        videoId: Id

    };
    $.post("/Video/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                debugger
                if (data.message != null) {
                    swal({
                        type: 'warning',
                        title: 'Warning!',
                        text: data.message,
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-warning'
                    });
                }
                else {
                    swal({
                        title: 'Deleted!',
                        text: "Video data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {

                        window.location.reload();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Quote is in use.",
                    'error',

                )
        });

}
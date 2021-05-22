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

function PostDeleteLevel(Id) {
    debugger
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
                        text: "Video lesson has been deleted.",
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
                    "Module is in use.",
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
                    "Video is in use.",
                    'error',

                )
        });

}

function DeleteEditPageVideo(Id) {

    var jsonData = {

        videoId: Id

    };
    $.post("/Video/DeleteVideo",
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
                        debugger
                        $("#previousVideo").hide();
                        $("#uploadFile").show();

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


function DeleteEditPageImage(Id) {

    var jsonData = {

        imageId: Id

    };
    $.post("/Image/DeleteImage",
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
                        text: "Image data has been deleted.",
                        type: 'success',
                        confirmButtonColor: '#2f47c2',
                        confirmButtonText: 'Ok',
                        confirmButtonClass: 'btn btn-lg btn-primary',
                        buttonsStyling: false
                    }).then(function () {
                        debugger
                        $("#previousImage").hide();
                        $("#uploadImageFile").show();

                    })
                }

            }
            else
                swal(
                    'Error!',
                    "Image is in use.",
                    'error',

                )
        });

}


function PostDeleteImage(Id) {

    var jsonData = {

        imageId: Id

    };
    $.post("/Image/Delete",
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
                        text: "Image data has been deleted.",
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
                    "Image is in use.",
                    'error',

                )
        });

}






function PostDeleteFeelGoodStory(Id) {

    var jsonData = {

        storyId: Id

    };
    $.post("/FeelGoodStory/Delete",
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
                        text: "Story has been deleted.",
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
                    "story is in use.",
                    'error',

                )
        });

}







function PostDeleteLanguage(Id) {

    var jsonData = {

        languageId: Id

    };
    $.post("/Language/Delete",
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
                        text: "Language has been deleted.",
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
                    "language is in use.",
                    'error',

                )
        });

}


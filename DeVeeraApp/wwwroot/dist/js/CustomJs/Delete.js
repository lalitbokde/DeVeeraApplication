function ShowDeleteConfirmation(url) {

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
    debugger
    var jsonData = {

        userId: Id

    };
    $.post("/Admin/Admin/Delete",
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
    $.post("/Admin/Admin/Delete",
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
    
    var jsonData = {

        videoId: Id

    };
    $.post("/Admin/Level/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
            
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

                        window.location.href = "/Admin/Level/List"

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
    $.post("/Admin/WeeklyUpdate/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
           
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
    $.post("/Admin/DashboardQuote/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
             
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
    $.post("/Admin/Level/DeleteModule",
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
    $.post("/Admin/Video/Delete",
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

function DeleteEditPageVideo(Id,i) {











  var jsonData = {

        videoId: Id , keyId: i

    };


    $.post("/Admin/Video/DeleteVideo",
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
                        //$("#previousVideo").hide();
                        //$("#uploadFile").show();

location.reload();


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


function DeleteEditPageImage(Id,imagekey) {

    var jsonData = {

        imageId: Id,imagekeyval:imagekey

    };
    $.post("/Admin/Image/DeleteImage",
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
                        $("#previousImage1").hide();
                        $("#uploadImageFile1").show();

                        $("#previousImage2").hide();
                        $("#uploadImageFile2").show(); 

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
    $.post("/Admin/Image/Delete",
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
    $.post("/Admin/FeelGoodStory/Delete",
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
    $.post("/Admin/Language/Delete",
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



function PostDeleteEmotion(Id) {
    debugger
    var jsonData = {

        emotionId: Id

    };
    $.post("/Admin/Emotion/Delete",
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
                        text: "Emotion has been deleted.",
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
                    "Emotion is in use.",
                    'error',

                )
        });

}

function PostDeleteLocaleStringResource(Id) {
    debugger
    var jsonData = {

        id: Id

    };
    $.post("/Admin/LocalStringResources/Delete",
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
                        text: "Local String Resources has been deleted.",
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
                    "Local String Resources is in use.",
                    'error',

                )
        });

}

function PostDeleteQuestion(Id) {
    debugger
    var jsonData = {

        id: Id

    };
    $.post("/Admin/QuestionAnswer/Delete",
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
                        text: "Question has been deleted.",
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
                    "Emotion is in use.",
                    'error',

                )
        });

}

function PostDeleteLayoutSetup(Id) {
    debugger
    var jsonData = {

        id: Id

    };
    $.post("/Admin/LayoutSetup/Delete",
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
                        text: "LayoutSetup has been deleted.",
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
                    "Emotion is in use.",
                    'error',

                )
        });

}
//Sweet Alert for Delete
function ShowDeleteConfirmationWithResponse(url) {
    swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#0CC27E',
        cancelButtonColor: '#FF586B',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No',
        confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white',
        cancelButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white',
        buttonsStyling: false
    }).then(function () {
        var codeToExecute = url;
        var tmpFunc = new Function(codeToExecute);
        tmpFunc();
        showloaderfn();
    }, function (dismiss) {
        // dismiss can be 'overlay', 'cancel', 'close', 'esc', 'timer'
        if (dismiss === 'cancel') {
            swal(
                '',
                'Your file is safe :',
                'error'
            )
        }
    })
}
//delete UserRole
function PostDeleteUserRole(Id) {

    var jsonData = {

        id: Id


    };
    $.post("/UserRole/Delete",
        jsonData
        ,
        function (data, status) {
            if (data.success == true) {
                swal({
                    title: 'Success!',
                    text: "User Role has been deleted.",
                    type: 'success',                    
                    confirmButtonColor: '#2f47c2',                    
                    confirmButtonText: 'Ok',                   
                    confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white',                   
                    buttonsStyling: false
                }).then(function () {
                    window.location.reload();
                })
              
            }
            else
                swal(
                    'Error!',
                    "User Role Is In Use.",
                    'error',

                )
        });

}

//delete User
function PostDeleteUsers(Id) {

    var jsonData = {

        id: Id

    };
    $.post("/User/DeleteUser",
        jsonData
        ,
        function (data) {
            if (data.success == true) {
                swal({
                    title: 'Success!',
                    text: "User has been deleted.",
                    type: 'success',
                    confirmButtonColor: '#2f47c2',
                    confirmButtonText: 'Ok',
                    confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white',
                    buttonsStyling: false
                }).then(function () {
                    window.location.reload();
                })
            }
            else
                swal(
                    'Error!',
                     "User has not been deleted.",
                   'error',
                    
                )
        });

}
function onSuccessCreateAndEdit(data) {

    if (data.success == true) {


        swal({

            type: 'success',
            title: 'Success!',
            text: 'User Role has been saved',
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white'


        }

        ).then(function () {

            window.location.reload();
        })

    }
    else {
        window.location.reload();
        //swal({
        //    type: 'error',
        //    title: 'Error!',
        //    text: data.message,
        //    confirmButtonText: 'Dismiss',
        //    buttonsStyling: false,
        //    confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white'
        //})



    }
}

function onFailed(event) {
    event.preventDefault();
    event.stopPropagation();
}
//User Role
function UserRoleValidation() {
    if (document.getElementById('UserRole').selectedIndex == 0) {
        alert("Please select Role");

        return false;
    }
    return true;
}
function GetUserRoleDataById(Id) {

    $.ajax({
        url: "/UserRole/GetUserRoleData",
        type: "get", //send it through get method
        data: {
            id: Id
        },
        success: function (response) {
            if (response.name != null) {
                $('#AddUserModal').modal('show');
                $("#UserRoleData_Name").val(response.name);
                $("#UserRoleData_Active").val(response.active);
                $("#UserRoleData_IsSystemRole").val(response.isSystemRole);
                $("#UserRoleData_SystemName").val(response.systemName);
                $("#UserRoleData_Id").val(response.id);
               
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
} 


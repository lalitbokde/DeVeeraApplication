
function onSuccessCreateAndEdit(data) {
    
    if (data.success == true) {
        
        swal({
           
            type: 'success',
            title: 'Success!',
            text: 'User has been saved',
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white'
           
           
        }
            
        ).then(function () {
           
            window.location.reload();
        })

    }
    else
        swal({
            type: 'error',
            title: 'Error!',
            text: data.message,
            confirmButtonText: 'Dismiss',
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white'
        })
    
    $("#loader").hide();

}

 
function UserRoleValidation() {
    if (document.getElementById('UserRole').selectedIndex == 0) {
        alert("Please select Role");

        return false;
    }
    return true;
}


function GetStateByCountryId(stateId) {
    $.post("/User/GetStateByCountryId",
        {
            Id: $("#CountryId").val(),
            SelectedId: $("#StateId").val()
        },
        function (data, status) {
            //clear previous filled states
            $("#StateId").html("");

            //append the states from selected country id
            $.each(data, function (i, item) {
                $("#StateId").append(
                    $('<option></option>').val(item.value).html(item.text));
            });

            $("#StateId").val(stateId).trigger('change');
        });
}

function GetUserDataById(Id) {

    $.ajax({
        url: "/User/GetUserData",
        type: "get", //send it through get method
        data: {
            Id: Id
        },
        success: function (response) {
            if (response.firstName != null) {
                $('#AddUserModal').modal('show');
                $("#UserData_BillingAddress_FirstName").val(response.firstName);
                $("#UserData_BillingAddress_LastName").val(response.lastName);
                $("#UserData_BillingAddress_Email").val(response.email);
                $("#UserData_CustomerPassword_Password").val(response.password);
                $("#UserData_BillingAddress_PhoneNumber").val(response.contactNo);
                $("#UserData_Alias").val(response.alias);
                $("#UserRole").val(response.customerRoleId).trigger('change');
                $("#UserData_BillingAddress_Address1").val(response.address1);
                $("#UserData_BillingAddress_Address2").val(response.address2);
                $("#CountryId").val(response.countryId).trigger('change');
                $("#StateId").val(response.stateId).trigger('change');
                $("#UserData_BillingAddress_City").val(response.city);
                $("#UserData_BillingAddress_ZipPostalCode").val(response.zipCode);
                $("#UserData_Id").val(response.id);
            }
        },
        error: function (xhr) {
            //Do Something to handle error
        }
    });
} 

function onChangePasswordSuccess(data) {
    debugger
    if (data.success == true) {
        swal({

            type: 'success',
            title: 'Success!',
            text: data.message,
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white'

        }

        ).then(function () {

            window.location.href = "/User/Login";
        })
    } else {
        swal({
            type: 'error',
            title: 'Error!',
            text: data.message,
            confirmButtonText: 'Dismiss',
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white'
        })
    }
}
function onResetPasswordSuccess(data) {
    debugger
    if (data.success == true) {
        swal({

            type: 'success',
            title: 'Success!',
            text: data.message,
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-9 text-white'

        }

        ).then(function () {

            window.location.href = "/User/Login";
        })
    } else {
        swal({
            type: 'error',
            title: 'Error!',
            text: data.message,
            confirmButtonText: 'Dismiss',
            buttonsStyling: false,
            confirmButtonClass: 'button w-24 mr-1 mb-2 bg-theme-6 text-white'
        })
    }
}

function uploadProfile(input, userId) {
    debugger
    var input = document.getElementById(input);
    var files = input.files;
    var formData = new FormData();
    //var userId = $("#UserModel_Id").val();

    formData.append("UserId", userId);
    formData.append("files", input.files[0]);

    $("#Dotloader").removeAttr('hidden',true);

    $.ajax(
        {
            url:"/Technician/UploadProfile",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
           
            success: function (data, status) {              
                if (data.success == true) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) { 
                            debugger
                            $('#imgPrev').attr('src', e.target.result);
                        }
                        reader.readAsDataURL(input.files[0]); // convert to base64 string
                    }

                    $("#Dotloader").attr('hidden',true);
                    swal({
                        type: 'success',
                        title: 'Success!',
                        text: "Profile uploaded!",
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-success'
                    }) 
                }
                
                else {
                    $("#Dotloader").attr('hidden',true);
                    swal({
                        type: 'error',
                        title: 'Error!',
                        text: data,
                        confirmButtonText: 'Dismiss',
                        buttonsStyling: false,
                        confirmButtonClass: 'btn btn-lg btn-danger'
                    })

                }
            }
        })

}

function BrowseImage() {
    $(".inputfile").click();
}

function RemoveProfile(userId) {
    debugger
    var jsondata = { UserId : userId };
    $("#Dotloader").removeAttr('hidden', true);
    $.post("/Technician/RemoveProfile", jsondata, function (data) {
        debugger
        if (data == true) {
            document.getElementById("imgPrev").attributes.src.value = '/dist/images/user12.png';
            $("#Dotloader").attr('hidden', true);

            swal({
                type: 'success',
                title: 'Success!',
                text: "Profile Removed !",
                buttonsStyling: false,
                confirmButtonClass: 'btn btn-lg btn-success'
            }) 
        }
        else {
            $("#Dotloader").attr('hidden', true);
            swal({
                type: 'error',
                title: 'Error!',
                text: data,
                confirmButtonText: 'Dismiss',
                buttonsStyling: false,
                confirmButtonClass: 'btn btn-lg btn-danger'
            })

        }
       
    })
}
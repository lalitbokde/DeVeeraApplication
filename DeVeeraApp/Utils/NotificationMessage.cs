namespace DeVeeraApp.Utils
{
    public static class NotificationMessage
    {
        public static string TitleSuccess = "Success";
        public static string TypeSuccess = "success";

        public static string TitleError = "Error";
        public static string TypeError = "error";


        public static string Titlewarning = "Warning";
        public static string Typewarning = "warning";
        //Error Msg
        public static string ErrorMsg = "There is something wrong.Please try again.";
        //Vendor
        public static string msgAddVendor = "The new vendor has been added successfully.";
     
        public static string msgEditVendor = "The vendor info has been edited successfully.";
        public static string msgDeleteVendor = "The vendor has been deleted successfully.";
        public static string ErrormsgDeleteVendor = "The  vendor is in use.";
        //PDF header
        public static string msgAddPDFHeader = "The new header has been added successfully.";
        public static string msgEditPDFHeader = "The  header has been edited successfully.";


        //User
        public static string msgCreateUser = "The new User has been added successfully.";

        public static string msgAddUser = "The new user has been added successfully.";
      
        public static string msgEditUser = "The user info has been edited successfully.";
       
        public static string UserAlreadyRegistered = "User is already registered.";
        public static string msgUserDeleted = "User has been deleted successfully.";
        public static string ErrormsgUserDeleted = "User is in use.";     
       
        public static string Emailisalreadyregistered = "Email is already registered.";
    

     
       
        //Address
        public static string msgAddressEdit = "The address has been edited successfully.";
        public static string ErrormsgAddressEdit = "The  address has not been edited  to database.";


        //UserRole
        public static string msgCreateUserRole = "The new User role has been added successfully.";
     
        public static string msgEditUserRole = "The User role has  been edited successfully.";
       
        public static string msgUserRoleDeleted = "User Role has been deleted successfully.";

        public static string ErrormsgUserRoleDeleted = "User Role is in use.";

        //User Login
        public static string msgLoginSuccessfull { get; set; }
       
        //Permissions
         public static string msgSavePermission = "The permission has been saved successfully.";
        public static string ErrormsgSavePermission = "The permission has not been saved to database.";
      

    }
}

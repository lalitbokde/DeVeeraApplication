

function hideloaderfn() {
   
    $("#loader").hide();
}
setTimeout(function () {
    $("#loader").hide();
}, 100);


function showloaderfn() {
$('.ovalicon').css('display', 'block');
$('.oval').css('display', 'block');
$('.loader').removeAttr("hidden");
}
function ShowLoader(formid) {
  
    var isValid = $("#" + formid).valid(); 
    debugger
    if (isValid) {
        showloaderfn();
    }
}
function ShowLoaderUser(formid) {
    var isValid = $("#" + formid).valid();
    
    if (isValid) {
        showloaderfn();      
    }
    window.onload();
}

 


 function GetFileName() {debugger
        var filename = $("#single-file-upload1 .dz-filename").text();

 
        document.getElementById("FileName").value = filename;

   var SpanishFileName = $("#single-file-upload2 .dz-filename").text();//document.getElementById("single-file-upload2").getElementsByClassName("dz-filename")[0];alert(SpanishFileName);//
        document.getElementById("SpanishFileName").value = SpanishFileName;
    }


function showvideopop(){
//alert("Video not Available !!.");
document.getElementById("VideoNotAvailable").style.display='inline';     
 //$("#VideoNotAvailable").removeAttr('hidden');
}



function hidepopup(){
document.getElementById("VideoNotAvailable").style.display='none';     
 //$("#VideoNotAvailable").removeAttr('hidden');
}



function showvideopopland(a,b){
 Swal.fire(a,b );

//document.getElementById("VideoNotAvailableland").style.display='inline';  

}

function GetProfilePicName()
{

var filename = $("#custom-file-input").text();

 
        document.getElementById("ProfileImageUrl").value = filename;

}





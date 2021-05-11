function GetModuleByLevelId(levelId) {
    debugger
    $.get("/QuestionAnswer/GetModuleByLevelId",
        {
            Id: $("#levelId").val(),
            SelectedId: $("#moduleId").val()
        },
        function (data, status) {
            debugger
            //clear previous filled modules
            $("#moduleId").html("");

            //append the modules from selected level id
            $.each(data, function (i, item) {
                debugger             
                $("#moduleId").append(
                    $('<option></option>').val(item.value).html(item.text));
               
            });
          
        });
}
function LoadImageList(ApplicantId) {
    debugger
    var url = "/Admin/Emotion/GetImageList";
    var jsonData = {
        ApplicantId: ApplicantId
    };

    $("#addEmotionImages").load(url, jsonData, function (response, status, xhr) {
        debugger
    
    });
}
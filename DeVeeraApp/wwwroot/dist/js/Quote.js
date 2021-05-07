function ImportExcel() {
    debugger
    var filename = $(".dz-filename").text();
    var jsondata = { filename: filename };

    $.post("/DashboardQuote/ImportExcel", jsondata, function (data) {
        if (data.success == true) {
            location.reload()
        }

    });
}
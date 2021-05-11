function ImportExcel() {
    debugger
    var filename = $(".dz-filename").text();
    var jsondata = { filename: filename };

    $.post("/DashboardQuote/ImportExcel", jsondata, function (data) {
        debugger
        if (data) {
            location.reload()
        }

    });
}





var table = new Tabulator("#tabulator1",
    {
        index: "id",
        layout: "fitColumns",
        responsiveLayout: "collapse",
        columns: [
            
            //{ title: "#", field: "", sorter: "number", width: 50, editor: true, headerSort: false},
            { title: "Id",  field: "Id", sorter: "string", width: 90 },
            { title: "Quote", field: "Title", sorter: "string", width: 290 },
            { title: "Author", field: "Author", sorter: "string", width: 170 },
            { title: "Level", field: "Level", sorter: "string", width: 140 },
            { title: "Dashboard Quote", field: "IsDashboardQuote", sorter: "boolean", width: 170 },
            { title: "Random Quote", field: "IsRandom", sorter: "boolean", width: 150 },
            { title: "Edit", field: "", hozAlign: "center", width: 90, headerSort: false, formatter: function (e, t) { return `<div class="flex lg:justify-center items-center">\n  <a href="/DashboardQuote/Edit/${e.getData().Id}" >\n <i data-feather="edit" class="text-theme-10"></i> \n</a>\n</div>   ` }, },
            { title: "Delete", field: "", hozAlign: "center", width: 90, headerSort: false, formatter: function (e, t) { return `<div class="flex lg:justify-center items-center">\n  <a onclick="ShowDeleteConfirmation('PostDeleteDashboardQuote(${e.getData().Id})')"" >\n <i data-feather="trash-2" class="text-theme-24"></i> \n</a>\n</div>   ` }, },],

    });

//var tableData = [{ "LevelNo": 1, "VideoId": 1, "Image1": 0, "Image2": 0, "Image3": 0, "Title": "Happyness", "Subtitle": "scar Wilde.", "Quote": null, "VideoUrl": null, "VideoName": "Happyness", "FullDescription": "<p>aaaa</p>", "Modules": { "LevelId": 0, "VideoId": null, "FullDescription": null, "Level": null, "Video": null, "Id": 0 }, "Video": { "LazyLoader": {}, "Name": "Happyness", "VideoUrl": "", "Key": null, "Id": 1 }, "Image": null, "ModuleList": null, "AvailableVideo": [], "AvailableImages": [], "srno": 0, "DiaryText": null, "DiaryLatestUpdateDate": null, "Id": 19 }, { "LevelNo": 2, "VideoId": 3, "Image1": 0, "Image2": 0, "Image3": 0, "Title": "Forgivenes", "Subtitle": "scar Wilde.", "Quote": null, "VideoUrl": null, "VideoName": "Presence", "FullDescription": "<p>aaaa</p>", "Modules": { "LevelId": 0, "VideoId": null, "FullDescription": null, "Level": null, "Video": null, "Id": 0 }, "Video": { "LazyLoader": {}, "Name": "Presence", "VideoUrl": "", "Key": null, "Id": 3 }, "Image": null, "ModuleList": null, "AvailableVideo": [], "AvailableImages": [], "srno": 0, "DiaryText": null, "DiaryLatestUpdateDate": null, "Id": 20 }, { "LevelNo": 3, "VideoId": 3, "Image1": 0, "Image2": 0, "Image3": 0, "Title": " Experience", "Subtitle": "Wilde Data", "Quote": null, "VideoUrl": null, "VideoName": "Presence", "FullDescription": "<p><span style=\"background-color:rgb(255,255,255);color:rgb(32,33,36);\">1a : direct observation of or participation in events as a basis of knowledge. b : the fact or state of having been affected by or gained knowledge through direct observation or participation.</span></p>", "Modules": { "LevelId": 0, "VideoId": null, "FullDescription": null, "Level": null, "Video": null, "Id": 0 }, "Video": { "LazyLoader": {}, "Name": "Presence", "VideoUrl": "", "Key": null, "Id": 3 }, "Image": null, "ModuleList": null, "AvailableVideo": [], "AvailableImages": [], "srno": 0, "DiaryText": null, "DiaryLatestUpdateDate": null, "Id": 21 }, { "LevelNo": 4, "VideoId": 3, "Image1": 0, "Image2": 0, "Image3": 0, "Title": "Presence", "Subtitle": "Wilde", "Quote": null, "VideoUrl": null, "VideoName": "Presence", "FullDescription": "<p><i>The </i><a href=\"https://dictionary.cambridge.org/dictionary/english/strong\"><i>strong</i></a><i> </i><a href=\"https://dictionary.cambridge.org/dictionary/english/police\"><i>police</i></a><i> presence only </i><a href=\"https://dictionary.cambridge.org/dictionary/english/heighten\"><i>heightened</i></a><i> the </i><a href=\"https://dictionary.cambridge.org/dictionary/english/tension\"><i>tension</i></a><i> among the </i><a href=\"https://dictionary.cambridge.org/dictionary/english/crowd\"><i>crowd</i></a><i>.Some </i><a href=\"https://dictionary.cambridge.org/dictionary/english/worker\"><i>workers</i></a><i> were </i><a href=\"https://dictionary.cambridge.org/dictionary/english/inhibited\"><i>inhibited</i></a><i> from </i><a href=\"https://dictionary.cambridge.org/dictionary/english/speaking\"><i>speaking</i></a><i> by the presence of </i><a href=\"https://dictionary.cambridge.org/dictionary/english/their\"><i>their</i></a><i> </i><a href=\"https://dictionary.cambridge.org/dictionary/english/manager\"><i>managers</i></a><i>.He didn't </i><a href=\"https://dictionary.cambridge.org/dictionary/english/even\"><i>even</i></a><i> </i><a href=\"https://dictionary.cambridge.org/dictionary/english/acknowledge\"><i>acknowledge</i></a><i> my presence.He </i><a href=\"https://dictionary.cambridge.org/dictionary/english/sign\"><i>signed</i></a><i> the </i><a href=\"https://dictionary.cambridge.org/dictionary/english/treaty\"><i>treaty</i></a><i> in the presence of two </i><a href=\"https://dictionary.cambridge.org/dictionary/english/witness\"><i>witnesses</i></a><i>.He </i><a href=\"https://dictionary.cambridge.org/dictionary/english/stood\"><i>stood</i></a><i> there in the </i><a href=\"https://dictionary.cambridge.org/dictionary/english/corner\"><i>corner</i></a><i> of the </i><a href=\"https://dictionary.cambridge.org/dictionary/english/room\"><i>room</i></a><i>, a </i><a href=\"https://dictionary.cambridge.org/dictionary/english/dark\"><i>dark</i></a><i>, </i><a href=\"https://dictionary.cambridge.org/dictionary/english/brooding\"><i>brooding</i></a><i> presence.</i></p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>", "Modules": { "LevelId": 0, "VideoId": null, "FullDescription": null, "Level": null, "Video": null, "Id": 0 }, "Video": { "LazyLoader": {}, "Name": "Presence", "VideoUrl": "", "Key": null, "Id": 3 }, "Image": null, "ModuleList": null, "AvailableVideo": [], "AvailableImages": [], "srno": 0, "DiaryText": null, "DiaryLatestUpdateDate": null, "Id": 22 }]

var tableData = $("#QuoteTable").val();
table.setData(tableData);
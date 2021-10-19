

var table = new Tabulator("#tabulatorW",
    {


         index: "id",
        layout: "fitColumns",
        responsiveLayout: "collapse",
        pagination: "local",
        paginationSize: 10,
        paginationSizeSelector: [10, 20, 30, 40],
        resizableColumns: true,
        columns: [
            {
                formatter: "responsiveCollapse",
            },
           
            { title: "#", width: 150, headerSort: true, sorter: "number", formatter: "rownum" },
            { title: "Title", field: "Title", headerSort: false, sorter: "string", width: 170 },


            { title: "Video", field: "VideoName", sorter: "boolean", width: 170 },
            { title: "Play", field: "VideoId", width: 150, headerSort: false, formatter: function (e, t) { return `<a href="/Admin/Video/Play/${e.getValue()}"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity"><polygon points="5 3 19 12 5 21 5 3"></polygon></svg></a></div>` }, },
            { title: "Active", field: "IsActive", sorter: "boolean", width: 140, formatter: function (e, t) { return `<input class="checkbox-custom" type="checkbox" ${e.getValue() == true ? "checked" : ""}>` }, },
            { title: "Edit", field: "actions",  width: 100, headerSort: false, formatter: function (e, t) { return `<a href="/Admin/WeeklyUpdate/Edit/${e.getData().Id}"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit text-theme-10"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path></svg></a></div>` }, },
            { title: "Delete", field: "actions", width: 100, headerSort: false, formatter: function (e, t) { return `<a onclick="ShowDeleteConfirmation('PostDeleteQuote(${e.getData().Id})')"" ><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2 text-theme-24"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path><line x1="10" y1="11" x2="10" y2="17"></line><line x1="14" y1="11" x2="14" y2="17"></line></svg></a></div>` }, },],

    });


var tableData = $("#WeeklyUpdate").val();
table.setData(tableData);

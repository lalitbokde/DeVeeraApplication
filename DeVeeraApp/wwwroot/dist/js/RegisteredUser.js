

var table = new Tabulator("#tabulatorReg",
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

         
            { title: "#", width: 90, headerSort: false, formatter: "rownum", width: 100 },
            { title: "User Name", field: "Email", headerSort: true, sorter: "string", width: 170 },
            { title: "Gender", field: "GenderType", headerSort: true, sorter: "string", width: 170 },
            { title: "Age", field: "Age", headerSort: true, sorter: "string", width: 170 },
            { title: "Occupation", field: "Occupation", headerSort: true, sorter: "string", width: 170 },

            { title: "Edit", field: "actions", width: 100, headerSort: false, formatter: function (e, t) { return `<a href="/Admin/Admin/EditUser/${e.getData().Id}"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-edit text-theme-10"><path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path><path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path></svg></a></div>` }, },
            { title: "Delete", field: "actions", width: 100, headerSort: false, formatter: function (e, t) { return `<a onclick="ShowDeleteConfirmation('PostDeleteRegisteredUser(${e.getData().Id})')"" ><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round" class="feather feather-trash-2 text-theme-24"><polyline points="3 6 5 6 21 6"></polyline><path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path><line x1="10" y1="11" x2="10" y2="17"></line><line x1="14" y1="11" x2="14" y2="17"></line></svg></a></div>` }, },],

    });

var tableData = $("#Register").val();
table.setData(tableData);


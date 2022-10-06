var datatables;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    datatables = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "coverType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    
                     return ` <span >
                    <a class="btn btn-primary" href="/Admin/Product/Upsert?id=${data}"> Edit</a>
                </span>
                    <span>
                    <a class="btn btn-primary"> Detail</a>
                </span>
                 <span>
                    <a class="btn btn-primary"> Delete</a>
                 </span>
                   `

                },
                // "render": function (d) {
                //    return
                //        `
                //    <a> Edit</a>
               
                //   `

                //},
                "width": "15%"
            },
        ]

        //"columns": [
        //    { "title", "width": "15%" },
        //    { "isbn", "width": "15%" },
        //    {  "price", "width": "15%" },
        //    { "author", "width": "15%" },
        //    { "category.name", "width": "15%" },
        //    { "coverType.name", "width": "15%" },
        //]
    });
}
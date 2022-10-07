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
                    <a onClick=Delete('/Admin/Product/Delete/${data}') class="btn btn-primary"> Delete</a>
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
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    //datatables.ajax.reload();
                    data.success = true;
                    if (data.success) {
                        datatables.ajax.reload();
                        tostr.success(data.message);
                    }
                    else {
                        tostr.error(data.message);
                    }
                }
            })
        }
    })
}
var datatables;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    datatables = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAdress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            
            {
                "data": "id",
                "render": function (data) {
                    
                     return ` <span >
                    <a class="btn btn-primary" href="/Admin/Company/Upsert?id=${data}"> Edit</a>
                </span>
                    <span>
                    <a class="btn btn-primary"> Detail</a>
                </span>
                 <span>
                    <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-primary"> Delete</a>
                 </span>
                   `

                },
             
                "width": "15%"
            },
        ]
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
                    //data.success = true;
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
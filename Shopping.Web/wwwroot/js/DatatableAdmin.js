


var dtbl;
$(document).ready(function () {
    loadData();
})


function loadData() {
    dtbl =$("#myTable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetByAjax"
        },
        "columns": [
            { "data": "name" },
            { "data": "price" },
            { "data": "description" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                <a href="/Admin/Product/Edit/${data}" class="btn btn-secondary">Edit</a>
                 <a onClick=Certian("/Admin/Product/Delete/${data}") class="btn btn-danger">Delete</a>
                 <a href="/Admin/Product/Details/${data}" class="btn btn-info">Details</a>

                   


                            `
                }
            },

            


        ]
    })

}


function Certian(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {

                    if (data.success) {
                        
                        dtbl.ajax.reload();
                       
                        toastr["error"](data.message);


                        


                    } else {
                      
                        toastr["error"](data.message);


                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    })

}




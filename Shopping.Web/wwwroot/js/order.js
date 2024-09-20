
var dtblOrder;
$(document).ready(function () {
    loadDataOrder();
})


function loadDataOrder() {
    dtblOrder = $("#myTable").DataTable({
        "ajax": {
            "url": "/Admin/Order/Get"
        },
        "columns": [
            { "data": "id" },
            { "data": "userData.fullName" },
            { "data": "userData.email" },
            { "data": "orderStatus" },
            { "data": "total" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                
                  <a href="/Admin/Order/Details/${data}" class="btn btn-secondary">Edit</a>





                            `
                }
            },




        ]
    })

}






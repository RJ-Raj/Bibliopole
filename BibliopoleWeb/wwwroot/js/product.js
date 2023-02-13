var dataTable;



$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-75 btn-group" role="group">
                            <a href = "/Admin/Product/Upsert?id=${data}"
                               class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a></div>
                            <div class="w-75 btn-group" role="group">
                            <a onClick=Delete("/Admin/Product/Delete?id=${data}")
                               class="btn btn-danger mx-2"> <i class="bi bi-trash"></i> Delete</a>
                        </div>
                        `
                },
                "width": "15%"
            },
        ]
    });
}

//href = "/Admin/Product/Delete?id=${data}"

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this product!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            swal(data.message, {
                                icon: "success",
                            });
                        }
                        else {
                            swal("Your product is not deleted!");
                        }
                    }
                })
            } 
        });
}
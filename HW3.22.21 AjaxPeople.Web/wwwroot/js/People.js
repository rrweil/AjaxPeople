$(() => {

    function fillTable() {
        $("tbody").empty();
        $.get('/home/getall', function (ppl) {
            ppl.forEach(p => {
                $("tbody").append(`
<tr>
    <td>${p.firstName}</td>
    <td>${p.lastName}</td>
    <td>${p.age}</td>
    <td><button class="btn btn-block btn-primary" id="edit" data-id="${p.id}">Edit</button></td>
    <td><button class="btn btn-block btn-primary" id = "delete" data-id="${p.id}">Delete</button></td>
</tr>`);
            });
        });
    }

    fillTable();

    $("tbody").on('click', "#edit", function () {
        let id = $(this).data('id');
        $.get('/home/getpersonbyid', { id }, function (p) {
            console.log(p);
            $("#modal-first-name").val(p.firstName);
            $("#modal-last-name").val(p.lastName);
            $("#modal-age").val(p.age);
            $("#modal-id").val(id);
        })
        $('#editModal').modal('show');

    });

    $('#save').on('click', function () {
        const firstName = $("#modal-first-name").val();
        const lastName = $("#modal-last-name").val();
        const age = $("#modal-age").val();
        const id = $("#modal-id").val();
        $.post('/home/updatePerson', { id, firstName, lastName, age }, function (p) {
            fillTable();
        })
        $('#editModal').modal('hide');
    })


    $("tbody").on('click', "#delete", function () {
        const id = $(this).data('id');
        $.post('/home/deleteperson', { id }, function (p) {
            fillTable();
        })
    })

    $("#add").on('click', function () {
        const firstName = $("#first-name").val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();

        $("#first-name").val('');
        $("#last-name").val('');
        $("#age").val('');

        $.post('/home/add', { firstName, lastName, age }, function (p) {
            fillTable();
        });

    });
})
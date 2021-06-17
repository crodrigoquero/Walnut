$('.btnDelete').click(function () {

    recordNumber = $(this).attr('value');

    $('#btnDelete' + recordNumber).hide();
    $('#imgDelete' + recordNumber).show();

});

$('.btnEdit').click(function () {

    recordNumber = $(this).attr('value');

    $('#btnEdit' + recordNumber).hide();
    $('#imgEdit' + recordNumber).show();

});

$('.btnDetails').click(function () {

    recordNumber = $(this).attr('value');

    $('#btnDetails' + recordNumber).hide();
    $('#imgDetails' + recordNumber).show();

});

//Special actions buttons (create, download, etc)
$('.btnNewFile').click(function () {

    recordNumber = $(this).attr('value');

    $('#btnNewFile' + recordNumber).hide();
    $('#imgNewFile' + recordNumber).show();

});
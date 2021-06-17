// This script Controls the status of conditional buttons in the CREATE and EDIT views  for the CANDIATE entity
//$(function () {
//    $('[visa-aditional-info]').hide(); //DEFAULT status is HIDEN
//});

//$(function () {
//    $('#VisaNeeded').click(function () { // switch VISA INFO FIELD visibility everytime user clicks the checkbox
//        if ($('[visa-aditional-info]').is(':visible')) {
//            $('[visa-aditional-info]').hide();
//        } else {
//            $('[visa-aditional-info]').show();
//        }
//    });
//});
//$('input[type="date"]').attr('type', 'text'); // to deactivate Google Chrome datepicker facilities
//$('input[type="date"]').datepicker().attr('type', 'text'); // to deactivate Google Chrome datepicker facilities (date dield arrow)

//task: to move next code lines in a separate file
//$(function () {
//    $('.datepicker').datepicker({
//        calendarWeeks: false,
//        todayHighlight: true,
//        format: 'dd/mm/yyyy',
//        autoclose: true
//    });
//});


//INITIAL SETUP FOR RADIO BUTON "VisaYesNo":
//Explanation: because at this point model data has already loaded,and has been used to setup
//UI elements, we can setup the conditional text field (contained into the "visa-aditional-info" DIV) accordingly
if ($('#visaYES').is(':checked')) { $('[visa-aditional-info]').show(); }
if ($('#visaNO').is(':checked')) { $('[visa-aditional-info]').hide(); }

$('#visaYES').click(function () {
    if ($('#visaYES').is(':checked')) { $('[visa-aditional-info]').show(); }

});

$('#visaNO').click(function () {
    if ($('#visaNO').is(':checked')) { $('[visa-aditional-info]').hide(); }
});

//Read the selected image from File upload HTML control
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ImageData').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

$("#PictureFile").change(function () {
    readURL(this);
});



// Adding a loading / saving (nice) effect when btn save is clicked
// in order to improve the user experience
$('#BtnSave').click(function () {
    $('#BtnSave').hide();
    $('#ImgLoader').show();

});
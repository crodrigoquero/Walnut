// PAGINATION: Use this script in every single data list
$(function () {

    $('.LoadingPageIcon').hide(); //using "hidden" HTML attribute as well

    $('.PagerPageNumber').click(function () {
        numeroImagen = $(this).attr('value');
        $('#PagerPageNumber' + numeroImagen).hide();
        $('#LoadingPageIcon' + numeroImagen).show();

        $('.PagerPageNumber').off('click'); // no more clicks please...

    });


});
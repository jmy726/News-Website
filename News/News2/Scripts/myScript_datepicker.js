/// <reference path="myScript_datepicker.js" />
$(function ()
{
    $('#datePub').datepicker({
        dateFormat: 'dd/mm/yy',
        changeYear: true,
        yearRange: "1947:2017"
    });
    $("#jourDob").datepicker({
        dateFormat: 'dd/mm/yy',
        changeYear: true,
        yearRange: "1947:2017"
    });
    $("#JourDob").datepicker({
        dateFormat: 'mm/dd/yy',
        changeYear: true,
        yearRange: "1947:2017"
    });
});
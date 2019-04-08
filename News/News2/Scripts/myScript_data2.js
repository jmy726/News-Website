$(document).ready(function () {
    var oTable = $('#data2').DataTable({

        "sDom": '<"navig"lf>t<"navig"i>',
        "order": [
           [2, 'desc']
        ],
        "sPaginationType": "full_numbers",
        //"columnDefs": [{
        //    "searchable": false,
        //    "targets": [2, 3, 4, 5]
        //}],
        language: {
            "sSearch": "Search all columns:",
            searchPlaceholder: "Search any office name, journalist name..."
        },
        "bJQueryUI": true,
        "bFilter": true
    });
    $('.dataTables_filter input').unbind().bind('keyup', function () {
        var colIndex = document.querySelector('#mySelect').selectedIndex;
        oTable.column(colIndex).search(this.value).draw();
    });
    //dt.fnCreateFilterColumnSelector();
});
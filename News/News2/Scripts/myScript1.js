
$(document).ready(function () {
    var dt = $('#data').DataTable({
        "sDom": '<"navig"fl>t<"navig"ip>',
        "order": [
                      [1, 'desc']
        ],
        "sPaginationType": "full_numbers",
        scrollY: "700px",
        scrollX: true,
        scrollCollapse: true,
        //"columnDefs": [{
        //    "targets": [0, 1, 5],
        //    "bSearchable": false
        //}],
        //{
        //    "targets": 3,
        //    width: '15%'
        //},
        paging: true,
        fixedColumns: true,
        "bJQueryUI": true,
        "bFilter": true,
        language: {
            searchPlaceholder: "Search any headlines, topics..."
        }
    });
    //dt.fnCreateFilterColumnSelector();
    $('.dataTables_filter input').unbind().bind('keyup', function () {
        var colIndex = document.querySelector('#mySelect_article').selectedIndex;
        dt.column(colIndex).search(this.value).draw();
    });

    $('#docu').DataTable({
        "sDom": '<l>t<i>',
    });
});
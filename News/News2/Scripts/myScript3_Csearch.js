
$(document).ready(function () {
    $('#user tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });
    var dt = $('#user').DataTable({
        "sDom": '<"navig"fl>t<"navig"ip>',
        "sPaginationType": "full_numbers",
        scrollY: "700px",
        scrollX: true,
        scrollCollapse: true,
        paging: true,
        fixedColumns: true,
        "bJQueryUI": true,
        "bFilter": true,
        language: {
            searchPlaceholder: "Search any user Names, emails..."
        }
    });
    // Apply the search
    dt.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
        //$('.dataTables_filter input').unbind().bind('keyup', function () {
        //    var colIndex = document.querySelector('#mySelect_article').selectedIndex;
        //    dt.column(colIndex).search(this.value).draw();
        //});
    });
});
(function ($, window, document) {
    $.fn.dataTableExt.oApi.fnCreateFilterColumnSelector = function (oSettings) {
        var filterColumnSelectElement;
        $(oSettings.aanFeatures.f).each(function (index, filterDiv) {
            filterDiv = $(filterDiv);
            // see if we have a select here or not yet
            if (filterDiv.find(".dataTables_filterColumnSelect").length == 0) {
                // nope, so add one ... see if we have build the select yet
                if (filterColumnSelectElement === undefined) {
                    // the select hasn't been built yet, build it now
                    filterColumnSelectElement = $("<select></select>").addClass("dataTables_filterColumnSelect").css({ width: "120px", marginLeft: "10px" });
                    // add the all option
                    $("<option>*all</option>").val("all").appendTo(filterColumnSelectElement);
                    // add our bSearchable columns
                    $(oSettings.aoColumns).each(function (index, column) {
                        // see if this is a searchable column
                        if (column.bSearchable) {
                            // it is, add it (be sure to trim and remove \n chars
                            $("<option></option>").text(column.sTitle.replace(/\n/g, "").trim()).val(index).appendTo(filterColumnSelectElement);
                        }
                    });
                    // bind change event to select
                    filterColumnSelectElement.change(function () {
                        var theSelect = $(this);
                        // change all other elements to the same value
                        $(oSettings.aanFeatures.f).each(function (index, filterDiv) {
                            $(filterDiv).find(".dataTables_filterColumnSelect").val(theSelect.val());
                        });
                        // instead of overriding what the input does with our on code, we just just toggle the "bSearchable" values of our
                        // columns when we want to target an individual column ...
                        // see if we are going to search all
                        if (theSelect.val() === "all") {
                            // change all our columns back to searchable
                            $(theSelect.find("option")).each(function (i, theOption) {
                                // make sure the option has a number as a val
                                var columnIndex = parseInt($(theOption).val());
                                if (!isNaN(columnIndex)) {
                                    // switch it back to bSearchable
                                    oSettings.aoColumns[columnIndex].bSearchable = true;
                                }
                            });
                        }
                        else {
                            // change all the columns to not searchable
                            $(oSettings.aoColumns).each(function (i, column) {
                                column.bSearchable = false;
                            });
                            // change our selected colum to b searchable
                            oSettings.aoColumns[parseInt(theSelect.val())].bSearchable = true;
                        }
                        // kick off a search again ... need to clear out the last search to get it to do anything
                        oSettings.oPreviousSearch.sSearch = "dummySearch";
                        $(theSelect).closest("div").find("input").trigger("keyup.DT");
                    });
                }
                // append a clone of our select element
                filterDiv.append(filterColumnSelectElement.clone(true));
                // increase the width of the div to fit the select
                filterDiv.css("width", (filterDiv.width() + filterColumnSelectElement.width() + 10).toString() + "px");
            }
        });
    }
})(jQuery, window, document);
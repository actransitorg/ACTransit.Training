// fix rate's cell positioning
FixCellLayout = function () {
    $('.cell-description').each(function(e) {
        $(this).html($(this).text());
    });
    $('div[data-content="RatingArea"]').each(function (e) {
        var height = $(this).height();
        $(this).find('.cell-upper').each(function (e2) {
            var cellHeight = $(this).height();
            var description = $(this).find('.cell-description').html();
            var diff = Math.abs(cellHeight - height);
            if (diff > 5) {
                $(this).find('.cell-description').append("<br/>");
                cellHeight = $(this).height();
            }
            diff = Math.abs(cellHeight - height);
            if (diff > 5) {
                $(this).find('.cell-description').append("<br/>");
                cellHeight = $(this).height();
            }
        });
    });
};

$(function () {
    if ($("#Rate").length == 1)
        FixCellLayout();
    $(window).resize(function () {
        FixCellLayout();
    });

});
if ($("#Rate").length > 0)
    $('html, body').animate({
        scrollTop: $("#Rate").length == 1 ? $("#Rate").offset().top : 0
    }, 10);

viewModel.Update = function () {
    if ($('#Progress_EvaluationDate').length > 0)
        viewModel.Progress.EvaluationDate = $('#Progress_EvaluationDate').val();
    if ($('#Progress_SuperintendentApprovalDate').length > 0)
        viewModel.Progress.SuperintendentApprovalDate = $('#Progress_SuperintendentApprovalDate').val();

    $('div[data-content="DayOff"]').each(function(index, elem) {
        var progressDayId = $(this).data('id');
        if (!progressDayId) return;
        var isDayOff = $(this).find("input:checked").val();
        if (viewModel.ProgressDay) {
            var p = viewModel.ProgressDay;
        } else {
            var p = viewModel.ProgressDays.firstOrDefault({ ProgressDayId: progressDayId });
        }
        if (p) {
            p.ApprenticeDayOff = isDayOff;
        }
    });

    $('div[data-content="DailyPerformance"]').each(function (index, elem) {
        var progressDayId = $(this).data('id');
        var dailyPerformanceId = $(this).find("input:checked").val();
        var comment = $(this).next().find("textarea").text();
        if (!dailyPerformanceId) return;
        dailyPerformanceId = parseInt(dailyPerformanceId);
        if (viewModel.ProgressDay) {
           var p = viewModel.ProgressDay;
        } else {
           var p = viewModel.ProgressDays.firstOrDefault({ ProgressDayId: progressDayId });
        }
        if (p) {
            p.DailyPerformanceId = dailyPerformanceId;
            p.Comment = comment;
        }
    });

    if (viewModel.Progress)
        viewModel.Progress.ProgressRatingCellScores = [];  // trash old values
    $('div[data-content="RatingArea"]').each(function (index, elem) {
        var ratingAreaId = $(this).data('id');
        var ratingCellScoreId = $(this).find("input:checked").val();
        if (!ratingCellScoreId) return;
        ratingCellScoreId = parseInt(ratingCellScoreId);
        viewModel.Progress.ProgressRatingCellScores.push({
            ProgressId: viewModel.Progress.ProgressId,
            RatingCellScoreId: ratingCellScoreId
        });
    });
}


viewModel.Post = function (that) {
    var url = that.prop("action");
    $.ajax({
        url: url,
        cache: false,
        type: that.prop("method").toUpperCase(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(this),
    }).fail(function (jqXhr, textStatus, errorThrown) {
        alert(errorThrown);
        window.location.hash = '#Rate';
        window.location.reload(true);
    }).done(function (data) {
        if (data == null) return;
        if (data.Model) {
            var model = JSON.parse(data.Model);
            if ((model.AreDailyEvaluationsDone) && (model.Progress.ScoreTotal == 0)) {
                window.location.hash = '#Rate';
                window.location.reload(true);
            }
            else
                location.href = '../../Apprentice';
        }
        else
            location.href = '../../Apprentice';
    });
}

var ratingAreasCount = 0;
var ratingAreasSelected = [];
var ratingAreasSelectedCount = 0;
$('div[data-content="RatingArea"]').each(function (index, elem) {
    ratingAreasCount++;
    var ratingAreaId = $(this).data('id');
    var ratingCellScoreId = $(this).find("input:checked").val();
    if (!ratingCellScoreId) return;
    ratingAreasSelected[ratingAreaId] = true;
    ratingAreasSelectedCount++;
});

$('div[data-content="RatingArea"] div input:radio').change(function () {
    var ratingAreaId = $(this).parent().parent().parent().parent().parent().data('id');
    var ratingCellId = $(this).parent().parent().data('id');
    var ratingCellScoreId = $(this).val();
    if (!ratingCellScoreId) return;
    if (!(ratingAreasSelected[ratingAreaId])) {
        ratingAreasSelected[ratingAreaId] = true;
        ratingAreasSelectedCount++;
    }
});

var warnIncomplete = false;
ResetDateTimePickerAlert = function () {
    setTimeout(function () {
        warnIncomplete = true;
    }, 100);
}
ResetDateTimePickerAlert();

$(".date").datetimepicker(
    {
        timepicker: false,
        format: 'm/d/Y',
        onShow: function (date, elem) {
            if (!warnIncomplete) return;
            if (ratingAreasCount != ratingAreasSelectedCount) {
                warnIncomplete = false;
                alert("Not all performance ratings have been selected.");
                ResetDateTimePickerAlert();
            }
        }
    });

$(document).ready(function () {
    if (window.location.hash && ($(window.location.hash).offset())) {
        $(document.body).animate({
            'scrollTop': $(window.location.hash).offset().top
        }, 1);
    }
});

PrintForm = function (selector, titleName, contentDir, scriptsDir) {
    var dispSetting = "toolbar=no,location=no,directories=no,menubar=no,scrollbars=no,width=816,height:1344,left=5,top=5";
    var printWindow = window.open("", "_blank", dispSetting);
    try {
        var content = ($(selector).length) ? $(selector).html() : $("body").html();
        // make some modification from on-screen version for pretty print -- make sure full week can fit on legal paper.
        content = content.replace(/textarea/g, "label");
        content = content.replace(/disabled=["']disabled["']/g, "");
        content = content.replace(/class="col-md-10 work-details" style="height: 60px;"/g, 'class="col-md-10 work-details" style="height: 21px;"');
        content = content.replace(/id="comments-supervisor"( style="height: 42px;"){0,1}/g, 'id="comments-supervisor" style="height: 12px;"');
        content = content.replace(/<span class="weekly-eval".*?\/span>/, '');
        content = content.replace(/id="Rate" style="padding-bottom: 10px;"/g, 'id="Rate"');

        var html =
'<!DOCTYPE html>' +
'<html lang="en" moznomarginboxes mozdisallowselectionprint>' +
'<head><title></title>' +
'<link type="text/css" rel="stylesheet" href="' + contentDir + '/bootstrap.min.css" />' +
'<link type="text/css" rel="stylesheet" href="' + contentDir + '/print.css" />' +
'<script src="' + scriptsDir + '/jquery-1.10.2.js"></script>' +
'<script src="' + scriptsDir + '/Apprentice/FixRateLayout.js"></script>' +
'</head><body><div class="container-fluid">' + content + '</div></body></html>';
        printWindow.document.open();
        printWindow.document.write(html);
    } catch (e) {
        alert("Error Printing: " + e.message);
    } finally {
        setTimeout(function () {
            printWindow.document.close();
            printWindow.print();
            setTimeout(function() {
                printWindow.close();
            }, 1000);
        }, 1000);
    }
}


Array.prototype.where = function (filter) {
    var collection = this;
    switch (typeof filter) {
        case 'function':
            return $.grep(collection, filter);
        case 'object':
            for (var property in filter) {
                if (!filter.hasOwnProperty(property))
                    continue; // ignore inherited properties
                collection = $.grep(collection, function (item) {
                    return item[property] === filter[property];
                });
            }
            return collection.slice(0); // copy the array
            // (in case of empty object filter)
        default:
            throw new TypeError('func must be either a' +
                'function or an object of properties and values to filter by');
    }
};
Array.prototype.firstOrDefault = function (func) {
    return this.where(func)[0] || null;
};

setupModalDetails = function() {
    $(".work-task").click(function(){
        var elem = $(this);
        var title = elem.data("title");
        var content = elem.data("work");
        $(".modal-body").html(content);
        $("#dialog-title").html("Work Completed for " + title);
        $("#dialog-container").modal('show');
    });
}
setupModalDetails();
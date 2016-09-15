if (ko && ko.bindingHandlers && !ko.bindingHandlers.dateTimeExtender) {
    ko.bindingHandlers.dateTimeExtender = {
        init: function (elem, valueAccessor, allBindings) {
            var options = allBindings.get('dateTimeOptions') || {};
            var minDate = options.minDate || null;
            var maxDate = options.maxDate || null;
            options.minDate = false;
            options.maxDate = false;
            var tempShow = options.onShow;
            if (minDate || maxDate) {
                options.onShow = function (ct) {
                    var min = ko.observable(minDate) ? ko.utils.unwrapObservable(minDate) : minDate;
                    var max = ko.observable(maxDate) ? ko.utils.unwrapObservable(maxDate) : maxDate;
                    this.setOptions({
                        formatDate:options.format,
                        minDate: min != null ? min : false,
                        maxDate: max != null ? max : false
                    });
                    if (tempShow && typeof tempShow == "function") tempShow();
                };
            }
            $(elem).dateTimeExtender(options);
        },
        update: function (elem, valueAccessor, allBindings) {
        }
    }
}

(function ($) {
    $.fn.dateTimeExtender = function (options) {
        "use strict";
        var me = this;
        options = options || {};
        var openImageUrl = typeof (options.openImageUrl) === 'undefined' ? null : options.openImageUrl;
        var resetImageUrl = typeof (options.resetImageUrl) === 'undefined' ? null : options.resetImageUrl;
        var resetImageClass = typeof (options.resetImageClass) === 'undefined' ? null : options.resetImageClass;

        function initial() {
            $(me).datetimepicker(options);            
            if (resetImageClass || resetImageUrl) {
                var resetImage = '';
                if (resetImageUrl)
                    resetImage = $("<img src='" + resetImageUrl + "' class='btn btn-sm imgbtn-sm' title='Reset' data-type-usage='resetCalendar' style='margin-left:5px;'/>");
                else
                    resetImage = $("<a href='#' title='Reset' data-type-usage='resetCalendar' style='margin-left:5px;'><span class='" + resetImageClass + "'></span></a>");

                $(me).after(resetImage);
                resetImage.click((function (tId) {
                    return function () {
                        $(tId).val('');
                        $(tId).blur();

                    }
                })(me));
            }

            if (openImageUrl != null) {
                var openImage = $("<img src='" + openImageUrl + "' class='btn btn-sm imgbtn-sm' title='Open Calendar' data-type-usage='openCalendar' style='margin-left:5px;' />");
                $(me).after(openImage);
                openImage.click((function (tId) {
                    return function () {
                        $(tId).datetimepicker({ open: 'show'});
                    }
                })(me));
            }       
        }

        initial();
        return this;
    };

}(jQuery));

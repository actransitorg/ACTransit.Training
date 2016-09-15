if (ko && ko.bindingHandlers && !ko.bindingHandlers.time) {
    ko.bindingHandlers.time = {
        init: function (elem, valueAccessor, allBindings) {
            var options = allBindings.get('timeOptions') || {};
            var time = $(elem).time(options);
            time.reset();
        }
    }
}

(function ($) {
    $.fn.numeric = function (options) {
        "use strict";        
        var me = this;
        var allowKeyCodes = [8, 35, 36, 37, 38, 39, 40, 46];
        var allowKeyCodesJustOnce = [110,190]; //. 
        options = options || {};

        var floatAllowd = typeof (options.float) === 'undefined' ? null : options.float;
        var max = typeof (options.max) === 'undefined' ? null : options.max;
        var onInitialized = typeof (options.onInitialized) === 'undefined' ? null : options.onInitialized;

        var isSelected=false;
     
        function initial() {
            $(me).bind("paste", function (e) {
                var oldvalue = e.target.value;
                setTimeout(function () {
                    var passed = $(me).val();
                    if (!$.isNumeric(passed) || max && passed && passed.length > max)
                        $(me).val(oldvalue);
                }, 100);

            });
            $(me).select(function(e) {
                var start = e.target.selectionStart;
                var end = e.target.selectionEnd;
                isSelected = start != end;
                
            });
            $(me).keydown(function (e) {
                var value = $(me).val();
                var allowAllTime = allowKeyCodes.indexOf(e.keyCode);
                var allowOnce = allowKeyCodesJustOnce.indexOf(e.keyCode);
                var start = e.target.selectionStart;
                var end = e.target.selectionEnd;
                isSelected = start != end;
                if (!isSelected && allowAllTime == -1 && max && value && value.length >= max)
                    return false;

                if (!$.isNumeric(e.char) && allowAllTime == -1 && allowOnce==-1) {
                    return false;
                }
                else if (e.char == '.') {
                    if (!floatAllowd)
                        return false;
                    else if (e.char == '.' && value.indexOf(".") != -1)
                        return false;
                }
            });
            if (onInitialized && typeof onInitialized === 'function')
                onInitialized(me);

        }
     

        initial();
        return this;
    };

}(jQuery));
(function ($) {
    $.fn.time = function (options) {
        "use strict";
        var me = this;
        var resetValue = '__:__';
        var validRegex24 = /([01]\d|2[0-3]):([0-5]\d)/;
        var validRegex = /(\d\d):([0-5]\d)/;
        var allowKeyCodes = [8, 16, 35, 36, 37, 38, 39, 40, 46];
        var notAllowdChars=["'",'#','$','%','&','(','.'];
        options = options || {};
        

        var max = typeof (options.max) === 'undefined' ? null : options.max;
        var shouldValidate = typeof (options.validate) === 'undefined' ? true : options.validate;
        var onInitialized = typeof (options.onInitialized) === 'undefined' ? null : options.onInitialized;
        var restrict24 = typeof (options.restrict24) === 'undefined' ? true : options.restrict24;

        me.getTime = function (value) {
            if (!value) value = $(me).val();
            if (me.isEmpty(value))
                return null;
            var values = value.split(':');
            var hour = values.length > 0 ? values[0] : null;
            var minutes = values.length > 1 ? values[1] : null;
            hour = parseInt(hour, 10);
            minutes = parseInt(minutes, 10);
            if (isNaN(hour) && isNaN(minutes))
                return null;
            if (isNaN(hour))
                return new Date(0, 0, 0, 0, minutes, 0, 0);
            if (isNaN(minutes))
                return new Date(0, 0, 0, hour, 0, 0, 0);
            return new Date(0, 0, 0, hour, minutes, 0, 0);
        };
        me.reset = function() {
            $(me).val(resetValue);
        };
        me.validate = function () {
            var error = false;
            if (shouldValidate) {
                var value = $(me).val();
                error = !isValidEntry(value);
            }
            if (error)
                me.setError();
            else
                me.clearError();
        };
        me.isValid = function() {
            return isValidEntry($(me).val());
        };
        me.setError = function() {
            $(me).addClass("input-validation-error");
        };
        me.clearError = function() {
            $(me).removeClass("input-validation-error");
        };
        me.isEmpty = function() {
            var v = $(me).val();
            if (v == null)
                return true;
            v = v.trim();
            if (v == '' || v == resetValue || v=='00:00')
                return true;
            return false;
        };

        function initial() {
            $(me).val(resetValue);
            $(me).bind("paste", function(e) {
                var oldvalue = e.target.value;
                setTimeout(function() {
                    var passed = $(me).val();
                    var o = { start: 0 };
                    var newValue = formatValue(passed,o);
                    if (!o.stop)
                        $(me).val(newValue);
                    else
                        $(me).val(oldvalue);
                }, 100);
            });
            $(me).on('blur', function () {
                me.validate();
            });
            $(me).keypress(function (e) {
                var key = getKey(e);
                var value = $(me).val();
                var allowAllTime = allowKeyCodes.indexOf(e.keyCode);
                var notAllowdCharsFound = notAllowdChars.indexOf(key)!=-1;
                if (allowAllTime != -1 && !notAllowdCharsFound)
                    return true;
                var cursor = getInputSelection(e.target);
                var start = cursor.selectionStart;
                var end = cursor.selectionEnd;
                
                if (end - start == value.length)
                    me.reset();
                if (notAllowdCharsFound || (key != "Shift" && !isValidKey(e)))
                    return false;
                if (start == 2) start++;
                if (start == end) end++;
                var newValue=value.replaceAt(start, end, key);
                if (newValue.length > max)
                    return false;
                
                var o = {start:start};
                newValue = formatValue(newValue, o);
                if  (!o.stop)
                    $(me).val(newValue);
                $(me).selectRange(o.start, o.start);

                return false;
                
            });

            if (onInitialized && typeof onInitialized === 'function')
                onInitialized(me);


            function formatValue(value, formatOptions) {
                formatOptions.start = formatOptions.start || 0;
                formatOptions.stop = formatOptions.stop || false;
                var originalStart = formatOptions.start;
                value = value.replace(":", "");
                value = paddingRight(value, "_", 4);

                
                var hour = value.substring(0,2);
                var min = value.substring(2, 4);

                hour = hour.replace("_", "");
                min = min.replace("_", "");
                if (hour != '') {
                    var t = parseInt(hour, 10);
                    if (isNaN(t))
                        hour = '__';
                    else if (restrict24 && hour > 23) {
                        hour = 23;
                        formatOptions.start = 3;
                    }                        
                    else if (restrict24 && hour.length == 1 && hour > 2) {
                        hour = '0' + hour;
                        formatOptions.start = 3;
                    }                        
                    else if (restrict24 && hour.length == 2 && hour[0] == '2' && hour[1] > 3) {
                        hour[1] = '3';
                        formatOptions.stop = true;
                    }
                    else if (hour.length == 1 && formatOptions.start == 1) {
                        hour = '0' + hour;
                        formatOptions.start = 3;
                    }
                }
                if (min != '') {
                    var t = parseInt(min, 10);
                    if (isNaN(t))
                        min = '__';
                    else if (min > 59) {
                        min = 59;
                        formatOptions.start = 5;
                    }
                    else if (min.length == 1 && min > 5) {
                        min = '0' + min;
                        formatOptions.start = 5;
                    }
                    else if (min.length == 1 && formatOptions.start == 4)
                        min = '0' + min;
                }
                if (formatOptions.start==originalStart)
                    formatOptions.start++;
                return paddingRight(hour, '_', 2) + ':' + paddingRight(min, '_', 2);
            }
          
            function isValidKey(e) {
                var key = getKey(e);
                var allowAllTime = allowKeyCodes.indexOf(e.keyCode);

                if (!$.isNumeric(key) && allowAllTime == -1) {
                    return false;
                } 
                return true;
            }           

            function paddingRight(str, chr, maxLength) {
                if (str == null)
                    str = '';
                str = str.toString();
                if (str.length >= maxLength)
                    return str;
                var deltaLength = maxLength - str.length;
                for (var i = 0; i < deltaLength; i++)
                    str = str + chr;
                return str;
            }        
        }
        function getKey(e) {
            if (e.char)
                return e.char;
            var location = e.originalEvent.location;
            var keycode = e.which || e.keyCode;
            if (location == 3 && e.type && e.type!='keypress') //numpad
            {
                keycode = keycode - 48;
            }
            return String.fromCharCode(keycode);
        }

        function isValidEntry(value) {
            if (restrict24)
                return me.isEmpty(value) || validRegex24.test(value);
            else
                return me.isEmpty(value) || validRegex.test(value);
        }
        initial();
        return this;
    }
 
}(jQuery));



function getInputSelection(el) {
    var start = 0, end = 0, normalizedValue, range,
        textInputRange, len, endRange;
    if (typeof el.selectionStart == "number" && typeof el.selectionEnd == "number") {
        start = el.selectionStart;
        end = el.selectionEnd;
    } else {
        range = document.selection.createRange();

        if (range && range.parentElement() == el) {
            len = el.value.length;
            normalizedValue = el.value.replace(/\r\n/g, "\n");

            // Create a working TextRange that lives only in the input
            textInputRange = el.createTextRange();
            textInputRange.moveToBookmark(range.getBookmark());

            // Check if the start and end of the selection are at the very end
            // of the input, since moveStart/moveEnd doesn't return what we want
            // in those cases
            endRange = el.createTextRange();
            endRange.collapse(false);

            if (textInputRange.compareEndPoints("StartToEnd", endRange) > -1) {
                start = end = len;
            } else {
                start = -textInputRange.moveStart("character", -len);
                start += normalizedValue.slice(0, start).split("\n").length - 1;

                if (textInputRange.compareEndPoints("EndToEnd", endRange) > -1) {
                    end = len;
                } else {
                    end = -textInputRange.moveEnd("character", -len);
                    end += normalizedValue.slice(0, end).split("\n").length - 1;
                }
            }
        }
    }

    return {
        selectionStart: start,
        selectionEnd: end
    };
}
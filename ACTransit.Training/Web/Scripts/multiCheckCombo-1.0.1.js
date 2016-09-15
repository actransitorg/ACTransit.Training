if (ko && ko.bindingHandlers && !ko.bindingHandlers.multiCheckCombo) {
    ko.bindingHandlers.multiCheckCombo = {
        init: function (elem, valueAccessor, allBindings) {
            var onClosedfunc = allBindings.get('onClosed') || function () { };
            var showAllButton = typeof (allBindings.get('showAll')) == 'undefined' ? true : allBindings.get('showAll');
            var allButtonObj = allBindings.get('allButton') || { text: 'All', top: true };            
            var delimiterStr = allBindings.get('delimiter') || ',';
            var optionsText = allBindings.get('optionsText') || 'text';
            var optionsValue = allBindings.get('optionsValue') || 'value';
            var optionsSelected = allBindings.get('optionsSelected') || 'selected';
            var multiCheckComboInitilized = allBindings.get('multiCheckComboInitilized') || null;
            var disabled = allBindings.get('disabled') || null;
            var value = allBindings.get('multiComboValue') || null;
            var valueUnwrapped = ko.unwrap(valueAccessor());
            if (value && ko.isObservable(value)) value.allowRefresh = true;
            if (!showAllButton)
                allButtonObj = undefined;
            var multiCombo = $(elem).multiCheckCombo({
                onClosed: function (e) {
                    if (value != null) {
                        if (value && ko.isObservable(value)) value.allowRefresh = false;
                        value(multiCombo.toValues());
                        if (value && ko.isObservable(value)) value.allowRefresh = true;
                    }
                        
                    onClosedfunc(e);
                },
                allButton: allButtonObj,
                delimiter: delimiterStr,
            });            
            $.data(elem, elem.id, multiCombo);
            multiCombo.removeAll();
            ko.utils.arrayForEach(valueUnwrapped, function (obj) {
                multiCombo.add(ko.unwrap(obj[optionsText]), ko.unwrap(obj[optionsValue]), ko.unwrap(obj[optionsSelected]));
            });
            multiCombo.refresh();
            if (value != null)
                value(multiCombo.toValues());
            if (multiCheckComboInitilized && typeof multiCheckComboInitilized === "function")
                multiCheckComboInitilized(multiCombo);
            if (disabled) {
                if (ko.isObservable(disabled)) {
                    multiCombo.disable(disabled());
                    disabled.subscribe(function () {
                        multiCombo.disable(disabled());
                    });
                } else {
                    multiCombo.disable(disabled);
                }
            }
        },
        update: function (elem, valueAccessor, allBindings) {
            var optionsText = allBindings.get('optionsText') || 'text';
            var optionsValue = allBindings.get('optionsValue') || 'value';
            var optionsSelected = allBindings.get('optionsSelected') || 'selected';
            var value = allBindings.get('multiComboValue') || null;
            var valueUnwrapped = ko.unwrap(valueAccessor());
            var multiCombo = $.data(elem, elem.id);
            multiCombo.removeAll();
            ko.utils.arrayForEach(valueUnwrapped, function (obj) {
                multiCombo.add(ko.unwrap(obj[optionsText]), ko.unwrap(obj[optionsValue]), ko.unwrap(obj[optionsSelected]));
            });
            multiCombo.refresh();
            if (value != null)
                value(multiCombo.toValues());
            value.subscribe(function () {
                if (value && ko.isObservable(value) && value.allowRefresh) {
                    multiCombo.fromValues(ko.unwrap(value));
                    multiCombo.refresh();
                }
                    
            });
        }
    }
}

(function ($) {
    $.fn.multiCheckCombo = function (options) {
        "use strict";
        var me = this;
        options = options || {};

        var _allButton = typeof (options.allButton) === 'undefined' ? null : options.allButton;
        var _delimiter = typeof (options.delimiter) === 'undefined' ? ';' : options.delimiter;
        var autoInitial = typeof (options.autoInit) === 'undefined' ? true : options.autoInit;

        var contentDiv,scrollDiv,ul;
        var _canBeClosedByFocusout = false;
        var _valueWhenOpened = '';
        var _disbaled = false;
        

        me.onClosed = function () {
            var temp = me.toValueString();
            var e = { 'changedSinceOpened': false, valueString:temp };
            if (temp != _valueWhenOpened) {
                _valueWhenOpened=temp;
                e = { 'changedSinceOpened': true, valueString: temp };
            }
            if (typeof (options.onClosed) !== 'undefined') 
                options.onClosed.call(me,e);
        };

        me.onClosing = function () {
            var canContinue = true;
            if (typeof (options.onClosing) !== 'undefined') {
                var e = { 'continue': true };
                options.onClosing.call(me,e);
                canContinue = e["continue"];
            }
            if (canContinue) { setValues(); }
            return canContinue;
        };
        me.onOpen = function () {
            _valueWhenOpened=me.toValueString();
            if (typeof (options.onOpen) !== 'undefined') {
                var e = { 'continue': true };
                options.onOpen.call(me,e);
                return e["continue"];
            }
            return true;
        };

        me.isOpen = function () {
            return $(contentDiv).is(':visible');
        };

        me.toggle = function () {
            var allowToProceed = false;
            var callClosed = false;
            if (me.isOpen.call(me)) {
                if (me.onClosing.call(me)) { allowToProceed = true;
                    callClosed = true;
                }
            }
            else if (me.onOpen.call(me)) {
                allowToProceed = true;
            }
            if (allowToProceed)
                $(contentDiv).toggle(
                    {
                        duration: 100, done: function () {
                            if (callClosed)  me.onClosed.call(me);                            
                        }
                    }
                );
        };

        me.close = function () {
            var allowToProceed = false;
            var closing = false;
            if (me.isOpen.call(me)) {
                closing = true;
                if (me.onClosing.call(me)) allowToProceed = true;
            }
            if (allowToProceed) {                
                $(contentDiv).toggle();
                if (closing ) me.onClosed.call(me);
            }
        };

        me.toString = function () {
            var selectedStr = '';
            $(ul).find('>li>input[type="checkbox"]').each(function () {
                if ($(this).attr('data-multiCombo-role') !== 'all') {
                    if ($(this).prop('checked')) {
                        if (selectedStr.length > 0) selectedStr += _delimiter + " ";
                        selectedStr += $(this).next('span').html();
                    }
                }
            });
            return selectedStr;
        };

        me.toSelectListItems = function() {
            var result = [];
            $(ul).find('>li>input[type="checkbox"]').each(function () {
                if ($(this).attr('data-multiCombo-role') !== 'all') {
                    var obj = new Object();
                    obj.Text = $(this).next('span').html();
                    obj.Value = $(this).attr('data-value');
                    obj.selected = $(this).prop('checked');
                    result.push(obj);
                }
            });
            return result;
        };

        me.toValueString = function () {
            var selectedStr = '';
            $(ul).find('>li>input[type="checkbox"]').each(function () {
                if ($(this).attr('data-multiCombo-role') !== 'all') {
                    if ($(this).prop('checked')) {
                        if (selectedStr.length > 0) selectedStr += _delimiter;
                        selectedStr += $(this).attr('data-value');
                    }
                }
            });
            return selectedStr;
        };

        me.fromValueString = function (valueString) {
            if (valueString) {
                var v = valueString.split(_delimiter);
                me.fromValues(v);
            } else
                me.deSelectAll();
        }

        me.toValues = function () {
            var selectedStr = [];
            $(ul).find('>li>input[type="checkbox"]').each(function () {
                if ($(this).attr('data-multiCombo-role') !== 'all') {
                    if ($(this).prop('checked')) {
                        selectedStr.push($(this).attr('data-value'));
                    }
                }
            });
            return selectedStr;
        };

        me.fromValues = function (values) {
            if (values) {
                $(ul).find('>li>input[type=checkbox]').each(function (e) {
                    var val = $(this).attr('data-value');
                    if (inArrayStringComp(val, values) != -1)
                        $(this).prop('checked', true);
                    else
                        $(this).prop('checked', false);
                });
                setValues();
            } else {
                me.deSelectAll();
            }
        }

        me.add = function (name, value, checked) {
            var checkedStr = '';
            if (checked)
                checkedStr = 'checked="checked"';
            $(ul).append('<li><input type="checkbox" value="' + value + '" data-value="' + value + '" ' + checkedStr + '/><span>' + name + '</span></li>');
        };

        me.remove = function (name, value, checked) {
            var input = $(ul).find('li input[data-value="' + value + '"');
            if (input.length > 0)
                input.parent('li').remove();
        };

        me.removeAll = function () {
            var input = $(ul).empty();
        };

        me.count = function() {
            return $(ul).find("li").length;
        };


        me.selectAll = function () { setAll(true); };

        me.deSelectAll = function () { setAll(false); };

        me.refresh = function () {
            initial();
        };

        me.disable= function(disable) {
            _disbaled = disable;
            setEnablity();
        };

        function initial() {
            contentDiv = $(me).find('>div').first();
            scrollDiv = $(contentDiv).find('>div').first();
            ul = $(scrollDiv).find('>ul');

            if (ul.length === 0) ul = $(scrollDiv).find('>ol');
            
            $(me).addClass("multiCheckCombo");
            setEnablity();

            $(me).find('#multiCheckCombo_span_down').remove();
            $(me).find('>input[type="text"]').after("<span id='multiCheckCombo_span_down'>&or;</span>");
            $(me).find("li>input[data-multiCombo-role='all']").parent("li").remove();
            if (_allButton && _allButton.top) ul.prepend("<li><input type='checkbox' data-multiCombo-role='all' />" + _allButton.text + "</li>");
            else if (_allButton && !_allButton.top) ul.append("<li><input type='checkbox' data-multiCombo-role='all' />" + _allButton.text + "</li>");                                

            $(me).mouseleave(function () { _canBeClosedByFocusout = true; });
            $(me).mouseenter(function () { _canBeClosedByFocusout = false; });
            
            $(me).focusout(function () {
                if (_canBeClosedByFocusout) {
                    me.close.call(me);
                }
            });

            $(me).unbind('click');
            $(me).click(function () {
                if (!_disbaled) me.toggle.call(me);
            });
            
            $(contentDiv).unbind('click');
            $(contentDiv).click(function (e) {
                e.stopPropagation();
            });
            $(ul).find(">li").unbind('click');            
            $(ul).find(">li").click(function (e) {                
                var checked = $(this).find('>input[type="checkbox"]').prop('checked');
                $(this).find('>input[type="checkbox"]').prop('checked', !checked);
                $(this).find('>input[type="checkbox"]').each(function () { $(this).change(); });
                e.stopPropagation();
            });
            $(ul).find(">li>input[type='checkbox']").unbind('change');
            $(ul).find(">li>input[type='checkbox']").change(function (e) {
                var checked = $(this).prop('checked');
                if ($(this).attr('data-multiCombo-role') === 'all') {
                    $(ul).find(">li>input[type='checkbox']").prop('checked', checked);
                }
                checkforAll();                
            });
            $(ul).find(">li>input[type='checkbox']").unbind('click');
            $(ul).find(">li>input[type='checkbox']").click(function (e) {
                e.stopPropagation();
            });

            initialValues();
        }

        function setEnablity() {
            $(me).find("input").prop('disabled', _disbaled);
            $(me).removeClass("disabled");
            $(me).removeClass("enabled");

            if (_disbaled) 
                $(me).addClass("disabled");
            else 
                $(me).addClass("enabled");
        }

        function initialValues() {
            setValues();
            checkforAll();
        }

        function checkforAll() {
            var allSelected = true;
            $(ul).find('>li>input[type=checkbox]').each(function (e) {
                if (!$(this).prop('checked') && $(this).attr('data-multiCombo-role') !== 'all') {
                    allSelected = false;
                    return;
                }
            });
            var all = $(ul).find('>li>input[type="checkbox"][data-multiCombo-role]');
            all.each(function () { $(this).prop('checked', allSelected); });
        }

        function setAll(checked) {
            $(ul).find('>li>input[type=checkbox]').each(function (e) {
                $(this).prop('checked', checked);
            });
            setValues();
        }

        function setValues() {
            $(me).find('>input[type="text"]').val(me.toString());
            $(me).find('>input[type="hidden"]').val(me.toValueString());
        }
        function inArrayStringComp(elem, array) {
            if (elem && array) {
                array = array || [];
                for (var i = 0; i < array.length; i++) {
                    if (elem.toString() == array[i].toString())
                        return i;
                }
            }
            return -1;
        }

        if (autoInitial)
            initial();
        return this;
    };

}(jQuery));

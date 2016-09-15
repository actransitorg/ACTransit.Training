if (typeof String.prototype.replaceAt !== 'function') {
    String.prototype.replaceAt = function(index, endindex, character) {
        return this.substr(0, index) + character + this.substr(index + endindex);
    }
}
if (typeof String.prototype.trim !== 'function') {
    String.prototype.trim = function () {
        return this.replace(/^\s+|\s+$/g, '');
    }
}
if (typeof Date.prototype.parseTime !== 'function') {
    Date.prototype.parseTime = function (value) {
        if (value == null || value == '')
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
    }
}
if (typeof Date.prototype.addMinutes !== 'function') {
    Date.prototype.addMinutes = function (value) {
        return new Date(this.getTime() + value * 60000);
    }
}

$.fn.selectRange = function (start, end) {
    if (!end) end = start;
    return this.each(function () {
        if (this.setSelectionRange) {
            this.focus();
            this.setSelectionRange(start, end);
        } else if (this.createTextRange) {
            var range = this.createTextRange();
            range.collapse(true);
            range.moveEnd('character', end);
            range.moveStart('character', start);
            range.select();
        }
    });
}

if (ko && ko.bindingHandlers && !ko.bindingHandlers.enableAll) {
    ko.bindingHandlers.enableAll = {
        update: function (elem, valueAccessor) {
            var enabled = ko.utils.unwrapObservable(valueAccessor());

            ko.utils.arrayForEach(elem.getElementsByTagName('input'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('a'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('select'), function (i) {
                i.disabled = !enabled;
            });
            ko.utils.arrayForEach(elem.getElementsByTagName('textarea'), function (i) {
                i.disabled = !enabled;
            });            
        }
    }
}

if (ko && ko.bindingHandlers && !ko.bindingHandlers.autocomplete) {
    ko.bindingHandlers.autocomplete = {
        init: function (elem, valueAccessor, allBindings) {
            var oldValue='';
            var autocompleteOptions = allBindings.get('autocompleteOptions') || { showOnClick: false, minLength: 1};
            autocompleteOptions.showOnClick = typeof autocompleteOptions.showOnClick === 'undefined' ? false : autocompleteOptions.showOnClick;
            autocompleteOptions.autoFocus = typeof autocompleteOptions.autoFocus === 'undefined' ? true : autocompleteOptions.autoFocus;
            autocompleteOptions.minLength = typeof autocompleteOptions.minLength === 'undefined' ? 1 : autocompleteOptions.minLength;
            autocompleteOptions.delay = typeof autocompleteOptions.delay === 'undefined' ? 300 : autocompleteOptions.delay;
            autocompleteOptions.label = typeof autocompleteOptions.label === 'undefined' ? null : autocompleteOptions.label;
            autocompleteOptions.optionsClose = typeof autocompleteOptions.optionsClose === 'undefined' ? function (event, ui) {
                $(elem).blur(); $(elem).focus(); $(elem).trigger("change");
            }: autocompleteOptions.optionsClose;
            $(elem).autocomplete({
                source: valueAccessor.call(this),
                html: true,
                autoFocus : autocompleteOptions.autoFocus,
                minLength: autocompleteOptions.minLength,
                delay: autocompleteOptions.delay,
                change: function (event, ui) {
                    var source = ui.item || null;
                    if (source == null) {
                        if (autocompleteOptions.label && typeof autocompleteOptions.label == "function")
                            autocompleteOptions.label('');
                    } else {
                        if (autocompleteOptions.label && typeof autocompleteOptions.label == "function") {
                            if (source.name)
                                autocompleteOptions.label(source.name);
                            else
                                autocompleteOptions.label('');
                        }
                    }
                },
                close: autocompleteOptions.optionsClose
        });
            if (autocompleteOptions.showOnClick) {
                $(elem).click(function () {
                    var menu = $(elem).autocomplete("widget");
                    if (menu.css('display') == 'none')
                        $(elem).autocomplete("search", "");
                    else
                        $(elem).autocomplete("close");
                });
            }            
        },
        update: function (elem, valueAccessor, allBindings) {
        }
    }
}

var Common = {    
    hasQueryString:function() {
        return document.URL.indexOf("?") > -1;
    },
    removeQueryString: function () {
        try {
            var hasPushstate = !!(window.history && history.pushState);
            if (hasPushstate) {
                var url = document.URL.split('?')[0];
                var hash = document.URL.split('#')[1];
                url = url + "#" + hash;
                window.history.pushState(null, "after quesry string removed.", url);
            }
        }
        catch(e) {}
    },
    redirect:function(url) {
        window.location = url;
    },
    Date: {
        parse: function (str, format) {
            if (!str)
                return null;
            var dateSpliter = '/', timeSpliter=':';
            var dateStr='',fDateStr='', timeStr='',fTimeStr='';
            var i,f, s;
            var y = 1, m = 0, d = 1, hour = 0, minute = 0, second = 0;
            if (!format) format = 'mm/dd/yyyy';
            if (format.indexOf(' ') != -1) {
                fDateStr = format.split(' ')[0];
                fTimeStr = format.split(' ')[1];
            }
            else if (format.indexOf(':') != -1)
                fTimeStr = format;
            else
                fDateStr = format;

            if (str.indexOf(' ')!=-1) {
                dateStr = str.split(' ')[0];
                timeStr = str.split(' ')[1];
            }
            else if (str.indexOf(':') != -1)
                timeStr = str;
            else
                dateStr = str;

            if (fDateStr.indexOf('.')!=-1)
                dateSpliter = '.';
            f = fDateStr.split(dateSpliter);
            s = dateStr.split(dateSpliter);
            for (i = 0; i < f.length; i++) {
                if (f[i].indexOf('m') != -1 && s.length > i)
                    m = parseInt(s[i],10)-1;
                else if ((f[i].indexOf('y') != -1 || f[i].indexOf('Y') != -1) && s.length > i)
                    y = parseInt(s[i], 10);
                else if (f[i].indexOf('d') != -1 && s.length > i)
                    d = parseInt(s[i], 10);
            }
            f = fTimeStr.split(timeSpliter);
            s = timeStr.split(timeSpliter);
            for (i = 0; i < f.length; i++) {
                if (f[i].indexOf('h') != -1 && s.length > i)
                    hour = parseInt(s[i], 10);
                else if ((f[i].indexOf('M') != -1 || f[i].indexOf('m') != -1) && s.length > i)
                    minute = parseInt(s[i], 10);
                else if (f[i].indexOf('s') != -1 && s.length > i)
                    second = parseInt(s[i], 10);
            }
            if (y<100)  //y.length<=2
                y += 2000;
            return new Date(y,m,d,hour,minute,second);
        }
        , formatDate: function (date) {
            var m = date.getMonth() + 1;
            if (m < 10)
                m = '0' + m.toString();
            var d = date.getDate();
            if (d < 10)
                d = '0' + d.toString();

            return m + "/" + d + "/" + date.getFullYear();
        }
    },
    LocalStorage: {
        get: function (key) {
            if (typeof (Storage) !== "undefined") {
                try {
                    if (window.localStorage)
                        return localStorage.getItem(key);
                } catch (e) {alert(e);}
            }
            return null;
        },
        getInt: function (key) {
            if (typeof (Storage) !== "undefined") {
                try {
                    if (window.localStorage) {
                        var t = localStorage.getItem(key);
                        return parseInt(t, 10);
                    }
                } catch (e) { alert(e); }
            }
            return null;
        },
        set: function (key, value) {
            if (typeof (Storage) !== "undefined" && value) {
                if (window.localStorage)
                    localStorage.setItem(key, value);
            }
        },
        remove: function (key) {
            if (typeof (Storage) !== "undefined") {
                if (window.localStorage) return localStorage.removeItem(key);
            }
        },
    },
    Cookie: {
        get:function(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for(var i=0; i<ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0)==' ') c = c.substring(1);
                if (c.indexOf(name) == 0) return c.substring(name.length,c.length);
            }
            return "";
        },
        set: function (cname, cvalue, exdays) {
            exdays = exdays || 1;
            var d = new Date();
            d.setTime(d.getTime() + (exdays*24*60*60*1000));            
            var expires = "expires="+d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        }
    }
}

function EditableGrid(table,options) {
    var me = this;
    var thead, tbody,inputs;
    var template=null;
    options = options || {};
    this.add=function() {
        $($(template).clone()).appendTo(tbody);
    };
    function initial() {
        thead = $(table).find("thead");
        tbody = $(table).find("tbody");
        template = $(tbody).find("tr[data-control-type='template']");
        inputs = $(template).find("input");
        options.allowAdd = options.allowAdd || true;
        options.allowEdit = options.allowEdit || true;        
        options.beginEdit = options.beginEdit || null;
        options.endEdit = options.beginEdit || null;
        inputs.focus(function () {
            if (options.beginEdit) {
                options.beginEdit(this, $(this).parent("td").index());
            }
        });
        inputs.blur(function () {
            if (options.endEdit) {
                options.endEdit(this,$(this).parent("td").index());
            }
        });
        return me;
    };
    return initial();
}

      //var s = ["c++", "java", "php", "coldfusion", "javascript", "asp", "ruby"];
        /*
         * jQuery UI Autocomplete HTML Extension
         *
         * Copyright 2010, Scott González (http://scottgonzalez.com)
         * Dual licensed under the MIT or GPL Version 2 licenses.
         *
         * http://github.com/scottgonzalez/jquery-ui-extensions
         */
        (function($) {

            var proto = $.ui.autocomplete.prototype,
                initSource = proto._initSource;

            function filter(array, term) {
                var matcher = new RegExp($.ui.autocomplete.escapeRegex(term), "i");
                return $.grep(array, function(value) {
                    return matcher.test($("<div>").html(value.label || value.value || value).text());
                });
            }

            $.extend(proto, {
                _initSource: function() {
                    var me = this;
                    $(this.element[0]).change(function () {
                        if ($.isArray(me.options.source)) {
                            var items = this.getItems(me.options.source, $(this).val());
                            if (items == null || items.length == 0)
                                alert("nothing found!");
                        }
                    });

                    initSource.call(this);
                },
                getItems: function(source, term) {
                    var result = [];
                    term = term.toLowerCase();
                    for (var i = 0; i < source.length; i++) {
                        if (source[i].label.toLowerCase().indexOf(term) > -1)
                            result.push(s[i]);
                    }
                    return result;
                }
            });

        })(jQuery);
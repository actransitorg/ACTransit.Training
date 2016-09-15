var CourseEnrollmentsPage = function (baseUrl, enrollUrl, initialValues) {
    var me = this;
    var lastCouseSchedule = null;

    me.prototype = new BaseSinglePage(baseUrl);
    me.base = BaseSinglePage;
    me.base(baseUrl);

    var courseEnrollmentsPageUrl = baseUrl + '/GetCourseEnrollmentsPage';
    var courseEnrollmentUrl = baseUrl + '/GetCourseEnrollemntViewModel';
    var courseScheduleUrl = baseUrl + '/GetCourseSchedules';
    var courseEnrollmentsUrl = baseUrl + '/GetCourseEnrollments';
    var deleteCourseEnrollmentUrl = baseUrl + '/Delete';


    var now = new Date();


    var multiComboCourseType, multiComboCourse;
    me.courseTypes = ko.observableArray([]);
    me.courseTypeIds = ko.observable();    
    me.courses= ko.observableArray([]);
    me.courseIds= ko.observable();    
    me.cboCourseTypeInit = function (obj) {
        multiComboCourseType = obj;
    };
    me.onCourseTypeClosed = function (e) {
        if (e.changedSinceOpened)
            me.refresh();
    }
    me.cboCourseInit = function (obj) {
        multiComboCourse = obj;
    };
    me.onCourseClosed = function (e) {
        var loadPromises = [];
        if (e.changedSinceOpened) {
            var res = $.Deferred();
            var cts = multiComboCourseType.toValues();
            me.pageReady(false);            
            var waitDialog = waitBox.isOpen() ? null : waitBox;
            if (waitDialog) waitDialog.show();
            setTimeout(function () {
                try {
                    var values = multiComboCourse.toValues();
                    var courses = me.courses() || [];
                    for (var i = 0; i < courses.length; i++) {
                        var found = false;
                        var v = courses[i].courseId();
                        for (var j = 0; j < values.length; j++) {
                            if (values[j] == v) {
                                found = true;
                                break;
                            }
                        }
                        courses[i].visible(found);
                        courses[i].collapsed(values.length != 1);
                        if (values.length == 1) {
                            loadPromises.push(me.loadCourseSchedules(courses[i], true));
                        }
                    }
                    me.courses(courses);
                } finally {
                    $.when.apply($, loadPromises).always(function () {
                        res.resolve();
                    });                    
                }
            }, 10);            
            $.when(res).always(function () {
                me.pageReady(true);
                if (cts) multiComboCourseType.fromValues(cts);
                if (waitDialog) waitDialog.hide();
            });
        }
    }
     
    me.badge = ko.observable('');
    me.employee = ko.observable();
    me.dateFrom = ko.observable(Common.Date.formatDate(now));
    me.dateTo = ko.observable();

    me.badge.subscribe(function (e) { if (me.formShown) me.refresh(); });
    me.dateFrom.subscribe(function (e) {
        me.dateChanged('from');
    });
    me.dateTo.subscribe(function (e) {
        me.dateChanged('to');
    });

    me.courses = ko.observable([]);

    me.dateChanged=function(initiated) {
        if (me.dateTo() == '') {
            if (me.formShown) me.refresh();
        } else {
            var dt = Common.Date.parse(me.dateTo());
            var df = Common.Date.parse(me.dateFrom());
            if (dt < df) {
                if (initiated=='to')
                    me.dateFrom(me.dateTo());   //will be refreshed on datefrom subsription.
                else 
                    me.dateTo(me.dateFrom());
            }
                
            else
                if (me.formShown) me.refresh();
        }
    }

    me.clearBadge = function () { me.badge(''); };
    me.enroll = function (parents, data) {
        var courseEnrollmentId = data.courseEnrollmentId();
        var courseTypeId = 0;
        var temp = parents || [];
        if (temp[1])
            courseTypeId= temp[1].courseTypeId() || 0;
        PageNavigation.navigateToEnroll(courseEnrollmentId,courseTypeId);
    };
    me.deleteEnrollment = function (courseSchedule, data) {
        modal.show('Warning', 'Are you sure you wish to delete this enrollment?', { showOk: true, showCancel: true, showRefresh: false }).done(function (res) {
            if (res == 'OK') {
                var courseEnrollmentId = data.courseEnrollmentId();
                deleteCourseEnrollment(courseEnrollmentId).done(function () {
                    me.loadEnrolees(courseSchedule, true);
                });
            }
        });
    };
    me.addNew = function (course, data) {
        var courseScheduleId = null;
        if (data && data.courseScheduleId) {
            courseScheduleId = data.courseScheduleId();
            lastCouseSchedule = data;
        }
        PageNavigation.navigateToNew(courseScheduleId);
    }
    me.edit = function (parent, data) {
        var courseEnrollmentId = data.courseEnrollmentId();
        lastCouseSchedule = parent;
        me.close();
        PageNavigation.navigateToEdit(courseEnrollmentId);
    };

    

    me.isAttendanceVisible = function (courseEnrollment, courseSchedule) {
        var d = courseSchedule.beginEffDateStr();
        if (d && Common.Date.parse(d) > new Date())
            return false;
        return true;
    };

    me.refresh = function () {
        var promises = [];
        me.loading = true;        
        waitBox.show();
        var res= getMainData().done(function (data) {
            var i, j, csPromis, temp;
            if (data) {
                try {
                    temp = [];
                    if (me.courseTypes() && me.courseTypes().length == 0) {
                        for (i = 0; i < data.CourseTypes.length; i++) {
                            var courseType = data.CourseTypes[i];
                            temp.push(courseType);
                        }
                        me.courseTypes(temp);
                    }
                    if (data.Badge)
                        me.badge(data.Badge);
                    if (data.Courses) {
                        temp = [];
                        for (i = 0; i < data.Courses.length; i++) {
                            var newCourse = null;
                            for (j = 0; j < me.courses().length; j++) {
                                if (me.courses()[j].courseId && data.Courses[i].CourseId == me.courses()[j].courseId()) {
                                    newCourse = me.courses()[j];
                                    newCourse = newCourse.initial(data.Courses[i]);
                                    break;
                                }
                            }
                            if (newCourse == null) newCourse = new Course(data.Courses[i], initialValues);

                            if (!newCourse.collapsed()) {
                                csPromis = me.loadCourseSchedules(newCourse, true);
                                promises.push(csPromis);
                            }
                                
                            temp.push(newCourse);
                        }
                        me.courses(temp);
                    }

                } finally {
                    //To Do
                }
                if (!initialValues.hasValue() && Common.hasQueryString()) {
                    Common.removeQueryString();
                }                
                $.when(csPromis).always(function () { initialValues.reset(); });
            }
        });

        $.when(res).always(function () {
            $.when.apply($, promises).always(function () {
                me.loading = false;
                waitBox.hide();
            });
        });
        return res;
    };

    me.loadCourseSchedules = function (course, forceToLoad) {
        var res = $.Deferred();
        var schedulePromis=null;
        var promises = [];
        var loadData = forceToLoad ||
                ((course.courseSchedulesOutOfDate() || !course.courseSchedules || !course.courseSchedules() || course.courseSchedules().length == 0) && course.collapsed());
        var courseId = course.courseId() || 0;
        if (loadData && courseId && courseId != 0)
            schedulePromis = getCourseSchedules(course.courseId());
        $.when(schedulePromis).done(function (data) {
            data = data || [];
            if (data.length > 0) {
                var serverCourse = course.toServerObj();
                serverCourse.CourseSchedules = data;
                course.initial(serverCourse);

            }
            if (forceToLoad) {
                course.collapsed(false);
                if (course && data.length > 0) {
                    for (var i = 0; i < course.courseSchedules().length; i++) {
                        if (!course.courseSchedules()[i].collapsed())
                            promises.push(me.loadEnrolees(course.courseSchedules()[i], forceToLoad));
                    }
                }

            }
            else
                course.toggle();
        });

        $.when(schedulePromis).always(function() {
            $.when.apply($, promises).fail(function() {
                res.reject();
            }).done(function() {
                res.resolve();
            });
        });
        return res;
    };

    me.loadEnrolees = function (courseSchedule, forceToLoad) {
        me.loading = true;
        var res = null;
        var loadData = forceToLoad ||
            ((courseSchedule.courseEnrollmentsOutOfDate() || !courseSchedule.courseEnrollments || !courseSchedule.courseEnrollments() || courseSchedule.courseEnrollments().length == 0) && courseSchedule.collapsed());
        var courseScheduleId = courseSchedule.courseScheduleId() || 0;
        loadData = loadData && courseScheduleId && courseScheduleId != 0;
        if (loadData)
            res = getEnrolees(courseScheduleId);
        return $.when(res).done(function (data) {
            if (loadData) {
                data = data || [];
                var serverCourseSchedule = courseSchedule.toServerObj();
                serverCourseSchedule.CourseEnrollments = data;
                courseSchedule.initial(serverCourseSchedule);
            }
            forceToLoad ? courseSchedule.collapsed(false) : courseSchedule.toggle();
        }).always(function () { me.loading = false; });
    };

    function getMainData(waitDialog) {
        if (waitDialog) waitDialog.show();
        var obj = getParameters();
        return Ajax.ajax(courseEnrollmentsPageUrl, {
            cache: false,
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitDialog).always(function () {
            if (waitDialog) waitDialog.hide();
        });
    }
    function getCourseSchedules(courseId) {
        var waitDialog = waitBox.isOpen() ? null : waitBox;         
        var obj = getParameters();
        obj.CourseId = courseId;
        return Ajax.ajax(courseScheduleUrl, {
            cache: false,
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitDialog);
    }
    function getEnrolees(courseScheduleId) {
        var waitDialog = waitBox.isOpen() ? null : waitBox;
        var obj = getParameters();
        obj.Badge = null;
        obj.CourseScheduleId = courseScheduleId;
        return Ajax.ajax(courseEnrollmentsUrl, {
            cache: false,
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitDialog);
    }

    function getParameters() {
        var obj = new Object();
        obj.Badge = me.badge();
        if (multiComboCourseType)
            obj.CourseTypes = multiComboCourseType.toSelectListItems();
        if ((obj.CourseTypes == null || obj.CourseTypes.length == 0) && !isNaN(initialValues.courseTypeId)) {
            var tempObj = new Object();
            tempObj.Text = '';
            tempObj.Value = initialValues.courseTypeId;
            tempObj.selected = true;
            obj.CourseTypes = [tempObj];
            obj.CourseScheduleId = initialValues.courseScheduleId;
            me.dateFrom(initialValues.beginEffDateStr);
            me.dateTo(initialValues.endEffDateStr);
        }
        obj.JustShowCurrent = false;
        obj.DateFrom = me.dateFrom();
        obj.DateTo = me.dateTo();

        return obj;
    }
    function deleteCourseEnrollment(id) {
        return Ajax.ajax(deleteCourseEnrollmentUrl, {
            cache: false,
            data: JSON.stringify({ 'id': id }),
            dataType: 'json',
            type: 'DELETE',
            contentType: "application/json; charset=utf-8;"
        }, waitBox);
    }

    me.providEmployeeSource = function (req, res) {
        EmployeeSearch.providEmployeeSource(req, res);
    };


    me.show = function (courseId, courseScheduleId, courseEnrollmentId, forceToRefresh) {
        var d1, d2;
        if (me.courses() == null || me.courses().length == 0 || (courseEnrollmentId != 0 && lastCouseSchedule == null) || (forceToRefresh && lastCouseSchedule == null)) {
            d1 = me.refresh();
        }
        else if (courseEnrollmentId != 0 || forceToRefresh)
            d2 = me.loadEnrolees(lastCouseSchedule, true);
        $.when(d1, d2).always(function () {            
            me.pageReady(true);
            me.formShown = true;
        });
    };
    me.close = function () {
        me.formShown = false;
        me.pageReady(false);
    };
}

var CourseEnrollmentPage = function (url) {
    var me = this;    
    me.prototype = new BaseSinglePage(url);
    me.base = BaseSinglePage;
    me.base(url);

    var state;
    me.result = {
        error: false,
        saved: false,
        savedId: 0,
        setSaved: function (id) {
            me.result.savedId = id;
            me.result.saved = true;
            me.result.error = false;
        },
        setException: function () {
            me.result.savedId = 0;
            me.result.saved = false;
            me.result.error = true;
        },
        setCancel: function () {
            me.result.savedId = 0;
            me.result.saved = false;
        },
        clear: function () {
            me.result.savedId = 0;
            me.result.saved = false;
            me.result.error = false;
        },
    };
    me.selectedCourseEnrollment = ko.observable(null);

    me.courseEnrollmentId = 0;
    me.selectedCourseSchedule = ko.observable();
    me.selectedCourseScheduleRequired = ko.observable('');

    me.nonEmployee = ko.observable(false);

    me.selectedNonEmployee = ko.observable(null);
    me.selectedNonEmployeeRequired = ko.observable('');

    me.selectedBadge = ko.observable();
    me.selectedBadgeRequired = ko.observable('');
    me.employee = ko.observable();

    me.selectedNote = ko.observable();
    me.selectedNoShow = ko.observable();

    me.selectedNonEmployee.subscribe(function () {
        if (me.formShown) validate();
        return true;
    });    
    me.selectedBadge.subscribe(function() {
        if (me.formShown) validate();
        return true;
    });

    me.nonEmployee.subscribe(function (nonEmployee) {
        if (nonEmployee) 
            me.selectedBadge('');            
        if (me.formShown) validate();
    });

    me.getCourseSchedule = function (id) {
        if (me.selectedCourseEnrollment() != null && me.selectedCourseEnrollment().CourseSchedules != null) {
            for (var i in me.selectedCourseEnrollment().CourseSchedules)
                if (me.selectedCourseEnrollment().CourseSchedules[i].Value == id)
                    return me.selectedCourseEnrollment().CourseSchedules[i];
        }
        return null;
    };
    
    me.cancel = function () {
        me.result.setCancel();
        PageNavigation.navigateToMain();
    };
    me.save = function () {
        if (!validate())
            return;
        saveCourseEnrollment().done(function (result) {
            if (result && result.Success) {
                clear();
                me.result.setSaved(result.Data.CourseEnrollment.CourseEnrollmentId);
                PageNavigation.navigateToMain();
            }
        }).fail(function () {
            me.result.setException();
        });
    };

    function validate() {
        var validated = true;
        clearValidation();

        if (!me.selectedCourseSchedule() || me.selectedCourseSchedule() == null || !me.selectedCourseSchedule().Value || me.selectedCourseSchedule().Value == 0) {
            me.selectedCourseScheduleRequired("CourseSchedule cannot be empty.");
            validated = false;
        }
        if (me.nonEmployee()) {
            if (!me.selectedNonEmployee() || me.selectedNonEmployee() == null || me.selectedNonEmployee() == 0) {
                validated = false;
                me.selectedNonEmployeeRequired('Trainee should be selected.');
            }
        }
        else {
            if (!me.selectedBadge() || me.selectedBadge() == null || me.selectedBadge() == '') {
                validated = false;
                me.selectedBadgeRequired("Trainee should be selected.");
            }
        }
        return validated;
    }
    function saveCourseEnrollment() {
        var obj = new Object();
        obj.State = state;
        obj.CourseEnrollment = new Object();
        obj.CourseEnrollment.CourseEnrollmentId = me.courseEnrollmentId;
        obj.CourseEnrollment.CourseScheduleId = me.selectedCourseSchedule().Value;
        obj.CourseEnrollment.NonEmployeeId = (me.nonEmployee() ? me.selectedNonEmployee() : null);
        obj.CourseEnrollment.Badge = (me.nonEmployee() ? null : me.selectedBadge());
        obj.CourseEnrollment.Note = me.selectedNote();
        obj.CourseEnrollment.noShow = me.selectedNoShow();
        me.loading = true;
        return Ajax.ajax(url, {
            cache: false,
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitBox).always(function () { me.loading = false; });
    }

    function clear() {
        me.selectedCourseEnrollment(null);
        clearValidation();
        me.courseEnrollmentId = 0;
        me.nonEmployee(false);
        me.selectedNonEmployee(null);

        me.selectedBadge('');
    }

    function clearValidation() {
        me.selectedCourseScheduleRequired('');
        me.selectedNonEmployeeRequired('');
        me.selectedBadgeRequired('');
    }

    me.providEmployeeSource = function (req, res) {
        EmployeeSearch.providEmployeeSource(req, res);
    };

    function getCourseEnrollment(id, courseScheduleId) {
        clear();
        me.loading = true;
        return Ajax.ajax(url, {
            cache: false,
            data: { 'id': id, 'courseScheduleId': courseScheduleId },
            dataType: 'json',
            type: 'GET',
            contentType: "application/json; charset=utf-8;"
        }, waitBox).done(function (data) {
            if (data.CourseEnrollment) {
                state = data.State;
                me.selectedCourseEnrollment(data);

                me.courseEnrollmentId = data.CourseEnrollment.CourseEnrollmentId;
                if (data.CourseEnrollment.NonEmployeeId && data.CourseEnrollment.NonEmployeeId != 0) {
                    me.nonEmployee(true);
                    me.selectedNonEmployee(data.CourseEnrollment.NonEmployeeId);
                }
                else
                    me.nonEmployee(false);

                me.selectedBadge(data.CourseEnrollment.Badge);
                me.employee(data.CourseEnrollment.Name);
                me.selectedNote(data.CourseEnrollment.Note);
                me.selectedNoShow(data.CourseEnrollment.NoShow);
                var cs;
                if (courseScheduleId != null && courseScheduleId != 0)
                    cs = me.getCourseSchedule(courseScheduleId);
                else
                    cs = me.getCourseSchedule(data.CourseEnrollment.CourseScheduleId);
                me.selectedCourseSchedule(cs);
            }
        }).always(function () { me.loading = false; });
    }

    me.show = function (courseScheduleId, courseEnrollmentId) {
        me.pageReady(false);
        me.formShown = false;
        me.result.clear();
        getCourseEnrollment(courseEnrollmentId, courseScheduleId).fail(function () {
            me.result.setException();
            PageNavigation.navigateToMain();
        }).always(function () {
            me.pageReady(true);
            me.formShown = true;            
        });
    };
    me.close = function () {
        me.formShown = false;
        me.pageReady(false);
    };
}

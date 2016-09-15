//----------------------------------Transportation Template---------------------------------------
var TransportationEnrollmentTemplate = function () {
    var me = this;
    me.timeStamp = (new Date()).getTime();
    me.editState = ko.observable(false);
    me.enrollmentId = ko.observable(0);
    me.enrollmentVehicleId = ko.observable('');
    me.date = ko.observable('');
    me.date.existCount= ko.observable(0);
    me.instructorId = ko.observable('');
    me.noShow = ko.observable(false);
    me.topicIds = ko.observableArray([]);
    me.routeId = ko.observableArray([]);
    me.vehicleId = ko.observable('');
    me.vehicleStr = ko.computed(function () {
        return me.vehicleId();
    });

    me.wheelTimeStr = ko.observable('');
    me.qT = ko.observable(false);
    me.lift = ko.observable(false);
    me.secure = ko.observable(false);    
    me.qTClick = function () {        
        if (me.qT() && !me.lift() && !me.secure()) {
            me.lift(true);
            me.secure(true);
        }
        return true;
    };

    me.topicDisbaled= ko.observable(false);


    me.onTemplateTimeInitialized = function () {

    };
    me.onNoShowClick = function () {

    };
    me.cboTopicInit = function (obj) {

    };
    me.onTopicClosed = function () {

    };

    me.NewUpdateTemplateButtonText = ko.computed(function () {
        if (me.editState()) {
            return "Update";
        } else {
            return "Add";
        }
    });
    me.ClearTemplateButtonText = ko.computed(function () {
        if (me.editState()) {
            return "Cancel";
        } else {
            return "Clear";
        }
    });

    me.validate = function () {
        return true;
    };

    me.hasValue = function () {
        if (me.date() && me.date() != '') return true;
        if (me.instructorId() && me.instructorId() != '') return true;
        if (me.routeId() && me.routeId() != '') return true;        
        if (me.vehicleId() && me.vehicleId() != '') return true;

        if (me.wheelTimeStr() && me.wheelTimeStr() != '') return true;
        if (me.qT() && me.qT() != '') return true;
        if (me.lift() && me.lift() != '') return true;
        if (me.secure() && me.secure() != '') return true;
        if (me.noShow() && me.noShow()) return true;

        return false;
    };

    me.set = function (obj) {

        me.editState(obj.editState());
        me.date(obj.date());
        me.instructorId(obj.instructorId());
        me.noShow(obj.noShow());
        me.topicIds(obj.topicIds());
        me.routeId(obj.routeId());
        me.vehicleId(obj.vehicleId());

        me.wheelTimeStr(obj.wheelTimeStr());
        me.qT(obj.qT());
        me.lift(obj.lift());
        me.secure(obj.secure());
    };
    me.clone = function () {
        var res = new TransportationEnrollmentTemplate();
        res.date(me.date());
        res.instructorId(me.instructorId());
        res.noShow(me.noShow());
        res.topicIds(me.topicIds());

        res.routeId(me.routeId());
        res.vehicleId(me.vehicleId());

        res.wheelTimeStr(me.wheelTimeStr());
        res.qT(me.qT());
        res.lift(me.lift());
        res.secure(me.secure());
        return res;
    };
    me.clear = function() {
        me.date(null);
        me.instructorId('');
        me.noShow(false);
        me.topicIds([]);
        
        me.routeId('');
        me.vehicleId('');
        
        me.wheelTimeStr("");
        me.qT(false);
        me.lift(false);
        me.secure(false);
    };
};
TransportationEnrollmentTemplate.convertFromEnrollment = function (enrollment) {
    var res = [];
    var tet, temp, i;
    if (enrollment) {
        var editState = enrollment.editState();
        var enrollmentId = enrollment.enrollmentId() || 0;        
        var noShow = enrollment.noShow();
        var date = enrollment.sessionDateStr();
        var topicIds = [];
        if (enrollment.enrollmentTopics() && enrollment.enrollmentTopics().length > 0) {
            topicIds = [];
            for (i = 0; i < enrollment.enrollmentTopics().length; i++) {
                topicIds.push(enrollment.enrollmentTopics()[i].topicId());
            }
        }

        if (enrollment.enrollmentVehicles().length > 0) {
            for (i = 0; i < enrollment.enrollmentVehicles().length; i++) {
                tet = new TransportationEnrollmentTemplate();
                tet.enrollmentId(enrollmentId);
                tet.enrollmentVehicleId(enrollment.enrollmentVehicles()[i].enrollmentVehicleId());
                tet.date(date);
                tet.noShow(noShow);
                tet.editState(editState);
                if (i == 0)
                    tet.topicIds(topicIds);
                else
                    tet.topicIds([]);
                if (enrollment.enrollmentInstructors() && enrollment.enrollmentInstructors().length > i)
                    tet.instructorId(enrollment.enrollmentInstructors()[i].instructorId);
                tet.wheelTimeStr(enrollment.enrollmentVehicles()[i].wheelTimeStr());
                var r = enrollment.enrollmentVehicles()[i].routeAlpha();
                tet.routeId(r);                
                var v = enrollment.enrollmentVehicles()[i].vehicleId();
                tet.vehicleId(v);                
                tet.qT(enrollment.enrollmentVehicles()[i].qualifiedTraining());
                tet.lift(enrollment.enrollmentVehicles()[i].ougLiftRampOps());
                tet.secure(enrollment.enrollmentVehicles()[i].ougSecurement());

                if (enrollment.enrollmentInstructors().length > i) {
                    tet.instructorId(enrollment.enrollmentInstructors()[i].instructorId());
                }

                res.push(tet);
            }
        } else {
            tet = new TransportationEnrollmentTemplate();
            tet.enrollmentId(enrollmentId);
            tet.date(date);
            tet.noShow(noShow);
            tet.editState(editState);
            tet.topicIds(topicIds);
            if (enrollment.enrollmentInstructors() && enrollment.enrollmentInstructors().length > 0)
                tet.instructorId(enrollment.enrollmentInstructors()[0].instructorId());
            if (enrollment.enrollmentInstructors().length > 0) {
                tet.instructorId(enrollment.enrollmentInstructors()[0].instructorId());
            }

            res.push(tet);
        }
    }
    return res;
};
TransportationEnrollmentTemplate.convertToEnrollment = function (template) {
    var res = new Enrollment();
    var i;
    if (template) {
        var ev = new EnrollmentVehicle();
        ev.enrollmentId(template.enrollmentId() || 0);
        ev.enrollmentVehicleId(template.enrollmentVehicleId() || 0);
        ev.wheelTimeStr(template.wheelTimeStr());
        ev.qualifiedTraining(template.qT());
        ev.ougLiftRampOps(template.lift());
        ev.ougSecurement(template.secure());
        ev.routeAlpha(template.routeId());
        ev.vehicleId(template.vehicleId());
        res.enrollmentId(template.enrollmentId() || 0);
        res.noShow(template.noShow());
        res.sessionDateStr(template.date());
        res.editState(template.editState());
        if (template.instructorId() && template.instructorId() != 0) {
            var ins = new EnrollmentInstructure();
            ins.instructorId(template.instructorId());
            res.enrollmentInstructors.push(ins);
        }
        res.enrollmentVehicles([ev]);
        var topicIds = template.topicIds() || [];
        for (i = 0; i < topicIds.length; i++) {
            var et = new EnrollmentTopic();
            et.topicId(topicIds[i]);
            res.enrollmentTopics().push(et);
        }
    }
    return res;
};

//----------------------------------/Transportation Template--------------------------------------

//----------------------------------Shared between Transportation and Maintenance-----------------
var EnrollmentBasePage = function (baseUrl) {
    var me = this;
    me.dateFormat = 'm/d/Y';
    me.prototype = new BaseSinglePage;
    me.prototype.constructor.call(this, baseUrl);

    me.courseTypesCourses = [];

    //-------------observable-----------------------
    me.initialized = ko.observable(false);    

    me.courseTypes = ko.observableArray([]);
    me.courses = ko.observableArray([]);
    me.courseSchedules = ko.observableArray([]);
    me.instructors = ko.observableArray([]);

    me.nonEmployees = ko.observableArray([]);
    me.isNonEmployee = ko.observable(false);
    me.employeeBadge = ko.observable('');
    me.nonEmployeeId = ko.observable(0);
    me.personName = ko.observable('');

    me.courseTypeId = ko.observable(0);
    me.courseId = ko.observable(0);
    me.courseScheduleId = ko.observable(0);
    me.courseEnrollmentId = ko.observable(0);
    me.hasWheelTime = ko.observable(false);

    me.allowToChangeCourseType = ko.observable(false);
    me.allowToChangeCourse = ko.observable(false);
    me.allowToChangeCourseSchedule = ko.observable(false);
    me.allowToChangeEmployee = ko.observable(false);
    me.allowToChangeNonEmployee = ko.observable(true);

    me.courseTypeRequiredDummy = ko.observable();
    me.courseRequiredDummy = ko.observable();
    me.courseScheduleRequiredDummy = ko.observable();
    me.employeeBadgeRequiredDummy = ko.observable();
    me.nonEmployeeIdRequiredDummy = ko.observable();
    //-------------/observable-----------------------

    //-------------subscribe-----------------------
    me.courseTypeId.subscribe(function () {
        var ctId = me.courseTypeId();
        if (me.hasCourses(ctId)) {
            var temp = me.courseTypesCourses[ctId.toString()] || [];
            me.courses(temp);
        }
    });
    me.courseId.subscribe(function () {
        var courses = me.courses() || [];
        for (var i = 0; i < courses.length; i++) {
            if (courses[i].courseId() == me.courseId()) {
                me.hasWheelTime(courses[i].hasWheelTime());
            }
        }
    });
    me.courseScheduleId.subscribe(function () {
        var arr = me.courseSchedules() || [];
        var csId = me.courseScheduleId();
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].courseScheduleId() == csId) {
                me.minDate(arr[i].beginEffDateStr());
                me.maxDate(arr[i].endEffDateStr());
            }
        }
    });
    me.minDate = ko.observable();
    me.maxDate = ko.observable();

    //-------------/subscribe-----------------------

    //-------------events-----------------------
    //-------------/events-----------------------

    //-------------computed-----------------------

    me.courseTypeRequired = ko.computed(function () {
        var dummy = me.courseTypeRequiredDummy();
        var value = me.courseTypeId() || 0;
        if (me.formShown && value == 0)
            return true;
        return false;
    });
    me.courseRequired = ko.computed(function () {
        var dummy = me.courseRequiredDummy();
        var value = me.courseId() || 0;
        if (me.formShown && value == 0) return true;
        return false;
    });
    me.courseScheduleRequired = ko.computed(function () {
        var dummy = me.courseScheduleRequiredDummy();
        var value = me.courseScheduleId() || 0;
        if (me.formShown && value == 0) return true;
        return false;
    });
    me.employeeBadgeRequired = ko.computed(function () {
        var dummy = me.employeeBadgeRequiredDummy();
        var value = me.employeeBadge() || '';
        if (me.formShown && !me.isNonEmployee() && value == '') return true;
        return false;
    });
    me.nonEmployeeIdRequired = ko.computed(function () {
        var dummy = me.nonEmployeeIdRequiredDummy();
        var value = me.nonEmployeeId() || 0;
        if (me.formShown && me.isNonEmployee() && value == 0) return true;
        return false;
    });
   
    //-------------/computed-----------------------

    //-------------public methods-----------------------
    me.baseInit = function() {
        var ceId = me.courseEnrollmentId() || 0;
        if (ceId == 0) {
            me.allowToChangeCourseType(true);
            me.allowToChangeCourse(true);
            me.allowToChangeCourseSchedule(true);
            me.allowToChangeEmployee(true);
            me.allowToChangeNonEmployee(true);
        } else {
            me.allowToChangeCourseType(false);
            me.allowToChangeCourse(false);
            me.allowToChangeCourseSchedule(false);
            me.allowToChangeEmployee(false);
            me.allowToChangeNonEmployee(false);
        }
            

    };
    me.baseValidate = function () {
        me.courseTypeRequiredDummy(new Date());
        me.courseRequiredDummy(new Date());
        me.courseScheduleRequiredDummy(new Date());
        me.employeeBadgeRequiredDummy(new Date());
        me.nonEmployeeIdRequiredDummy(new Date());
        if (me.courseTypeRequired()) return false;
        if (me.courseRequired()) return false;
        if (me.courseScheduleRequired()) return false;
        if (me.employeeBadgeRequired()) return false;
        if (me.nonEmployeeIdRequired()) return false;

        return true;
    };
    me.loadCourseTypes = function (data) {
        if (data.IncludeCourseTypes) {
            data.CourseTypes = data.CourseTypes || [];
            var temp = [];
            for (var i = 0; i < data.CourseTypes.length; i++) {
                var ct = new CourseType(data.CourseTypes[i]);
                temp.push(ct);
            }
            me.courseTypes(temp);
        }
        me.courseTypeId(data.CourseTypeId);
    };
    me.loadCourses = function (data) {
        if (data.IncludeCourses) {
            data.Courses = data.Courses || [];
            var temp = [];
            for (var i = 0; i < data.Courses.length; i++) {
                var c = new Course(data.Courses[i]);
                temp.push(c);
            }
            me.courses(temp);
            var ctId = me.courseTypeId();
            if (ctId)
                me.courseTypesCourses[ctId.toString()] = temp;            
        }
        if (data.CourseId) me.courseId(data.CourseId);
    };
    me.loadCourseSchedules = function (data) {
        if (data.IncludeCourseSchedules) {
            data.CourseSchedules = data.CourseSchedules || [];
            var temp = [];
            for (var i = 0; i < data.CourseSchedules.length; i++) {
                var cs = new CourseSchedule(data.CourseSchedules[i]);
                temp.push(cs);
            }
            me.courseSchedules(temp);
            me.courseScheduleId(data.CourseScheduleId);
        }
    };
    me.loadEmployeeNonEmployee = function (data) {
        var ne, i,temp;
        data.EmployeeBadge = data.EmployeeBadge || '';
        data.PersonName = data.PersonName || '';
        if (data.EmployeeBadge != '') {
            me.employeeBadge(data.EmployeeBadge);
            me.personName(data.PersonName);
            me.nonEmployeeId(0);
            me.isNonEmployee(false);
        } else if (data.NonEmployeeId != 0) {
            ne = new NonEmployee();
            ne.name(data.PersonName);
            ne.nonEmployeeId(data.NonEmployeeId);
            me.nonEmployees(ne);
            me.nonEmployeeId(data.NonEmployeeId);
            me.personName('');
            me.employeeBadge('');            
            me.isNonEmployee(true);
        }
        if (data.IncludeNonEmployees) {
            temp = [];
            data.NonEmployees = data.NonEmployees || [];
            for (i = 0; i < data.NonEmployees.length; i++) {
                ne = new NonEmployee(data.NonEmployees[i]);
                temp.push(ne);
            }
            me.nonEmployees(temp);
        }
    };
    me.loadInstructors = function (data) {
        if (data.IncludeInstructors) {
            var temp = [];
            data.Instructors = data.Instructors || [];
            for (var i = 0; i < data.Instructors.length; i++) {
                var ins = new Instructor(data.Instructors[i]);
                temp.push(ins);
            }
            me.instructors(temp);
        }
    };
    me.providEmployeeSource = function (req, res) {
        EmployeeSearch.providEmployeeSource(req, res);
    };

    me.clearEmployeeNonEmployee = function() {
        me.employeeBadge('');
        me.personName('');
        me.nonEmployeeId(0);
    };

    me.getInstructorStr = function (instructorId) {
        if (instructorId) {
            var insId = ko.unwrap(instructorId);
            var ins = getInstructor(insId);
            if (ins != null)
                return ins.instructor();
        }       

        return "";
    };
    me.hasCourses = function (courseTypeId) {
        if (!courseTypeId)
            return false;
        var result = me.courseTypesCourses[courseTypeId.toString()] || [];
        if (result.length == 0)
            return false;
        return true;
    };

    //-------------/public methods-----------------------

    function getInstructor(instructorId) {
        var insAll = me.instructors();
        for (var i = 0; i < insAll.length; i++) {
            if (insAll[i].instructorId() == instructorId)
                return insAll[i];
        }
        return null;
    }

};
//----------------------------------/Shared between Transportation and Maintenance----------------


var TransportationEnrollment = function (owner) {
    var me = this;
    var cboTopic, cboRoute;

    //-------------observable-----------------------
    me.enrollments = ko.observableArray([]);    
    me.routes = ko.observableArray([]);
    me.vehicles = ko.observableArray([]);
    me.grades = ko.observableArray([]);
    me.topics = ko.observableArray([]);
    me.enrollmentAndVehicles = ko.observableArray([]);
    me.isTemplateValid = ko.observable(true);
    me.template = ko.observable(new TransportationEnrollmentTemplate());
    me.dateRequiredDummy = ko.observable();
    me.dateError = ko.observable('');
    me.wheelTimeRequiredDummy = ko.observable();
    me.coachRequiredDummy = ko.observable();
    me.wheelTimeError = ko.observable('');
    me.coachError = ko.observable('');
    //-------------/observable-----------------------

    //-------------subscribe-----------------------
    me.template().date.subscribe(function () {
        setTopicEnability();
    });
    me.template().topicDisbaled.subscribe(function () {
        var disabled = me.template().topicDisbaled();
        if (disabled) {
            if (cboTopic) {
                me.template().topicIds([]);                
            }
        }
    });
    //-------------/subscribe-----------------------

    //-------------events-----------------------
    me.onTopicInit = function (obj) {
        cboTopic = obj;
    };
    me.onTopicClosed = function (e) {
        if (e.changedSinceOpened) {

        }
    };
    me.onRouteInit = function (obj) {
        cboRoute = obj;
    };
    me.onRouteClosed = function (e) {
        if (e.changedSinceOpened) {

        }
    };
    me.noShowClicked = function () {
        if (me.template().noShow()) {
            modal.show("Warning", "All the other fields (except the date) will be cleared, are you sure?", { showRefresh: false, showOk: true, showCancel: true }).done(function (reason) {
                if (reason == "OK") {
                    var d = me.template().date();
                    me.template().clear();
                    if (d) me.template().date(d);
                    me.template().noShow(true);
                    me.template().topicDisbaled(true);                    
                    return true;
                }
                me.template().noShow(false);
            });
        } else {
            setTopicEnability();
        }
        return true;
    };

    me.NewUpdateTemplateClicked = function () {
        if (!me.validateTemplate())
            return;
        setTopicEnability();
        var template = me.template();
        var res = me.enrollmentAndVehicles();
        if (template.editState()) {
            for (var i = 0; i < res.length; i++) {
                if (res[i].editState()) {
                    template.editState(false);
                    res[i].set(template.clone());
                    break;
                }
            }
        } else {
            res.push(template.clone());
            sortTemplates(res);
        }
        clearDateAndRoute();
        me.enrollmentAndVehicles(res);
    };
    me.clearTemplateClicked = function () {
        var template = me.template();
        var res = me.enrollmentAndVehicles();
        if (template.editState()) {     //functionality is cancel.
            for (var i = 0; i < res.length; i++) {
                if (res[i].editState()) {
                    template.noShow(false);
                    clearDateAndRoute();
                    template.editState(false);
                    res[i].editState(false);
                    break;
                }
            }
            me.enrollmentAndVehicles(res);
        }
        else {
            me.formShown = false;
            me.template().clear();
            me.formShown = true;
        }
    }

    me.editEnrollmentTemplate = function (template) {
        template.editState(true);
        me.template().date('');
        me.template().set(template.clone());
        me.template().editState(true);
        if (cboTopic) cboTopic.fromValues(template.topicIds());
        if (cboRoute) cboRoute.fromValues(template.routeId());
    };
    me.removeEnrollmentTemplate = function (index) {
        modal.show("Warning", "Are you sure?", { showRefresh: false, showOk: true, showCancel: true }).done(function (reason) {
            if (reason == "OK") {
                var res = me.enrollmentAndVehicles() || [];
                res.splice(index(), 1);
                me.enrollmentAndVehicles(res);
            }
        });
    };

    //-------------/events-----------------------

    //-------------computed-----------------------
    me.totalDriveTime = ko.computed(function () {
        var arr = me.enrollmentAndVehicles();
        if (arr) {
            var dt = new Date();
            var time = new Date(0, 0, 0, 0, 0, 0, 0);
            for (var i = 0; i < arr.length; i++) {
                var t1 = dt.parseTime(arr[i].wheelTimeStr());
                if (t1 != null) {
                    time = time.addMinutes(t1.getHours() * 60 + t1.getMinutes());
                }
            }
            return time.getHours() + ":" + time.getMinutes();
        }
    }, me);
    me.dateRequired = ko.computed(function () {
        var dateFormat = me.dateFormat;
        var dummy = me.dateRequiredDummy();
        var value = me.template().date() || '';
        if (owner.formShown && value == '' && me.template().hasValue()) {
            me.dateError("SessionDate can not be empty.");
            return true;
        }

        var minDate = Common.Date.parse(owner.minDate(), dateFormat);
        var maxDate = Common.Date.parse(owner.maxDate(), dateFormat);
        var date = Common.Date.parse(me.template().date(), dateFormat);
        if (date && minDate && maxDate) {
            if (minDate.getTime() > date.getTime() || maxDate.getTime() < date.getTime()) {
                me.dateError("SessionDate should be between the Course Schedule dates.");
                return true;
            }
        }
        me.dateError(null);
        return false;
    });
    me.wheelTimeRequired = ko.computed(function () {
        var dummy = me.wheelTimeRequiredDummy();
        if (!owner.hasWheelTime()) return false;
        var template = me.template();
        var temp = template.wheelTimeStr() || '';
        if (owner.formShown) {
            if (temp == '__:__') {
                temp = 0;
                template.wheelTimeStr('');
            }
            var value = 0;
            try {
                var temp1 = Common.Date.parse("00:00", 'hh:mm').getTime();
                value = temp == '' ? 0 : Common.Date.parse(temp, 'hh:mm').getTime()-temp1;
                return false;
            } catch(e)  {
                me.wheelTimeError("Wheel time is in wrong format.");
                return true;            
            }            
        }
        return false;

    });
    me.coachRequired = ko.computed(function () {
        var dummy = me.coachRequiredDummy();
        if (!owner.hasWheelTime()) return false;
        var template = me.template();
        var temp = template.wheelTimeStr() || '';        
        if (owner.formShown) {
            if (temp == '__:__') {
                temp = 0;
                template.wheelTimeStr('');
            }
            var coach = template.vehicleId() || '';
            try {
                var temp1= Common.Date.parse("00:00", 'hh:mm').getTime();
                var value = temp==''?0:Common.Date.parse(temp, 'hh:mm').getTime() -temp1;
                if (value != 0 && template.hasValue() && coach == '') {
                    me.coachError("Coach can not be empty.");
                    return true;                    
                }
                return false;

            }
            catch(e)  {                
                return true;            
            }                  
        }
        return false;
    });
    me.noShowDisabled = ko.computed(function() {
        var template = me.template();
        if (template.date.existCount() > 1 || (template.date.existCount()==1 && !template.editState()))
            return true;
        return false;
    });            
    
    //-------------/computed-----------------------

    //-------------public methods-----------------------
    me.initial = function () {        
        me.enrollments([]);
        var temp = me.enrollmentAndVehicles() || [];
        for (var i = 0; i < temp.length; i++) {
            temp[i].enrollmentId(0);
            temp[i].editState(false);
        }            
        me.template().editState(false);
    };
    me.close = function() {
        me.clearTemplateClicked();
        me.enrollments([]);
        me.enrollmentAndVehicles([]);
    };
    me.validateTemplate = function () {
        var result = true;

        me.dateRequiredDummy(new Date());
        me.wheelTimeRequiredDummy(new Date());
        me.coachRequiredDummy(new Date());        
        if (me.dateRequired()) result = false;
        if (me.wheelTimeRequired()) result = false;
        if (me.coachRequired()) result = false;
        if (!me.template().hasValue())
            return false;
        return result;
    };
    me.validate = function () {
        if (me.template().editState()) {
            modal.show("Stop", "You are editing a row, you need to finish it first.", { showRefresh: false, showCancel: false, showOk: false });
            return false;
        }
            
        var temp = me.enrollmentAndVehicles() || [];
        
        if (temp.length == 0) {
            modal.show("Stop", "No data entered. There is nothing to save.", { showRefresh: false, showCancel: false, showOk: false });
            return false;
        }        
        return true;
    };
    me.enrollmentTopicsStr = function (template) {
        var res = '';
        if (template && template.topicIds) {
            var ets = template.topicIds();
            var topicAll = me.topics();
            for (var i = 0; i < topicAll.length; i++) {
                for (var j = 0; j < ets.length; j++) {
                    if (topicAll[i].topicId() == ets[j]) {
                        if (res != '') res += ', ';
                        res += topicAll[i].name();
                    }
                }
            }
        }
        return res;
    };
    me.getEnrollmentsToSave = function (courseEnrollmentId, courseScheduleId, nonEmployeeId, employeeBadge) {
        var i, j, tempArr1, tempArr2, tempArr;
        var enrollments = [];
        var templates = me.enrollmentAndVehicles() || [];
        if (templates.length == 0)
            return null;
        for (i = 0; i < templates.length; i++) {
            var template = templates[i];
            var enrollment = null;
            var date = template.date();
            var found = false;
            for (j = 0; j < enrollments.length; j++) {
                if (enrollments[j].sessionDateStr() == date) {
                    enrollment = enrollments[j];
                    found = true;
                    break;
                }
            }
            if (enrollment == null) {
                enrollment = TransportationEnrollmentTemplate.convertToEnrollment(template);
                enrollment.courseEnrollmentId(courseEnrollmentId);
                enrollment.courseScheduleId(courseScheduleId);
                enrollment.nonEmployeeId(nonEmployeeId || 0);
                enrollment.badge(employeeBadge || '');
            } else {
                var eTemp = TransportationEnrollmentTemplate.convertToEnrollment(template);
                tempArr1 = enrollment.enrollmentInstructors() || [];
                tempArr2 = eTemp.enrollmentInstructors() || [];
                tempArr = tempArr1.concat(tempArr2);
                enrollment.enrollmentInstructors(tempArr);

                tempArr1 = enrollment.enrollmentVehicles() || [];
                tempArr2 = eTemp.enrollmentVehicles() || [];
                tempArr = tempArr1.concat(tempArr2);
                enrollment.enrollmentVehicles(tempArr);

                tempArr1 = enrollment.enrollmentTopics() || [];
                tempArr2 = eTemp.enrollmentTopics() || [];
                tempArr = tempArr1.concat(tempArr2);
                enrollment.enrollmentTopics(tempArr);

            }

            if (!found)
                enrollments.push(enrollment);
        }

        var result = [];
        for (i = 0; i < enrollments.length; i++) {
            result.push(enrollments[i].toServerObj());
        }

        return result;
    };        
    me.loadRoutes = function (data) {
        if (data.IncludeRoutes) {
            var temp = [];
            data.Routes = data.Routes || [];
            for (var i = 0; i < data.Routes.length; i++) {
                var r = new Route(data.Routes[i]);
                temp.push(r);
            }
            me.routes(temp);
        }
    };
    me.loadVehicles = function (data) {
        if (data.IncludeVehicles) {
            var temp = [];
            data.Vehicles = data.Vehicles || [];
            for (var i = 0; i <= data.Vehicles.length; i++) {
                var v = new VehicleRegister(data.Vehicles[i]);
                temp.push(v);
            }
            me.vehicles(temp);
        }
    };    
    me.loadTopics = function (data) {
        if (data.IncludeTopics) {
            var temp = [];
            data.Topics = data.Topics || [];
            for (var i = 0; i < data.Topics.length; i++) {
                var t = new Topic(data.Topics[i]);
                temp.push(t);
            }
            me.topics(temp);
        }
    };

    me.loadEnrollments = function (data) {
        if (data.IncludeEnrollments) {
            var i,j,en;
            var temp = [];
            var res = [];
            var ceId = owner.courseEnrollmentId() || 0;
            data.Enrollments = data.Enrollments || [];
            if (data.CourseScheduleId == owner.courseScheduleId() && data.Enrollments.length == 0)
                return;
            //if (data.Enrollments.length > 0) {
                for (i = 0; i < data.Enrollments.length; i++) {
                    en = new Enrollment(data.Enrollments[i]);
                    temp.push(en);
                    var t = TransportationEnrollmentTemplate.convertFromEnrollment(en) || [];
                    for (j = 0; j < t.length; j++)
                        res.push(t[j]);
                }
                if (data.CourseEnrollmentId != 0 && ceId != data.courseEnrollmentId)
                    owner.courseEnrollmentId(data.CourseEnrollmentId);
                me.enrollments(temp);
                sortTemplates(res);
                me.enrollmentAndVehicles(res);
            //}
        }
    };

    me.load = function (data) {
        me.loadRoutes(data);
        me.loadVehicles(data);
        me.loadTopics(data);
        me.loadEnrollments(data);
    }
    //-------------/public methods-----------------------

    function sortTemplates(arr) {
        arr.sort(function (a, b) {
            var d1 = Common.Date.parse(a.date(), me.dateFormat);
            var d2 = Common.Date.parse(b.date(), me.dateFormat);
            if (d1 > d2)
                return 1;
            else if (d1 < d2)
                return -1;
            else {
                if (a.timeStamp > b.timeStamp)
                    return 1;
                else if (a.timeStamp == b.timeStamp)
                    return 0;
                return -1;
            }
        });
        return arr;
    }

    function clearDateAndRoute() {
        enableValidation(false);
        me.template().date('');
        me.template().routeId('');
        enableValidation(true);
    }

    function setTopicEnability() {
        var template = me.template();
        var noShow = template.noShow();
        var date = template.date();
        if (date && date != '') {
            var d = Common.Date.parse(date);
            var d1 = Common.Date.formatDate(d);
            if (d1 != date) {
                template.date(d1);
                date = d1;
            }
        }

        var rowNum = 0;
        var topicDisbaled = false;
        var isEditing = false;
        var count=0;
        if (owner.formShown && !template.editState()) {
            var res = me.enrollmentAndVehicles() || [];
            for (var i = 0; i < res.length; i++) {
                if (res[i].date() == date) {
                    rowNum++;
                    count ++ ;
                    if (rowNum == 1 && res[i].editState())
                        isEditing = true;
                    else if (!isEditing)
                        topicDisbaled = true;
                }
            }
        }
        template.date.existCount(count);
        template.topicDisbaled(topicDisbaled || noShow);
    }

    var allowToChangeValidation=true;
    function enableValidation(enabled) {
        if (!enabled) {
            if (!owner.formShown) //already disabled. so don't do anything.
                allowToChangeValidation = false;
            else {
                allowToChangeValidation = true;
                owner.formShown = false;
            }
        } else if (allowToChangeValidation)  // in case the formShown has been changed before this function is called.
            owner.formShown = true;
        else if (owner.formShown)
            allowToChangeValidation = true;
    }
}

var MaintenanceEnrollment = function (owner) {
    var me = this;
    var timeRegex = /^(((\d|\d\d|\d\d\d):([0-5]\d))|(\d|\d\d|\d\d\d))?$/;
    //-------------observable-----------------------
    me.enrollment = ko.observable();
    me.grades = ko.observableArray([]);

    me.primaryInstructorId = ko.observable();
    me.secondaryInstructorId = ko.observable();
    me.lectureHoursValid = ko.observable(true);
    //-------------/observable-----------------------

    //-------------subscribe-----------------------

    //-------------/subscribe-----------------------

    //-------------events-----------------------
    me.onNoShowClick = function () {
        if (me.enrollment() && me.enrollment().noShow() == true) {
            me.enrollment().lectureTimeStr(0);
            me.enrollment().letterGrade('');
        }
            
        return true;
    };
    //-------------/events-----------------------

    //-------------computed-----------------------
    me.totalTrainingHours = ko.computed(function() {
        var enrollment = me.enrollment();
        var time = new Date(0);
        if (enrollment) {
            var dt = new Date();
            var timeStr = enrollment.lectureTimeStr() || '';
            if (timeRegex.test(timeStr)) {
                me.lectureHoursValid(true);
                if (timeStr != '') {
                    var parts = timeStr.split(':');
                    if (parts[0] && parts[0] != '') {
                        var h = parseInt(parts[0], 10);
                        h = h * 60000 * 60;
                        time = new Date(time.getTime() + h);
                    }
                    if (parts[1] && parts[1] != '') {
                        var m = parseInt(parts[1], 10);
                        m = m * 60000;
                        time = new Date(time.getTime() + m);
                    }
                }
                for (var i = 0; i < enrollment.enrollmentVehicles().length; i++) {
                    var t1 = dt.parseTime(enrollment.enrollmentVehicles()[i].wheelTimeStr());
                    if (t1 != null) {
                        time = time.addMinutes(t1.getHours() * 60 + t1.getMinutes());
                    }
                }
            } else
                me.lectureHoursValid(false);
        }
        var totalMiliSeconds = time.getTime();
        var totalMinutes = totalMiliSeconds / (1000 * 60);
        var totalHours = Math.floor(totalMinutes / 60);

        return totalHours + ":" + totalMinutes % 60;

    }, me);
    //-------------/computed-----------------------

    //-------------public methods-----------------------
    me.initial = function() {
        var enrollment = me.enrollment();
        if (enrollment) enrollment.enrollmentId(0);
    };
    me.close = function () {
        me.enrollment(null);
    };
    me.validate = function () {
        return true;
    };
 
    me.getEnrollmentsToSave = function (courseEnrollmentId, courseScheduleId, nonEmployeeId, employeeBadge) {
        var i,ins, pInsId, sInsId;
        var result = [],temp=[];
        var enrollments = [];
        var enrollment = me.enrollment();

        if (courseEnrollmentId) enrollment.courseEnrollmentId(courseEnrollmentId);
        enrollment.courseScheduleId(courseScheduleId);
        enrollment.nonEmployeeId(nonEmployeeId || 0);
        enrollment.badge(employeeBadge || '');
        enrollment.sessionDateStr(owner.minDate());


        pInsId = me.primaryInstructorId() || 0;
        sInsId = me.secondaryInstructorId() || 0;
        if (pInsId != 0) {
            ins = new EnrollmentInstructure();
            ins.enrollmentId(enrollment.enrollmentId() || 0);
            ins.instructorId(me.primaryInstructorId() || 0);
            ins.isPrimary(true);
            temp.push(ins);
        }
        if (sInsId != 0) {
            ins = new EnrollmentInstructure();
            ins.enrollmentId(enrollment.enrollmentId() || 0);
            ins.instructorId(me.secondaryInstructorId() || 0);
            ins.isPrimary(false);
            temp.push(ins);
        }

        enrollment.enrollmentInstructors(temp);        

        enrollments.push(me.enrollment());        

        for (i = 0; i < enrollments.length; i++)
            result.push(enrollments[i].toServerObj());
        
        return result;
    };
    me.loadGrades = function (data) {
        if (data.IncludeGrades) {
            var temp = [];
            data.Grades = data.Grades || [];
            for (var i = 0; i < data.Grades.length; i++) {
                var g = new Grade(data.Grades[i]);
                temp.push(g);
            }
            me.grades(temp);
        }
    };
    me.loadEnrollments = function (data) {
        var i;
        var temp = [];
        var ceId = owner.courseEnrollmentId() || 0;
        if (data.IncludeEnrollments) {
            var en;
            data.Enrollments = data.Enrollments || [];
            if (data.CourseEnrollmentId != 0 && ceId != data.courseEnrollmentId)
                owner.courseEnrollmentId(data.CourseEnrollmentId);

            for (i = 0; i < data.Enrollments.length; i++) {
                en = new Enrollment(data.Enrollments[i]);
                temp.push(en);
            }
            if (temp.length>0)
                me.enrollment(temp[0]);
            else if (me.enrollment()==null)
                me.enrollment(new Enrollment());
            var inss = me.enrollment().enrollmentInstructors() || [];
            for (i = 0; i < inss.length; i++) {
                if (inss[i].isPrimary())
                    me.primaryInstructorId(inss[i].instructorId());
                else
                    me.secondaryInstructorId(inss[i].instructorId());
            }
            if (me.enrollment() && me.enrollment().enrollmentId() == 0) {
                try {
                    var cSs = owner.courseSchedules() || [];
                    var cSId = owner.courseScheduleId() || 0;
                    if (cSId != 0 && cSs.length > 0) {
                        for (i = 0; i < cSs.length; i++) {
                            if (cSs[i].courseScheduleId() == cSId) {
                                var beginDate = cSs[i].beginEffDateStr() || '';
                                var endDate = cSs[i].endEffDateStr() || '';
                                var startTime = cSs[i].startTimeStr() || '';
                                var endTime = cSs[i].endTimeStr() || '';
                                if (beginDate != '' && endDate != '' && startTime != '' && endTime != '') {
                                    var t1 = Common.Date.parse(beginDate + ' ' + startTime, 'mm/dd/yyyy hh:mm');
                                    var t2 = Common.Date.parse(beginDate + ' ' + endTime, 'mm/dd/yyyy hh:mm');
                                    var timeInMs = t2 - t1;
                                    var d1 = Common.Date.parse(beginDate );
                                    var d2 = Common.Date.parse(endDate);
                                    var days = Math.floor((d2 - d1)/(1000 * 60 * 60 * 24)) ||1;
                                    var ms = timeInMs * days;
                                    
                                    if (ms > 0) {
                                        var totalMinutes = ms / (1000 * 60);
                                        var hours = Math.floor(totalMinutes / 60);
                                        var minutes = totalMinutes - (hours * 60);
                                        var hourStr = (hours < 10) ? '0' + hours : hours.toString();
                                        var minuteStr = (minutes < 10) ? '0' + minutes : minutes.toString();
                                        me.enrollment().lectureTimeStr(hourStr + ':' + minuteStr);
                                    }
                                }
                                break;
                            }
                        }
                    }
                } catch (e) {
                    alert(e.message);
                }
            }
        }
        else if (!me.enrollment())
            me.enrollment(new Enrollment());        
    };
    me.load = function (data) {
        me.loadGrades(data);
        me.loadEnrollments(data);
    }
    //-------------/public methods----------------------- 
}

var TransportationMaintenanceEnrollmentBasePage = function (baseUrl) {
    var me = this;
    var ignoreSubscription = false;
    //--------------Inherite from EnrollmentBasePage--------------
    me.prototype = new EnrollmentBasePage;
    me.prototype.constructor.call(this, baseUrl);
    //--------------/Inherite from EnrollmentBasePage--------------

    //-------------observable-----------------------
    me.transportation = ko.observable(new TransportationEnrollment(me));
    me.maintenance = ko.observable(new MaintenanceEnrollment(me));
    me.isTransportation = ko.observable(false);
    me.canBeDeletedDummy = ko.observable();
    //-------------/observable-----------------------

    //-------------subscribe-----------------------
    me.courseTypeId.subscribe(function () {
        if (me.courseTypeId() == 1)
            me.isTransportation(true);
        else
            me.isTransportation(false);

        if (me.formShown && !ignoreSubscription) {
            try {
                ignoreSubscription = true;
                me.courseEnrollmentId(0);
                me.courseId(0);
                me.courseScheduleId(0);
                updateCourse();
            } finally {
                ignoreSubscription = false;
            }
        }
    });
    me.courseId.subscribe(function () {
        if (me.formShown && !ignoreSubscription) {
            try {
                ignoreSubscription = true;
                me.courseEnrollmentId(0);
                me.courseSchedules([]);
                me.courseScheduleId(0);
                updateCourseSchedule();
            } finally {
                ignoreSubscription = false;
            }
        }
    });
    me.courseScheduleId.subscribe(function () {
        if (me.formShown && !ignoreSubscription) {
            try {
                ignoreSubscription = true;
                me.courseEnrollmentId(0); updateInstructors();
            } finally {
                ignoreSubscription = false;
            }
        }
    });
    me.employeeBadge.subscribe(function () {
        if (me.formShown && !ignoreSubscription) {
            try {
                ignoreSubscription = true;
                me.courseEnrollmentId(0); updateCourseSchedule();
            } finally {
                ignoreSubscription = false;
            }
        }
    });
    me.nonEmployeeId.subscribe(function () {
        if (me.formShown && !ignoreSubscription) {
            try {
                ignoreSubscription = true;
                me.courseEnrollmentId(0); updateCourseSchedule();
            } finally {
                ignoreSubscription = false;
            }
        }         
    });
    //-------------/subscribe-----------------------

    //-------------events-----------------------
    //-------------/events-----------------------

    //-------------computed-----------------------
    me.canBeDeleted = ko.computed(function () {
        var dummy = me.canBeDeletedDummy();
        var res = false;
        var ctId = me.courseTypeId();
        var ceId = me.courseEnrollmentId();
        var isTransportation = me.isTransportation();
        if (ctId && ceId && ceId > 0) {
            if (isTransportation && me.transportation() && me.transportation().enrollments() && me.transportation().enrollments().length > 0)
                res = true;
            else if (!isTransportation && me.maintenance() && me.maintenance().enrollment() && me.maintenance().enrollment().enrollmentId() > 0)
                res = true;
        }
        return res;
    });
    //-------------/computed-----------------------

    //-------------public methods-----------------------    
    me.initial = function () {
        me.baseInit();
        me.transportation().initial();
        me.maintenance().initial();
        return load();
    };
    me.close = function () {
        me.initialized(false);
        me.pageReady(false);
        me.formShown = false;
        me.transportation().close();
        me.maintenance().close();
    };
  
    me.validate = function () {
        if (!me.baseValidate())
            return false;
        if (me.isTransportation() && !me.transportation().validate())
            return false;
        if (!me.isTransportation() && !me.maintenance().validate())
            return false;
        me.courseTypeRequiredDummy(new Date());
        me.courseRequiredDummy(new Date());
        me.courseScheduleRequiredDummy(new Date());
        me.employeeBadgeRequiredDummy(new Date());
        me.nonEmployeeIdRequiredDummy(new Date());
        if (me.courseTypeRequired()) return false;
        if (me.courseRequired()) return false;
        if (me.courseScheduleRequired()) return false;
        if (me.employeeBadgeRequired()) return false;
        if (me.nonEmployeeIdRequired()) return false;
        return true;
    };
  
    me.clearEnrollmentIdWithoutLoad = function() {
        me.formShown = false;
        me.clearEmployeeNonEmployee();
        me.formShown = true;
    };
    me.getEnrollmentsToSave = function () {
        if (me.isTransportation())
            return me.transportation().getEnrollmentsToSave(me.courseEnrollmentId(), me.courseScheduleId(), me.nonEmployeeId(), me.employeeBadge());
        else 
            return me.maintenance().getEnrollmentsToSave(me.courseEnrollmentId(), me.courseScheduleId(), me.nonEmployeeId(), me.employeeBadge());     
    };

 
    //-------------/public methods-----------------------

    function getParam(forceCourse, forCourseSchedule, forceInstructor) {
        var ctId;
        var obj = {};
        if (me.courseTypes().length == 0) obj.IncludeCourseTypes = true;
        if (!me.transportation() || me.transportation().vehicles().length == 0) obj.IncludeVehicles = true;
        if (!me.transportation() || me.transportation().routes().length == 0) obj.IncludeRoutes = true;
        if (!me.maintenance() || me.maintenance().grades().length == 0) obj.IncludeGrades= true;
        if (me.nonEmployees().length == 0 || me.nonEmployeeId()!=0) obj.IncludeNonEmployees = true;
        obj.IncludeEnrollments = true;
        obj.EmployeeBadge = me.employeeBadge();
        obj.NonEmployeeId = me.nonEmployeeId();

        ctId = me.courseTypeId();

        if (forceCourse) {            
            if (ctId) {
                obj.CourseTypeId = ctId;
                if (!me.hasCourses(ctId)) obj.IncludeCourses = true;
            }
            else 
                obj.IncludeCourses = true;
            obj.IncludeCourseSchedules = true;
        }
        else if (forCourseSchedule) {
            obj.IncludeCourseSchedules = true;            
            if (ctId)
                obj.CourseTypeId = ctId;
            if (me.courseId())
                obj.CourseId = me.courseId();

            if (me.courseScheduleId()) {
                obj.CourseScheduleId = me.courseScheduleId();
            }
        }
        else if (forceInstructor) {            
            if (ctId) {
                obj.IncludeCourseSchedules = true;
                obj.CourseTypeId = ctId;
                if (me.courseId()) {
                    obj.CourseId = me.courseId();
                    if (me.courseScheduleId()) {
                        obj.CourseScheduleId = me.courseScheduleId();
                    }
                }
            }
        }
        else {

            if (me.courseEnrollmentId()) {
                obj.CourseEnrollmentId = me.courseEnrollmentId();
                if (!ctId || !me.hasCourses(ctId)) obj.IncludeCourses = true;
                obj.IncludeCourseSchedules = true;
                obj.IncludeInstructors = true;
                obj.IncludeTopics = true;
            } else {
                if (ctId) {
                    obj.IncludeInstructors = true;
                    if (!me.hasCourses(ctId)) obj.IncludeCourses = true;                    
                    obj.CourseTypeId = ctId;
                    if (me.courseId()) {
                        obj.IncludeCourseSchedules = true;
                        obj.CourseId = me.courseId();
                        if (me.courseScheduleId()) {
                            obj.CourseScheduleId = me.courseScheduleId();
                            obj.IncludeTopics = true;
                        }
                    }
                }
            }
        }
      
        return obj;
    }
    function updateInstructors() {
        if (me.formShown) {
            me.formShown = false;
            var d1 = load(waitBox, getParam(false, false,true));
            $.when(d1).always(function () { me.formShown = true; });
        }
    }
    function updateCourseSchedule() {
        if (me.formShown) {
            me.formShown = false;
            var d1 = load(waitBox, getParam(false,true));
            $.when(d1).always(function() { me.formShown = true; });
        }            
    }
    function updateCourse() {
        if (me.formShown) {
            me.formShown = false;
            var d1 = load(waitBox, getParam(true));
            $.when(d1).always(function () { me.formShown = true; });
        }
    }
    function load(waitDialog, param) {
        if (waitDialog) waitDialog.show();
        if (!param)
            param = getParam();
        return Ajax.ajax(baseUrl + "/GetModel", {
            data: JSON.stringify(param),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitDialog).done(function (data) {
            data = data || {};
            me.loadCourseTypes(data);
            me.loadCourses(data);
            me.loadCourseSchedules(data);
            me.loadEmployeeNonEmployee(data);
            me.transportation().load(data);
            me.maintenance().load(data);
            me.loadInstructors(data);
            me.canBeDeletedDummy(new Date()); //To refresh it.
        }).always(function () {
            if (waitDialog) waitDialog.hide();
        });
    }

};


var EnrollmentPage = function (url) {
    var me = this;    

    me.prototype = new TransportationMaintenanceEnrollmentBasePage;
    me.prototype.constructor.call(this, url);


    me.isCanceled = false;
    me.forceSavedStatus = false; // in case of SaveAndNew, if user press cancel after "Save And New", we want the Main page to refresh it self.

    me.show = function (enrollmentId, courseEnrollmentId,courseTypeId,isSaveCopyEnrollment) {
        me.formShown = false;
        waitBox.show();
        if (!isSaveCopyEnrollment) me.close();
        if (courseTypeId) me.courseTypeId(courseTypeId);
        courseEnrollmentId = courseEnrollmentId || 0;
        me.courseEnrollmentId(courseEnrollmentId);
        me.initial().always(function () {
            try {                
                me.pageReady(true);
                sammyApp.trigger('pageLoaded');
            } finally {
                waitBox.hide();
                me.formShown = true;
            }
        });        
    };

    me.cancel = function () {
        me.isCanceled = (true & !me.forceSavedStatus);
        PageNavigation.navigateToMain();
        me.close();
    };

    me.save = function () {
        if (!me.validate())
            return;
        internalSave().done(function (data) {
            me.isCanceled = false;
            me.forceSavedStatus = false;
            PageNavigation.navigateToMain();
            me.close();
        });
    };

    me.saveNew = function () {
        if (!me.validate())
            return;
        internalSave().done(function (data) {
            me.forceSavedStatus = true;
            me.clearEnrollmentIdWithoutLoad();
            PageNavigation.navigateToEnrollSaveCopy(me.courseTypeId());
            fadeBox.show("Saved...");
        });        
    };

    me.remove = function () {
        var deleteUrl = url + "/Delete";
        modal.show("Warning", "Are you sure you wish to delete this attendace?", { showRefresh: false, showCancel: true, showOk: true }).done(
            function (data) {
                if (data == 'OK') {
                    Ajax.ajax(deleteUrl, {
                        data: '{ courseEnrollmentId: ' + me.courseEnrollmentId() + ' }',
                        dataType: 'json',
                        type: 'DELETE',
                        contentType: "application/json; charset=utf-8;"
                    }, waitBox).done(function () {
                        me.isCanceled = false;
                        PageNavigation.navigateToMain();
                        me.close();
                    });
                }
            });
    }

    function internalSave() {
        var obj = {};
        obj.Enrollments = me.getEnrollmentsToSave();
        return Ajax.ajax(url + "/Save", {
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitBox);
    }
};

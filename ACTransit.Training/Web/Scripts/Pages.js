var throttle = 100;
var CourseType = function (serverObj, initialValues) {
    var self = this;
    self.visible = ko.observable(true);
    self.courseTypeId = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.isActive = ko.observable();
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {self.collapsed(!self.collapsed());};
    self.initial = function (c) {
        try {
            if (c != null) {
                self.courseTypeId(c.CourseTypeId);
                self.name(c.Name);
                self.description(c.Description);
                self.isActive(c.IsActive);
                if (initialValues && !isNaN(initialValues.courseTypeId)) {
                    if (self.courseTypeId() != initialValues.courseTypeId) {
                        self.visible(false);
                        self.collapsed(true);
                    } else {
                        self.collapsed(false);
                    }
                }
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);

}
var Course = function (serverObj, initialValues) {
    var self = this;
    self.visible = ko.observable(true);
    self.courseId = ko.observable();
    self.courseTypeId = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.hasWheelTime = ko.observable();
    self.isActive = ko.observable();
    self.courseSchedules = ko.observable([]);
    self.topics = ko.observable([]);
    self.componentTopics = ko.observable([]);
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {        
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.courseSchedulesOutOfDate = ko.observable(false);
    self.toggle = function () {        
        self.collapsed(!self.collapsed());
        return self.collapsed();
    };
    self.toServerObj = function () {
        var result = {};
        result.CourseId=self.courseId();
        result.CourseTypeId=self.courseTypeId();
        result.Name=self.name();
        result.Description=self.description();
        result.HasWheelTime = self.hasWheelTime();
        result.IsActive = self.isActive();
        return result;
    };
    self.initial = function (c) {        
        try {            
            if (c != null) {
                self.courseId(c.CourseId);
                self.courseTypeId(c.CourseTypeId);
                self.name(c.Name);
                self.description(c.Description);
                self.hasWheelTime(c.HasWheelTime);
                self.isActive(c.IsActive);
                if (initialValues && !isNaN(initialValues.courseId)) {
                    if (self.courseId() != initialValues.courseId) {
                        self.visible(false);
                        self.collapsed(true);
                    } else {
                        self.collapsed(false);                        
                    }
                }
                var temp = [];

                //update and insert and delete
                if (c.CourseSchedules) {
                    var updated = false;
                    for (var i = 0; i < c.CourseSchedules.length; i++) {
                        updated = true;
                        var newCourseS = null;
                        for (var j in self.courseSchedules()) {
                            if (self.courseSchedules()[j].courseScheduleId && c.CourseSchedules[i].CourseScheduleId == self.courseSchedules()[j].courseScheduleId()) {
                                newCourseS = self.courseSchedules()[j].initial(c.CourseSchedules[i]);
                                break;
                            }
                        }
                        if (newCourseS == null) newCourseS = new CourseSchedule(c.CourseSchedules[i], initialValues);
                        temp.push(newCourseS);
                    }
                    if (updated) self.courseSchedules(temp);
                    self.courseSchedulesOutOfDate(!updated);
                }

                //update and insert and delete
                if (c.Topics) {
                    temp = [];
                    for (var i = 0; i < c.Topics.length; i++) {
                        var newTopic = null;
                        for (var j in self.topics()) {
                            if (self.topics()[j].topicId && c.Topics[i].TopicId == self.topics()[j].topicId()) {
                                newTopic = self.topics()[j].initial(c.Topics[i]);
                                break;
                            }
                        }
                        if (newTopic == null) newTopic = new Topic(c.Topics[i], initialValues);
                        temp.push(newTopic);
                    }
                    self.topics(temp);
                }
                if (c.ComponentTopics) {
                    temp = [];
                    for (var i = 0; i < c.ComponentTopics.length; i++) {
                        var newTopic = null;
                        for (var j in self.componentTopics()) {
                            if (self.componentTopics()[j].topicId && c.ComponentTopics[i].TopicId == self.componentTopics()[j].topicId()) {
                                newTopic = self.componentTopics()[j].initial(c.ComponentTopics[i]);
                                break;
                            }
                        }
                        if (newTopic == null) newTopic = new Topic(c.ComponentTopics[i], initialValues);
                        temp.push(newTopic);
                    }
                    self.componentTopics(temp);
                }

            }            
        } finally {
        }
        return self;
        
    }    
    self.initial(serverObj);
    
}
var CourseScheduleInstructor = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.courseScheduleId = ko.observable();
    self.instructorId = ko.observable();
    self.isPrimary = ko.observable();

    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
        return self.collapsed();
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.courseScheduleId(c.CourseScheduleId);
                self.instructorId(c.InstructorId);
                self.isPrimary(c.IsPrimary);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var CourseSchedule = function (serverObj, initialValues) {
    var self = this;
    self.courseScheduleId = ko.observable();
    self.courseId= ko.observable();
    self.beginEffDate = ko.observable();
    self.beginEffDateStr = ko.observable('');
    self.endEffDate = ko.observable();
    self.endEffDateStr = ko.observable('');
    self.startTime = ko.observable();
    self.startTimeStr = ko.observable('');
    self.endTime = ko.observable();
    self.endTimeStr = ko.observable('');
    self.totalSeat = ko.observable();
    self.frequency = ko.observable();
    self.note = ko.observable();
    self.description = ko.observable();
    self.divisionId = ko.observable();
    self.courseEnrollments = ko.observable([]);
    self.courseScheduleInstructors = ko.observableArray([]);
    self.collapsed = ko.observable(true);
    self.isCurrent = ko.observable(false);
    self.getCollapsed = ko.computed(function () {        
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function() {
        self.collapsed(!self.collapsed());
        return self.collapsed();
    };   
    self.occupiedSeats = ko.computed(function () {        
        return self.courseEnrollments().length;
    }).extend({ throttle: throttle });
    self.getName = ko.computed(function (courseSchedule) {
        
        var res = self.beginEffDateStr() + ' - ' + self.endEffDateStr() + ' (' + self.startTimeStr() + ' - ' + self.endTimeStr() + ')';
        return res;
    }).extend({ throttle: throttle });
    self.getAddNewName = ko.computed(function (courseSchedule) {        
        return 'Add New (' + self.occupiedSeats() + ' of ' + self.totalSeat() + ')' ;
    }).extend({ throttle: throttle });
    self.courseEnrollmentsOutOfDate = ko.observable(false);

    self.toServerObj = function() {
        var result = {};
        result.CourseScheduleId = self.courseScheduleId();
        result.CourseId = self.courseId();
        result.BeginEffDate = self.beginEffDate();
        result.BeginEffDateStr = self.beginEffDateStr();
        result.EndEffDate = self.endEffDate();
        result.EndEffDateStr = self.endEffDateStr();
        result.AtartTime = self.startTime();
        result.StartTimeStr = self.startTimeStr();
        result.EndTime = self.endTime();
        result.EndTimeStr = self.endTimeStr();
        result.TotalSeat = self.totalSeat();
        result.Frequency = self.frequency();
        result.Note = self.note();
        result.Description = self.description();
        result.DivisionId = self.divisionId();
        result.IsCurrent = self.isCurrent();        
        return result;
    };
    self.initial = function (cs) {
        var i;
        var temp = [];
        if (cs != null) {
            self.courseScheduleId(cs.CourseScheduleId);
            self.courseId(cs.CourseId);
            self.beginEffDate(cs.BeginEffDate);
            self.beginEffDateStr(cs.BeginEffDateStr);
            self.endEffDate(cs.EndEffDate);            
            self.endEffDateStr(cs.EndEffDateStr);
            self.startTime(cs.StartTime);
            self.startTimeStr(cs.StartTimeStr);
            self.endTime(cs.EndTime);
            self.endTimeStr(cs.EndTimeStr);
            self.totalSeat(cs.TotalSeat);
            self.frequency(cs.Frequency);
            self.note(cs.Note);
            self.description(cs.Description);
            self.divisionId(cs.DivisionId);
            self.isCurrent(cs.IsCurrent);
            if (initialValues && !isNaN(initialValues.courseScheduleId)) {
                if (self.courseScheduleId() != initialValues.courseScheduleId) {
                    self.collapsed(true);
                } else {
                    self.collapsed(false);
                }
            }

            if (cs.CourseEnrollments) {
                var updated = false;
                temp = [];
                updated = true;
                for (i = 0; i < cs.CourseEnrollments.length; i++) {
                    var newCourseE = null;
                    for (var j in self.courseEnrollments()) {
                        if (self.courseEnrollments()[j].courseEnrollmentId && cs.CourseEnrollments[i].CourseEnrollmentId == self.courseEnrollments()[j].courseEnrollmentId()) {
                            newCourseE = self.courseEnrollments()[j].initial(cs.CourseEnrollments[i]);
                        }
                    }
                    if (newCourseE == null) newCourseE = new CourseEnrollment(cs.CourseEnrollments[i]);
                    temp.push(newCourseE);
                }
                if (updated) self.courseEnrollments(temp);
                self.courseEnrollmentsOutOfDate(!updated);
            }
            if (cs.CourseScheduleInstructors) {
                temp = [];
                for (i = 0; i < cs.CourseScheduleInstructors.length; i++) {
                    temp.push(new CourseScheduleInstructor(cs.CourseScheduleInstructors[i]));
                }
                self.courseScheduleInstructors(temp);
            }
        }
        return self;
    }

    self.initial(serverObj);

}
var CourseEnrollment = function (serverObj) {
    var self = this;
    self.courseEnrollmentId = ko.observable();                
    self.badge = ko.observable();
    self.noneEmployeeId = ko.observable();
    self.name = ko.observable();
    
    self.note = ko.observable();
    self.dept = ko.observable();
    self.division = ko.observable();
    self.noShow = ko.observable();
    self.initial = function (ce) {
        if (ce != null) {
            self.courseEnrollmentId (ce.CourseEnrollmentId);
            self.badge (ce.Badge);
            self.noneEmployeeId (ce.NoneEmployeeId);
            self.name(ce.Name);            
            self.note(ce.Note);
            self.dept (ce.Dept);
            self.division(ce.Division);
            self.noShow(ce.NoShow);
        }
        return self;
    }

    self.initial(serverObj);
}
var EnrollmentVehicle = function (serverObj) {
    var self = this;
    self.enrollmentVehicleId = ko.observable('');
    self.enrollmentId = ko.observable('');
    self.vehicleId = ko.observable('');
    self.vehicleDescription= ko.observable('');
    self.wheelTime = ko.observable('');
    self.wheelTimeStr = ko.observable('');
    self.routeAlpha = ko.observable('');
    self.qualifiedTraining = ko.observable(false);
    self.ougLiftRampOps = ko.observable(false);
    self.ougSecurement = ko.observable(false);
    self.editState = ko.observable(false);
    self.clone = function() {
        var res = new EnrollmentVehicle();
        res.enrollmentVehicleId(self.enrollmentVehicleId());
        res.enrollmentId(self.enrollmentId());
        res.vehicleId(self.vehicleId());
        res.vehicleDescription(self.vehicleDescription());
        res.wheelTime(self.wheelTime());
        res.wheelTimeStr(self.wheelTimeStr());
        res.routeAlpha(self.routeAlpha());
        res.qualifiedTraining(self.qualifiedTraining());
        res.ougLiftRampOps(self.ougLiftRampOps());
        res.ougSecurement(self.ougSecurement());
        res.editState(self.editState());
        return res;
    };
    self.toServerObj = function () {
        var result = {};
        result.EnrollmentVehicleId= self.enrollmentVehicleId();
        result.EnrollmentId=self.enrollmentId();
        result.VehicleId=self.vehicleId();
        result.VehicleDescription=self.vehicleDescription();
        result.WheelTime=self.wheelTime();
        result.WheelTimeStr=self.wheelTimeStr();
        result.RouteAlpha=self.routeAlpha();
        result.QualifiedTraining=self.qualifiedTraining();
        result.OugLiftRampOps=self.ougLiftRampOps();
        result.OugSecurement = self.ougSecurement();
        result.EnrollmentVehicleRoutes = [];
        return result;
    };
    self.initial = function (ev) {
        if (ev != null) {            
            self.enrollmentVehicleId(ev.EnrollmentVehicleId);
            self.enrollmentId(ev.EnrollmentId);
            self.vehicleId(ev.VehicleId);
            self.vehicleDescription(ev.VehicleDescription);
            self.wheelTime(ev.WheelTime);
            self.wheelTimeStr(ev.WheelTimeStr);            
            self.qualifiedTraining(ev.QualifiedTraining);
            self.ougLiftRampOps(ev.OugLiftRampOps);
            self.ougSecurement(ev.OugSecurement);
            self.routeAlpha(ev.RouteAlpha);
        }
        return self;
    }

    self.initial(serverObj);
}

var EnrollmentInstructure = function (serverObj) {
    var self = this;
    self.enrollmentInstructorId = ko.observable('');
    self.enrollmentId = ko.observable('');
    self.instructorId = ko.observable('');
    self.isPrimary = ko.observable('');
    self.clone = function () {
        var res = new EnrollmentInstructure();
        res.enrollmentInstructorId(self.enrollmentInstructorId);
        res.enrollmentId(self.enrollmentId);
        res.instructorId(self.instructorId);
        res.isPrimary(self.isPrimary);
        return res;
    };
    self.toServerObj = function () {
        var result = {};
        result.EnrollmentInstructorId = self.enrollmentInstructorId();
        result.EnrollmentId = self.enrollmentId();
        result.InstructorId = self.instructorId();
        result.IsPrimary = self.isPrimary();

        return result;
    };
    self.initial = function (ei) {
        if (ei != null) {
            self.enrollmentInstructorId(ei.EnrollmentInstructorId);
            self.enrollmentId(ei.EnrollmentId);
            self.instructorId(ei.InstructorId);
            self.isPrimary(ei.IsPrimary);
        }
        return self;
    }

    self.initial(serverObj);
}
var EnrollmentTopic = function (serverObj) {
    var self = this;
    self.enrollmentTopicId = ko.observable('');
    self.enrollmentId = ko.observable('');
    self.topicId = ko.observable('');
    self.clone = function () {
        var res = new EnrollmentInstructure();
        res.enrollmentTopicId(self.enrollmentTopicId);
        res.enrollmentId(self.enrollmentId);
        res.topicId(self.topicId);
        return res;
    };
    self.toServerObj = function () {
        var result = {};
        result.EnrollmentTopicId = self.enrollmentTopicId();
        result.EnrollmentId = self.enrollmentId();
        result.TopicId = self.topicId();
        return result;
    };
    self.initial = function (t) {
        if (t != null) {
            self.enrollmentTopicId(t.EnrollmentTopicId);
            self.enrollmentId(t.EnrollmentId);
            self.topicId(t.TopicId);
        }
        return self;
    }

    self.initial(serverObj);
}
var Topic = function (serverObj, initialValues) {
    var self = this;
    self.visible = ko.observable(true);
    self.topicId = ko.observable();
    self.courseTypeId = ko.observable();
    self.topicTypeId= ko.observable();
    self.name = ko.observable();
    self.selected = ko.observable(false);
    self.description = ko.observable();
    self.isActive = ko.observable();
    self.collapsed = ko.observable(true);
    self.title = ko.computed(function() {
        var name = self.name();
        var description = self.description();
        var topicTypeId = self.topicTypeId() || 1;
        if (topicTypeId == 1)
            return name;
        else
            return name + " - " + description ;

    });
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.topicId(c.TopicId);
                self.courseTypeId(c.CourseTypeId);
                self.name(c.Name);
                self.description(c.Description);
                self.topicTypeId(c.TopicTypeId);
                self.isActive(c.IsActive);
                if (initialValues && !isNaN(initialValues.topicId)) {
                    if (self.courseTypeId() != initialValues.topicId) {
                        self.visible(false);
                        self.collapsed(true);
                    } else {
                        self.collapsed(false);
                    }
                }
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var Enrollment = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);
    self.enrollmentId = ko.observable(0);
    self.courseEnrollmentId = ko.observable();
    self.courseScheduleId = ko.observable();
    self.badge = ko.observable();
    self.employee = ko.observable();    
    self.nonEmployeeId = ko.observable(0);
    self.courseName = ko.observable();
    self.sessionDate = ko.observable(0);
    self.sessionDateStr = ko.observable();
    self.lectureTime = ko.observable(0);    
    self.lectureTimeStr = ko.observable('00:00');
    self.note = ko.observable('');
    self.letterGrade = ko.observable('');
    self.noShow = ko.observable(false);
    self.enrollmentVehicles = ko.observableArray([]);
    self.enrollmentInstructors = ko.observableArray([]);
    self.enrollmentTopics = ko.observableArray([]);
    self.hasVehicles = ko.computed(function() {
        return true;
    });
    self.editState = ko.observable(false);
    self.editState.subscribe(function() {
        if (self.hasVehicles()) {
            var es = self.editState();
            var v = self.enrollmentVehicles();
            for (var i = 0; i < v.length; i++)
                v[i].editState(es);
        }
    });
    self.collapsed = ko.observable(true);  
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toServerObj = function () {
        var i;
        var result = {};
        result.EnrollmentId = self.enrollmentId();
        result.CourseEnrollmentId = self.courseEnrollmentId();
        result.CourseScheduleId = self.courseScheduleId();
        result.Badge = self.badge();
        result.NonEmployeeId = self.nonEmployeeId();
        result.SessionDate = self.sessionDate();
        result.SessionDateStr = self.sessionDateStr();
        result.LectureTime = self.lectureTime();
        result.LectureTimeStr = self.lectureTimeStr();
        result.Note = self.note();
        result.LetterGrade = self.letterGrade();
        result.NoShow = self.noShow();
        result.EnrollmentVehicles = [];
        result.EnrollmentInstructors = [];
        result.EnrollmentTopics = [];
        if (self.enrollmentVehicles()) {
            for (i = 0; i < self.enrollmentVehicles().length; i++) {
                result.EnrollmentVehicles.push(self.enrollmentVehicles()[i].toServerObj());
            }
        }
        if (self.enrollmentInstructors()) {
            for (i = 0; i < self.enrollmentInstructors().length; i++) {
                result.EnrollmentInstructors.push(self.enrollmentInstructors()[i].toServerObj());
            }
        }
        if (self.enrollmentTopics()) {
            for (i = 0; i < self.enrollmentTopics().length; i++) {
                result.EnrollmentTopics.push(self.enrollmentTopics()[i].toServerObj());
            }
        }
        return result;
    };
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                var i;
                var temp = [];
                self.enrollmentId(c.EnrollmentId);
                self.courseEnrollmentId(c.CourseEnrollmentId);
                self.courseScheduleId(c.CourseScheduleId);
                self.badge(c.Badge);
                self.employee(c.Employee);
                self.nonEmployeeId(c.NonEmployeeId);
                self.courseName(c.CourseName);                
                self.sessionDate(c.SessionDate);
                self.sessionDateStr(c.SessionDateStr);
                self.lectureTime(c.LectureTime);
                self.lectureTimeStr(c.LectureTimeStr);
                self.note(c.Note);
                self.letterGrade(c.LetterGrade);
                self.noShow(c.NoShow);
                if (c.EnrollmentVehicles) {
                    temp = [];
                    for (i = 0; i < c.EnrollmentVehicles.length; i++) {
                        var ev = new EnrollmentVehicle(c.EnrollmentVehicles[i]);
                        temp.push(ev);
                    }
                    self.enrollmentVehicles(temp);                    
                }                
                if (c.EnrollmentInstructors) {
                    temp = [];
                    for (i = 0; i < c.EnrollmentInstructors.length; i++) {
                        var ei = new EnrollmentInstructure(c.EnrollmentInstructors[i]);
                        temp.push(ei);
                    }
                    self.enrollmentInstructors(temp);
                }                

                if (c.EnrollmentTopics) {
                    temp = [];
                    for (i = 0; i < c.EnrollmentTopics.length; i++) {
                        var t = new EnrollmentTopic(c.EnrollmentTopics[i]);
                        temp.push(t);
                    }
                    self.enrollmentTopics(temp);
                }
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var VehicleRegister = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.vehicleId = ko.observable();
    self.description = ko.observable();
    self.active = ko.observable();
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.vehicleId(c.VehicleId);
                self.description (c.Description);
                self.active (c.Active);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var Route = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.routeAlpha = ko.observable();
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                //self.routeId(c.RouteId);
                self.routeAlpha(c.RouteAlpha);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var Instructor = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);
    
    self.instructorId = ko.observable();
    self.badge = ko.observable();
    self.instructor = ko.observable();
    self.nonEmployeeId = ko.observable();
    self.courseTypeId = ko.observable();
    self.isActive = ko.observable();

    self.selected = ko.observable(false);
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.instructorId(c.InstructorId);
                self.badge(c.Badge);
                self.instructor(c.Instructor);
                self.nonEmployeeId(c.NonEmployeeId);
                self.courseTypeId(c.CourseTypeId);
                self.isActive(c.IsActive);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var Grade = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.letterGrade = ko.observable();
    self.description = ko.observable();
    self.isPassing = ko.observable();

    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.letterGrade(c.LetterGrade);
                self.description(c.Description);
                self.isPassing(c.IsPassing);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var Division = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.divisionId = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.isActive= ko.observable();

    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.divisionId(c.DivisionId);
                self.name(c.Name);
                self.description(c.Description);
                self.isActive(c.IsActive);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}
var NonEmployee = function (serverObj) {
    var self = this;
    self.visible = ko.observable(true);

    self.nonEmployeeId = ko.observable();
    self.name = ko.observable();
    self.description = ko.observable();
    self.isActive = ko.observable();
    self.collapsed = ko.observable(true);
    self.getCollapsed = ko.computed(function () {
        if (self.collapsed())
            return 'accordion-toggle collapsed';
        return 'accordion-toggle';
    });
    self.getCollapse = ko.computed(function () {
        if (self.collapsed())
            return 'collapsed collapse';
        return '';
    });
    self.toggle = function () {
        self.collapsed(!self.collapsed());
    };
    self.initial = function (c) {
        try {
            if (c != null) {
                self.nonEmployeeId(c.NonEmployeeId);
                self.name(c.Name);
                self.description(c.Description);
                self.isActive(c.IsActive);
            }
        } finally {
        }
        return self;

    }
    self.initial(serverObj);
}

var CoursePage=function(courseId,baseUrl, cancelUrl, topicUrl) {
    var me = this;
    var formShown = false;
    me.state = ko.observable();
    me.hasEnrollment = ko.observable(false);
    me.course = ko.observable(new Course());
    me.courseTypeError = ko.observable("");
    me.courseTypes = ko.observableArray();
    me.courseTypeIdChanged = ko.computed(function () {
        getTopics();
        if (me.course().courseTypeId() == 2) //Maintenance
            getComponentTopics();
    });    
    me.topics = ko.observableArray([]);
    me.componentTopics = ko.observableArray([]);
    me.selectedTopics = ko.observableArray([]);
    me.selectedComponentTopics = ko.observableArray([]);

    me.courseTypeRequiredDummy = ko.observable();
    me.courseTypeRequired = ko.computed(function () {
        var dummy = me.courseTypeRequiredDummy();
        var c = me.course() || {};
        var value = c.courseTypeId() || 0;
        if (formShown && value == 0)
            return true;
        return false;
    });
    me.nameRequiredDummy = ko.observable();
    me.nameRequired= ko.computed(function () {
        var dummy = me.nameRequiredDummy();
        var c = me.course() || {};
        var value =  c.name() || '';
        if (formShown && value == '')
            return true;
        return false;
    });

    me.isEdit = ko.computed(function() {
        return courseId != 0;
    });
    me.save = function () {
        if (!me.validate()) return;
        var saveUrl = baseUrl + (me.isEdit()?"/UpdateCourse":"/AddCourse");
        var c = me.course().toServerObj();
        var temp = [];
        var selectedTopics = ko.unwrap(me.selectedTopics);
        for (var i = 0; i < selectedTopics.length; i++) {
            var t = new Object();
            t.TopicId = selectedTopics[i];
            temp.push(t);
        }
        c.Topics = temp;

        temp = [];
        var selectedComponentTopics = ko.unwrap(me.selectedComponentTopics);
        for (var i = 0; i < selectedComponentTopics.length; i++) {
            var t = new Object();
            t.TopicId = selectedComponentTopics[i];
            temp.push(t);
        }
        c.ComponentTopics = temp;


        var model = new Object();
        model.Course = c;

        Ajax.ajax(saveUrl, {
            data: JSON.stringify(model),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitBox).done(function (data) {
            Common.redirect(cancelUrl);
        });

    };
    me.deleteClicked = function () {
        var deleteUrl = baseUrl + "/Delete";
        modal.show("Warning", "Are you sure you wish to delete this course?", { showRefresh: false, showCancel: true, showOk: true }).done(
            function(data) {
                if (data == 'OK') {
                    Ajax.ajax(deleteUrl, {
                        data: '{ id: ' + courseId + ' }',
                        dataType: 'json',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8;"
                    }, waitBox).done(function () {
                        Common.redirect(cancelUrl);
                    });
                }
            });
    };

    me.validate = function() {
        me.courseTypeRequiredDummy(new Date());
        me.nameRequiredDummy(new Date());
        if (me.courseTypeRequired()) return false;
        if (me.nameRequired()) return false;
        return true;
    };
    me.load = function() {
        loadCourse();
    };
    function loadCourse() {
        Ajax.ajax(baseUrl + "/GetCourse", {
            data: '{id:' + courseId + '}',
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitBox).done(function (data) {
            var temp = [];
            if (data) {
                data.CourseTypes = data.CourseTypes || [];
                for (var i = 0; i < data.CourseTypes.length; i++) {
                    var ct = new CourseType(data.CourseTypes[i]);
                    temp.push(ct);
                }
                me.hasEnrollment(data.HasEnrollment||false);
                me.courseTypes(temp);
                me.course(new Course(data.Course));
            }
        }).always(function () {
            formShown = true;
        });
    }
    function getTopics() {
        var courseTypeId = me.course().courseTypeId();
        if (courseTypeId) {
            return Ajax.ajax(baseUrl + "/Topics", {
                data: '{courseTypeId:' + courseTypeId + ', courseId:' + (me.isEdit() ? courseId : 'null') + '}',
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var selectedTopics = me.course().topics();
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new Topic(data[i]);
                    for (var j = 0; j < selectedTopics.length; j++) {
                        if (ct.topicId() == selectedTopics[j].topicId()) {
                            ct.selected(true);
                            break;
                        }
                    }
                    temp.push(ct);
                }

                me.topics(temp);
            });
        }
    }
    function getComponentTopics() {
        var courseTypeId = me.course().courseTypeId();
        if (courseTypeId) {
            return Ajax.ajax(baseUrl + "/ComponentTopics", {
                data: '{courseTypeId:' + courseTypeId + ', courseId:' + (me.isEdit() ? courseId : 'null') + '}',
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var selectedComponentTopics = me.course().componentTopics();
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new Topic(data[i]);
                    for (var j = 0; j < selectedComponentTopics.length; j++) {
                        if (ct.topicId() == selectedComponentTopics[j].topicId()) {
                            ct.selected(true);
                            break;
                        }
                    }
                    temp.push(ct);
                }

                me.componentTopics(temp);
            });
        }
    }

    me.load();
}

var CourseSchedulePage = function (url) {
    var me = this;
    var loading = false;
    var formShown = false;
    me.state = ko.observable();
    me.initialized = ko.observable(false);
    me.courseSchedule = ko.observable(new CourseSchedule());
    
    me.courseTypes = ko.observableArray([]);
    me.courses = ko.observableArray([]);
    me.courseTypeId = ko.observable();
    me.selectedInstructors = ko.observable();
    me.instructors = ko.observableArray([]);
    me.divisions = ko.observableArray([]);

    me.canBeDeleted = ko.computed(function() {
        return (me.courseSchedule().courseScheduleId()||0);
    });
    me.onCourseTypeChanged = ko.computed(function () {
        var id = me.courseTypeId();
        if (loading) return;
        me.courses([]);
        me.instructors([]);
        loadCourse();
        loadInstructors();
    });

    me.sunday = ko.observable(false);
    me.monday = ko.observable(false);
    me.tuesday= ko.observable(false);
    me.wednesday = ko.observable(false);
    me.thursday = ko.observable(false);
    me.friday = ko.observable(false);
    me.saturday = ko.observable(false);

    me.courseTypeRequiredDummy = ko.observable();
    me.courseTypeRequired = ko.computed(function () {
        var dummy = me.courseTypeRequiredDummy();
        var coursetypeId = me.courseTypeId() || 0;
        if (formShown && coursetypeId == 0) return true;
        return false;
    });
    me.courseRequiredDummy = ko.observable();
    me.courseRequired = ko.computed(function () {
        var dummy = me.courseRequiredDummy();
        var courseId = me.courseSchedule().courseId() || 0;
        if (formShown && courseId == 0)  return true;
        return false;
    });
    me.startDateRequiredDummy = ko.observable();
    me.startDateRequired = ko.computed(function () {       
        var dummy = me.startDateRequiredDummy();
        var value = me.courseSchedule().beginEffDateStr() || '';
        if (formShown && value == '') return true;
        return false;
    });
    me.endDateRequiredDummy = ko.observable();
    me.endDateRequired = ko.computed(function () {
        var dummy = me.endDateRequiredDummy();
        var value = me.courseSchedule().endEffDateStr() || '';
        if (formShown && value == '') return true;
        return false;
    });
    me.startTimeRequiredDummy = ko.observable();
    me.startTimeRequired = ko.computed(function () {
        var dummy = me.startTimeRequiredDummy();
        var value = me.courseSchedule().startTimeStr() || '';
        if (formShown && value == '') return true;
        return false;
    });
    me.endTimeRequiredDummy = ko.observable();
    me.endTimeRequired = ko.computed(function () {
        var dummy = me.endTimeRequiredDummy();
        var value = me.courseSchedule().endTimeStr() || '';
        if (formShown && value == '') return true;
        return false;
    });
    me.validate = function() {
        me.courseTypeRequiredDummy(new Date());
        me.courseRequiredDummy(new Date());
        me.startDateRequiredDummy(new Date());
        me.endDateRequiredDummy(new Date());
        me.startTimeRequiredDummy(new Date());
        me.endTimeRequiredDummy(new Date());
        if (me.courseTypeRequired()) return false;
        if (me.courseRequired()) return false;
        if (me.startDateRequired()) return false;
        if (me.endDateRequired()) return false;
        if (me.startTimeRequired()) return false;
        if (me.endTimeRequired()) return false;

        return true;
    };

    me.save = function () {
        if (!me.validate()) return false;

        var courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
        var obj = {};
        obj.State = courseScheduleId == 0 ? 0 : 1;  //0:New, 1:Edit
        me.courseSchedule().frequency(
            (me.sunday()    ? 1 : 0) + 
            (me.monday()    ? 2 : 0) + 
            (me.tuesday()   ? 4 : 0) + 
            (me.wednesday() ? 8 : 0) + 
            (me.thursday()  ? 16 : 0) + 
            (me.friday()    ? 32 : 0) +
            (me.saturday()  ? 64 : 0) 
        );
        obj.CourseSchedule = me.courseSchedule().toServerObj();
        obj.CourseSchedule.BeginEffDate = new Date(obj.CourseSchedule.BeginEffDateStr).toDateString();
        obj.CourseSchedule.EndEffDate = new Date(obj.CourseSchedule.EndEffDateStr).toDateString();
        obj.CourseSchedule.CourseScheduleInstructors = [];
        var instructors = me.selectedInstructors();        
        for (var i = 0; i < instructors.length; i++) {
            obj.CourseSchedule.CourseScheduleInstructors.push({ 'InstructorId': instructors[i], 'CourseScheduleId': courseScheduleId });
        }
        Ajax.ajax(url + "/Save", {
            data: JSON.stringify(obj),
            dataType: 'json',
            type: 'POST',
            contentType: "application/json; charset=utf-8;"
        }, waitBox).done(function (data) {
            Common.redirect(url);
        });
    };
    me.cancel = function () {
        Common.redirect(url);
    };
    me.remove = function() {
        var courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
        modal.show("Warning", "Are you sure you wish to delete this schedule?", { showRefresh: false, showCancel: true, showOk: true }).done(
            function(data) {
                if (data == 'OK') {
                    Ajax.ajax(url + "/Delete", {
                        data: JSON.stringify({ 'id': courseScheduleId }),
                        dataType: 'json',
                        type: 'POST',
                        contentType: "application/json; charset=utf-8;"
                    }, waitBox).done(function(data) {
                        Common.redirect(url);
                    });
                }
            }
        );
    };
    me.show = function (courseScheduleId) {
        me.courseSchedule().courseScheduleId(courseScheduleId);
        load().always(function () {
            loading = false;
            me.initialized(true);
            setDefaultValues();
        });
        formShown = true;
    };
    function setDefaultValues() {
        var id = me.courseTypeId() || 0;
        if (me.courseTypes().length == 1 && id == 0) {
            me.courseTypeId(me.courseTypes()[0].courseTypeId());
        }        
    }
    function load() {
        var promis = $.Deferred();
        loading = true;
        var d1 = loadCourseType();
        var d2 = loadCourseSchedule();
        $.when(d1, d2).always(function() {
                var d3 = loadCourse();
                var d4 = loadInstructors();
                var d5 = loadDivisions();
                $.when(d3, d4, d5).done(function() {
                    promis.resolve();
                }).fail(function() {
                    promis.reject();
                });
            }
        );

        return promis;
    }
    function loadCourseSchedule() {        
        var courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
        if (courseScheduleId != 0) {
            return Ajax.ajax(url + "/GetModel", {
                data: JSON.stringify({ 'id': courseScheduleId }),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var temp = [];
                data = data || {};
                data.CourseSchedule = data.CourseSchedule || {};
                me.courseSchedule(new CourseSchedule(data.CourseSchedule));
                me.courseTypeId(data.CourseTypeId);                
                me.sunday(me.courseSchedule().frequency() & 1);
                me.monday(me.courseSchedule().frequency() & 2);
                me.tuesday(me.courseSchedule().frequency() & 4);
                me.wednesday(me.courseSchedule().frequency() & 8);
                me.thursday(me.courseSchedule().frequency() & 16);
                me.friday(me.courseSchedule().frequency() & 32);
                me.saturday(me.courseSchedule().frequency() & 64);
            });
        }
    }
    function loadCourseType() {
        if (me.courseTypes().length == 0) {
            var obj = {};            
            obj.courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
            
            return Ajax.ajax(url + "/GetCourseTypes", {
                data: JSON.stringify({ 'courseScheduleId': (me.courseSchedule().courseScheduleId() || "0") }),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new CourseType(data[i]);
                    temp.push(ct);
                }
                me.courseTypes(temp);                
            });
        }
    }
    function loadCourse() {
        if (me.courseTypes().length != 0 && me.courseTypeId() && me.courseTypeId()!=0) {
            var obj = {};
            obj.courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
            obj.courseTypeId= me.courseTypeId() || 0;

            return Ajax.ajax(url + "/GetCourses", {
                data: JSON.stringify(obj),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new Course(data[i]);
                    temp.push(ct);
                }
                me.courses(temp);
            });
        }
    }
    function loadInstructors() {
        if (me.courseTypes().length != 0 && me.courseTypeId() && me.courseTypeId()!=0) {
            var courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
            var courseTypeId = me.courseTypeId() || 0;

            return Ajax.ajax(url + "/GetInstructors", {
                data: JSON.stringify({ 'courseScheduleId': courseScheduleId, 'courseTypeId': courseTypeId }),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new Instructor(data[i]);
                    for (var j = 0; j < me.courseSchedule().courseScheduleInstructors().length; j++) {
                        if (ct.instructorId() == me.courseSchedule().courseScheduleInstructors()[j].instructorId()) {
                            ct.selected(true);
                            break;
                        }
                    }
                    temp.push(ct);
                }
                me.instructors(temp);
            });
        }
    }
    function loadDivisions() {
        if (me.courseTypes().length != 0) {
            var courseScheduleId = me.courseSchedule().courseScheduleId() || 0;
            var courseTypeId = me.courseTypeId() || 0;

            return Ajax.ajax(url + "/GetDivisions", {
                data: JSON.stringify({ 'courseScheduleId': courseScheduleId}),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, waitBox).done(function (data) {
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new Division(data[i]);
                    temp.push(ct);
                }
                me.divisions(temp);
            });
        }
    }

}

var BaseSinglePage = function (url) {
    var me = this;
    me.url = url;
    me.loading = false;
    me.formShown = false;
    me.pageReady = ko.observable(false);
    
}

var EnrollmentsPage = function (baseUrl) {
    var me = this;
    var now = new Date();
    var lastWeek = new Date();
    var tomorrow = new Date();    
    var formShown = false;
    lastWeek.setDate(now.getDate() - 7);
    tomorrow.setDate(now.getDate() + 1) ;
    me.currentPage = ko.observable(1);
    me.totalPages = ko.observable(1);
    me.rowsPerPage = ko.observable(50);
    me.cboCourse=null;
    me.cboCourseInit=function(obj) {
        me.cboCourse = obj;
    }
    me.paggingText = ko.computed(function() {
        return me.currentPage() + " of " + me.totalPages();
    });
    me.firstPage = function () {
        if (me.currentPage() > 1) {
            me.currentPage(1);
            load(true);
        }
    };
    me.previousPage = function () {
        if (me.currentPage() > 1) {
            me.currentPage(me.currentPage() - 1);
            load(true);
        }
    };
    me.nextPage = function () {
        if (me.currentPage() < me.totalPages()) {
            me.currentPage(me.currentPage() + 1);
            load(true);
        }
    };
    me.lastPage = function () {
        if (me.currentPage() < me.totalPages()) {
            me.currentPage(me.totalPages());
            load(true);
    }
    };

    me.pageReady = ko.observable(false);
    me.dateFrom = ko.observable(Common.Date.formatDate(lastWeek));
    me.dateTo = ko.observable(Common.Date.formatDate(tomorrow));
    me.employeeBadge = ko.observable('');
    me.employee= ko.observable('');
    me.selectedCourseType = ko.observable();
    me.selectedCourse = ko.observable();
    me.courseTypes = ko.observableArray([]);
    me.courses = ko.observableArray([]);

    me.enrollments = ko.observableArray([]);
    me.selectedCourseType.subscribe(function () {        
        if (me.cboCourse) me.cboCourse.deSelectAll();
        me.search();
    });
    me.courseUpdated = function (e) {
        if (e.changedSinceOpened)
            me.search();
    }
    me.dateFrom.subscribe(function () { me.search(); });
    me.dateTo.subscribe(function () { me.search(); });
    me.employeeBadge.subscribe(function () { me.search(); });
    
    me.search = function () {
        me.currentPage(1);
        if (formShown) {
            load(true);
        }
            
    };
   
    me.providEmployeeSource = function (req, res) {
        EmployeeSearch.providEmployeeSource(req, res);
    }
    me.addNew = function () {
        navigateToNew();
    };

    me.edit = function (data) {
        var enrollmentId = data.enrollmentId();
        navigateToEdit(enrollmentId);
    };

    function navigateToMain() { location.hash = "main"; };
    function navigateToNew(id) {
        if (id == null) {
            location.hash = "new";
        } else {
            location.hash = "new/" + id;
        }
    };
    function navigateToEdit(id) { location.hash = "edit/" + id; };

    me.show = function (showLoaded) {
        var d1, d2;
        var wb = waitBox;
        wb.show();

        d1 = loadCourseType(false);
        $.when(d1).done(function() { 
            if (showLoaded ||  // if coursetypes loaded successfully, then attempt to load enrollments if it is needed.
                !formShown) { // in case it is the first time loading this form, we should load enrollments (happen if on new/edit page, user press refresh and then cancel the form)
                d2 = load(false);
            }
        });

        $.when(d1, d2).always(function() {
            wb.hide();
            formShown = true;
            me.pageReady(true);
        });
    }

    function load(handleWait) {
        var wb = handleWait ? waitBox : null;
        if (me.selectedCourseType()) {
            var obj = new Object();
            obj.DateFrom = me.dateFrom();
            obj.DateTo = me.dateTo();
            obj.Badge = me.employeeBadge();
            obj.CourseTypeId = me.selectedCourseType();
            obj.RowsPerPage = me.rowsPerPage();
            obj.SkipRows = me.rowsPerPage() * (me.currentPage() - 1);            
            obj.CourseIds = me.selectedCourse() || [];
            if (obj.CourseIds.length == me.courses().length)
                obj.CourseIds = [];
            return Ajax.ajax(baseUrl, {
                data: JSON.stringify(obj),
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, wb).done(function (data) {
                var c, ct;
                var temp = [];
                var totalRows = data.TotalRows;
                var totalpages=Math.ceil(totalRows / me.rowsPerPage());
                me.totalPages(totalpages);
                data.Enrollments = data.Enrollments || [];
                for (var i = 0; i < data.Enrollments.length; i++) {
                    ct = new Enrollment(data.Enrollments[i]);
                    temp.push(ct);
                }
                me.enrollments(temp);
                temp = [];
                

                for (var i = 0; i < data.Courses.length; i++) {
                    temp.push(data.Courses[i]);
                }
                me.courses(temp);
                
            });
        }
    }

    function loadCourseType(handleWait) {
        var wb = handleWait ? waitBox : null;
        if (me.courseTypes().length == 0) {
            return Ajax.ajax(baseUrl + "/CourseTypes", {
                data: '{}',
                dataType: 'json',
                type: 'POST',
                contentType: "application/json; charset=utf-8;"
            }, wb).done(function (data) {
                var temp = [];
                data = data || [];
                for (var i = 0; i < data.length; i++) {
                    var ct = new CourseType(data[i]);
                    temp.push(ct);
                }
                me.courseTypes(temp);
            });
        }
    }   
}

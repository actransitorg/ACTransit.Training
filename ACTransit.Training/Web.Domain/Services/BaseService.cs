using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Framework.Extensions;
using ACTransit.Framework.Logging;
using ACTransit.Training.Web.Business.Apprentice;
using ACTransit.Training.Web.Business.Employee;
using ACTransit.Training.Web.Business.Maintenance;
using ACTransit.Training.Web.Business.Training;
using ACTransit.Training.Web.Domain.Interfaces;
using ACTransit.Training.Web.Domain.Models;
using ACTransit.Training.Web.Domain.Infrastructure;

namespace ACTransit.Training.Web.Domain.Services
{
    public abstract class BaseService:IDisposable
    {
        protected const string TimeFormat = @"h:mm tt";
        protected const string ShortDateFormat = "MM/dd/yy";
        protected const string ShortDateTimeFormat = "MM/dd/yyyy HH:mm";

        private bool _disposed;
        protected readonly Logger Logger;

        protected BaseService()
        {
            Logger = new Logger("Domain." + GetType().Name);
            CurrentUserName = Common.CurrentUserName;            
        }

        protected string CurrentUserName { get; private set; }

        protected string AclUserName
        {
            get { return AclService.PrepareUser(CurrentUserName); }
        }

        private CourseScheduleService _courseScheduleService;
        protected CourseScheduleService CourseScheduleService {
            get
            {
                if (_courseScheduleService==null)
                    _courseScheduleService = new CourseScheduleService(CurrentUserName);
                return _courseScheduleService;
            }
        }

        private CourseEnrollmentService _courseEnrollmentService;
        protected CourseEnrollmentService CourseEnrollmentService
        {
            get
            {
                if (_courseEnrollmentService == null)
                    _courseEnrollmentService = new CourseEnrollmentService(CurrentUserName);
                return _courseEnrollmentService;
            }
        }

        private CourseService _courseService;
        protected CourseService CourseService
        {
            get
            {
                if (_courseService == null)
                    _courseService = new CourseService(CurrentUserName);
                return _courseService;
            }
        }

        private CourseTypeService _courseTypeService;
        protected CourseTypeService CourseTypeService
        {
            get
            {
                if (_courseTypeService == null)
                    _courseTypeService = new CourseTypeService(CurrentUserName);
                return _courseTypeService;
            }
        }

        private InstructorService _instructorService;
        protected InstructorService InstructorService
        {
            get
            {
                if (_instructorService == null)
                    _instructorService = new InstructorService(CurrentUserName);
                return _instructorService;
            }
        }

        private EmployeeAllService _employeeAllService;
        protected EmployeeAllService EmployeeAllService
        {
            get
            {
                if (_employeeAllService == null)
                    _employeeAllService = new EmployeeAllService();
                return _employeeAllService;
            }
        }

        private NonEmployeeService _nonEmployeeService;
        protected NonEmployeeService NonEmployeeService
        {
            get
            {
                if (_nonEmployeeService == null)
                    _nonEmployeeService = new NonEmployeeService(CurrentUserName);
                return _nonEmployeeService;
            }
        }

        private TopicService _topicService;
        protected TopicService TopicService
        {
            get
            {
                if (_topicService == null)
                    _topicService = new TopicService(CurrentUserName);
                return _topicService;
            }
        }

        private DivisionService _divisionService;
        protected DivisionService DivisionService
        {
            get
            {
                if (_divisionService == null)
                    _divisionService = new DivisionService(CurrentUserName);
                return _divisionService;
            }
        }

        private EnrollmentService _enrollmentService;
        protected EnrollmentService EnrollmentService
        {
            get
            {
                if (_enrollmentService == null)
                    _enrollmentService = new EnrollmentService(CurrentUserName);
                return _enrollmentService;
            }
        }

        private GradeService _gradeService;
        protected GradeService GradeService
        {
            get
            {
                if (_gradeService == null)
                    _gradeService = new GradeService(CurrentUserName);
                return _gradeService;
            }
        }

        private RouteService _routeService;
        protected RouteService RouteService
        {
            get
            {
                if (_routeService == null)
                    _routeService = new RouteService(CurrentUserName);
                return _routeService;
            }
        }

        private VehicleRegisterService _vehicleService;
        protected VehicleRegisterService VehicleService
        {
            get
            {
                if (_vehicleService == null)
                    _vehicleService = new VehicleRegisterService(CurrentUserName);
                return _vehicleService;
            }
        }

        private AclService _aclService;
        protected AclService AclService
        {
            get
            {
                if (_aclService == null)
                    _aclService = AclService.Create();
                return _aclService;
            }
        }

        private MenuService _menuService;
        protected MenuService MenuService
        {
            get
            {
                if (_menuService == null)
                    _menuService = new MenuService(CurrentUserName);
                return _menuService;
            }
        }

        #region Apprentice

        private ParticipantProgramLevelGroupService _participantProgramLevelGroupService;
        protected ParticipantProgramLevelGroupService ParticipantProgramLevelGroupService
        {
            get
            {
                return _participantProgramLevelGroupService ?? (_participantProgramLevelGroupService = new ParticipantProgramLevelGroupService(CurrentUserName));
            }
        }

        private DailyPerformanceProgramLevelGroupService _dailyPerformanceProgramLevelGroupService;
        protected DailyPerformanceProgramLevelGroupService DailyPerformanceProgramLevelGroupService
        {
            get
            {
                return _dailyPerformanceProgramLevelGroupService ?? (_dailyPerformanceProgramLevelGroupService = new DailyPerformanceProgramLevelGroupService(CurrentUserName));
            }
        }

        private ParticipantService _participantService;
        protected ParticipantService ParticipantService
        {
            get
            {
                return _participantService ?? (_participantService = new ParticipantService(CurrentUserName));
            }
        }

        private ParticipantStatusService _participantStatusService;
        protected ParticipantStatusService ParticipantStatusService
        {
            get
            {
                return _participantStatusService ?? (_participantStatusService = new ParticipantStatusService(CurrentUserName));
            }
        }

        private ParticipantWorkService _participantWorkService;
        protected ParticipantWorkService ParticipantWorkService
        {
            get
            {
                return _participantWorkService ?? (_participantWorkService = new ParticipantWorkService(CurrentUserName));
            }
        }

        private ParticipantWorkSeedService _participantWorkSeedService;
        protected ParticipantWorkSeedService ParticipantWorkSeedService
        {
            get
            {
                return _participantWorkSeedService ?? (_participantWorkSeedService = new ParticipantWorkSeedService(CurrentUserName));
            }
        }

        private ParticipantWorkDetailService _participantWorkDetailService;
        protected ParticipantWorkDetailService ParticipantWorkDetailService
        {
            get
            {
                return _participantWorkDetailService ?? (_participantWorkDetailService = new ParticipantWorkDetailService(CurrentUserName));
            }
        }

        private ProgramLevelGroupService _programLevelGroupService;
        protected ProgramLevelGroupService ProgramLevelGroupService
        {
            get
            {
                return _programLevelGroupService ?? (_programLevelGroupService = new ProgramLevelGroupService(CurrentUserName));
            }
        }

        private ProgramLevelService _programLevelService;
        protected ProgramLevelService ProgramLevelService
        {
            get
            {
                return _programLevelService ?? (_programLevelService = new ProgramLevelService(CurrentUserName));
            }
        }

        private ProgramService _programService;
        protected ProgramService ProgramService
        {
            get
            {
                return _programService ?? (_programService = new ProgramService(CurrentUserName));
            }
        }

        private ProgressDayService _progressDayService;
        protected ProgressDayService ProgressDayService
        {
            get
            {
                return _progressDayService ?? (_progressDayService = new ProgressDayService(CurrentUserName));
            }
        }

        private ProgressService _progressService;
        protected ProgressService ProgressService
        {
            get
            {
                return _progressService ?? (_progressService = new ProgressService(CurrentUserName));
            }
        }

        private RatingAreaService _ratingAreaService;
        protected RatingAreaService RatingAreaService
        {
            get
            {
                return _ratingAreaService ?? (_ratingAreaService = new RatingAreaService(CurrentUserName));
            }
        }

        private RatingCellScoreService _ratingCellScoreService;
        protected RatingCellScoreService RatingCellScoreService
        {
            get
            {
                return _ratingCellScoreService ?? (_ratingCellScoreService = new RatingCellScoreService(CurrentUserName));
            }
        }

        private RatingCellService _ratingCellService;
        protected RatingCellService RatingCellService
        {
            get
            {
                return _ratingCellService ?? (_ratingCellService = new RatingCellService(CurrentUserName));
            }
        }

        private ProgressRatingCellScoreService _progressRatingCellScoreService;
        protected ProgressRatingCellScoreService ProgressRatingCellScoreService
        {
            get
            {
                return _progressRatingCellScoreService ?? (_progressRatingCellScoreService = new ProgressRatingCellScoreService(CurrentUserName));
            }
        }

        private SyncService _SyncService;
        protected SyncService SyncService
        {
            get
            {
                return _SyncService ?? (_SyncService = new SyncService(CurrentUserName));
            }
        }

        private ActionItemsService _ActionItemsService;
        protected ActionItemsService ActionItemsService
        {
            get
            {
                return _ActionItemsService ?? (_ActionItemsService = new ActionItemsService(CurrentUserName));
            }
        }


        #endregion


        protected string Trim(string str)
        {
            return Common.Trim(str);
        }
        protected string Trim(string str, string defaultValue)
        {
            return Common.Trim(str, defaultValue);
        }

        protected void LogDebug(string methodName, string message)
        {
            Logger.WriteDebug(message);
        }
        protected void LogInfo(string methodName, string message)
        {
            Logger.Write(message);
        }
        protected void LogError(string methodName, string message)
        {
            Logger.WriteError(message);
        }
        protected void LogFatal(string methodName, string message)
        {
            Logger.WriteFatal(message);
        }


        public virtual List<CourseTypeViewModel> GetCourseTypes(Expression<Func<CourseType, bool>> filter=null)
        {
            var result = CourseTypeService.Get(filter).OrderBy(m=>m.SortOrder).ThenBy(m=>m.Name).ToList().Where(m => AclService.HasDynamicAccess(m, CurrentUserName)).Select(Converter.ToViewModel).ToList();
            
            return result;
        }

        public virtual List<TopicViewModel> GetTopics(long courseTypeId, long? courseId)
        {
            List<TopicViewModel> topics;

            if (courseId.HasValue)
            {
                topics =
                    TopicService.GetTopicsInCourse(courseId.Value)
                        .Where(m => m.CourseTypeId == courseTypeId)
                        .Select(Converter.ToViewModel)
                        .ToList();
            }
            else
                topics = TopicService.GetTopicsByCourseType(courseTypeId).ToList().Select(Converter.ToViewModel).ToList();

            return topics;
        }

        public virtual List<EmployeeTrainee> GetEmployees(string badge = "", string lastName = "", string firstName = "", string name = "", string ntLogin = "", string jobTitle = "")
        {
            var employees = EmployeeAllService.GetEmployees(badge, name, firstName, lastName, null, null, null, null, null, jobTitle).OrderBy(e => e.LastName).Select(Converter.ToViewModel).ToList();
            if (!string.IsNullOrWhiteSpace(ntLogin))
            {
                var employees1 = EmployeeAllService.GetEmployees(ntLogin: ntLogin).OrderBy(e => e.LastName).Select(Converter.ToViewModel).ToList();

                var employeesNotInOne = employees1.Except(employees).ToList();
                employees.AddRange(employeesNotInOne);
                employees = employees.OrderBy(m => m.LastName).ToList();
            }
            return employees;
        }

        protected virtual object PrepareModel<T>(T model) where T:class, new()
        {
            if (model==null) 
                model=new T();
            if (model is ICourseTypeRequired)
            {
                var courseTypeModel = (ICourseTypeRequired) model;
                var selectedCouseTypeIds = new string[0];
                if (courseTypeModel.CourseTypes != null)
                    selectedCouseTypeIds = courseTypeModel.CourseTypes.Where(m => m.Selected).Select(m => m.Value).ToArray();
                var courseTypes = new SelectList(GetCourseTypes().Select(m => new { m.Name, m.CourseTypeId }).ToList(), "CourseTypeId", "Name").ToList();
                if (selectedCouseTypeIds.Length == 0)
                    selectedCouseTypeIds = courseTypes.Select(m => m.Value).ToArray();

                foreach (var courseType in courseTypes)
                    courseType.Selected = selectedCouseTypeIds.Contains(courseType.Value);

                courseTypeModel.CourseTypes = courseTypes;
                return courseTypeModel;                
            }
            return model;
        }

        protected void AddIfNotExistCourseType(long courseTypeId, List<CourseTypeViewModel> courseTypes)
        {
            if (courseTypeId != 0)
            {
                if (!courseTypes.Any(m => m.CourseTypeId == courseTypeId))
                {
                    var coursetype = CourseTypeService.GetById(courseTypeId);
                    if (coursetype == null)
                        throw new Exception("Coursetype " + courseTypeId + " not found.");
                    courseTypes.Add(Converter.ToViewModel(coursetype));
                }
            }
        }
        protected void AddIfNotExistCourseType(long courseTypeId, List<SelectListItem> courseTypes, bool select = false)
        {
            if (courseTypeId != 0)
            {
                if (!courseTypes.Any(m => m.Value == courseTypeId.ToString()))
                {
                    var coursetype = CourseTypeService.GetById(courseTypeId);
                    if (coursetype == null)
                        throw new Exception("Coursetype " + courseTypeId + " not found.");
                    courseTypes.Add(new SelectListItem { Text = coursetype.Name, Value = coursetype.CourseTypeId.ToString(), Selected = select });
                }                
            }
        }

        protected void AddIfNotExistTopic(long topicId, List<TopicViewModel> topics)
        {
            if (topicId != 0)
            {
                if (!topics.Any(m => m.TopicId == topicId))
                {
                    var topic = TopicService.GetById(topicId);
                    if (topic == null)
                        throw new Exception("Topic " + topicId + " not found.");
                    topics.Add(Converter.ToViewModel(topic));
                }
            }
        }

        protected void AddIfNotExistTopic(long topicId, List<SelectListItem> topics, bool select = false)
        {
            if (topicId != 0)
            {
                if (!topics.Any(m => m.Value == topicId.ToString()))
                {
                    var coursetype = TopicService.GetById(topicId);
                    if (coursetype == null)
                        throw new Exception("Topic " + topicId + " not found.");
                    topics.Add(new SelectListItem { Text = coursetype.Name, Value = coursetype.CourseTypeId.ToString(), Selected = select });
                }
            }
        }

        protected void AddIfNotExistCourse(long courseId, List<CourseViewModel> courses)
        {
            if (courseId != 0)
            {
                if (!courses.Any(m => m.CourseId == courseId))
                {
                    var course = CourseService.GetById(courseId);
                    if (course == null)
                        throw new Exception("Course " + courseId + " not found.");
                    courses.Add(Converter.ToViewModel(course));
                }
            }
        }

        protected void AddIfNotExistCourseSchedule(long courseScheduleId, List<CourseScheduleViewModel> courseSchedules)
        {
            if (courseScheduleId != 0)
            {
                if (!courseSchedules.Any(m => m.CourseScheduleId == courseScheduleId))
                {
                    var course = CourseScheduleService.GetById(courseScheduleId);
                    if (course == null)
                        throw new Exception("Course " + courseScheduleId + " not found.");
                    courseSchedules.Add(Converter.ToViewModel(course));
                }
            }
        }

        protected IEnumerable<Division> GetDivisions()
        {
            return DivisionService.Get(null).ToList();
        }

        protected DateTime ConvertToDate(string s)
        {
            const string methodName = "ConvertToDate";
            try
            {
                var now = DateTime.Now;
                var parts = s.Split('/');
                return new DateTime(parts[2].ToInt().GetValueOrDefault(now.Year),
                    parts[0].ToInt().GetValueOrDefault(now.Month),
                    parts[1].ToInt().GetValueOrDefault(now.Day), 0, 0, 0);
            }
            catch (Exception ex)
            {
                LogError(methodName, "Cound not convert " + s + " to datetime.");
                LogError(methodName, ex.Message);
                throw;
            }            
        }
        protected bool TryConvertToDate(string s, out DateTime result)
        {
            const string methodName = "TryConvertToDate";
            try
            {
                result = ConvertToDate(s);
                return true;
            }
            catch (Exception ex)
            {
                LogError(methodName, "Cound not convert " + s + " to datetime." + ex.Message);
                result = DateTime.MinValue;
            }
            return false;
        }

        public void Dispose()
        {
            LogDebug("Dispose","Called.");
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_courseScheduleService!=null){
                    _courseScheduleService.Dispose();
                    _courseScheduleService = null;
                }
                if (_courseEnrollmentService != null)
                {
                    _courseEnrollmentService.Dispose();
                    _courseEnrollmentService = null;
                }
                if (_courseService != null)
                {
                    _courseService.Dispose();
                    _courseService = null;
                }
                if (_courseTypeService != null)
                {
                    _courseTypeService.Dispose();
                    _courseTypeService = null;
                }
                if (_instructorService != null)
                {
                    _instructorService.Dispose();
                    _instructorService = null;
                }
                if (_nonEmployeeService != null)
                {
                    _nonEmployeeService.Dispose();
                    _nonEmployeeService = null;
                }
                if (_divisionService != null)
                {
                    _divisionService.Dispose();
                    _divisionService = null;
                }
                if (_enrollmentService != null)
                {
                    _enrollmentService.Dispose();
                    _enrollmentService = null;
                }
                if (_gradeService != null)
                {
                    _gradeService.Dispose();
                    _gradeService = null;
                }
                if (_routeService != null)
                {
                    _routeService.Dispose();
                    _routeService = null;
                }
                if (_vehicleService != null)
                {
                    _vehicleService.Dispose();
                    _vehicleService = null;
                }

                if (_menuService != null)
                {
                    _menuService.Dispose();
                    _menuService = null;
                }
            }

            _disposed = true;
        }
    }
}

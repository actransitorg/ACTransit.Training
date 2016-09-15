﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACTransit.DataAccess.Training
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ACTransit.Entities.Training;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TrainingEntities : DbContext
    {
        public TrainingEntities()
            : base("name=TrainingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DailyPerformance> DailyPerformances { get; set; }
        public virtual DbSet<DailyPerformanceProgramLevelGroup> DailyPerformanceProgramLevelGroups { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<ParticipantProgramLevelGroup> ParticipantProgramLevelGroups { get; set; }
        public virtual DbSet<ParticipantStatus> ParticipantStatus { get; set; }
        public virtual DbSet<ParticipantWork> ParticipantWorks { get; set; }
        public virtual DbSet<ParticipantWorkSeed> ParticipantWorkSeeds { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramLevel> ProgramLevels { get; set; }
        public virtual DbSet<ProgramLevelGroup> ProgramLevelGroups { get; set; }
        public virtual DbSet<Progress> Progresses { get; set; }
        public virtual DbSet<ProgressDay> ProgressDays { get; set; }
        public virtual DbSet<ProgressRatingCellScore> ProgressRatingCellScores { get; set; }
        public virtual DbSet<RatingArea> RatingAreas { get; set; }
        public virtual DbSet<RatingCategory> RatingCategories { get; set; }
        public virtual DbSet<RatingCell> RatingCells { get; set; }
        public virtual DbSet<RatingCellScore> RatingCellScores { get; set; }
        public virtual DbSet<RatingItem> RatingItems { get; set; }
        public virtual DbSet<WorkCategory> WorkCategories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public virtual DbSet<CourseSchedule> CourseSchedules { get; set; }
        public virtual DbSet<CourseScheduleInstructor> CourseScheduleInstructors { get; set; }
        public virtual DbSet<CourseTopic> CourseTopics { get; set; }
        public virtual DbSet<CourseType> CourseTypes { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<EnrollmentInstructor> EnrollmentInstructors { get; set; }
        public virtual DbSet<EnrollmentTopic> EnrollmentTopics { get; set; }
        public virtual DbSet<EnrollmentVehicle> EnrollmentVehicles { get; set; }
        public virtual DbSet<EnrollmentVehicleRoute> EnrollmentVehicleRoutes { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<NonEmployee> NonEmployees { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<TopicType> TopicTypes { get; set; }
        public virtual DbSet<ParticipantWorkDetail> ParticipantWorkDetails { get; set; }
        public virtual DbSet<WorksheetParticipantDetail> WorksheetParticipantDetails { get; set; }
        public virtual DbSet<WorksheetScoreTemplate> WorksheetScoreTemplates { get; set; }
        public virtual DbSet<WorksheetTemplate> WorksheetTemplates { get; set; }
        public virtual DbSet<ComponentTopic> ComponentTopics { get; set; }
        public virtual DbSet<CourseRoster> CourseRosters { get; set; }
        public virtual DbSet<CourseRosterBase> CourseRosterBases { get; set; }
        public virtual DbSet<CourseScheduleDetail> CourseScheduleDetails { get; set; }
        public virtual DbSet<EnrollmentList> EnrollmentLists { get; set; }
        public virtual DbSet<EnrollmentTopicTypeOneBase> EnrollmentTopicTypeOneBases { get; set; }
        public virtual DbSet<EnrollmentTopicTypeOtherBase> EnrollmentTopicTypeOtherBases { get; set; }
        public virtual DbSet<EnrollmentTraineeDetail> EnrollmentTraineeDetails { get; set; }
        public virtual DbSet<EnrollmentVehicleTime> EnrollmentVehicleTimes { get; set; }
        public virtual DbSet<EquipmentGroupVehicle> EquipmentGroupVehicles { get; set; }
        public virtual DbSet<InstructorEnrollmentList> InstructorEnrollmentLists { get; set; }
        public virtual DbSet<InstructorList> InstructorLists { get; set; }
        public virtual DbSet<RouteList> RouteLists { get; set; }
    
        [DbFunction("TrainingEntities", "CharToTable")]
        public virtual IQueryable<CharToTable_Result> CharToTable(string list, string delimiter)
        {
            var listParameter = list != null ?
                new ObjectParameter("list", list) :
                new ObjectParameter("list", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("delimiter", delimiter) :
                new ObjectParameter("delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<CharToTable_Result>("[TrainingEntities].[CharToTable](@list, @delimiter)", listParameter, delimiterParameter);
        }
    
        public virtual ObjectResult<GetActionItems_Result> GetActionItems(string badge)
        {
            var badgeParameter = badge != null ?
                new ObjectParameter("Badge", badge) :
                new ObjectParameter("Badge", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetActionItems_Result>("GetActionItems", badgeParameter);
        }
    
        public virtual ObjectResult<GetApprenticeDetailUnitTest_Result> GetApprenticeDetailUnitTest(string badge)
        {
            var badgeParameter = badge != null ?
                new ObjectParameter("Badge", badge) :
                new ObjectParameter("Badge", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetApprenticeDetailUnitTest_Result>("GetApprenticeDetailUnitTest", badgeParameter);
        }
    
        public virtual int GetParticipantWork(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, string badge, Nullable<bool> summarize, Nullable<bool> isActive, Nullable<long> programId)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var badgeParameter = badge != null ?
                new ObjectParameter("Badge", badge) :
                new ObjectParameter("Badge", typeof(string));
    
            var summarizeParameter = summarize.HasValue ?
                new ObjectParameter("Summarize", summarize) :
                new ObjectParameter("Summarize", typeof(bool));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var programIdParameter = programId.HasValue ?
                new ObjectParameter("ProgramId", programId) :
                new ObjectParameter("ProgramId", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetParticipantWork", startDateParameter, endDateParameter, badgeParameter, summarizeParameter, isActiveParameter, programIdParameter);
        }
    
        public virtual ObjectResult<SyncParticipantWork_Result> SyncParticipantWork()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SyncParticipantWork_Result>("SyncParticipantWork");
        }
    
        public virtual ObjectResult<SyncWithEnterprise_Result> SyncWithEnterprise(Nullable<System.DateTime> todayAsDate)
        {
            var todayAsDateParameter = todayAsDate.HasValue ?
                new ObjectParameter("TodayAsDate", todayAsDate) :
                new ObjectParameter("TodayAsDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SyncWithEnterprise_Result>("SyncWithEnterprise", todayAsDateParameter);
        }
    
        public virtual ObjectResult<GetAttendanceList_Result> GetAttendanceList(string courseTypeId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, string trainee, string attended, string courseId, string division)
        {
            var courseTypeIdParameter = courseTypeId != null ?
                new ObjectParameter("CourseTypeId", courseTypeId) :
                new ObjectParameter("CourseTypeId", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var traineeParameter = trainee != null ?
                new ObjectParameter("Trainee", trainee) :
                new ObjectParameter("Trainee", typeof(string));
    
            var attendedParameter = attended != null ?
                new ObjectParameter("Attended", attended) :
                new ObjectParameter("Attended", typeof(string));
    
            var courseIdParameter = courseId != null ?
                new ObjectParameter("CourseId", courseId) :
                new ObjectParameter("CourseId", typeof(string));
    
            var divisionParameter = division != null ?
                new ObjectParameter("Division", division) :
                new ObjectParameter("Division", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAttendanceList_Result>("GetAttendanceList", courseTypeIdParameter, startDateParameter, endDateParameter, traineeParameter, attendedParameter, courseIdParameter, divisionParameter);
        }
    
        public virtual int GetEnrollmentTraineeDetail(string courseTypeId, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<bool> qualifiedTraining, string equipmentGroupNum, string routeAlpha, string courseId, string trainee, string instructorId, string topicId, Nullable<bool> showTopic, string div)
        {
            var courseTypeIdParameter = courseTypeId != null ?
                new ObjectParameter("CourseTypeId", courseTypeId) :
                new ObjectParameter("CourseTypeId", typeof(string));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var qualifiedTrainingParameter = qualifiedTraining.HasValue ?
                new ObjectParameter("QualifiedTraining", qualifiedTraining) :
                new ObjectParameter("QualifiedTraining", typeof(bool));
    
            var equipmentGroupNumParameter = equipmentGroupNum != null ?
                new ObjectParameter("EquipmentGroupNum", equipmentGroupNum) :
                new ObjectParameter("EquipmentGroupNum", typeof(string));
    
            var routeAlphaParameter = routeAlpha != null ?
                new ObjectParameter("RouteAlpha", routeAlpha) :
                new ObjectParameter("RouteAlpha", typeof(string));
    
            var courseIdParameter = courseId != null ?
                new ObjectParameter("CourseId", courseId) :
                new ObjectParameter("CourseId", typeof(string));
    
            var traineeParameter = trainee != null ?
                new ObjectParameter("Trainee", trainee) :
                new ObjectParameter("Trainee", typeof(string));
    
            var instructorIdParameter = instructorId != null ?
                new ObjectParameter("InstructorId", instructorId) :
                new ObjectParameter("InstructorId", typeof(string));
    
            var topicIdParameter = topicId != null ?
                new ObjectParameter("TopicId", topicId) :
                new ObjectParameter("TopicId", typeof(string));
    
            var showTopicParameter = showTopic.HasValue ?
                new ObjectParameter("ShowTopic", showTopic) :
                new ObjectParameter("ShowTopic", typeof(bool));
    
            var divParameter = div != null ?
                new ObjectParameter("Div", div) :
                new ObjectParameter("Div", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetEnrollmentTraineeDetail", courseTypeIdParameter, startDateParameter, endDateParameter, qualifiedTrainingParameter, equipmentGroupNumParameter, routeAlphaParameter, courseIdParameter, traineeParameter, instructorIdParameter, topicIdParameter, showTopicParameter, divParameter);
        }
    }
}

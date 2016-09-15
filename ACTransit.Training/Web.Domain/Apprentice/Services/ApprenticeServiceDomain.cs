 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Apprentice.Models;
using ACTransit.Training.Web.Domain.Services;

namespace ACTransit.Training.Web.Domain.Apprentice.Services
{
    public class ApprenticeServiceDomain : BaseService
    {
        #region ProgramsViewModel

        public ProgramsViewModel GetProgramsViewModel(bool OnlyActive, DateTime? StartDate, DateTime? EndDate)
        {
            return GetProgramsViewModel(ProgramsViewModel.Create(OnlyActive, StartDate, EndDate));
        }

        public ProgramsViewModel GetProgramsViewModel(ProgramsViewModel model)
        {
            if (model == null)
                model = new ProgramsViewModel();
            model = (ProgramsViewModel)base.PrepareModel(model);
            if (model.Items == null)
                model.Items = ProgramService.Get(p => p.IsActive, m => m.Participants).ToList();
            return model;
        }

        #endregion

        #region ProgramViewModel

        public ProgramViewModel GetProgramViewModel(int id, bool OnlyActive, DateTime? StartDate, DateTime? EndDate)
        {
            return GetProgramViewModel(ProgramViewModel.Create(id, OnlyActive, StartDate, EndDate));
        }

        public ProgramViewModel GetProgramViewModel(ProgramViewModel model)
        {
            if (model == null)
                model = new ProgramViewModel();
            model = (ProgramViewModel)base.PrepareModel(model);
            if (model.Items == null && model.Program.ProgramId > 0)
            {
                model.Items = ParticipantService.GetProgramParticipants(model.Program.ProgramId,
                    m => m.ParticipantStatus,
                    m => m.ParticipantProgramLevelGroups,
                    m => m.Program).ToList();
                if (model.Items == null)
                    return model;

                var employees = new List<string>(model.Items.Select(p => p.Badge).ToArray()).ToArray();
                model.Employees = EmployeeAllService.GetEmployeesByBadges(employees).ToList();

                model.Items = model.OnlyActive
                    ? model.Items.Where(i => i.ParticipantStatusId == 1).OrderBy(i => model.EmployeeLastName(i.Badge)).ToList()
                    : model.Items.OrderBy(m => m.ParticipantStatus.Name).ThenBy(i => model.EmployeeLastName(i.Badge)).ToList();

                model.ProgramLevelGroups = ProgramLevelGroupService.GetProgramLevelGroups(model.Program.ProgramId, m => m.ProgramLevels, m => m.Program.WorkCategories).ToList();

                if (model.ItemLevel.Count != model.Items.Count)
                    foreach (var item in model.Items)
                        model.ItemLevel.Add(item, item.ProgramLevel);

                var employee = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
                if (employee != null)
                {
                    model.ParticipantsActionItems = new Dictionary<int, string>();
                    var actionItems = ActionItemsService.GetActionItems(employee.Badge);
                    foreach (var actionItem in actionItems)
                        model.ParticipantsActionItems.Add(actionItem.ParticipantId, actionItem.ActionItems);
                }
            }

            return model;
        }

        #endregion

        #region ParticipantProgressViewModel

        public ParticipantProgressViewModel GetParticipantProgressViewModel(int id, DateTime? StartDate, DateTime? EndDate)
        {
            return GetParticipantProgressViewModel(ParticipantProgressViewModel.Create(id, StartDate, EndDate));
        }

        public ParticipantProgressViewModel GetParticipantProgressViewModel(ParticipantProgressViewModel model)
        {
            if (model == null)
                model = ParticipantProgressViewModel.Create();
            model = (ParticipantProgressViewModel)base.PrepareModel(model);

            if (model.Participant != null && model.Participant.ParticipantId > 0 && string.IsNullOrEmpty(model.Participant.Badge))
                model.Participant = ParticipantService.Get(p => p.ParticipantId == model.Participant.ParticipantId, m => m.ParticipantStatus).FirstOrDefault();
            if (model.Items == null)
            {
                var startDate = model.StartDate.GetValueOrDefault();
                var endDate = model.EndDate.GetValueOrDefault();
                var participantProgramLevelGroups = ParticipantProgramLevelGroupService.Get(p => p.ParticipantId == model.Participant.ParticipantId).OrderBy(p => p.BeginEffDate).ToList();
                model.Items = new List<Progress>();
                if (participantProgramLevelGroups.Count > 0)
                    foreach (var participantProgramLevelGroup in participantProgramLevelGroups)
                        if (model.StartDate.HasValue || model.EndDate.HasValue)
                            model.Items.AddRange(ProgressService.GetProgresses(
                                participantProgramLevelGroup.ParticipantProgramLevelGroupId,
                                startDate,
                                endDate,
                            m => m.ProgressDays).OrderByDescending(p => p.StartDate));
                        else
                            model.Items.AddRange(ProgressService.GetProgresses(
                                participantProgramLevelGroup.ParticipantProgramLevelGroupId,
                            m => m.ProgressDays).OrderByDescending(p => p.StartDate));
                ParticipantWorkService.DisableProxy();
                model.ParticipantWork = ParticipantWorkService.GetParticipantWorks(model.Participant.ParticipantId).ToList();
                model.HasWorkOrders = new Dictionary<Progress, bool>();
                model.IsHistorical = new Dictionary<Progress, bool>();
                foreach (var item in model.Items)
                {
                    // make sure apprentice has left comments and supervisor signed off on day (ignore days with "Day off" like comments)
                    var dayEvaluationDone = item.ProgressDays.All(pd => (pd.SupervisorBadge != null && !pd.IsDayOff) || (pd.SupervisorBadge == null && pd.IsDayOff));
                    model.IsDailyEvaluationDone.Add(item, dayEvaluationDone);
                    item.ParticipantWork = model.ParticipantWork.Where(pw => pw.StartDate == item.StartDate).ToList();
                    var hasWorkOrders = item.ParticipantWork.Sum(pw => pw.WorkHour) > 0;
                    model.HasWorkOrders.Add(item, hasWorkOrders);
                    model.IsHistorical.Add(item, item.Historical);
                }
            }

            return model;
        }

        #endregion

        #region ParticipantProgressDaysViewModel

        public ParticipantProgressDaysViewModel GetParticipantProgressDaysViewModel(int id)
        {
            return GetParticipantProgressDaysViewModel(ParticipantProgressDaysViewModel.Create(id));
        }

        public ParticipantProgressDaysViewModel GetParticipantProgressDaysViewModel(ParticipantProgressDaysViewModel model)
        {
            if (model == null)
                model = ParticipantProgressDaysViewModel.Create();
            model = (ParticipantProgressDaysViewModel)base.PrepareModel(model);

            if (model.Progress != null && model.Progress.ProgressId > 0 && model.Progress.StartDate == DateTime.MinValue)
                model.Progress = ProgressService.Get(p => p.ProgressId == model.Progress.ProgressId, 
                    m => m.ParticipantProgramLevelGroup, 
                    m => m.ParticipantProgramLevelGroup.Participant,
                    m => m.ParticipantProgramLevelGroup.ProgramLevelGroup).FirstOrDefault();

            if (model.Progress == null)
                return model;

            if (model.ParticipantProgramLevelGroup == null && model.Progress.ParticipantProgramLevelGroup != null)
                model.ParticipantProgramLevelGroup = model.Progress.ParticipantProgramLevelGroup;

            if (model.Participant == null && model.ParticipantProgramLevelGroup != null && model.ParticipantProgramLevelGroup.Participant != null)
                model.Participant = model.ParticipantProgramLevelGroup.Participant;

            if (model.ProgramLevelGroup == null && model.ParticipantProgramLevelGroup != null && model.ParticipantProgramLevelGroup.ProgramLevelGroup != null)
                model.ProgramLevelGroup = model.ParticipantProgramLevelGroup.ProgramLevelGroup;

            if (model.Items == null)
                model.Items = ProgressDayService.Get(p => p.ProgressId == model.Progress.ProgressId).ToList();

            return model;
        }

        #endregion

        #region ParticipantViewModel

        public ParticipantViewModel GetParticipantViewModel(int id)
        {
            return GetParticipantViewModel(ParticipantViewModel.Create(id));
        }

        public ParticipantViewModel GetParticipantViewModel(ParticipantViewModel model)
        {
            if (model == null)
                model = ParticipantViewModel.Create();
            model = (ParticipantViewModel)base.PrepareModel(model);

            if (model.Participant != null && model.Participant.ParticipantId > 0 && string.IsNullOrEmpty(model.Participant.Badge))
            {
                model.Participant = ParticipantService.Get(p => p.ParticipantId == model.Participant.ParticipantId,
                    m => m.ParticipantStatus,
                    m => m.ProgramLevel,
                    m => m.ParticipantProgramLevelGroups,
                    m => m.Program,
                    m => m.Program.WorkCategories).FirstOrDefault();

                if (model.Participant != null)
                    model.ProgramLevel = model.Participant.ProgramLevel;

                model.Program = model.Participant.Program;
                model.WorkCategory = model.Program.WorkCategories.ToList();
                model.NewProgramLevel = model.ProgramLevel;
            }

            if (model.WorkCategoryCompleted == null)
            {
                ParticipantWorkDetailService.DisableProxy();
                var participantWorkDetail = ParticipantWorkDetailService.GetParticipantWorkDetails(model.Participant.ParticipantId).ToList();
                model.WorkCategoryCompleted = new List<WorkCategoryHour>();
                model.WorkCategoryRemaining = new List<WorkCategoryHour>();
                foreach (var workCategory in model.WorkCategory)
                {
                    var completedItem = new WorkCategoryHour
                    {
                        WorkCategory = workCategory,
                        Hours = Math.Min(
                            participantWorkDetail.Where(p => p.WorkCategoryId == workCategory.WorkCategoryId && p.WorkHour != new decimal(0.0)).Sum(i => i.WorkHour), 
                            workCategory.HourGoal)
                    };
                    model.WorkCategoryCompleted.Add(completedItem);
                    var remainingItem = new WorkCategoryHour
                    {
                        WorkCategory = workCategory,
                        Hours = Math.Max(
                            workCategory.HourGoal - completedItem.Hours, 
                            0)
                    };
                    model.WorkCategoryRemaining.Add(remainingItem);
                }
            }

            if (model.Participant != null && model.EmployeeDetails == null)
                model.EmployeeDetails = EmployeeAllService.GetEmployees(badge: model.Participant.Badge).FirstOrDefault();

            if (model.ParticipantStatuses == null || !model.ParticipantStatuses.Any())
                model.ParticipantStatuses = ParticipantStatusService.GetParticipantStatuses(null)
                    .Select(
                        ps => new SelectListItem
                        {
                            Text = ps.Name,
                            Value = ps.ParticipantStatusId.ToString()
                        }).ToArray();

            var completedProgramLevels = new List<int>();
            if (model.Participant != null && model.ParticipantProgramLevelGroup == null)
            {
                model.ParticipantProgramLevelGroups = ParticipantProgramLevelGroupService.GetParticipantProgramLevelGroups(model.Participant.ParticipantId,
                    m => m.ProgramLevelGroup,
                    m => m.ProgramLevelGroup.ProgramLevels).ToList();
                model.ParticipantProgramLevelGroup = model.ParticipantProgramLevelGroups.OrderBy(p=>p.BeginEffDate).LastOrDefault();
                if (model.ParticipantProgramLevelGroup != null)
                    model.ProgramLevelGroup = model.ParticipantProgramLevelGroup.ProgramLevelGroup;
                completedProgramLevels.AddRange(
                    from participantProgramLevelGroup in model.ParticipantProgramLevelGroups.Where(plg => plg.ProgramLevelGroupId != model.ProgramLevelGroup.ProgramLevelGroupId) 
                    from programLevel in participantProgramLevelGroup.ProgramLevelGroup.ProgramLevels 
                    select programLevel.ProgramLevelId);
            }

            if (model.ProgramLevel == null && model.ParticipantProgramLevelGroup != null)
            {
                var progress = ProgressService.GetLastProgress(model.ParticipantProgramLevelGroup.ParticipantProgramLevelGroupId);
                var progressDay = ProgressDayService.GetLastProgressDay(progress.ProgressId, m=>m.ProgramLevel);
                model.ProgramLevel = progressDay.ProgramLevel;
            }

            if (model.Participant != null && model.ProgramLevels == null)
            {
                var programLevels =
                    ProgramLevelService.GetProgramLevels(m => m.ProgramLevelGroup)
                        .Where(pl => !completedProgramLevels.Contains(pl.ProgramLevelId))
                        .OrderBy(p => p.Level)
                        .ToList();

                model.ProgramLevels =
                    programLevels.Select(
                        pl => new SelectListItem
                        {
                            Text = pl.Level.ToString(),
                            Value = pl.ProgramLevelId.ToString()

                        }).ToArray();
            }

            model.AllowChangeApprenticeLevel = AclService.HasAccess("AllowChangeApprenticeLevel", AclUserName);

            return model;
        }

        public void SaveParticipantViewModel(ParticipantViewModel model)
        {
            if (model.NewProgramLevel != null)
                model.Participant.ProgramLevelId = model.NewProgramLevel.ProgramLevelId;
            if (model.Participant.ProgramLevelId == null && model.ProgramLevel.ProgramLevelId > 0)
                model.Participant.ProgramLevelId = model.ProgramLevel.ProgramLevelId;
            ParticipantService.Update(model.Participant);

            // assess change to ProgramLevelGroup
            var allProgramLevelGroups = ProgramLevelGroupService.GetProgramLevelGroups(model.Participant.ProgramId, m => m.ProgramLevels).ToList();
            var foundProgramLevelGroup = (
                from programLevelGroup in allProgramLevelGroups
                from programLevel in programLevelGroup.ProgramLevels
                where programLevel.ProgramLevelId == model.Participant.ProgramLevelId
                select programLevelGroup).Distinct().First();
            var participantProgramLevelGroup = ParticipantProgramLevelGroupService.GetCurrentParticipantProgramLevelGroup(model.Participant.ParticipantId, 
                m => m.ProgramLevelGroup);
            if (participantProgramLevelGroup != null && foundProgramLevelGroup.ProgramLevelGroupId != participantProgramLevelGroup.ProgramLevelGroupId)
            {
                participantProgramLevelGroup.EndEffDate = DateTime.Now.Date;
                ParticipantProgramLevelGroupService.Update(participantProgramLevelGroup);
                ParticipantProgramLevelGroupService.Add(new ParticipantProgramLevelGroup
                {
                    BeginEffDate = DateTime.Now.Date,
                    ParticipantId = model.Participant.ParticipantId,
                    ProgramLevelGroupId = foundProgramLevelGroup.ProgramLevelGroupId
                });
            }
        }

        #endregion

        #region ProgressViewModel

        public ProgressViewModel GetProgressViewModel(int id)
        {
            return GetProgressViewModel(ProgressViewModel.Create(id));
        }
        public ProgressViewModel GetProgressViewModel(ProgressViewModel model)
        {
            if (model == null)
                model = ProgressViewModel.Create();
            model = (ProgressViewModel)base.PrepareModel(model);

            ProgressService.DisableProxy();
            if (model.Progress != null && model.Progress.ProgressId > 0 && model.Progress.ScoreTotal == 0)
                model.Progress = ProgressService.Get(p => p.ProgressId == model.Progress.ProgressId,
                    m => m.ParticipantProgramLevelGroup,
                    m => m.ProgressRatingCellScores,

                    m => m.ParticipantProgramLevelGroup.ProgramLevelGroup,

                    m => m.ParticipantProgramLevelGroup.Participant,
                    m => m.ParticipantProgramLevelGroup.Participant.ParticipantStatus,
                    m => m.ParticipantProgramLevelGroup.Participant.Program,
                    m => m.ParticipantProgramLevelGroup.Participant.Program.WorkCategories).FirstOrDefault();

            if (model.Progress == null)
                return model;

            model.AclUserName = AclUserName;
            var employee = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
            if (employee != null)
                model.CurrentUser = employee;

            model.ParticipantProgramLevelGroup = model.Progress.ParticipantProgramLevelGroup;
            model.ProgressRatingCellScores = model.Progress.ProgressRatingCellScores;
            model.ProgramLevelGroup = model.ParticipantProgramLevelGroup.ProgramLevelGroup;
            model.Participant = model.ParticipantProgramLevelGroup.Participant;
            model.ParticipantStatus = model.ParticipantProgramLevelGroup.Participant.ParticipantStatus;
            model.Program = model.ParticipantProgramLevelGroup.Participant.Program;
            ParticipantWorkService.DisableProxy();
            model.Progress.ParticipantWork = ParticipantWorkService.GetParticipantProgressWorks(model.Participant.ParticipantId, model.Progress.StartDate).ToList();
            model.ParticipantWork = model.Progress.ParticipantWork;
            model.WorkCategory = model.Program.WorkCategories.ToList();            

            model.DailyPerformanceProgramLevelGroups = DailyPerformanceProgramLevelGroupService.Get(p => p.ProgramLevelGroupId == model.ProgramLevelGroup.ProgramLevelGroupId, m => m.DailyPerformance).ToList();

            ProgressDayService.DisableProxy();
            model.ProgressDays = ProgressDayService.GetProgressDays(model.Progress.ProgressId,
                m => m.DailyPerformance,
                m => m.Division,
                m => m.ProgramLevel)
                .OrderBy(pd => pd.CalendarDate)
                .ToList();

            try
            {
                model.SignAsApprentice = AclService.HasAccess("SignAsApprentice", AclUserName);
                model.SignAsSupervisor = AclService.HasAccess("SignAsSupervisor", AclUserName);
                model.SignAsSuperintendent = AclService.HasAccess("SignAsSuperintendent", AclUserName);
            }
            catch (Exception e)
            {

            }

            var employees = new List<string>
            {
                model.Participant.Badge,
                model.Progress.SupervisorBadge,
                model.Progress.SuperintendentBadge,
            };
            employees.AddRange(model.ProgressDays.Where(pd => !string.IsNullOrEmpty(pd.SupervisorBadge)).Select(p => p.SupervisorBadge));
            employees.AddRange(model.ProgressDays.Where(pd => !string.IsNullOrEmpty(pd.CommentBadge)).Select(p => p.CommentBadge));
            model.Employees = EmployeeAllService.GetEmployeesByBadges(employees.ToArray()).ToList();
            if (employee != null)
                model.Employees.Add(employee);

            foreach (var progressDay in model.ProgressDays)
            {
                var progressDayViewModel = FillProgressDayViewModel(new ProgressDayViewModel(progressDay, model), model);
                model.ProgressDayViewModels.Add(progressDay, progressDayViewModel);
            }

            RatingCellService.DisableProxy();
            model.RatingCells = RatingCellService.Get(rc => rc.ProgramLevelGroupId == model.ProgramLevelGroup.ProgramLevelGroupId,
                m => m.RatingCategory,
                m => m.RatingArea,
                m => m.RatingItem,
                m => m.RatingCellScores)
                .OrderBy(r => r.SortOrderCategory)
                .ThenBy(r => r.SortOrderArea)
                .ThenBy(r => r.SortOrderCell)
                .ToList();

            var lastProgress = ProgressService.GetLastProgress(model.ParticipantProgramLevelGroup.ParticipantProgramLevelGroupId);
            model.IsLastProgress = lastProgress != null && lastProgress.ProgressId == model.Progress.ProgressId;

            return model;
        }

        public ProgressViewModel SaveProgressViewModel(ProgressViewModel model)
        {
            var oldModel = GetProgressViewModel(model.Progress.ProgressId);
            model.Participant = oldModel.Participant;

            model.AclUserName = AclUserName;
            model.Employees = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).ToList();
            var user = model.Employees.FirstOrDefault();
            if (user != null)
                model.CurrentUser = user;

            // prevent saving if already signed by superintendent
            if (!string.IsNullOrEmpty(model.Progress.SuperintendentBadge))
                return model;

            // save each day's progress
            foreach (var day in model.ProgressDays)
                SaveProgressDayViewModel(new ProgressDayViewModel(day, model), model);

            // save ratings
            ProgressRatingCellScoreService.Delete(model.Progress);
            foreach (var ratingCellScore in model.Progress.ProgressRatingCellScores)
            {
                ProgressRatingCellScoreService.Add(new ProgressRatingCellScore
                {
                    ProgressId = model.Progress.ProgressId,
                    RatingCellScoreId = ratingCellScore.RatingCellScoreId
                });
            }

            // calculate total score (rehydrate first)
            model.RatingCells = oldModel.RatingCells;
            var scores = model.RatingCellScores();
            if (scores != null)
                model.Progress.ScoreTotal = scores.Sum(p => p.Score);

            // sign as supervisor if criteria is met
            if (oldModel.Progress.EvaluationDate == null
                && model.Progress.EvaluationDate != null
                && model.AreDailyEvaluationsDone
                && model.ShowProgressEvaluationSection
                && model.Progress.ScoreTotal > 0)
            {
                model.SignAsSupervisor = AclService.HasAccess("SignAsSupervisor", AclUserName);
                if (model.SignAsSupervisor)
                {
                    var employee = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
                    if (employee != null)
                    {
                        model.Progress.SupervisorBadge = employee.Badge;
                    }
                }
            }

            if (model.Progress.EvaluationDate != null
                && string.IsNullOrEmpty(model.Progress.SupervisorBadge))
                model.Progress.EvaluationDate = null;

            // sign as apprentice if criteria is met
            if (oldModel.Progress.EvaluationDate == null 
                && model.Progress.EvaluationDate != null)
            {
                model.SignAsApprentice = AclService.HasAccess("SignAsApprentice", AclUserName);
                if (model.SignAsApprentice && model.Progress.EvaluationDate == null)
                {
                    model.Progress.EvaluationDate = DateTime.Now.Date;
                }
            }

            if (model.Progress.EvaluationDate != null
                && string.IsNullOrEmpty(model.Progress.SupervisorBadge))
                model.Progress.EvaluationDate = null;

            // TODO: uncomment when roles are ready for testing

            // sign as superintendent if criteria is met
            if (oldModel.Progress.SuperintendentApprovalDate == null
                && model.Progress.SuperintendentApprovalDate != null
                && model.Progress.EvaluationDate != null)
            {
                model.SignAsSuperintendent = AclService.HasAccess("SignAsSuperintendent", AclUserName);
                if (model.SignAsSuperintendent)
                {
                    var employee = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
                    if (employee != null) {
                        model.Progress.SuperintendentBadge = employee.Badge;
                    }

                    CloseProgress(model);
                }
            }

            if (model.Progress.SuperintendentApprovalDate != null
                && string.IsNullOrEmpty(model.Progress.SuperintendentBadge))
                model.Progress.SuperintendentApprovalDate = null;

            ProgressService.Update(model.Progress);

            return model;
        }

        public void CloseProgress(ProgressViewModel model)
        {
            // prevent work items from changing for week
            var items = ParticipantWorkService.GetParticipantWorks(model.Participant.ParticipantId, model.Progress.StartDate, model.Progress.EndDate).ToList();
            foreach (var item in items)
            {
                item.IsReadOnly = true;
                ParticipantWorkService.Update(item);
            }
        }


        #endregion

        #region ProgressDayViewModel

        public ProgressDayViewModel GetProgressDayViewModel(int id)
        {
            return GetProgressDayViewModel(ProgressDayViewModel.Create(id, null));
        }

        public ProgressDayViewModel GetProgressDayViewModel(int id, ProgressViewModel parent)
        {
            return GetProgressDayViewModel(ProgressDayViewModel.Create(id, parent));
        }

        public ProgressDayViewModel GetProgressDayViewModel(ProgressDayViewModel model = null, ProgressViewModel parent = null)
        {
            if (model == null)
                model = ProgressDayViewModel.Create(null, parent);
            model = (ProgressDayViewModel)base.PrepareModel(model);

            if (model.ProgressDay.ProgressDayId > 0 && model.ProgressDay.DivisionId == 0)
            {
                model.ProgressDay = ProgressDayService.Get(p => p.ProgressDayId == model.ProgressDay.ProgressDayId,
                   m => m.DailyPerformance,
                   m => m.Division,
                   m => m.ProgramLevel,
                   m => m.Progress,
                   m => m.Progress.ParticipantProgramLevelGroup,
                   m => m.Progress.ParticipantProgramLevelGroup.Participant,
                   m => m.Progress.ParticipantProgramLevelGroup.ProgramLevelGroup,
                   m => m.Progress.ParticipantProgramLevelGroup.ProgramLevelGroup.Program,
                   m => m.Progress.ParticipantProgramLevelGroup.ProgramLevelGroup.Program.WorkCategories).FirstOrDefault();

                if (model.ProgressDay == null)
                    return model;

                model.Progress = model.ProgressDay.Progress;
                model.ParticipantProgramLevelGroup = model.Progress.ParticipantProgramLevelGroup;
                model.ProgramLevelGroup = model.ParticipantProgramLevelGroup.ProgramLevelGroup;
                model.Participant = model.ParticipantProgramLevelGroup.Participant;
                model.ParticipantStatus = model.ParticipantProgramLevelGroup.Participant.ParticipantStatus;
                model.Program = model.ParticipantProgramLevelGroup.Participant.Program;
                model.WorkCategory = model.Program.WorkCategories.ToList();
                model.Division = model.ProgressDay.Division;
                model.DailyPerformance = model.ProgressDay.DailyPerformance;
                model.DailyPerformanceProgramLevelGroups = DailyPerformanceProgramLevelGroupService.Get(p => p.ProgramLevelGroupId == model.ProgramLevelGroup.ProgramLevelGroupId, m => m.DailyPerformance).ToList();
                model.ProgramLevel = model.ProgressDay.ProgramLevel;

                if (model.ProgressDay.ParticipantWork == null)
                    model.ProgressDay.ParticipantWork = model.ParticipantWork;
            }

            model.AclUserName = AclUserName;
            var employee = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
            if (employee != null)
                model.CurrentUser = employee;

            ParticipantWorkService.DisableProxy();
            if (model.ParticipantWork == null)
                model.ParticipantWork = ParticipantWorkService.GetParticipantWorks(model.Participant.ParticipantId, model.ProgressDay.CalendarDate).ToList();

            var employees = EmployeeAllService.GetEmployeesByBadges(new []
            {
                model.Participant.Badge,
                model.ProgressDay.SupervisorBadge,
                model.ProgressDay.CommentBadge,
                model.Progress.SupervisorBadge,
                model.Progress.SuperintendentBadge,
            });
            model.Employees = parent == null 
                ? employees.ToList()
                : employees.Union(parent.Employees).Distinct().ToList();
            if (employee != null)
                model.Employees.Add(employee);

            return FillProgressDayViewModel(model, null);
        }

        public ProgressDayViewModel FillProgressDayViewModel(ProgressDayViewModel model, ProgressViewModel parentModel)
        {
            if (parentModel != null)
            {
                model.SignAsSupervisor = parentModel.SignAsSupervisor;
                model.SignAsSuperintendent = parentModel.SignAsSuperintendent;
            }
            else
            {
                model.SignAsSupervisor = AclService.HasAccess("SignAsSupervisor", AclUserName);
                model.SignAsSuperintendent = AclService.HasAccess("SignAsSuperintendent", AclUserName);
            }
            return model;
        }

        public void SaveProgressDayViewModel(ProgressDayViewModel model, ProgressViewModel parentModel = null)
        {
            if (model == null)
                throw new Exception(string.Format("model parameter is null"));
            if (model.ProgressDay == null)
                throw new Exception(string.Format("model.ProgressDay parameter is null"));
            var oldModel = ProgressDayService.Get(p => p.ProgressDayId == model.ProgressDay.ProgressDayId).FirstOrDefault();
            if (oldModel == null)
                throw new Exception(string.Format("Could not find ProgressDay with Id {0}", model.ProgressDay.ProgressDayId) );
            model.Progress = oldModel.Progress;
            model.ProgressDay.ProgressId = oldModel.ProgressId;
            model.ProgressDay.DivisionId = oldModel.DivisionId;
            model.ProgressDay.ProgramLevelId = oldModel.ProgramLevelId;
            model.ProgressDay.CalendarDate = oldModel.CalendarDate;
            model.ProgressDay.AddUserId = oldModel.AddUserId;
            model.ProgressDay.AddDateTime = oldModel.AddDateTime;

            model.AclUserName = AclUserName;
            if (parentModel != null && parentModel.Employees != null)
            {
                model.CurrentUser = parentModel.Employees.FirstOrDefault(e => e.NTLogin == AclUserName.ToLower());
                if (model.CurrentUser == null)
                    throw new Exception(string.Format("model.CurrentUser is null, AclUserName={0}", AclUserName.ToLower()));
            }                
            else
            {
                var user = EmployeeAllService.GetEmployees(ntLogin: AclUserName.ToLower()).FirstOrDefault();
                if (user != null)
                    model.CurrentUser = user;
                else
                    throw new Exception(string.Format("EmployeeAllService.GetEmployees returned null for {0}", AclUserName));
            }

            // has anything changed?

            if (oldModel.ApprenticeDayOff == model.ProgressDay.ApprenticeDayOff &&
                oldModel.DailyPerformanceId == model.ProgressDay.DailyPerformanceId &&
                oldModel.Comment == model.ProgressDay.Comment) return;

            if (oldModel.Comment != model.ProgressDay.Comment)
            {
                model.ProgressDay.SupervisorBadge = model.CurrentUser.Badge;
                model.ProgressDay.CommentDate = DateTime.Now;
            }

            ProgressDayService.Update(model.ProgressDay);
        }


        #endregion
    }
}

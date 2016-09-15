using System;
using System.Collections.Generic;
using System.Linq;
using ACTransit.DataAccess.Training;
using ACTransit.Entities.Training;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACTransit.Training.Web.UnitTest.Apprentice
{
    [TestClass]
    public class PopulateTestData
    {
        private readonly TrainingEntities context;
        private const bool isDisabled = false;

        private readonly List<DailyPerformance> dailyPerformance;
        private readonly List<DailyPerformanceProgramLevelGroup> dailyPerformanceProgramLevelGroup;
        private readonly List<ParticipantStatus> participantStatus;
        private readonly List<Program> programs;
        private readonly List<ProgramLevelGroup> programLevelGroups;
        private readonly List<ProgramLevel> programLevels;
        private readonly List<RatingItem> ratingItems;
        private readonly List<RatingArea> ratingAreas;
        private readonly List<RatingCategory> ratingCategories;
        private readonly List<WorkCategory> workCategories;

        public PopulateTestData()
        {
            context = new TrainingEntities();
            // load pre-populated tables
            dailyPerformance = context.DailyPerformances.ToList();
            dailyPerformanceProgramLevelGroup = context.DailyPerformanceProgramLevelGroups.ToList();
            participantStatus = context.ParticipantStatus.ToList();
            programs = context.Programs.ToList();
            programLevelGroups = context.ProgramLevelGroups.ToList();
            programLevels = context.ProgramLevels.ToList();
            ratingCategories = context.RatingCategories.ToList();
            ratingAreas = context.RatingAreas.ToList();
            ratingItems = context.RatingItems.ToList();
            workCategories = context.WorkCategories.ToList();
        }

        [TestMethod]
        public void SeedTestData()
        {
            //SeedHeavyDuty1to2();
            //SeedHeavyDuty3to5();
            //SeedHeavyDuty6to8();
            //SrElectronicTech();
            SeedParticipant1();
            //SeedParticipant2();
        }

        private void SeedHeavyDuty1to2()
        {
            if (isDisabled)
                return;

            // Add rating: Heavy Duty, Levels: 1-2

            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Heavy Duty Coach Mechanic" && p.StartLevel == 1 && p.EndLevel == 2);


            // Attitude Towards Job: Dependability


            var ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Extremely punctual & dependable. Always where assigned."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            var ratingScores = new List<RatingCellScore> { new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 } };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Very punctual, dependable except under very unusual circumstances."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 }
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Good observance of work hours; rarely away from job."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 }
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Late returning to job site frequently; occasionally must be found."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Excessive & persistent pattern of returning late to job assignments."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Initiative

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Eager to gain new knowledge. Enjoys challenges."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Displays willingness to gain new knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Requests assignments to broaden product knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Learns only what is necessary to do job."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Must be urged to learn anything new."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Acceptance of Responsibility

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Seeks advices only when necessary. Helps other regularly."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely makes wrong decisions. Seeks more responsibilities."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help if procedures aren't clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "May ask for help even if procedures ARE clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help often. Doesn't work well independently."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Thoroughness

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Without exception, completes each job as specified."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Close attention to detail. Follows specified procedures."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works outside of procedures & misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually follows jobs to their end. May need reminding."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Has poor follow-up procedures. Misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Accuracy

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has not made an error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made minor errors in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made a moderate error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several moderate and one major error."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several major or many minor errors in this period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Utilization of Time

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Time planned & used effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Plans time, equipment & tools. Executes job effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Normally meets commitments. Plans well."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Occasionally falters because of poor planning."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Works at random. Does not plan work prior to start."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Follows Written & Oral Instructions

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently completes tasks exactly as directed."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Completes tasks as directed. Rarely needs clarification."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Follows directions, usually follows through completely."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs limited help in follow through after starting job."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs constant follow up and redirection."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Reactions to Supervision

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares common goal. Only gives constructive criticism."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 15 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 14 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 13 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Readily accepts direction. Shows willingness."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 12 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 11 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs as directed. May criticize without offering solution."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shows mild resistance to questions and authority."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Resists direction. Has poor attitude."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Attitude Towards Other Employees

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Actively promotes good relationships. Shares knowledge."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 15 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 14 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 13 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares knowledge. Teaches & learns from coworkers."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 12 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 11 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Cooperates with others. Will assist if necessary or asked."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Trouble working with others. Hoards information."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Ineffective, creates animosity."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Safety

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Constantly aware of safety. Suggests fixes constructively."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Attitude of safety awareness. Assists in reducing risk."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs in a safe way. Usually contributes to risk reduction."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works in an unsafe manner. Notes problems."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Sometimes risks own or other's safety expediency."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Housekeeping

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Coworkers are happy to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Well organized. Rarely neglects tools/equip. Good to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually organized. May need reminding. OK to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Falls short of being organized. Complaints by coworkers."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Unorganized. Has poor work habits. None care to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);



            context.SaveChanges();

        }

        private void SeedHeavyDuty3to5()
        {
            if (isDisabled)
                return;

            // Add rating: Heavy Duty, Levels: 3-4

            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Heavy Duty Coach Mechanic" && p.StartLevel == 3 && p.EndLevel == 5);


            // Attitude Towards Job: Punctuality

            var ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Extremely punctual & dependable. Always where assigned."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            var ratingScores = new List<RatingCellScore> { new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 } };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Very punctual & dependable except under very unusual circumstances."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 }
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Good observance of work hours; rarely away from job."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Late returning to job site frequently; occasionally must be found."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Excessive & persistent pattern of returning late to job assignments."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Initiative

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Eager to gain new knowledge. Enjoys challenges."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Displays willingness to gain new knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Requests assignments to broaden product knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Learns only what is necessary to do job."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Must be urged to learn anything new."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Acceptance of Responsibility

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Seeks advices only when necessary. Helps other regularly."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely makes wrong decisions. Seeks more responsibilities."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help if procedures aren't clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "May ask for help even if procedures ARE clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help often. Doesn't work well independently."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Thoroughness

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Without exception, completes each job as specified."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Close attention to detail. Follows specified procedures."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works outside of procedures & misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually follows jobs to their end. May need reminding."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Has poor follow-up procedures. Misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Accuracy

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has not made an error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made minor errors in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made a moderate error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several moderate and one major error."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several major or many minor errors in this period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Utilization of Time

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Time planned & used effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Plans time, equipment & tools. Executes job effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Normally meets commitments. Plans well."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Occasionally falters because of poor planning."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Works at random. Does not plan work prior to start."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Follows Written & Oral Instructions

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently completes tasks exactly as directed."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Completes tasks as directed. Rarely needs clarification."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Follows directions, usually follows through completely."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs limited help in follow through after starting job."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs constant follow up and redirection."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Volume of Work

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Output is phenomenal as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently produces superior volume of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Volume of useful output wholly adequate."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Somewhat slow or erratic in production of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Inadequate volume of work as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Reactions to Supervision

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares common goal. Gives constructive criticism."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Readily accepts direction. Shows willingness."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs as directed. May criticize without offering solution."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shows mild resistance to questions and authority."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Resists direction. Has poor attitude."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Attitude Towards Other Employees

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Actively promotes good relationships. Shares knowledge."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares knowledge. Teaches & learns from coworkers."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Cooperates with others. Will assist if necessary or asked."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Trouble working with others. Hoards information."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Ineffective, creates animosity."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Safety

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Constantly aware of safety. Suggests fixes constructively."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Attitude of safety awareness. Assists in reducing risk."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs in a safe way. Usually contributes to risk reduction."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works in an unsafe manner. Notes problems."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Sometimes risks own or other's safety expediency."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Housekeeping

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Coworkers are happy to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Well organized. Rarely neglects tools/equip. Good to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually organized. May need reminding. OK to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Falls short of being organized. Complaints by coworkers."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Unorganized. Has poor work habits. None care to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);



            context.SaveChanges();

        }

        private void SeedHeavyDuty6to8()
        {
            if (isDisabled)
                return;

            // Add rating: Heavy Duty, Levels: 6-8

            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Heavy Duty Coach Mechanic" && p.StartLevel == 6 && p.EndLevel == 8);


            // Attitude Towards Job: Dependability

            var ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Always meets or exceeds goals of each job."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            var ratingScores = new List<RatingCellScore> { new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 } };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Meets goals except under unusual circumstances."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Always strives to meet goals, usually successful."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "Meets goals when no obstacles prevent it."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Dependability"),
                RatingItem = ratingItems.Single(i => i.Description == "UNRELIABLE."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Initiative

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Eager to gain new knowledge. Enjoys challenges."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Displays willingness to gain new knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Requests assignments to broaden product knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Learns only what is necessary to do job."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Must be urged to learn anything new."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Acceptance of Responsibility

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Seeks advices only when necessary. Helps other regularly."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely makes wrong decisions. Seeks more responsibilities."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help if procedures aren't clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "May ask for help even if procedures ARE clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help often. Doesn't work well independently."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Thoroughness

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Without exception, completes each job as specified."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Close attention to detail. Follows specified procedures."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works outside of procedures & misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually follows jobs to their end. May need reminding."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Has poor follow-up procedures. Misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Accuracy

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has not made an error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made minor errors in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made a moderate error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several moderate and one major error."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several major or many minor errors in this period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Utilization of Time

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Time planned & used effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Plans time, equipment & tools. Executes job effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Normally meets commitments. Plans well."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Occasionally falters because of poor planning."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Works at random. Does not plan work prior to start."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Follows Written & Oral Instructions

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently completes tasks exactly as directed."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Completes tasks as directed. Rarely needs clarification."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 9 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Follows directions, usually follows through completely."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs limited help in follow through after starting job."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs constant follow up and redirection."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Volume of Work

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Output is phenomenal as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently produces superior volume of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Volume of useful output wholly adequate."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Somewhat slow or erratic in production of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Inadequate volume of work as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Reactions to Supervision

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares common goal. Gives constructive criticism only."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Readily accepts direction. Shows willingness."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs as directed. May criticize without offering solution."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Shows mild resistance to questions and authority."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervision"),
                RatingItem = ratingItems.Single(i => i.Description == "Resists direction. Has poor attitude."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Attitude Towards Other Employees

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Actively promotes good relationships. Shares knowledge."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares knowledge. Teaches & learns from coworkers."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Cooperates with others. Will assist if necessary or asked."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Trouble working with others. Hoards information."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Ineffective, creates animosity."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Safety

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Constantly aware of safety. Suggests fixes constructively."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Attitude of safety awareness. Assists in reducing risk."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs in a safe way. Usually contributes to risk reduction."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works in an unsafe manner. Notes problems."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Sometimes risks own or other's safety expediency."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Housekeeping

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Coworkers are happy to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Well organized. Rarely neglects tools/equip. Good to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually organized. May need reminding. OK to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Falls short of being organized. Complaints by coworkers."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 4 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 3 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 2 },
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 1 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Unorganized. Has poor work habits. None care to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 0 },
            };
            context.RatingCellScores.AddRange(ratingScores);



            context.SaveChanges();

        }

        private void SrElectronicTech()
        {
            return;

            // Add rating: Senior Electronic Technician

            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Senior Electronic Technician" && p.StartLevel == 0 && p.EndLevel == 0);


            // Attitude Towards Job: Punctuality

            var ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Extremely punctual & dependable. Always where assigned."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            var ratingScores = new List<RatingCellScore> { new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 } };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Very punctual, dependable except under very unusual circumstances."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Good observance of work hours; rarely away from job."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Late returning to job site frequently; occasionally must be found."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Punctuality"),
                RatingItem = ratingItems.Single(i => i.Description == "Excessive & persistent pattern of returning late to job assignments."),
                SortOrderCategory = 1,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Initiative

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Eager to gain new knowledge. Enjoys challenges."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Displays willingness to gain new knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Requests assignments to broaden product knowledge."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Learns only what is necessary to do job."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Initiative"),
                RatingItem = ratingItems.Single(i => i.Description == "Must be urged to learn anything new."),
                SortOrderCategory = 1,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Attitude Towards Job: Acceptance of Responsibility

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Seeks advices only when necessary. Helps other regularly."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely makes wrong decisions. Seeks more responsibilities."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help if procedures are not clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "May ask for help even if procedures are clearly defined."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Attitude Towards Job"),
                RatingArea = ratingAreas.Single(a => a.Name == "Acceptance of Responsibility"),
                RatingItem = ratingItems.Single(i => i.Description == "Asks for help often, does not work well independently."),
                SortOrderCategory = 1,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Thoroughness

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Without exception, completes each job as specified."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Close attention to detail, follows specified procedures."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works outside of procedures & misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually follows jobs to their end. May need reminding."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Thoroughness"),
                RatingItem = ratingItems.Single(i => i.Description == "Has a poor follow-up procedure. Misses details."),
                SortOrderCategory = 2,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Quality: Accuracy

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has not made an error in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made minor errors in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made moderate errors in this review period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several moderate or one major error."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Quality"),
                RatingArea = ratingAreas.Single(a => a.Name == "Accuracy"),
                RatingItem = ratingItems.Single(i => i.Description == "Has made several major or many minor errors in this period."),
                SortOrderCategory = 2,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Utilization of Time

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Time planned & used effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Plans time, equipment & tools. Executes job effectively."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Normally meets commitments. Plans well."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Occasionally falters because of poor planning."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Utilization of Time"),
                RatingItem = ratingItems.Single(i => i.Description == "Works at random. Does not plan work prior to start."),
                SortOrderCategory = 3,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Follows Written & Oral Instructions

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently completes tasks exactly as directed."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Completes tasks as directed. Rarely needs clarification."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Follows directions usually. Follows through completely."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs limited help in follow through after starting job."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Follows Written & Oral Instructions"),
                RatingItem = ratingItems.Single(i => i.Description == "Needs constant follow up and redirection."),
                SortOrderCategory = 3,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Productivity: Volume of Work

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Output is phenomenal as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Consistently produces superior volume of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Volume of useful output wholly adequate."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Somewhat slow or erratic in production of useful work."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Productivity"),
                RatingArea = ratingAreas.Single(a => a.Name == "Volume of Work"),
                RatingItem = ratingItems.Single(i => i.Description == "Inadequate volume of work as required."),
                SortOrderCategory = 3,
                SortOrderArea = 3,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Reactions to Supervisior

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervisor"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares common goal. Gives constructive criticism only."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervisor"),
                RatingItem = ratingItems.Single(i => i.Description == "Readily accepts direction, shows willingness."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervisor"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs as directed. May criticize but offers solution."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervisor"),
                RatingItem = ratingItems.Single(i => i.Description == "Shows mild resistance to questions and authority."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Reactions to Supervisor"),
                RatingItem = ratingItems.Single(i => i.Description == "Resists direction. Has poor attitude."),
                SortOrderCategory = 4,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Working with Others: Attitude Towards Other Employees

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Actively promotes good relationships. Shares knowledge."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Shares knowledge. Teaches & learns from coworkers."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Cooperates with others, will assist if necessary."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Trouble working with others. Hoards information."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Working with Others"),
                RatingArea = ratingAreas.Single(a => a.Name == "Attitude Towards Other Employees"),
                RatingItem = ratingItems.Single(i => i.Description == "Ineffective, creates animosity."),
                SortOrderCategory = 4,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Safety

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Constantly aware of safety. Suggests fixes constructively."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Attitude of safety awareness. Assists in reducing risk."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Performs in a safe way. Usually contributes to risk reduction."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Rarely works in an unsafe manner, notes problems."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Safety"),
                RatingItem = ratingItems.Single(i => i.Description == "Sometimes risks own or other's safety expediency."),
                SortOrderCategory = 5,
                SortOrderArea = 1,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);


            // Work Habits: Housekeeping

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Orderly & systematic. Coworkers are happy to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 1
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 10 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Well organized. Rarely neglects tools/equip. Good to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 2
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 8 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Usually organized. May need reminding."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 3
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 7 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Falls short of being organized. Complaints by coworkers."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 4
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 6 },
            };
            context.RatingCellScores.AddRange(ratingScores);

            ratingCell = new RatingCell
            {
                RatingCellId = Seed.Item.Next("RatingCell"),
                ProgramLevelGroup = programLevelGroup,
                RatingCategory = ratingCategories.Single(c => c.Name == "Work Habits"),
                RatingArea = ratingAreas.Single(a => a.Name == "Housekeeping"),
                RatingItem = ratingItems.Single(i => i.Description == "Unorganized. Has poor work habits. None care to follow."),
                SortOrderCategory = 5,
                SortOrderArea = 2,
                SortOrderCell = 5
            };
            ratingScores = new List<RatingCellScore>
            {
                new RatingCellScore { RatingCellScoreId = Seed.Item.Next("RatingCellScore"), RatingCell = ratingCell, Score = 5 },
            };
            context.RatingCellScores.AddRange(ratingScores);



            context.SaveChanges();


        }

        private void SeedParticipant1()
        {
            if (isDisabled)
                return;

            // Add employee to Heavy Duty, Levels: 1-2, first week, 3rd day
            var startDate = DateTime.Now.Date.AddDays(-(int) DateTime.Now.DayOfWeek).AddDays(-7);
            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Heavy Duty Coach Mechanic" && p.StartLevel == 1 && p.EndLevel == 2);
            var programLevel = programLevels.Single(p => p.ProgramLevelGroup == programLevelGroup && p.Level == 1);

            var participant =
                new Participant
                {
                    ParticipantId = Seed.Item.Next("Participant"),
                    Badge = "032167",
                    Program = programLevelGroup.Program,
                    ParticipantStatus = context.ParticipantStatus.FirstOrDefault(s => s.Name == "Active"),
                    ProgramLevel = programLevel
                };
            context.Participants.Add(participant);

            var participantProgramLevelGroup = new ParticipantProgramLevelGroup
            {
                ParticipantProgramLevelGroupId = Seed.Item.Next("ParticipantProgramLevelGroup"),
                ParticipantId = participant.ParticipantId,
                ProgramLevelGroup = programLevelGroup,
                BeginEffDate = startDate
            };
            context.ParticipantProgramLevelGroups.Add(participantProgramLevelGroup);

            context.SaveChanges();

            var progress = new Progress
            {
                ProgressId = Seed.Item.Next("Progress"),
                ParticipantProgramLevelGroup = participantProgramLevelGroup,
                StartDate = startDate
            };
            context.Progresses.Add(progress);

            var progressDay0 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                ProgramLevel = programLevel,
                CalendarDate = startDate,
                ApprenticeDayOff = true
            };
            context.ProgressDays.Add(progressDay0);

            var progressDay1 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 8, // CMF
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(1),
                ParticipantWork = new List<ParticipantWork>
                {
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11111",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(1),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ENG").WorkCategoryId,
                        WorkHour = 8,
                        VehicleId = "V1111",
                        SeriesNum = "S11111",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC111",
                        CompDescription = "CD1111",
                        TaskComment = "Installed exhaust manifold and turbo and all collant and oil lines to the turbo.",
                        JobDescCode = "11"
                    }
                },
            };
            context.ProgressDays.Add(progressDay1);
            context.ParticipantWorks.AddRange(progressDay1.ParticipantWork);

            var progressDay2 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 8, // CMF
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(2),
                ParticipantWork = new List<ParticipantWork>
                {
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11112",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(2),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "4069",
                        SeriesNum = "S11112",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC112",
                        CompDescription = "CD1112",
                        TaskComment = "Jump started bus and charged batteries.",
                        JobDescCode = "12"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11112",
                        TaskNum = "02",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(2),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "4069",
                        SeriesNum = "S11112",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC112",
                        CompDescription = "CD1112",
                        TaskComment = "Checked charging system.",
                        JobDescCode = "12"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11113",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(2),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "6074",
                        SeriesNum = "S11113",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC113",
                        CompDescription = "CD1113",
                        TaskComment = "Inspected HVAC system.",
                        JobDescCode = "13"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11113",
                        TaskNum = "02",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(2),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "6074",
                        SeriesNum = "S11113",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC113",
                        CompDescription = "CD1113",
                        TaskComment = "Repaired HVAC electrical wiring.",
                        JobDescCode = "13"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11114",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(2),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "B").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "1316",
                        SeriesNum = "S11114",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC114",
                        CompDescription = "CD114",
                        TaskComment = "Repaired horn.",
                        JobDescCode = "14"
                    },
                },
            };
            context.ProgressDays.Add(progressDay2);
            context.ParticipantWorks.AddRange(progressDay2.ParticipantWork);

            var progressDay3 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(3),
                ParticipantWork = new List<ParticipantWork>
                {
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11115",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(3),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "1316",
                        SeriesNum = "S11115",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC115",
                        CompDescription = "CD115",
                        TaskComment = "Charged batteries CEL for turbo actuator.",
                        JobDescCode = "15"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11116",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(3),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "1442",
                        SeriesNum = "S11116",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC116",
                        CompDescription = "CD116",
                        TaskComment = "Repaired soat bell wiring circuit.",
                        JobDescCode = "16"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11116",
                        TaskNum = "02",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(3),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "B").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "1442",
                        SeriesNum = "S11116",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC116",
                        CompDescription = "CD116",
                        TaskComment = "Adjusted front doors.",
                        JobDescCode = "16"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11116",
                        TaskNum = "03",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(3),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ENG").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "1442",
                        SeriesNum = "S11116",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC116",
                        CompDescription = "CD116",
                        TaskComment = "Pressure tested and repaired collant leaks.",
                        JobDescCode = "16"
                    },
                },
            };
            context.ProgressDays.Add(progressDay3);
            context.ParticipantWorks.AddRange(progressDay3.ParticipantWork);

            var progressDay4 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(4),
                ParticipantWork = new List<ParticipantWork>
                {
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11117",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(4),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ENG").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "1414",
                        SeriesNum = "S11117",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC117",
                        CompDescription = "CD117",
                        TaskComment = "Did Yard check for no start and low coolent.",
                        JobDescCode = "17"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11118",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(4),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ES").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "2213",
                        SeriesNum = "S11118",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC118",
                        CompDescription = "CD118",
                        TaskComment = "Charged batteries.",
                        JobDescCode = "18"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11119",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(4),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "ENG").WorkCategoryId,
                        WorkHour = 1,
                        VehicleId = "1450",
                        SeriesNum = "S11119",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC119",
                        CompDescription = "CD119",
                        TaskComment = "Changed water pump belt.",
                        JobDescCode = "19"
                    },
                    new ParticipantWork
                    {
                        ParticipantId = participant.ParticipantId,
                        WorkOrderNum = "WO11120",
                        TaskNum = "01",
                        IsReadOnly = false,
                        StartDate = startDate,
                        WorkDate = startDate.AddDays(4),
                        WorkCategoryId = workCategories.First(wc=>wc.Name == "AS").WorkCategoryId,
                        WorkHour = 3,
                        VehicleId = "1027",
                        SeriesNum = "S11120",
                        TrainingSeriesName = "Mechanical",
                        CompCode = "CC120",
                        CompDescription = "CD120",
                        TaskComment = "Inspected air system for net building air.",
                        JobDescCode = "20"
                    },
                }
             };
            context.ProgressDays.Add(progressDay4);
            context.ParticipantWorks.AddRange(progressDay4.ParticipantWork);

            var progressDay5 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(5),
                ApprenticeDayOff = true
            };
            context.ProgressDays.Add(progressDay5);

            var progressDay6 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(6),
                ApprenticeDayOff = true
            };
            context.ProgressDays.Add(progressDay6);
            context.SaveChanges();

        }

        private void SeedParticipant2()
        {
            if (isDisabled)
                return;

            // Add employee to Heavy Duty, Levels: 1-2, first week completed, a week ago
            var startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek).AddDays(-7);

            var programLevelGroup = programLevelGroups.Single(p => p.Program.Name == "Heavy Duty Coach Mechanic" && p.StartLevel == 1 && p.EndLevel == 2);
            var programLevel = programLevels.Single(p => p.ProgramLevelGroup == programLevelGroup && p.Level == 1);

            var participant =
                new Participant
                {
                    ParticipantId = Seed.Item.Next("Participant"),
                    Badge = "032514",
                    Program = programLevelGroup.Program,
                    ParticipantStatus = context.ParticipantStatus.FirstOrDefault(s => s.Name == "Active"),
                    ProgramLevel = programLevel
                };
            context.Participants.Add(participant);

            context.SaveChanges();

            var participantProgramLevelGroup = new ParticipantProgramLevelGroup
            {
                ParticipantProgramLevelGroupId = Seed.Item.Next("ParticipantProgramLevelGroup"),
                ParticipantId = participant.ParticipantId,
                ProgramLevelGroup = programLevelGroup,
                BeginEffDate = startDate
            };
            context.ParticipantProgramLevelGroups.Add(participantProgramLevelGroup);

            context.SaveChanges();

            var progress = new Progress
            {
                ProgressId = Seed.Item.Next("Progress"),
                ParticipantProgramLevelGroup = participantProgramLevelGroup,
                StartDate = startDate
            };
            context.Progresses.Add(progress);

            context.SaveChanges();

            var progressDay0 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(0),
                ApprenticeDayOff = true
            };
            context.ProgressDays.Add(progressDay0);

            var progressDay1 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 8, // CMF
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(1),
                DailyPerformance = dailyPerformance.First(dp => dp.Name == "Meets Expectations"),
                DailyPerformanceId = dailyPerformance.First(dp => dp.Name == "Meets Expectations").DailyPerformanceId,
                SupervisorBadge = "041365",
                Comment = "This has been reviewed by supervisor."
            };
            context.ProgressDays.Add(progressDay1);

            var progressDay2 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 8, // CMF
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(2),
                DailyPerformance = dailyPerformance.First(dp => dp.Name == "Meets Expectations"),
                DailyPerformanceId = dailyPerformance.First(dp => dp.Name == "Meets Expectations").DailyPerformanceId,
                SupervisorBadge = "041365",
                Comment = "This has been reviewed by supervisor."
            };
            context.ProgressDays.Add(progressDay2);

            var progressDay3 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(3),
                DailyPerformance = dailyPerformance.First(dp => dp.Name == "Meets Expectations"),
                DailyPerformanceId = dailyPerformance.First(dp => dp.Name == "Meets Expectations").DailyPerformanceId,
            };
            context.ProgressDays.Add(progressDay3);

            var progressDay4 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(4),
                DailyPerformance = dailyPerformance.First(dp => dp.Name == "Needs Improvement"),
                DailyPerformanceId = dailyPerformance.First(dp => dp.Name == "Needs Improvement").DailyPerformanceId,
            };
            context.ProgressDays.Add(progressDay4);

            var progressDay5 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                DivisionId = 4, // D6
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(5),
            };
            context.ProgressDays.Add(progressDay5);

            var progressDay6 = new ProgressDay
            {
                ProgressDayId = Seed.Item.Next("ProgressDay"),
                Progress = progress,
                ProgramLevel = programLevel,
                CalendarDate = startDate.AddDays(6),
                ApprenticeDayOff = true
            };
            context.ProgressDays.Add(progressDay6);

            context.SaveChanges();

            // add ratings

            //var ratingAreas =
            //    (from ra in context.RatingArea
            //     from rc in context.RatingCell
            //     where rc.ProgramLevelGroupId == programLevelGroup.ProgramLevelGroupId && rc.RatingAreaId == ra.RatingAreaId
            //     orderby rc.SortOrderArea
            //     select ra).Distinct().ToList();

            //var ratingCells =
            //    (from rc in context.RatingCell
            //     where rc.ProgramLevelGroupId == programLevelGroup.ProgramLevelGroupId
            //     //join rcs in context.RatingCellScore on rc.RatingCellId equals rcs.RatingCellId
            //     orderby rc.SortOrderCell
            //     select rc).ToList();

            //progress.ScoreTotal = 0;

            //foreach (var area in ratingAreas)
            //{
            //    var scores = (from rc in ratingCells
            //                  where rc.RatingAreaId == area.RatingAreaId
            //                  select rc.RatingCellScore).SelectMany(i => i).ToArray();
            //    if (!scores.Any())
            //        throw new Exception("No scores found");

            //    var idx = Seed.Item.Random.Next(scores.Count());

            //    var progressRatingCellScore = new ProgressRatingCellScore
            //    {
            //        ProgressId = progress.ProgressId,
            //        Progress = progress,
            //        RatingCellScoreId = scores[idx].RatingCellScoreId
            //    };
            //    progress.ScoreTotal += scores[idx].Score;
            //    context.ProgressRatingCellScore.Add(progressRatingCellScore);
            //}

            //context.SaveChanges();

        }
    }
}

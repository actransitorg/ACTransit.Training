using System;
using System.Linq;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Business.Apprentice;
using ACTransit.Training.Web.Domain.Apprentice.Services;
using ACTransit.Training.Web.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACTransit.Training.Web.UnitTest.Apprentice
{
    [TestClass]
    public class ParticipantTests
    {
        private readonly string currentUser = Environment.UserName;

        [TestMethod]
        public void GetProgramParticipantsTest()
        {
            using (var service = new ParticipantService(currentUser))
            {
                var items = service.GetProgramParticipants("Mechanic").ToList();
                Assert.IsTrue(items.Count > 0);
            }
        }

        [TestMethod]
        public void GetAllParticipants()
        {
            using (var service = new ParticipantService(currentUser))
            {
                var people = service.GetParticipants().ToList();
                Assert.IsNotNull(people);
                Assert.IsTrue(people.Count > 0);
            }
        }

        [TestMethod]
        public void GetParticipantLastScore()
        {
            using (var service = new ParticipantService(currentUser))
            {
                var participant = service.GetParticipant("99999");
                Assert.IsNotNull(participant);
                var participantProgramLevelGroup = participant.ParticipantProgramLevelGroups.OrderBy(pl => pl.BeginEffDate).LastOrDefault();
                Assert.IsNotNull(participantProgramLevelGroup);
                var lastProgress = participantProgramLevelGroup.Progresses.OrderBy(p => p.StartDate).LastOrDefault();
                Assert.IsNotNull(lastProgress);
                var score = lastProgress.ScoreTotal;
                Assert.IsTrue(score > 0);
            }
        }

        [TestMethod]
        public void GetProgressViewModel()
        {
            using (var service = new ApprenticeServiceDomain())
            {
                var progressViewModel = service.GetProgressViewModel(1);
                Assert.IsNotNull(progressViewModel);
                Assert.IsTrue(progressViewModel.RatingCells.Count > 0);
                var cell1 = progressViewModel.RatingCells.FirstOrDefault();
                Assert.IsNotNull(cell1);
                var cellItem1Description = cell1.RatingItem.Description;
                Assert.IsNotNull(cellItem1Description);
            }
        }

        [TestMethod]
        public void GetApprenticeEmployees()
        {
            using (var service = new EmployeeServiceDomain())
            {
                var result = service.GetEmployees(jobTitle: "Apprentice Mechanic");
                Assert.IsTrue(result.Any());
            }
        }
    }
}

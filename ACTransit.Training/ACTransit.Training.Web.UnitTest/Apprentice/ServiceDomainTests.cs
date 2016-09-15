using System;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Apprentice.Models;
using ACTransit.Training.Web.Domain.Apprentice.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACTransit.Training.Web.UnitTest.Apprentice
{
    [TestClass]
    public class ServiceDomainTests
    {
        //[TestMethod]
        //public void ProgramLevelGroupsViewModelTests()
        //{
        //    var service = new ApprenticeServiceDomain();
        //    var model1 = service.GetProgramLevelGroupsViewModel(null);
        //    Assert.IsNotNull(model1);
        //    Assert.IsTrue(model1.Items.Count > 0);
        //    foreach (var item in model1.Items)
        //    {
        //        Assert.IsNotNull(item.Program);
        //        Assert.IsTrue(item.Program.Name.Length > 0);
        //    }

        //    var model2 = service.GetProgramLevelGroupsViewModel(model1);
        //    Assert.IsNotNull(model2);
        //    Assert.IsTrue(model2.Items.Count > 0);
        //    foreach (var item in model2.Items)
        //    {
        //        Assert.IsNotNull(item.Program);
        //        Assert.IsTrue(item.Program.Name.Length > 0);
        //    }
        //}

        //[TestMethod]
        //public void ProgramLevelGroupViewModelTests()
        //{
        //    var service = new ApprenticeServiceDomain();
        //    var model1 = service.GetProgramLevelGroupViewModel(new ProgramLevelGroupViewModel
        //    {
        //        ProgramLevelGroup = new ProgramLevelGroup
        //        {
        //            ProgramLevelGroupId = 1
        //        }
            
        //    });
        //    Assert.IsNotNull(model1);
        //    Assert.IsTrue(model1.Items.Count > 0);
        //    foreach (var item in model1.Items)
        //    {
        //        Assert.IsNotNull(item.Program);
        //        Assert.IsTrue(item.Program.Name.Length > 0);
        //    }

        //    var model2 = service.GetProgramLevelGroupViewModel(model1);
        //    Assert.IsNotNull(model2);
        //    Assert.IsTrue(model2.Items.Count > 0);
        //    foreach (var item in model2.Items)
        //    {
        //        Assert.IsNotNull(item.Program);
        //        Assert.IsTrue(item.Program.Name.Length > 0);
        //    }
        //}
    }
}

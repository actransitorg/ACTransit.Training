using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ACTransit.Entities.Training;
using ACTransit.Training.Web.Domain.Services;
using ACTransit.Training.Web.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

namespace ACTransit.Training.Web.UnitTest
{
    [TestClass]
    public class ACL
    {
        private const string User1 = "[User1]";
        private const string AclPath = @"[Enter the right path]ACL.xml";  //Make sure the path is correct
        public ACL()
        {
        }

        [TestMethod]
        public void TestACL()
        {
            AclService service = AclService.Create(AclPath);
            Assert.IsTrue(service.HasAccess("Admin","[user]"),"access denied!");
        }


        [TestMethod]
        public void TestMenuHelper()
        {
            const int maxThread = 20;
            var userName = User1;
            int errorCount = 0;            
            var context = CreateContext(userName);            
            CountdownEvent numberEvent = new CountdownEvent(maxThread);
            Action test1 = () =>
            {
                try
                {
                    HttpContext.Current = context;
                    var menus = MenuHelper.GetResources();
                    Console.WriteLine(menus.Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Interlocked.Increment(ref errorCount);
                    throw;
                }
                finally
                {                    
                    numberEvent.Signal();
                    
                }

            };
            Action test2 = () =>
            {
                try
                {
                    HttpContext.Current = context; 
                    var menus = MenuHelper.GetResources();
                    Console.WriteLine(menus.Count);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Interlocked.Increment(ref errorCount);
                    throw;
                }
                finally
                {                   
                    numberEvent.Signal();                    
                }

            };
            var tasks = new Task[maxThread];
            for (var i = 0; i < (maxThread / 2); i++)
            {
                tasks[(i * 2)] = Task.Factory.StartNew(test1);
                tasks[(i * 2) + 1] = Task.Factory.StartNew(test2);
            }
            numberEvent.Wait();
            Assert.IsTrue(errorCount == 0, errorCount + " error(s) found.");
            DistroyContext();
            

        }

        [TestMethod]
        public void TestMenuHelper1()
        {
            const string userName = User1;
            CreateContext(userName);
            var menus = MenuHelper.GetResources();
            Console.WriteLine(menus.Count);
            DistroyContext();
            CreateContext(userName);
            menus = MenuHelper.GetResources();
            Console.WriteLine(menus.Count);
            DistroyContext();

            Assert.IsNotNull(menus);



        }

        public bool HasAccess(string token, string user)
        {

            Console.WriteLine(string.Join(",", GetTokenNames()));
            Console.WriteLine(string.Join(",", GetGroups("Admin")));
            Console.WriteLine(string.Join(",", GetUsers("Admin")));
                
            return true;
        }

        public string[] GetTokenNames()
        {
            var tokens = Root.Elements("tokens");
            return tokens.Elements("token").Select(m => m.Attribute("name").Value).ToArray();
        }

        public string[] GetGroups(string token)
        {
            var tokens = Root.Elements("tokens");
            return
                tokens.Elements("token")
                    .Where(m => m.Attribute("name").Value == token)
                    .Elements("groups")
                    .Elements("group")
                    .Select(m => m.Value)
                    .ToArray();
        }

        public object GetUsers(string token)
        {
            var tokens = Root.Elements("tokens");
            return
                tokens.Elements("token")
                    .Where(m => m.Attribute("name").Value == token)
                    .Elements("users")
                    .Elements("user")
                    .Select(m => new { value = m.Value, access = (m.Attribute("allow").Value == "1" || string.Equals(m.Attribute("allow").Value,"true",StringComparison.OrdinalIgnoreCase)) ? true : false })
                    .ToArray();
        }


        private HttpContext CreateContext(string user)
        {
            HttpContext.Current = new HttpContext(
                           new HttpRequest(null, "http://tempuri.org", null),
                           new HttpResponse(null));

            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(user),
                new string[0]
                );
            return HttpContext.Current;
        }
        private void DistroyContext()
        {
            HttpContext.Current = null;
        }

        [TestMethod]
        public void HasDynamicAccess()
        {
            const int maxThread = 2;
            var userName = User1;
            int errorCount = 0;
            var path = AclPath;
            CountdownEvent numberEvent = new CountdownEvent(maxThread);
            CourseType ct1 = new CourseType { CourseTypeId = 1 };
            CourseType ct2 = new CourseType { CourseTypeId = 2 };            
            Action test1 = () =>
            {
                try
                {
                    using (var service = AclService.Create(path))
                    {
                        var res = service.HasDynamicAccess(ct1, userName);
                        Console.WriteLine(res);
                    }

                }
                catch (Exception ex)
                {
                    Interlocked.Increment(ref errorCount);
                    //Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    numberEvent.Signal();
                }

            };
            Action test2 = () =>
            {
                try
                {
                    using (var service = AclService.Create(path))
                    {
                        var res = service.HasDynamicAccess(ct2, userName);
                    }

                }
                catch (Exception ex)
                {
                    Interlocked.Increment(ref errorCount);
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    numberEvent.Signal();
                }

            };
            var tasks = new Task[maxThread];
            for (var i = 0; i < (maxThread/2); i++)
            {
                tasks[(i*2)]=Task.Factory.StartNew(test1);
                tasks[(i * 2) + 1] = Task.Factory.StartNew(test2);                
            }
            numberEvent.Wait();
            Assert.IsTrue(errorCount==0,errorCount +  " error(s) found.");
            Console.WriteLine("Done. number of Erros:" + errorCount.ToString());

        }
        private void Load()
        {
            _root = XElement.Load("ACL.xml");
        }
        private XElement _root;
        private XElement Root
        {
            get
            {
                if (_root == null)
                    Load();
                return _root;
            }
        }
    }
}

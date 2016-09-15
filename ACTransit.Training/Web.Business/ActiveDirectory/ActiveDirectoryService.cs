using System;
using System.Linq;
using ACTransit.DataAccess.ActiveDirectory.Repositories;
using ACTransit.Entities.ActiveDirectory;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Business.Infrastructure;

namespace ACTransit.Training.Web.Business.ActiveDirectory
{
    public class ActiveDirectoryService : ActiveDirectoryBaseService
    {
        private const string ClassName = "ACTransit.Training.Web.Business.ActiveDirectory.ActiveDirectoryService";

        private readonly ActiveDirectoryRepository _repository;

        public ActiveDirectoryService(string activeDirectoryUrl, string user = "", string password = "")
        {
            ActiveDirectoryUrl = activeDirectoryUrl;
            ActiveDirectoryUser = ActiveDirectoryUser;
            ActiveDirectoryPwd = ActiveDirectoryPwd;
            _repository = new ActiveDirectoryRepository(ActiveDirectoryUrl, user, password);
        }

        public User[] GetUsersInGroup(string group, bool recursive = false)
        {
            string key = ClassName + "_UserInGroup" + group + "_Recursive_" + recursive;
            var users = Cache.GetCache(key) as User[] ?? _repository.GetUsersInGroup(group, recursive);
            if (users != null && users.Any())
                Cache.AddCache(key, users, (int)DateTime.Now.TimeUntil(DateTime.Now.EndOfDay()).TotalMinutes);
            return users;
        }

        public Group[] GetGroupsOfUser(string user, bool recursive = false)
        {
            return _repository.GetGroupsOfUser(user, recursive).ToArray();
        }

        public override void RefreshCache()
        {
            //todo implement refresh cache
            Common.Cache.ClearAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                if (_repository != null)
                    _repository.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}

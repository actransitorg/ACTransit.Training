using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using ACTransit.Entities.ActiveDirectory;
using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.DataAccess.ActiveDirectory.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ActiveDirectoryRepository : IDisposable
    {
        // ReSharper disable once InconsistentNaming
        const int NORMAL_ACCOUNT = 0x200;
        // ReSharper disable once InconsistentNaming
        const int PWD_NOTREQD = 0x20;
// ReSharper disable once InconsistentNaming
        const int ADS_UF_ACCOUNTDISABLE = 2;


// ReSharper disable once InconsistentNaming
        private const string LDAP_MATCHING_RULE_BIT_AND = "1.2.840.113556.1.4.803";  //A match is found only if all bits from the attribute match the value. This rule is equivalent to a bitwise AND operator. 
// ReSharper disable once InconsistentNaming
        private const string LDAP_MATCHING_RULE_BIT_OR = "1.2.840.113556.1.4.804";  //A match is found if any bits from the attribute match the value. This rule is equivalent to a bitwise OR operator.
// ReSharper disable once InconsistentNaming
        private const string LDAP_MATCHING_RULE_IN_CHAIN = "1.2.840.113556.1.4.1941";  //This rule is limited to filters that apply to the DN. This is a special "extended match operator that walks the chain of ancestry in objects all the way to the root until it finds a match.


        private bool _disposed;
        private readonly string _domain;
        private readonly string _userName;
        private readonly string _password;
        private AuthenticationTypes _authenticationType;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain">Active directory domain, for example LDAP://DC=xxxxx,DC=yyy</param>
        /// <param name="userName">The user name to connect to Domain controller, if null, current user is used.</param>
        /// <param name="password"></param>        
        public ActiveDirectoryRepository(string domain, string userName = "", string password = "")
        {
            if (string.IsNullOrWhiteSpace(domain))
                throw new ArgumentException("Parameter name \"domain\" can not be empty.");

            _userName = userName;
            _password = password;
            _authenticationType = AuthenticationTypes.Secure;

            _domain = domain.Trim();
            if (!domain.Contains("://"))
            {
                _domain = "LDAP://" + domain;
            }
            _domain = _domain.Substring(_domain.IndexOf("://", StringComparison.Ordinal) + 3);
        }

        public User[] GetUsersInGroup(string groupName, bool recursive=false)
        {       
            var group = GetGroup(groupName);
            if (group == null)
                return null;
            var userFilter = new User();
            string filterStr;
            if (recursive)
                filterStr = CreateFilter(userFilter, String.Format("(memberOf:{0}:={1})",LDAP_MATCHING_RULE_IN_CHAIN,group.DistinguishedName));                
            else
                filterStr = CreateFilter(userFilter, "(memberOf=" + group.DistinguishedName + ")");                
                            
            var result = GetEntities(filterStr, userFilter.GetActiveDirectoryColumns());
            return result.Select(m => m.GetCorrespondingObject() as User).ToArray();
        }

        public Entity[] GetEntitiesInGroup(string groupName, bool recursive = false)
        {
            var group = GetGroup(groupName);
            if (group == null)
                return null;
            var entityFilter = new Entity();
            string filterStr;
            if (recursive)
                filterStr = CreateFilter(entityFilter, String.Format("(memberOf:{0}:={1})", LDAP_MATCHING_RULE_IN_CHAIN, group.DistinguishedName));
            else
                filterStr = CreateFilter(entityFilter, "(memberOf=" + group.DistinguishedName + ")");

            var result = GetEntities(filterStr, entityFilter.GetActiveDirectoryColumns());
            return result.ToArray();
        }

        /// <summary>
        /// Return the user associated with the SamAccountName
        /// </summary>
        /// <param name="samAccountName">SamAccountName of the user</param>
        /// <returns>returns the user. Returns null if no user found.</returns>
        public User GetUser(string samAccountName)
        {
            var users = GetUsers(new User { SamAccountName = samAccountName });
            if (users == null || users.Length == 0)
                return null;
            if (users.Length > 1)
                throw new Exception("Multiple users found with the same SamAccountName \"" + samAccountName + "\"!");
            return users[0];
        }

        public Entity GetEntity(string samAccountName)
        {
            var entities = GetEntities(new Entity { SamAccountName = samAccountName });
            if (entities == null || entities.Length == 0)
                return null;
            if (entities.Length > 1)
                throw new Exception("Multiple entities found with the same SamAccountName \"" + samAccountName + "\"!");
            return entities[0];
        }

        public Group GetGroup(string groupName)
        {
            var groups = GetGroups(new Group { SamAccountName = groupName });
            if (groups == null || groups.Length == 0)
                return null;
            if (groups.Length > 1)
                throw new Exception("Multiple users found with the same SamAccountName \"" + groupName + "\"!");
            return groups[0];
        }

        /// <summary>
        /// Fetch the list of users match the filter. please note it does not returns the contacts.
        /// </summary>
        /// <param name="filter">The filter, returns a new filter with no values set to get the list of all users.</param>
        /// <returns></returns>
        public User[] GetUsers(User filter) 
        {
            if (filter == null)
                return null;

            var filterStr = CreateFilter(filter);

            var result = GetEntities(filterStr, filter.GetActiveDirectoryColumns());
         
            return result.Select(m => m.GetCorrespondingObject() as User).ToArray();
        }

        public Computer[] GetComputers(Computer filter)
        {
            if (filter == null)
                return null;

            var filterStr = CreateFilter(filter);

            var result = GetEntities(filterStr, filter.GetActiveDirectoryColumns());
          
            return result.Select(m => m.GetCorrespondingObject() as Computer).ToArray();
        }

        public Group[] GetGroups(Group filter)
        {
            if (filter == null)
                return null;
      
            var filterStr = CreateFilter(filter);

            var result = GetEntities(filterStr, filter.GetActiveDirectoryColumns());
            return result.Select(m => m.GetCorrespondingObject() as Group).ToArray();
        }

        public Entity[] GetEntities(Entity filter)
        {
            if (filter == null)
                filter=new Entity();

            var filterStr = CreateFilter(filter);

            var result = GetEntities(filterStr, filter.GetActiveDirectoryColumns());
          
            return result.ToArray();
        }

        public void Export(string fileName, Entity[] users)
        {
            string[] columns = (new Entity()).GetActiveDirectoryColumns();
            using (var file = System.IO.File.CreateText(fileName))
            {
                string columnsHeader = "";
                foreach (var c in columns)
                {
                    columnsHeader += "\"" + c + "\",";
                }
                columnsHeader += "Active,Path,SchemaClassName";
                file.WriteLine(columnsHeader);

                foreach (var u in users)
                {
                    string str = "";
                    foreach (var c in columns)
                    {
                        var o = u.GetValue(c);
                        string val = "";
                        if (o != null)
                        {
                            val = o.ToString().Replace("\"", "\"\"");
                        }
                        str += "\"" + val + "\",";
                    }
                    str += "\"" + u.IsActive + "\"";
                    str += ",\"" + u.Path.Replace("\"", "\"\"") + "\"";
                    str += ",\"" + u.SchemaClassName.Replace("\"", "\"\"") + "\"";
                    file.WriteLine(str);
                }
                file.Close();
            }
        }

        public Entity UpdateEntity(string samAccountName, EntityBase entity, string[] columnsToUpdate, bool createIfNotExist = false)
        {
            if (string.IsNullOrWhiteSpace(samAccountName) || entity == null || columnsToUpdate == null || columnsToUpdate.Length == 0)
                throw new Exception("One or more parameteres are invalid.");

            PropertyInfo[] columnsPropsInfos = (new Entity()).GetProperties();

            var tempList = new List<string>();
            foreach (var prop in columnsPropsInfos)
            {
                if (columnsToUpdate.Contains(prop.Name))
                    tempList.Add(prop.GetActiveDirectoryName());
            }
            var adColumnsToUpdate = tempList.ToArray();


            if (string.IsNullOrWhiteSpace(samAccountName) || entity == null || adColumnsToUpdate == null || adColumnsToUpdate.Length == 0)
                throw new Exception("One or more parameteres are invalid.");

            
            string[] columns = (new Entity()).GetActiveDirectoryColumns();




            bool isNew = false;
            string filterStr = "(&(objectClass=user)(|{0} ) )";
            var conditions = new StringBuilder();
            conditions.Append(" (").Append("SamAccountName").Append("=").Append(samAccountName).Append(")");

            filterStr = string.Format(filterStr, conditions);

            using (var deParent = CreateDirectoryEntry())
            {
                deParent.AuthenticationType = _authenticationType;
                using (var ds = new DirectorySearcher(deParent, filterStr, columns, SearchScope.Subtree))
                {

                    ds.PageSize = 1000;
                    using (SearchResultCollection src = ds.FindAll())
                    {

                        if (src.Count > 1)
                            throw new Exception("More than one user found with the same SamAccountName:" + samAccountName);

                        DirectoryEntry userde;
                        if (src.Count == 0)
                        {
                            if (createIfNotExist)
                            {
                                isNew = true;
                                var p = CreatePath(entity.Path);
                                if (p == null)
                                    throw new Exception("Could not create path for " + entity.Path);

                                userde = p.Children.Add("CN=" + samAccountName, entity.SchemaClassName);

                                int accVal = NORMAL_ACCOUNT | PWD_NOTREQD;
                                if (entity.IsActive.HasValue && !entity.IsActive.Value)
                                    accVal = accVal | ADS_UF_ACCOUNTDISABLE;

                                //accVal = accVal & ~ADS_UF_ACCOUNTDISABLE;  To Enable it.
                                userde.Properties["sAMAccountName"].Value = samAccountName;
                                userde.Properties["userAccountControl"].Value = accVal;
                            }
                            else
                                throw new Exception("LanId: " + samAccountName + " not found.");
                        }
                        else
                            userde = src[0].GetDirectoryEntry();

                        foreach (PropertyInfo prop in columnsPropsInfos)
                        {
                            if (prop.IsActiveDirectory() && prop.UpdatableOnActiveDirectory())
                            {
                                string propName = prop.Name;
                                string col = prop.GetActiveDirectoryName();
                                var updateType = prop.UpdateTypeOnActiveDirectory();
                                if (adColumnsToUpdate.Contains(col))
                                {
                                    if (updateType == ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.New && !isNew)
                                        continue;
                                    if (updateType == ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.Na ||
                                        updateType == ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.Rename)
                                        continue;
                                    var val = entity.GetValue(propName);
                                    if (val != null && !string.IsNullOrWhiteSpace(val.ToString()))
                                    {
                                        if (userde.Properties.Contains(col))
                                            userde.Properties[col].Value = val;
                                        else
                                            userde.Properties[col].Add(val);
                                    }
                                    else
                                    {
                                        if (userde.Properties.Contains(col))
                                            userde.Properties[col].Clear();
                                    }
                                }
                            }

                        }                   

                        userde.CommitChanges();

                        if (adColumnsToUpdate.Contains("Name") &&
                            !string.IsNullOrWhiteSpace(entity.Name) &&
                            userde.Name != entity.Name)
                        {
                            string name = entity.Name;
                            if (!entity.Name.Trim().StartsWith("cn=", StringComparison.OrdinalIgnoreCase))
                                name = "CN=" + name;

                            if (userde.Name != name) userde.Rename(name);
                        }
                        else if (adColumnsToUpdate.Contains("Cn") && 
                            (!userde.Properties.Contains("Cn") || 
                            (string)userde.Properties["Cn"].Value!=entity.Cn))
                            userde.Rename("CN=" + entity.Cn);
                    }
                    return GetEntity(samAccountName);
                }
            }
        }

        public List<Group> GetGroupsOfUser(string samAccountName, bool recursive=false)
        {
            var user = GetUser(samAccountName);
            if (user == null)
                return null;
            var groupFilter = new Group();
            string filterStr;
            if (recursive)
                filterStr = CreateFilter(groupFilter, String.Format("(member:{0}:={1})", LDAP_MATCHING_RULE_IN_CHAIN, user.DistinguishedName));                
            else
                filterStr = CreateFilter(groupFilter, String.Format("(member={0})", user.DistinguishedName));
            var result = GetEntities(filterStr, groupFilter.GetActiveDirectoryColumns());
            return result.Select(m => m.GetCorrespondingObject() as Group).ToList();            
        }
            
        public string Domain
        {
            get { return "LDAP://" + _domain; }
        }

        private List<Entity> GetEntities(string filterStr, string[] columns)
        {
            var result = new List<Entity>();
            using (var deParent = CreateDirectoryEntry())
            {
                deParent.AuthenticationType = _authenticationType;
                using (var ds = new DirectorySearcher(deParent, filterStr, columns, SearchScope.Subtree))
                {

                    ds.PageSize = 1000;
                    using (SearchResultCollection src = ds.FindAll())
                    {
                        foreach (SearchResult sr in src)
                        {
                            var u = CreateEntity(sr);
                            if (u != null) result.Add(u);
                        }
                    }
                }
            }
            return result;
        }

        private DirectoryEntry CreateDirectoryEntry()
        {
            return string.IsNullOrWhiteSpace(_userName) ? new DirectoryEntry(Domain) : new DirectoryEntry(Domain, _userName, _password);
        }

        private DirectoryEntry CreatePath(string path)
        {
            DirectoryEntry retVal;
            var paths = new List<string>(path.Split(','));

            bool dispose = true;
            DirectoryEntry parentEntry = null;
            try
            {
                paths.Reverse();
                parentEntry = CreateDirectoryEntry();                
                retVal= CreatePath(parentEntry, paths.ToArray(), 0);

                if (parentEntry.Equals(retVal))  // for any reason, we don't want our return path to be disposed. 
                    dispose = false;
            }
            finally
            {
                if (dispose && parentEntry!=null)
                    parentEntry.Dispose();
            }
            return retVal;
        }

        private DirectoryEntry CreatePath(DirectoryEntry parent, string[] structures, int index)
        {
            if (index < structures.Length - 1)
            {
                if (structures[index].Trim().StartsWith("DC="))  //As long as it is DC, we should keep the parent, because we are not going to create another one!
                    return CreatePath(parent, structures, index + 1);

                bool commit = false;
                DirectoryEntry newRoot = null;
                try
                {
                    newRoot = parent.Children.Find(structures[index]);
                }
                catch (DirectoryServicesCOMException ex)
                {
                    //http://msdn.microsoft.com/en-us/library/39zxbb5w%28v=vs.110%29.aspx
                    if (!ex.Message.Contains("There is no such object on the server"))
                        throw;
                }


                if (newRoot == null)
                {
                    if (structures[index].Trim().StartsWith("OU="))
                        newRoot = parent.Children.Add(structures[index], "OrganizationalUnit");
                    else  // if it does not start with OU, it means it does not exist! at this point, we don't want to create anything but OU. so we return the parent.
                        throw new Exception("OU not found!");
                    commit = true;
                }


                bool dispose = true;
                try
                {
                    if (commit) newRoot.CommitChanges();
                    var retVal = CreatePath(newRoot, structures, index + 1);

                    if (newRoot.Equals(retVal)) // for any reason, we don't want our return path to be disposed. 
                        dispose = false;
                    return retVal;

                }
                finally
                {
                    if (dispose)
                        newRoot.Dispose();
                }

            }
            return parent;
        }

        private Entity CreateEntity(SearchResult sr)
        {
            Entity retVal;
            using (var de = sr.GetDirectoryEntry())
            {
                IEnumerable<PropertyInfo> properties;                
                
                retVal = new Entity(de.Name,sr.Path, de.SchemaClassName);

                switch (de.SchemaClassName)
                {
                    case "computer":
                        properties = (new Computer()).GetProperties();
                        break;
                    case "user":
                        properties = (new User()).GetProperties();
                        break;
                    case "group":
                        properties = (new Group()).GetProperties();
                        break;
                    default:
                        properties = retVal.GetProperties();
                        break;
                }

                foreach (var prop in properties)
                {
                    string col = prop.GetActiveDirectoryName();
                    if (string.IsNullOrWhiteSpace(col)) continue;
                    if (sr.Properties.Contains(col))
                        retVal.SetValue(prop.Name, sr.Properties[col][0]);
                }
                retVal.IsActive = (!Convert.ToBoolean(retVal.UserAccountControl & 0x0002));                
            }
            return retVal;
        }



        private string CreateFilter<T>(T filter, string additionalFilter="") where T:EntityBase
        {
            var conditions = new StringBuilder();
            string baseFilter;
            string activeFilter = "";

            if (filter.IsActive.HasValue)
            {
                if (filter.IsActive.Value)
                    activeFilter="(!userAccountControl:1.2.840.113556.1.4.803:=2)";
                else
                    activeFilter="(userAccountControl:1.2.840.113556.1.4.803:=2)";
            }

            if (filter is User)
            {
                baseFilter = "(objectCategory=person)(objectClass=user)" + activeFilter;
            }
            else if (filter is Computer)
            {
                baseFilter = "(objectCategory=computer)(objectClass=user)" + activeFilter;
            }
            else if (filter is Group)
            {
                baseFilter = "(objectClass=group)" + activeFilter;
            }
            else
            {
                baseFilter = "(|(objectClass=user)(objectClass=group))" + activeFilter;
            }

            foreach (var col in filter.GetActiveDirectoryColumnsForFilter())
            {
                var obj = filter.GetValue(col);
                if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    conditions.Append(" (").Append(col).Append("=").Append(obj).Append(")");
                }
            }

                

            if (conditions.Length == 0)
                return string.Format("(&{0} {1})", baseFilter, additionalFilter);

            return string.Format("(&{0}(|{1}){2})", baseFilter, conditions, additionalFilter);
        }
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
            }

            _disposed = true;
        }

    }
}

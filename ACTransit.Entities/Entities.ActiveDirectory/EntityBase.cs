using System;
using System.Collections.Generic;
using System.Reflection;
using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.Entities.ActiveDirectory
{
    public abstract class EntityBase
    {        

        protected EntityBase()
            : this("", "", "")
        {}

        protected EntityBase(string name, string path, string schemaClassName)
        {
            Name = name;
            Path = path;
            SchemaClassName = schemaClassName;
        }


        /// <summary>
        /// Lan Id/Logon Name (Pre windows 200)
        /// </summary>
        [ActiveDirectoryProperty(Updatable = false)]
        public string SamAccountName { get; set; }

        /// <summary>
        /// For inernal use only.
        /// </summary>
        [ActiveDirectoryProperty(Updatable= false, UseAsFilter = false)]
        public long? SamAccountType { get; set; }

        [ActiveDirectoryProperty]
        public string DisplayName { get; set; }

        /// <summary>
        /// For internal use only. It should start with "CN="
        /// </summary>
        [ActiveDirectoryProperty(UpdateType = ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.Rename)]
        public string Name { get; private set; }

        /// <summary>
        /// Full Name (First Middle Last)
        /// </summary>
        [ActiveDirectoryProperty(UpdateType = ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.Rename)]
        public string Cn { get; set; }


        /// <summary>
        /// Account Status (for internal use only)
        /// </summary>
        [ActiveDirectoryProperty(Updatable = false, UseAsFilter = false)]
        public int? UserAccountControl { get; set; }

        /// <summary>
        /// Uniq string to distinguish an object in A.D (for internal use only)
        /// </summary>
        [ActiveDirectoryProperty(Updatable = false)]
        public string DistinguishedName { get; set; }

        /// <summary>
        /// Active Diretory Path of the user. for example LDAP://CN=User Name,OU=OU2,OU=OU1,DC=somthing,DC=com (for internal use only)
        /// </summary>        
        [ActiveDirectoryProperty(Updatable = false, UseAsFilter = false)]        
        public string Path { get; private set; }

        [ActiveDirectoryProperty(Updatable= false, UseAsFilter = false)]
        public string SchemaClassName { get; private set; }

        /// <summary>
        /// Updated On
        /// </summary>
        [ActiveDirectoryProperty(UseAsFilter = false)]
        public DateTime? WhenChanged { get; set; }

        [ActiveDirectoryProperty(UseAsFilter = false)]
        public DateTime? WhenCreated { get; set; }


        public bool IsUser
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SchemaClassName) &&
                       SchemaClassName.Equals("user", StringComparison.OrdinalIgnoreCase);
            }
        }
        public bool IsGroup
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SchemaClassName) &&
                       SchemaClassName.Equals("group", StringComparison.OrdinalIgnoreCase);
            }
        }
        public bool IsComputer
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SchemaClassName) &&
                       SchemaClassName.Equals("computer", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// It checks whether account is enbled or not.
        /// </summary>
        public bool? IsActive { get; set; }

        public object GetValue(string propertyName)
        {
            return GetType().GetProperty(propertyName).GetValue(this);
        }

        public void SetValue(string propertyName, object value)
        {
            //if (string.Equals(propertyName, "path", StringComparison.OrdinalIgnoreCase) ||
            //    string.Equals(propertyName, "Name", StringComparison.OrdinalIgnoreCase) ||
            //    string.Equals(propertyName, "SchemaClassName", StringComparison.OrdinalIgnoreCase))
            //    return;

            var prop = GetType().GetProperty(propertyName);
            if (!prop.CanWrite) return;

            Type t = prop.PropertyType;

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value!=null)
                    GetType().GetProperty(propertyName).SetValue(this, Convert.ChangeType(value, Nullable.GetUnderlyingType(t)));
                else
                    GetType().GetProperty(propertyName).SetValue(this, null);
            }
            else
            {
                GetType().GetProperty(propertyName).SetValue(this, Convert.ChangeType(value, t));
            }


        }


        public PropertyInfo[] GetProperties()
        {
            return GetType().GetProperties();
        }

        public string[] GetActiveDirectoryColumns()
        {
            var columns = new List<string>();
            foreach (var propertyInfo in GetProperties())
            {
                if (propertyInfo.IsActiveDirectory())
                    columns.Add(propertyInfo.GetActiveDirectoryName());
            }
            return columns.ToArray();
        }

        public string[] GetActiveDirectoryColumnsForFilter()
        {
            var columns = new List<string>();
            foreach (var propertyInfo in GetProperties())
            {
                if (propertyInfo.IsActiveDirectory() && propertyInfo.UseAsFilterOnActiveDirectory())
                    columns.Add(propertyInfo.GetActiveDirectoryName());
            }
            return columns.ToArray();
        }

    }
}

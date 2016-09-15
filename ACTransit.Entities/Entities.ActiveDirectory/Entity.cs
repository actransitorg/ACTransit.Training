using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.Entities.ActiveDirectory
{
    public class Entity:EntityBase
    {
   
        /// <summary>
        /// Create a user with Path="" and schemaClassName=""
        /// </summary>
        public Entity() : base("","","") { }

        public Entity(string name,string path, string schemaClassName) : base(name,path, schemaClassName) { }

        /// <summary>
        /// Last Name
        /// </summary>
        [ActiveDirectoryProperty]
        public string Sn { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [ActiveDirectoryProperty]
        public string GivenName { get; set; }

        /// <summary>
        /// Office Telephone
        /// </summary>
        [ActiveDirectoryProperty]
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Email Address
        /// </summary>
        [ActiveDirectoryProperty]
        public string Mail { get; set; }

        [ActiveDirectoryProperty(Updatable = false)]
        public string MailNickname { get; set; }

        /// <summary>
        /// Lan Id/Logon Name, Format: User@xxx.com
        /// </summary>
        [ActiveDirectoryProperty]
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// Employee description
        /// </summary>
        [ActiveDirectoryProperty]
        public string Description { get; set; }

        /// <summary>
        /// Middle initial
        /// </summary>
        [ActiveDirectoryProperty]
        public string Initials { get; set; }

        /// <summary>
        /// Suffix
        /// </summary>
        [ActiveDirectoryProperty]
        public string WwwHomepage { get; set; }

        /// <summary>
        /// Prefered first name
        /// </summary>
        [ActiveDirectoryProperty]
        public string Info { get; set; }

        /// <summary>
        /// Job title
        /// </summary>
        [ActiveDirectoryProperty]
        public string Title { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        [ActiveDirectoryProperty]
        public string Department { get; set; }

        /// <summary>
        /// Office location
        /// </summary>
        [ActiveDirectoryProperty]
        public string PhysicalDeliveryOfficeName { get; set; }

        /// <summary>
        /// Mobile phone
        /// </summary>
        [ActiveDirectoryProperty]
        public string Mobile { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        [ActiveDirectoryProperty]
        public string Company { get; set; }

        /// <summary>
        /// Manager's DistinguishedName
        /// </summary>
        [ActiveDirectoryProperty(PropertyName = "Manager")]
        public string ManagerDistinguishedName { get; set; }

        /// <summary>
        /// Manager's user
        /// </summary>
        public User Manager { get; set; }

        /// <summary>
        /// Update By
        /// </summary>
        [ActiveDirectoryProperty(UseAsFilter = false)]
        public string UpdatedBy { get; set; }

        [ActiveDirectoryProperty]
        public string EmployeeId { get; set; }

        [ActiveDirectoryProperty(UseAsFilter = false, PropertyName = "groupType")]
        public int GroupType { get; set; }

        public bool IsSecurityGroup
        {
            get
            {
                if (!IsGroup || GroupType == 2)
                    return false;                
                return true;
            }
        }

        public EntityBase GetCorrespondingObject()
        {
            EntityBase retVal;
            //string[] columns;
            switch (SchemaClassName)
            {
                case "user":
                    //columns = User.Columns;
                    retVal = new User(Name,Path);                                        
                    break;
                case "computer":
                    //columns = Computer.Columns;
                    retVal = new Computer(Name,Path);
                    break;
                case "group":
                    //columns = Group.Columns;
                    retVal = new Group(Name,Path);
                    break;
                default:
                    return this;
            }

            //foreach (var col in columns)
            //    retVal.SetValue(col, GetValue(col));
            //retVal.IsActive = IsActive;

            foreach (var prop in retVal.GetProperties())
            {
                if (prop.CanWrite)
                    retVal.SetValue(prop.Name, GetValue(prop.Name));
            }
                
            

            return retVal;
        }
    }
}

using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.Entities.ActiveDirectory
{

    public class User : EntityBase
    {       

        /// <summary>
        /// Create a user with Path="" and schemaClassName="user"
        /// </summary>
        public User() : base("","","user") { }

        public User(string name, string path) : base(name, path, "user") { }


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
        [ActiveDirectoryProperty(UpdateType = ActiveDirectoryPropertyAttribute.ActiveDirectoryUpdateType.New)]
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

    }
}

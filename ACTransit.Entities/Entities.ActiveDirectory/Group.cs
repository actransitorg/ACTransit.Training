using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.Entities.ActiveDirectory
{
    public class Group : EntityBase
    {

        /// <summary>
        /// Create a user with Path="" and schemaClassName="computer"
        /// </summary>
        public Group() : base("","","Group") { }

        public Group(string name, string path) : base(name, path, "Group") { }

        /// <summary>
        /// Update By
        /// </summary>
        [ActiveDirectoryProperty(UseAsFilter = false)]
        public string UpdatedBy { get; set; }

        [ActiveDirectoryProperty(UseAsFilter = false,PropertyName = "groupType")]
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

    }
}

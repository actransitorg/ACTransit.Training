using ACTransit.Entities.ActiveDirectory.Infrastructure;

namespace ACTransit.Entities.ActiveDirectory
{
    public class Computer : EntityBase
    {      

        /// <summary>
        /// Create a user with Path="" and schemaClassName="computer"
        /// </summary>
        public Computer() : base("","","computer") { }

        public Computer(string name,string path) : base(name,path, "computer") { }

        /// <summary>
        /// Update By
        /// </summary>
        [ActiveDirectoryProperty(UseAsFilter = false)]
        public string UpdatedBy { get; set; }

    }
}

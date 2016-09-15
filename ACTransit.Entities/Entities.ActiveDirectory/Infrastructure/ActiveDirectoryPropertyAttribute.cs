using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACTransit.Entities.ActiveDirectory.Infrastructure
{
    public class ActiveDirectoryPropertyAttribute:Attribute
    {
        public ActiveDirectoryPropertyAttribute()
        {
            Updatable = true;
            UpdateType=ActiveDirectoryUpdateType.Update;
            UseAsFilter = true;
        }

        /// <summary>
        /// the Name that should be mapped in the active directory.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Specify whether can be used as filter to search in active directory or not.
        /// </summary>
        public bool UseAsFilter { get; set; }

        /// <summary>
        /// Specify whether it can be updated in active directory or not.
        /// </summary>
        public bool Updatable{ get; set; }

        /// <summary>
        /// How it should be updated in active directory
        /// </summary>
        public ActiveDirectoryUpdateType UpdateType { get; set; }

        public enum ActiveDirectoryUpdateType
        {
            Na,
            /// <summary>
            /// Changes should be submited.
            /// </summary>
            Update,

            /// <summary>
            /// Changes should be submited only when the object is created.
            /// </summary>
            New,
            /// <summary>
            /// Changes should be renamed.
            /// </summary>
            Rename,

        }
    }

 

}

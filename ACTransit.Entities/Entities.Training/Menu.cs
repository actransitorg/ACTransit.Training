//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ACTransit.Entities.Training
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public Menu()
        {
            this.Menu1 = new HashSet<Menu>();
        }
    
        public long MenuId { get; set; }
        public Nullable<long> ParentMenuId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int SortOrder { get; set; }
        public bool NewWindow { get; set; }
        public string AccessToken { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<Menu> Menu1 { get; set; }
        public virtual Menu Menu2 { get; set; }
    }
}
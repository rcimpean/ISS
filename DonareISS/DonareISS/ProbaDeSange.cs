//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DonareISS
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProbaDeSange
    {
        public int Id_ProbaDeSange { get; set; }
        public Nullable<System.DateTime> DataProba { get; set; }
        public string StatusProba { get; set; }
    
        public virtual Donator Donator { get; set; }
        public virtual PungaDeSange PungaDeSange { get; set; }
    }
}

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
    
    public partial class Stoc
    {
        public int Id_Stoc { get; set; }
        public Nullable<int> CantitateTrombocite { get; set; }
        public Nullable<int> CantitateGlobuleRosii { get; set; }
        public Nullable<int> CantitatePlasma { get; set; }
    
        public virtual OperatorCentru OperatorCentru { get; set; }
    }
}
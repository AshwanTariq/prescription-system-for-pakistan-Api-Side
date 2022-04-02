//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rxApi.Models.EMRModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prescription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prescription()
        {
            this.RxHaveDrugs = new HashSet<RxHaveDrugs>();
        }
    
        public int rxid { get; set; }
        public Nullable<System.DateTime> rxDate { get; set; }
        public string DocUName { get; set; }
        public string PharmacyUName { get; set; }
        public string PatientUName { get; set; }
        public Nullable<int> rxStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RxHaveDrugs> RxHaveDrugs { get; set; }
    }
}
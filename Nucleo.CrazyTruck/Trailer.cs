//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nucleo.CrazyTruck
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trailer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trailer()
        {
            this.Flete = new HashSet<Flete>();
        }
    
        public int id { get; set; }
        public string matricula { get; set; }
        public string modelo { get; set; }
        public string anio { get; set; }
        public string color { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flete> Flete { get; set; }
    }
}

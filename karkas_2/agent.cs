//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace karkas_2
{
    using System;
    using System.Collections.Generic;
    
    public partial class agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public agent()
        {
            this.product_agent = new HashSet<product_agent>();
        }
    
        public int ID { get; set; }
        public string Type_agent { get; set; }
        public string Name_agent { get; set; }
        public string Pochta { get; set; }
        public Nullable<double> Phone { get; set; }
        public string Logo { get; set; }
        public string Adress { get; set; }
        public Nullable<double> Prioritet { get; set; }
        public string Director { get; set; }
        public Nullable<double> INN { get; set; }
        public Nullable<double> KPP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_agent> product_agent { get; set; }

        public int Sale { get; set; }
        public int Skid { get; set; }
        public string Foreground { get; internal set; }
    }
}

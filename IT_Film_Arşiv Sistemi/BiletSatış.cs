//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IT_Film_Arşiv_Sistemi
{
    using System;
    using System.Collections.Generic;
    
    public partial class BiletSatış
    {
        public int ID { get; set; }
        public Nullable<int> FilmID { get; set; }
        public Nullable<int> SalonID { get; set; }
        public Nullable<int> SeansID { get; set; }
        public string KoltukNo { get; set; }
        public Nullable<int> MusteriID { get; set; }
        public Nullable<int> UcretID { get; set; }
    
        public virtual Film Film { get; set; }
        public virtual KullanıcıGiris KullanıcıGiris { get; set; }
        public virtual Salon Salon { get; set; }
        public virtual Sean Sean { get; set; }
        public virtual Ucret Ucret { get; set; }
    }
}

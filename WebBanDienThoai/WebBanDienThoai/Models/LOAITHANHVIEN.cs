//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOAITHANHVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAITHANHVIEN()
        {
            this.LOAITHANHVIEN_QUYEN = new HashSet<LOAITHANHVIEN_QUYEN>();
            this.THANHVIEN = new HashSet<THANHVIEN>();
        }
    
        public int MaLoaiTV { get; set; }
        public string TenLoai { get; set; }
        public Nullable<int> UuDai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOAITHANHVIEN_QUYEN> LOAITHANHVIEN_QUYEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THANHVIEN> THANHVIEN { get; set; }
    }
}
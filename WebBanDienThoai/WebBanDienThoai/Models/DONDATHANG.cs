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
    
    public partial class DONDATHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONDATHANG()
        {
            this.CHITIETDONDATHANG = new HashSet<CHITIETDONDATHANG>();
        }
    
        public int MaDDH { get; set; }
        public Nullable<int> MaKH { get; set; }
        public System.DateTime NgayDatHang { get; set; }
        public Nullable<System.DateTime> NgayGiao { get; set; }
        public bool DaThanhToan { get; set; }
        public string QuaTang { get; set; }
        public string TinhTrang { get; set; }
        public Nullable<bool> DaXoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDATHANG> CHITIETDONDATHANG { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
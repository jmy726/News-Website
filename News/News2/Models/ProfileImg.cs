//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace News2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProfileImg
    {
        public int imgId { get; set; }
        public byte[] img { get; set; }
        public string jourId { get; set; }
    
        public virtual Journalist Journalist { get; set; }
    }
}

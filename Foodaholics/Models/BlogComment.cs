//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Foodaholics.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlogComment
    {
        public int id { get; set; }
        public int BlogId { get; set; }
        public string Comment { get; set; }
        public System.DateTime Posted { get; set; }
        public int WriterId { get; set; }
    
        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }
    }
}

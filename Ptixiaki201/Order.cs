//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ptixiaki201
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public Nullable<int> Quantity { get; set; }
        public double OverallPrice { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string Status { get; set; }
        public Nullable<int> Room { get; set; }
        public string Ordered { get; set; }
        public string Comments { get; set; }
        public string EmailSent { get; set; }
    }
}

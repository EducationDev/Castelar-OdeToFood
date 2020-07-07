using System;
using System.Collections.Generic;

namespace OdeToFood.Data.Model
{
    
    
    public class CartItem : IdentityBase
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
       
    
        public virtual Cart Cart { get; set; }
    }
}

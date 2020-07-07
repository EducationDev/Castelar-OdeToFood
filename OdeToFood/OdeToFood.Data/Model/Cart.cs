using System;
using System.Collections.Generic;

namespace OdeToFood.Data.Model
{
    public class Cart : IdentityBase
    {
        public Cart()
        {
            this.CartItem = new HashSet<CartItem>();
        }
    
        public string Cookie { get; set; }
        public System.DateTime CartDate { get; set; }
        public int ItemCount { get; set; }
        
    
        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data.Model
{
    public class Artist : IdentityBase
    {

        [Required]
        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Apellido")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string LifeSpan { get; set; }

        [DisplayName("País")]
        [Required]
        public string Country { get; set; }

        [DisplayName("Descripción")]
        [Required]
        public string Description { get; set; }

        [DisplayName("Total")]
        public int TotalProducts { get; set; }

        [NotMapped]
        public string FullName
        {
            get 
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        public virtual ICollection<Product> Product { get; set; }


    }
}

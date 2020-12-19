using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customModelValidation.Models
{

    [Table("posMagaza", Schema = "dbo")]
    public class posMagaza
    {
        [Key]
        public int mekanID { get; set; }

        public string mekanAd { get; set; }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab4P1.Models
{
    public class SportClub
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string ID { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public Decimal Fee { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}

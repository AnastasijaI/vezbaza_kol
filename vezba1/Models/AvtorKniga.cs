using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vezba1.Models
{
    public class AvtorKniga
    {
          [Key]
          public int Id { get; set; }
          [ForeignKey("Avtor")]
          public int AvtorId { get; set; }
          public Avtor? Avtor { get; set; }
          [ForeignKey("Kniga")]
          public int KnigaId { get; set; }
          public Kniga? Kniga { get; set; }
    }
}

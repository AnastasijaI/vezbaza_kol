using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace vezba1.Models
{
    public class Avtor
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Ime { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Prezime { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Pol { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Nacionalnost { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DatumRagjanje { get; set; }
        [NotMapped]
        public string FullName => $"{Ime} {Prezime}";
       
        public ICollection<AvtorKniga>? AvtorKnigi { get; set; }
    }
}

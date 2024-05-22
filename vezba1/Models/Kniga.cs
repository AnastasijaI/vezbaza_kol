using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vezba1.Models
{
    public class Kniga
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Naslov { get; set; }
        public int? Godina { get; set; } 
        public int? BrStrani { get; set; }
        public string? Opis { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Zanr { get; set;}
        public int? Tirazh { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Izdavac { get; set; }
        public string? SlikaUrl { get; set; }
        [NotMapped]
        public IFormFile? SlikaFile{ get; set; }
        public ICollection<AvtorKniga>? AvtorKnigi { get; set; }
    }
}

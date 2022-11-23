using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvc_MongoDB.Models
{
    [Table("Usuario")] //nome como está no banco
    public class Usuario
    {

        [Column("Id")]
        [Display(Name="Código")]
        public Guid Id { get; set; }
        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

    }
}

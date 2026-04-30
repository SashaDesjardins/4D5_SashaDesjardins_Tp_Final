using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Bataille", Schema = "Anormalites")]
public partial class Bataille
{
    [Key]
    [Column("BatailleID")]
    public int BatailleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [StringLength(500)]
    public string But { get; set; } = null!;

    [ForeignKey("BatailleId")]
    [InverseProperty("Batailles")]
    public virtual ICollection<Anormalite> Anormalites { get; set; } = new List<Anormalite>();

    [ForeignKey("BatailleId")]
    [InverseProperty("Batailles")]
    public virtual ICollection<Personnage> Personnages { get; set; } = new List<Personnage>();
}

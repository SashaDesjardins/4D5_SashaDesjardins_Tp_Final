using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Faction", Schema = "Personnages")]
public partial class Faction
{
    [Key]
    [Column("FactionID")]
    public int FactionId { get; set; }

    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [StringLength(15)]
    public string Type { get; set; } = null!;

    [StringLength(250)]
    public string? Description { get; set; }

    [InverseProperty("Faction")]
    public virtual ICollection<Personnage> Personnages { get; set; } = new List<Personnage>();
}

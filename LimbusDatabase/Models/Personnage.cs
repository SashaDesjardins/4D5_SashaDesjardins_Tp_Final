using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Personnage", Schema = "Personnages")]
public partial class Personnage
{
    [Key]
    [Column("PersonnageID")]
    public int PersonnageId { get; set; }

    [StringLength(50)]
    public string? Nom { get; set; }

    [StringLength(50)]
    public string Prenom { get; set; } = null!;

    [Column("FactionID")]
    public int FactionId { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string District { get; set; } = null!;

    public bool EnVie { get; set; }

    [InverseProperty("Personnage")]
    public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

    [ForeignKey("FactionId")]
    [InverseProperty("Personnages")]
    public virtual Faction Faction { get; set; } = null!;

    [ForeignKey("PersonnageId")]
    [InverseProperty("Personnages")]
    public virtual ICollection<Bataille> Batailles { get; set; } = new List<Bataille>();
}

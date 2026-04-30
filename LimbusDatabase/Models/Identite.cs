using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Keyless]
[Table("Identite", Schema = "Personnages")]
public partial class Identite
{
    [Column("IdentiteID")]
    public int IdentiteId { get; set; }

    [StringLength(50)]
    public string Titre { get; set; } = null!;

    public DateOnly DateTrouve { get; set; }

    public int Rarete { get; set; }

    [Column("PersonnageID")]
    public int PersonnageId { get; set; }

    [ForeignKey("PersonnageId")]
    public virtual Personnage Personnage { get; set; } = null!;
}

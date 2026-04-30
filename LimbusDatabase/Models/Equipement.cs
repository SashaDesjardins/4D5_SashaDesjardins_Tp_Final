using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Equipement", Schema = "Anormalites")]
public partial class Equipement
{
    [Key]
    [Column("EquipementID")]
    public int EquipementId { get; set; }

    [StringLength(25)]
    public string Nom { get; set; } = null!;

    [StringLength(10)]
    public string Type { get; set; } = null!;

    [StringLength(250)]
    public string Effets { get; set; } = null!;

    [StringLength(5)]
    public string Classification { get; set; } = null!;

    [Column("AnormaliteID")]
    public int AnormaliteId { get; set; }

    [Column("PersonnageID")]
    public int? PersonnageId { get; set; }

    [ForeignKey("AnormaliteId")]
    [InverseProperty("Equipements")]
    public virtual Anormalite Anormalite { get; set; } = null!;

    [ForeignKey("PersonnageId")]
    [InverseProperty("Equipements")]
    public virtual Personnage? Personnage { get; set; }
}

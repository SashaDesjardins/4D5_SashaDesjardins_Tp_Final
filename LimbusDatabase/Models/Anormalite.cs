using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Anormalite", Schema = "Anormalites")]
[Index("Nom", Name = "UC_Anormalite_Nom", IsUnique = true)]
public partial class Anormalite
{
    [Key]
    [Column("AnormaliteID")]
    public int AnormaliteId { get; set; }

    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [StringLength(5)]
    public string Classification { get; set; } = null!;

    [StringLength(20)]
    public string Origine { get; set; } = null!;

    [StringLength(500)]
    public string Apparence { get; set; } = null!;

    [InverseProperty("Anormalite")]
    public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();

    [ForeignKey("AnormaliteId")]
    [InverseProperty("Anormalites")]
    public virtual ICollection<Bataille> Batailles { get; set; } = new List<Bataille>();
}

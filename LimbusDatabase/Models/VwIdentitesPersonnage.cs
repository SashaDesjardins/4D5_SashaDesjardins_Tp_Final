using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Keyless]
public partial class VwIdentitesPersonnage
{
    [StringLength(101)]
    public string Nom { get; set; } = null!;

    [Column("Nombre d'identite")]
    public int? NombreDIdentite { get; set; }
}

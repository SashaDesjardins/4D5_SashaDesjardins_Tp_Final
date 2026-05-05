using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Keyless]
public partial class VwAnormaliteBataille
{
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("Nombre de participations")]
    public int? NombreDeParticipations { get; set; }
}

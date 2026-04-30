using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LimbusDatabase.Models;

[Table("Utilisateur", Schema = "Utilisateurs")]
public partial class Utilisateur
{
    [Key]
    [Column("UtilisateurID")]
    public int UtilisateurId { get; set; }

    [StringLength(50)]
    public string Pseudonyme { get; set; } = null!;

    [MaxLength(32)]
    public byte[] MotDePasseHache { get; set; } = null!;

    [MaxLength(16)]
    public byte[] MdpSel { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;
}

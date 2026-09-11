using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Models;

[Index("IdUsuario", "IdProduto", Name = "UQ_Avaliacao_Usuario_Produto", IsUnique = true)]
public partial class Avaliacao
{
    [Key]
    public Guid IdAvaliacao { get; set; }

    public Guid IdUsuario { get; set; }

    public Guid IdProduto { get; set; }

    public int Nota { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comentario { get; set; }

    public bool Situacao { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? MotivoOcultacao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataCriacao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataAlteracao { get; set; }

    [ForeignKey("IdProduto")]
    [InverseProperty("Avaliacao")]
    public virtual Produto IdProdutoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("Avaliacao")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

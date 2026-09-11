using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Models;

public partial class Produto
{
    [Key]
    public Guid IdProduto { get; set; }

    public Guid IdCategoria { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Imagem { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string DescricaoCurta { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string DescricaoLonga { get; set; } = null!;

    public bool Disponibilidade { get; set; }

    public bool Situacao { get; set; }

    [InverseProperty("IdProdutoNavigation")]
    public virtual ICollection<Avaliacao> Avaliacao { get; set; } = new List<Avaliacao>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Produto")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
}

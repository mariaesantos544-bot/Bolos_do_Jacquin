using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bolos_do_Jacquin.Models;

[Index("Nome", Name = "UQ__Categori__7D8FE3B2582795A3", IsUnique = true)]
public partial class Categoria
{
    [Key]
    public Guid IdCategoria { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [InverseProperty("IdCategoriaNavigation")]
    public virtual ICollection<Produto> Produto { get; set; } = new List<Produto>();
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class Categoria
    {

        public int CategoriaId { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome da categoria não pode exceder 80 caracteres")]

        public string CategoriaName { get; set; }

        public List<veiculosParaVenda> veiculosParaVenda { get; set; }

    }
}

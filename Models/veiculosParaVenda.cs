using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace final.Models
{
    public class veiculosParaVenda
    {

        public int Id { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome da marca não pode exceder 80 caracteres")]
        public string NomeMarca { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome da Modelo não pode exceder 80 caracteres")]
        public string NomeModelo { get; set; }

        [Required, MaxLength(8, ErrorMessage = "Ano não pode exceder 8 caracteres")]
        public string Ano { get; set; }

        [Required, MaxLength(10, ErrorMessage = "A Cilindrada não pode exceder 10 caracteres")]
        public string Cilindrada { get; set; }

        [Required]
        public double Preco { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
 

  
    }


}

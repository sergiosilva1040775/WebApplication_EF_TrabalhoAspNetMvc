using System;
using System.Collections.Generic;

namespace final.Models
{
    public class Licitacoes
    {
        public int LicitacoesId { get; set; }

        private DateTime dataHora { get; set; }

        public string licitador { get; set; }

        public double valorLicitado { get; set; }

        public int veiculosParaVendaId { get; set; }

        public veiculosParaVenda? veiculosParaVenda { get; set; }

    }
}

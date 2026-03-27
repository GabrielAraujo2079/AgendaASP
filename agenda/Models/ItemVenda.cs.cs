using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace agenda.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }
        public int Id_Agenda { get; set; }
        public int Id_Produto { get; set; }
        public int Quantidade { get; set; }
        public string NomeCliente { get; set; }
        public string NomeProduto { get; set; }
        public DateTime Data { get; set; }
    }
}
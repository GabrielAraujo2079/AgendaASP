using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace agenda.Models
{
    public class Agenda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}

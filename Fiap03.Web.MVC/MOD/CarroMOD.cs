using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.MOD
{
    public class CarroMOD
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public bool Esportivo { get; set; }
        public Combustivel Combustivel { get; set; }
        public string Descricao { get; set; }
    }
}
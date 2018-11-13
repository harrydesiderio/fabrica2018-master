using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    public class CarroModel
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public bool Esportivo { get; set; }
        public string Placa { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Combustível")]
        public Combustivel? Combustivel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    /**
     * Criar a tabela e realizar CRUD
     * */
    public class MarcaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Display(Name ="Data de Criação")]
        public DateTime DataCriacao { get; set; }
        public string Cnpj { get; set; }
    }
}
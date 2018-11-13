using Dapper;
using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class MarcaController : Controller
    {
        private IList<String> _nomes = new List<String>()
            { "Herbie" , "Mystery Machine" , "DeLorean" };
        [HttpGet]
        public ActionResult Pesquisar(int ano)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                //Pesquisa no banco de dados
                var sql = "SELECT * FROM Marca ORDER BY Nome DESC";
                var lista = db.Query<MarcaModel>(sql, new { Ano = ano }).ToList();
                //Retornar para a página de Listar enviando a lista de carros
                return View("Listar", lista);
            }
        }

        [HttpPost]
        public ActionResult Editar(MarcaModel model)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = @"UPDATE Marca SET Nome = @Nome, 
                    DataCriacao = @DataCriacao, Cnpj = @Cnpj
                    WHERE Id = @Id";
                db.Execute(sql, model);
                TempData["msg"] = "Atualizado com sucesso!";
                return RedirectToAction("Listar");
            }
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                //Buscar o carro no banco pelo id
                var sql = "SELECT * FROM Marca where Id = @Id";
                var marca = db.Query<MarcaModel>(sql, new { Id = id }).FirstOrDefault();
                //Carregar a lista de marcas para o "select"
                ViewBag.nomes = new SelectList(_nomes);
                //Mandar o carro para a view
                return View(marca);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                db.Execute("DELETE FROM Marca WHERE Id = @Id",
                                            new { id = codigo });
                TempData["msg"] = "Marca excluída";
                return RedirectToAction("Listar");
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.nomes = new SelectList(_nomes);
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(MarcaModel marca)
        {
            using (IDbConnection db = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = @"INSERT INTO Marca (Nome, DataCriacao, 
                    Cnpj) 
                    VALUES (@Nome, @DataCriacao, @Cnpj);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                int codigo = db.Query<int>(sql, marca).Single();
            }

            
            TempData["mensagem"] = "Marca registrada!";
          
            return RedirectToAction("Listar");
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //envia a lista de carros para a view
            using (IDbConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = "SELECT * FROM Marca";
                var lista = connection.Query<MarcaModel>(sql).ToList();
                return View(lista);
            }
        }
    }
}
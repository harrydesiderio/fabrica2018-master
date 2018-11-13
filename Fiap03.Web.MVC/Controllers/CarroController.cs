using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Fiap03.Web.MVC.Controllers
{
    public class CarroController : Controller
    {
        private IList<String> _marcas = new List<String>()
            { "Hyundai" , "Ferrari" , "Jeep" };

        [HttpGet]
        public ActionResult Pesquisar(int ano)
        {
            using(IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DbCarros"].ConnectionString))
            {
                //Pesquisa no banco de dados
                var sql = "SELECT * FROM Carro WHERE Ano = @Ano or 0 = @Ano";
                var lista = db.Query<CarroModel>(sql, new { Ano = ano }).ToList();
                //Retornar para a página de Listar enviando a lista de carros
                return View("Listar",lista);
            }
        }

        [HttpPost]
        public ActionResult Editar(CarroModel model)
        {
            using(IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = @"UPDATE Carro SET Marca = @Marca, 
                    Ano = @Ano, Esportivo = @Esportivo, Placa = @Placa, 
                    Combustivel = @Combustivel, Descricao = @Descricao 
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
                var sql = "SELECT * FROM Carro where Id = @Id";
                var carro = db.Query<CarroModel>(sql, new { Id = id }).FirstOrDefault();
                //Carregar a lista de marcas para o "select"
                ViewBag.marcas = new SelectList(_marcas);
                //Mandar o carro para a view
                return View(carro);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            using(IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                db.Execute("DELETE FROM Carro WHERE Id = @Id", 
                                            new { id = codigo });
                TempData["msg"] = "Carro excluído";
                return RedirectToAction("Listar");
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.marcas = new SelectList(_marcas);
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CarroModel carro)
        {
            using (IDbConnection db = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = @"INSERT INTO Carro (Marca, Ano, 
                    Esportivo, Placa, Combustivel, Descricao) 
                    VALUES (@Marca, @Ano, @Esportivo, @Placa, @Combustivel,
                    @Descricao); SELECT CAST(SCOPE_IDENTITY() as int);";

                int codigo = db.Query<int>(sql, carro).Single();
            }

            // _carros.Add(carro); //adiciona o carro na lista
            TempData["mensagem"] = "Carro registrado!";
            //Redireciona para uma URL, cria uma segunda request
            //para abrir a página de resposta
            //F5 não cadastra novamente
            return RedirectToAction("Cadastrar");
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //envia a lista de carros para a view
            using (IDbConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                var sql = "SELECT * FROM Carro";
                var lista = connection.Query<CarroModel>(sql).ToList() ;
                return View(lista);
            }
        }
    }
}
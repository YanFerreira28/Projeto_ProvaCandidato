using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Helper;
using ProvaCandidato.Models;

namespace ProvaCandidato.Controllers
{
    public class ClienteController : BaseController<ClienteController>
    {
        private ContextoPrincipal db = new ContextoPrincipal();

        public ActionResult Index(string filtername)
        {
            var clientes = db.Clientes.Include(c => c.Cidade).Where(c => c.Ativo == true);

            if (!string.IsNullOrEmpty(filtername))
            {
                clientes = db.Clientes.Include(c => c.Cidade).Where(c => c.Ativo == true && c.Nome.Contains(filtername));
            }



            return View(clientes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
            //Getting list of employees from DB.
            var list = GetCitiesAPI();
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach (CidadeViewModel emp in list)
            {
                selectlist.Add(new SelectListItem { Text = emp.nome, Value = emp.id.ToString() });
            }
            ViewBag.SelectList = selectlist;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente inserido com sucesso");
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nome,DataNascimento,CidadeId,Ativo")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                MessageHelper.DisplaySuccessMessage(this, "Cliente atualizado com sucesso");
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
            return View(cliente);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            MessageHelper.DisplaySuccessMessage(this, "Cliente deletado com sucesso");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        /*
         foi criado esse método que retorna cidades de SP de uma api, porém como o campo cidade possui relacionamento com o campo cidadeid da tabela cidades, vai dar pau na hora
        de gravar, mas fica como observação a existencia do método.
        */
        public List<CidadeViewModel> GetCitiesAPI()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://educacao.dadosabertosbr.com/api/cidades/sp");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsStringAsync().Result.Replace("-", "");

                var jsonObject = JsonConvert.SerializeObject(dataObjects);

                var myDeserializedClass = JsonConvert.DeserializeObject<string>(jsonObject).Replace("[", "").Replace("]", "").Split(',');

                var listaCidades = new List<CidadeViewModel>();

                foreach (var obj in myDeserializedClass)
                {
                    var objCity = obj.Split(':');
                    var id = objCity[0].ToString().Replace("\"", "");
                    var nome = objCity[1].ToString().Replace("\"", "");

                    listaCidades.Add(new CidadeViewModel() { id = Convert.ToInt32(id), nome = nome });
                }

                return listaCidades;

            }
            else
            {
                MessageHelper.DisplayErrorMessage(this, "Erro ao tentar comunicação com API externa.");
            }


            client.Dispose();
            return null;
        }
    }
}

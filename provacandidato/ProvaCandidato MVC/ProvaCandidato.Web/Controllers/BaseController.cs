using System.Web.Mvc;

namespace ProvaCandidato.Controllers
{
    public abstract class BaseController<T> : Controller where T: class
    {
       
    }
}
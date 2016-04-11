using asp.net_MVC.DataContexts;
using asp.net_MVC.Models;
using System.Web.Mvc;

namespace asp.net_MVC.Controllers
{
    public class FileController : Controller
    {
        private PetDBContext db = new PetDBContext();
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}

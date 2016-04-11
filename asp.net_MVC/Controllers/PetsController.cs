using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using asp.net_MVC.Models;
using PagedList;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity.Infrastructure;

namespace asp.net_MVC.Controllers
{
    public class PetsController : Controller
    {
        private PetDBContext db = new PetDBContext();
        string pCode = "LS9 7NR";
        int count;

        // GET: Pet
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pets = from s in db.Pets
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pets = pets.Where(s => s.petName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    pets = pets.OrderByDescending(s => s.petName);
                    break;
                case "Date":
                    pets = pets.OrderBy(s => s.missingDate);
                    break;
                case "date_desc":
                    pets = pets.OrderByDescending(s => s.missingDate);
                    break;
                default:  // Name ascending 
                    pets = pets.OrderBy(s => s.petName);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pets.ToPagedList(pageNumber, pageSize));
            // return View(db.Pets.ToList());
        }


        // GET: Pets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //this line fetches all files associated with the pet regardless of type.
            Pet pet = db.Pets.Include(s => s.Files).SingleOrDefault(s => s.petId == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "petId,petName,petTypes,missingDate,Description,Postcode,Reward")] Pet pet, System.Web.HttpPostedFileBase upload)
        {
            
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.petImage,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    pet.Files = new List<File> { avatar };
                }

                db.Pets.Add(pet);
                db.SaveChanges();
          
                var res1 = db.Pets.OrderByDescending(x => x.Postcode).Take(10);
       
                count = res1.Count();

                return RedirectToAction("Index");
                // return PopupPage(); 
               
            }
            return View(pet);
        }

        //popup window
        public ActionResult PopupPage()
        {

            ViewBag.PopupValue = ("Seems suspicious, " + count +" pets lost at the same Postcode. CALL 999 or FBI");
            
            return View();
        }


        // GET: Pets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Include(s => s.Files).SingleOrDefault(s => s.petId == id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var petToUpdate = db.Pets.Find(id);
            if (TryUpdateModel(petToUpdate, "",
               new string[] { "petId,petName,petTypes,missingDate,Description,Postcode,Reward" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (petToUpdate.Files.Any(f => f.FileType == FileType.petImage))
                        {
                            db.Files.Remove(petToUpdate.Files.First(f => f.FileType == FileType.petImage));
                        }
                        var avatar = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.petImage,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        petToUpdate.Files = new List<File> { avatar };
                    }
                    db.Entry(petToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(petToUpdate);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
            db.SaveChanges();
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
    
    }
}

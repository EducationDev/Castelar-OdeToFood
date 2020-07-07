using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Data.Model;
using OdeToFood.Data.Services;
using OdeToFood.WebSite.Services;

namespace OdeToFood.WebSite.Controllers
{
    public class ProductController : BaseController
    {
        private readonly RestoDbContext db = new RestoDbContext();

        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.Artist);
            return View(product.ToList());
        }
        public ActionResult Shop()
        {
            var product = db.Product.Include(p => p.Artist);
            return View(product.ToList());
        }
        public ActionResult Buy(int id)
        {

            var cookie= HelperCookie.GetFromCookie("shop-art", "shop-art-key");

            Cart cart = new Cart
            {
                CartDate = DateTime.Now,
                Cookie = cookie,
                ItemCount = 1,               
            };
            this.CheckAuditPattern(cart, true);

            CartItem item = new CartItem
            {
                Price = 100,
                ProductId = id,
                Quantity = 1
            };
            this.CheckAuditPattern(item, true);
            cart.CartItem = new List<CartItem>() { item };

            db.Cart.Add(cart);
            db.SaveChanges();


            return RedirectToAction("Index", "CartItem");
        }
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FirstName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/products"), _FileName);
                        file.SaveAs(_path);

                        product.Image = file.FileName;
                        this.CheckAuditPattern(product, true);
                        db.Product.Add(product);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
                catch (Exception ex)
                {
                    Logger.Instance.LogException(ex);
                }
            }
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FirstName", product.ArtistId);
            ViewBag.MessageDanger = "¡Error al cargar el archivo!";
            return View(product);


        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FirstName", product.ArtistId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    string _fileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Content/products"), _fileName);
                    file.SaveAs(_path);
                    product.Image = _fileName;
                }

                this.CheckAuditPattern(product, false);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artist, "Id", "FirstName", product.ArtistId);
            return View(product);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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

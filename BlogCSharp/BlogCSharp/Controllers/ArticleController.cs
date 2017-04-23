namespace BlogCSharp.Controllers
{
    using BlogCSharp.Models;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class ArticleController : Controller
    {
        public ActionResult List()
        {
            using (var db = new BlogDataBContext())
            {
                var articles = db.Articles
                    .Include(a => a.Author)
                    .ToList();

            return View(articles);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BlogDataBContext())
                {
                    var authorId = User.Identity.GetUserId();

                    article.AuthorId = authorId;

                    db.Articles.Add(article);
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
            }

            return View(article);
        }

        public ActionResult Details(int id)
        {
            using (var db = new BlogDataBContext())
            {
                var article = db.Articles
                    .Include(a => a.Author)
                    .Where(a => a.Id == id)
                    .FirstOrDefault();

                if (article == null)
                {
                    return HttpNotFound();
                }

                return View(article);
            }
        }
    }
}
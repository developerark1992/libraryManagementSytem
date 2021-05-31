using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        LMSEntities db = new LMSEntities();
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin ad)
        {

            var model = (from m in db.Admins
                         where m.AdminUsername == ad.AdminUsername &&
                         m.AdminPassword == ad.AdminPassword
                         select m).Any();
            if (model)
            {
                var loginInfo = db.Admins.Where(x => x.AdminUsername == ad.AdminUsername
                && x.AdminPassword == ad.AdminPassword).FirstOrDefault();

                Session["userID"] = loginInfo.aID;
                Session["name"] = loginInfo.AdminName;
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {

                ViewBag.msg = "Invalid Login";

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public ActionResult addAuthor()
        {
             return View();
        }
        [HttpPost]
        public ActionResult addAuthor(Author ad)
        {
            db.Authors.Add(ad);
            int k = db.SaveChanges();

            if (k > 0)
            {
                ViewBag.msg = "Author Added Successfully";
            }

            return View();
        }

        public ActionResult listAuthor()
        {
            var list = db.Authors.ToList();

            return View(list);
        }
        public ActionResult DeleteAuthor(int id)
        {
            var del = db.Authors.FirstOrDefault(x => x.AuthorID == id);
            db.Authors.Remove(del);
            db.SaveChanges();

            return RedirectToAction("listAuthor", "Home");
        }
        public ActionResult authorUpdate(int id)
        {
            var row = db.Authors.FirstOrDefault(x => x.AuthorID == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult authorUpdate(int id, Author ad)
        {
            Author update = db.Authors.FirstOrDefault(x => x.AuthorID == id);
            update.Name = ad.Name;
            update.PhoneNumber = ad.PhoneNumber;
            update.Publishers = ad.Publishers;
            update.Surname = ad.Surname;
            update.Title = ad.Title;
            db.SaveChanges();
            ViewBag.msg = "Author Updated";
            return View();
        }
        public ActionResult addCourse()
        {

            using (LMSEntities db = new LMSEntities())
            {

                List<Book> bk = new List<Book>();
                bk = db.Books.ToList();

                List<SelectListItem> item8 = new List<SelectListItem>();
                foreach (var c in bk)
                {
                    item8.Add(new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.BookID.ToString()
                    });
                }

                ViewBag.books = item8;


                //var listbook = new SelectList(db.Books.ToList(),"BookID","Name");
                //ViewBag.books = listbook;

                var listFaculty = new SelectList(db.Faculties.ToList(), "FacultyID", "FacultyName");
                ViewData["Faculty"] = listFaculty;

                var listDept = new SelectList(db.Departments.ToList(), "DeptID", "DeptName");
                ViewData["Dept"] = listDept;
            }
            return View();
        }
        [HttpPost]
        public ActionResult addCourse(Course c)
        {
            db.Courses.Add(c);
            int k = db.SaveChanges();

            if (k > 0)
            {
                ViewBag.msg = "Course Added Successfully";
            }
            return View();
        }

        public ActionResult listCourse()
        { 
            var list = db.Courses.ToList();
            return View(list);
        }
        public ActionResult addBook()
        {      
            return View();
        }
        [HttpPost]
        public ActionResult addBook(Book b)
        {
            db.Books.Add(b);
            if (db.SaveChanges() > 0)
            {
                ViewBag.msg = "Book Added Successfully";
            }
            return View();
        }
        public ActionResult listBooks()
        {
            var list = db.Books.ToList();
            return View(list);
        }

        public ActionResult deleteBook(int id) {

            var del = db.Books.FirstOrDefault(x => x.BookID == id);

            try {
                db.Books.Remove(del);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            

            return RedirectToAction("listBooks", "Home");
        }

        public ActionResult updateBook(int id)
        {
            var row = db.Books.FirstOrDefault(x => x.BookID == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult updateBook(int id, Book bk)
        {
            Book update = db.Books.FirstOrDefault(x => x.BookID == id);
            update.Name = bk.Name;
            update.ISBN = bk.ISBN;
            update.PublicationMembers = bk.PublicationMembers;
            update.BarcodeNumber = bk.BarcodeNumber;        
            db.SaveChanges();
            ViewBag.msg = "Book Updated";
            return View();
        }

        public ActionResult deleteCourse(int id)
        {
            var del = db.Courses.FirstOrDefault(x => x.CourseID == id);

            try
            {
                db.Courses.Remove(del);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            return RedirectToAction("listCourse", "Home");
        }

        public ActionResult addFaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addFaculty(Faculty f)
        {
            db.Faculties.Add(f);
            int k = db.SaveChanges();

            if (k > 0)
            {
                ViewBag.msg = "Faculty Added Succesfully";
            }
            return View();
        }

        public ActionResult viewFaculty()
        {
            var list = db.Faculties.ToList();
            return View(list);
        }

        public ActionResult deleteFaculty(int id)
        {
            var item = db.Faculties.FirstOrDefault(x=> x.FacultyID == id);
            db.Faculties.Remove(item);
            db.SaveChanges();

            return RedirectToAction("viewFaculty","Home");
        }


        public ActionResult addDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addDepartment(Department dp)
        {
            db.Departments.Add(dp);
            int k = db.SaveChanges();

            if (k > 0)
            {
                ViewBag.msg = "Department Added Successfully";
            }

            return View();
        }

        public ActionResult listDepartment()
        {
            var list = db.Departments.ToList();

            return View(list);
        }
        public ActionResult DeleteDepartment(int id)
        {
            var del = db.Departments.FirstOrDefault(x => x.DeptID == id);
            db.Departments.Remove(del);
            db.SaveChanges();

            return RedirectToAction("listDepartment", "Home");
        }
        public ActionResult departmentUpdate(int id)
        {
            var row = db.Departments.FirstOrDefault(x => x.DeptID == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult departmentUpdate(int id, Department dp)
        {
            Department update = db.Departments.FirstOrDefault(x => x.DeptID == id);
            update.DeptID = dp.DeptID;
            update.DeptName = dp.DeptName;
            db.SaveChanges();
            ViewBag.msg = "Department Updated";
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

    }
}
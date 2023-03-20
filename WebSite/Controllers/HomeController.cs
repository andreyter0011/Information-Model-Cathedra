using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;

using WebSite.Models;
namespace WebSite.Controllers
{
    public class HomeController : Controller
    {

        MonographContext db = new MonographContext();

        public async Task<IActionResult> Index(int? monograph, string name, SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Teacher> users = db.Teachers.Include(x => x.Subject);
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;

            users = sortOrder switch
            {
                SortState.NameDesc => users.OrderByDescending(s => s.Name),
                _ => users.OrderBy(s => s.Name),
            };
            if (monograph != null && monograph != 0)
            {
                users = users.Where(p => p.SubjectId == monograph);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }
            UserListViewModel viewModel = new UserListViewModel
            {
                Teachers = users.ToList(),
                Name= name,
            };
            return View(await users.AsNoTracking().ToListAsync());
        }
        [HttpGet]
        public ActionResult Create()
        {
            SelectList subjectloadType = new SelectList(db.Subjects, "Id", "loadType");
            SelectList subjectName = new SelectList(db.Subjects, "Id", "Name");
            SelectList subjectDescription = new SelectList(db.Subjects, "Id", "Description");
            SelectList subjectHours = new SelectList(db.Subjects, "Id", "hours");
            ViewBag.subjectloadType = subjectloadType;
            ViewBag.subjectName = subjectName;
            ViewBag.subjectDescription = subjectDescription;
            ViewBag.subjectHours = subjectHours;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }
        public ActionResult CreateM()
        {
            SelectList monographs = new SelectList(db.Subjects, "Id", "Publish");
            ViewBag.Monograph = monographs;
            return View();
        }

        [HttpPost]
        public ActionResult CreateM(Subject monograph)
        {
            db.Subjects.Add(monograph);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }
        public ActionResult CreateT()
        {
            SelectList teachers = new SelectList(db.Teachers, "Id", "Name");
            ViewBag.Monograph = teachers;
            return View();
        }

        [HttpPost]
        public ActionResult CreateT(Teacher teacher)
        {
            db.Teachers.Add(teacher);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Monograph()
        {
            SelectList monographs = new SelectList(db.Subjects, "Id", "Publish");
            ViewBag.Monograph = monographs;
            return View();
        }

        [HttpPost]
        public ActionResult Monograph(Subject monograph)
        {
            db.Subjects.Add(monograph);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }

        public ActionResult InputDoc()
        {
            SelectList monographs = new SelectList(db.Subjects, "Id", "Publish");
            ViewBag.Monograph = monographs;
            return View();
        }

        [HttpPost]
        public ActionResult InputDoc(Subject monograph)
        {
            db.Subjects.Add(monograph);
            db.SaveChanges();
            // перенаправляем на главную страницу
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher != null)
            {
                SelectList monographsN = new SelectList(db.Subjects, "Id", "Name");
                ViewBag.MonographN = monographsN;
                SelectList monograph = new SelectList(db.Subjects, "Id", "Description", teacher.SubjectId);
                ViewBag.Monograph = monograph;
                return View(teacher);
            }
            return RedirectToAction("Index");
        }
        public ActionResult AllPublish()
        {
            return View(db.Subjects);
        }
        public async Task<IActionResult> AllTeacher(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Teacher> teachers = db.Teachers.Include(x => x.Subject);
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            teachers = sortOrder switch
            {
                SortState.NameDesc => teachers.OrderByDescending(s => s.Name),
                _ => teachers.OrderBy(s => s.Name),
            };
            return View(await teachers.AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> TeacherPublish(int? monograph, string name)
        {
            IQueryable<Teacher> teachers = db.Teachers.Include(x => x.Subject);
            if (monograph != null && monograph != 0)
            {
                teachers = teachers.Where(p => p.SubjectId == monograph);
            }
            if (!String.IsNullOrEmpty(name))
            {
                teachers = teachers.Where(p => p.Name.Contains(name));
            }
            UserListViewModel viewModel = new UserListViewModel
            {
                Teachers = teachers.ToList(),
                Name = name
            };
            return View(await teachers.AsNoTracking().ToListAsync());
        }
        public async Task<IActionResult> SubjectPublish(int? monograph, string name)
        {
            IQueryable<Teacher> teachers = db.Teachers.Include(x => x.Subject);
            if (monograph != null && monograph != 0)
            {
                teachers = teachers.Where(p => p.SubjectId == monograph);
            }
            if (!String.IsNullOrEmpty(name))
            {
                teachers = teachers.Where(p => p.Subject.Name.Contains(name));
            }
            UserListViewModel viewModel = new UserListViewModel
            {
                Teachers = teachers.ToList(),
                Name = name
            };
            return View(await teachers.AsNoTracking().ToListAsync());
        }
        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            db.Entry(teacher).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Teacher b = db.Teachers.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher b = db.Teachers.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Teachers.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult PersonalData(string name)
        {
            IQueryable<Teacher> teachers = db.Teachers;
            if (!String.IsNullOrEmpty(name))
            {
                teachers = teachers.Where(p => p.Name.Contains(name));
            }
            UserListViewModel viewModel = new UserListViewModel
            {
                Teachers = teachers.ToList(),
                Name = name
            };
            return View(teachers);
        }
    }
}

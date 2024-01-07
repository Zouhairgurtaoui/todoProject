using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using todoproject.Models;

namespace todoproject.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ContextDb _context;
        public TasksController()
        {
            _context = new ContextDb();
        }
        public ActionResult Index()
        {
            
            TasksViewModel tasksView = new TasksViewModel
            {
                Tasks = _context.Tasks.Where(t => t.UserId == 1).ToList()
            };
            return View(tasksView);
        }
        public ActionResult Add()
        {
            ViewBag.Title = "Add Task";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PopulateTaskViewModel populatedTask)
        {
            if (ModelState.IsValid)
            {



                ClaimsIdentity identity = User.Identity as ClaimsIdentity;

                
                Claim userIdClaim = identity?.FindFirst("UserId");

                
                string userIdString = userIdClaim?.Value;
                int userId;
                int.TryParse(userIdString, out userId);
                Tasks task = new Tasks
                {
                    TaskTitle = populatedTask.TaskTitle,
                    TaskDescription = populatedTask.TaskDescription,
                    TaskTime = populatedTask.TaskTime,
                    Priority = populatedTask.Priority,
                    Status = false,
                    UserId = userId
                };
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index", "Tasks");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? TaskId)
        {
            if(TaskId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == TaskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction("Index", "Tasks");
            }
            ModelState.AddModelError("TaskId", "something went wrong");
            return RedirectToAction("Index", "Tasks");
        }
        public ActionResult Update(int? TaskId)
        {
            if(TaskId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = _context.Tasks.SingleOrDefault(t => t.TaskId == TaskId);
            if(task == null)
            {
                ModelState.AddModelError("TaskTitle", "error");
                return View("Update");
            }
            var TaskModel = new PopulateTaskViewModel
            {
                TaskId = task.TaskId,
                TaskTitle = task.TaskTitle,
                TaskDescription = task.TaskDescription,
                TaskTime = task.TaskTime,
                Priority = task.Priority
            };
            return View("Update",TaskModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PopulateTaskViewModel taskViewModel)
        {
            if(taskViewModel == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var task = _context.Tasks.Find(taskViewModel.TaskId);
            if(task == null)
            {
                return HttpNotFound();
            }
            task.TaskTitle = taskViewModel.TaskTitle;
            task.TaskDescription = taskViewModel.TaskDescription;
            task.TaskTime = taskViewModel.TaskTime;
            task.Priority = taskViewModel.Priority;
            _context.SaveChanges();
            return RedirectToAction("Index","Tasks");
        }

    }
}
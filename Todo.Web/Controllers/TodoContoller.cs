using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class TodoContoller:Controller
    {
        TodoContext todoContext;
        public TodoContoller(TodoContext  todoContext)
        {
            this.todoContext=todoContext;
        }

        public ViewResult Index(){
            List<TodoViewModel> todoList=todoContext.Todos.Select(p=>new TodoViewModel{
                // Category=p.Category.Name,
                Title=p.Title,
                Content=p.Content,
                Id=p.Id
            }).ToList();
            return View(todoList);
        }

        [HttpPost]
        public ActionResult Edit(Models.Todo  todo){

            todoContext.Attach(todo);
            todoContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Create(Models.Todo  todo){

            todoContext.Add(todo);
            todoContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult Edit(int id){
            var todo=todoContext.Todos.FirstOrDefault(p=>p.Id==id);
            return View(todo);
        }

        public ViewResult Detail(int id){
            var todoViewModel=todoContext.Todos.Select(p=>new TodoViewModel{
                // Category=p.Category.Name,
                Title=p.Title,
                Content=p.Content,
                Id=p.Id
            }).FirstOrDefault(p=>p.Id==id);

            return View(todoViewModel);
        }
    }
}
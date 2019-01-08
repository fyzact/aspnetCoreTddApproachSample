using System;
using Microsoft.EntityFrameworkCore;
using Todo.Web.Controllers;
using Todo.Web.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Todo.Web.Test.Contollers
{
    public class TodoControllerTest : IDisposable
    {
        private TodoContext todoContext;
                private TodoContoller todoController;
        public TodoControllerTest()
        {
            
            var optionsBuilder=new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseInMemoryDatabase("test");

            todoContext=new TodoContext(optionsBuilder.Options);
  
            todoContext.Todos.Add(new Models.Todo{  Title="Go Shopping",Content="Go Market and buy bread,tea and milk",CreateDate=DateTime.Now});
            todoContext.Todos.Add(new Models.Todo{Title="Go Work",Content="Go work and finish all task that you responsable",CreateDate=DateTime.Now});
            todoContext.SaveChanges();

            todoController=new TodoContoller(todoContext);
        }

         [Fact]
        public void Index_ReturnViewResult_AllTodoList(){
            var result=todoController.Index();
            var viewResult=Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<List<TodoViewModel>>(result.Model);
        }

        [Fact]
        public void CreatePost_ReturnRedirectToIndex_Succeeded()
        {
            var result=todoController.Create(new Models.Todo{  Title="Title 1", 
            Content="Content 1",
            CreateDate=DateTime.Now });
            var redirectToActionResult=Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void EditPost_ReturnRedirectToIndex_Succeeded(){
             var result=todoController.Edit(new Models.Todo{ Title="Title 1 Edit", 
            Content="Content 1 Edit",
            CreateDate=DateTime.Now });
            var redirectToActionResult=Assert.IsType<RedirectToActionResult>(result);

        }
 [Fact]
         public void Edit_ReturnViewResult_Todo(){
             var result=todoController.Edit(1);
             var todo=Assert.IsAssignableFrom<Web.Models.Todo>(result.Model);

        }

    [Fact]
     public void Detail_ReturnViewResult_Todo(){
             var result=todoController.Detail(1);
             var todoViewModel=Assert.IsAssignableFrom<TodoViewModel>(result.Model);
        }



        public void Dispose()
        {
          todoContext.Todos.RemoveRange(todoContext.Todos);
          todoContext.Categories.RemoveRange(todoContext.Categories);
          
        }
    }
}
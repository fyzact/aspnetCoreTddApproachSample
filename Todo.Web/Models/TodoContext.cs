using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Todo.Web.Models
{
    public class TodoContext:DbContext
    {
     public TodoContext(DbContextOptions options):base(options)
     {
         
     }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Todo>()
        //     .HasOne(p => p.Category)
        //     .WithMany(b => b.Todos);
    }

     private void InsertListener(){

     }

     public new  Task<int>   SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
     {
         InsertListener();
         return  base.SaveChangesAsync(cancellationToken);
     }
     public DbSet<Category> Categories{get;set;}
     public DbSet<Todo> Todos{get;set;}
    }

    public class Category:BaseEntity<byte>
    {
        public string Name { get; set; }
        public List<Todo> Todos{get;set;}
    }

    public class  Todo:BaseEntity<int>
     {
    //     public byte CategoryId { get; set; }
        // public Category Category { get; set; }
        public string Title { get; set; }
        public string  Content { get; set; }
    }

    public abstract class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
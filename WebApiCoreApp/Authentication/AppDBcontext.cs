using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCoreApp.Models;
using WebApiCoreApp.ViewModel;

namespace WebApiCoreApp.Authentication
{
    public class AppDBcontext :IdentityDbContext<ApplicationUser>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<RegisterVM> users { get; set; }


    }
}

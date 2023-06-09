﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Models;
using System.Runtime.CompilerServices;

namespace net_il_mio_fotoalbum.Areas
{
    //Comunicare a .NET la configurazione del nostro DB e delle classi da usare

    //public class PizzeriaContext : DbContext  //senza sistema auth
    public class PhotoContext : IdentityDbContext<IdentityUser>  //con sistema auth
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-photos;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
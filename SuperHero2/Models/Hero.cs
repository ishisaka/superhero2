using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace SuperHero2.Models
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class HeroContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
    }

}
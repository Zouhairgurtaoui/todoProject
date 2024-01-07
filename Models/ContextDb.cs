using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace todoproject.Models
{
    public class ContextDb: DbContext
    {
        public ContextDb() : base("name=todoDbEntities")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
    }
}
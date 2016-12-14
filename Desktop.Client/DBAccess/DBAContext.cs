using Desktop.Client.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Desktop.Client.DBAccess
{
    public class DBAContext : DbContext
    {
        private const string connectionStrig = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        
        private DBAContext(string connectionStrig) : base(connectionStrig){}

        public static DBAContext GetContext()
        {
            
            return new DBAContext(connectionStrig);
        }

        public DbSet<Product> Products { get; set; }
    }
}
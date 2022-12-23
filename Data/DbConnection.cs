using lab4.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;


namespace lab4.Data
{

    public class DbConnection : DbContext
    {

        public DbConnection(DbContextOptions<DbConnection> options) : base(options)
        {

        }

        public DbSet<Receipt> Receipts { get; set; } = null!;
        public DbSet<Consumer> Consumers { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;


    }
}

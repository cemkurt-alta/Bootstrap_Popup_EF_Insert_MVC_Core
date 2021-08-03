using Bootstrap_Popup_EF_Insert_MVC_Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Bootstrap_Popup_EF_Insert_MVC_Core
{
    public class DBCtx : DbContext
    {
        public DBCtx(DbContextOptions<DBCtx> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
using System.Threading.Tasks;
using customModelValidation.Models;
using Microsoft.EntityFrameworkCore;

namespace customModelValidation.Services
{
    public class DbServices : IDbServices
    {
        private readonly AppDbContext _db;
        public DbServices(AppDbContext db)
        {
            _db = db;

        }

        public async Task<bool> IsStoreExists(int id)
        {
            return await _db.posMagaza.AnyAsync(x => x.mekanID == id);
        }
    }
}
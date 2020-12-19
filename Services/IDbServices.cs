using System.Threading.Tasks;

namespace customModelValidation.Services
{
    public interface IDbServices
    {
        Task<bool> IsStoreExists(int id);
    }
}
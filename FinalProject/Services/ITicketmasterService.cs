using FinalProject.Models;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public interface ITicketmasterService
    {
        Task<TMModel> GetEventAsync(string postalcode);
    }
}
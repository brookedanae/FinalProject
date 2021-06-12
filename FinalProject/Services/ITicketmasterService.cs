using FinalProject.Services.APIModels;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public interface ITicketmasterService
    {
        Task<EventsResponse> GetEventAsync(string postalcode);
    }
}
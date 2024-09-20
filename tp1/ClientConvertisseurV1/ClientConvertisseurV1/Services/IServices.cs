using System.Collections.Generic;
using System.Threading.Tasks;
using ClientConvertisseurV1.Models;

namespace ClientConvertisseurV1.Services
{
    internal interface IServices
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}

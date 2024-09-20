using System.Collections.Generic;
using System.Threading.Tasks;
using ClientConvertisseurV2_2.Models;

namespace ClientConvertisseurV2_2.Services
{
    internal interface IServices
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}

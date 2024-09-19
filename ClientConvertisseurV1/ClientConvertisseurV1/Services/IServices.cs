using ClientConvertisseurV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV1.Services
{
    internal interface IServices
    {
        Task<List<Devise>> GetDevisesAsync(string nomControleur);
    }
}

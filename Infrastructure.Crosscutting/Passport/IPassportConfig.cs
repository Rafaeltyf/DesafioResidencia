using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Passport
{
    public interface IPassportConfig
    {
        bool UsaPassport { get; }
        string Authority { get; }
        Task<string> GenerateToken();
        void SincronizarPassports(string instituicao, List<string> passports, string token);
    }
}
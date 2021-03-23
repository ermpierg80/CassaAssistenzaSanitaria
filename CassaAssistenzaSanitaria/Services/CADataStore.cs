using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CassaAssistenzaSanitaria.Models;

namespace CassaAssistenzaSanitaria.Services
{
    public interface CADataStore
    {
        Task<string> LoginTokenAsync(Login login);
        Task<Iscritto> GetIscrittoAsync(int id);

        Task<IEnumerable<Prestazione>> GetPrestazioniAsync(bool forceRefresh = false);

        Task<bool> AddRichiestaAsync(Richiesta richiesta);
        Task<bool> UpdateRichiestaAsync(Richiesta richiesta);
        Task<Richiesta> GetRichiestaAsync(int id);
        Task<IEnumerable<Richiesta>> GetRichiesteAsync(bool forceRefresh = false);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CassaAssistenzaSanitaria.Models;

namespace CassaAssistenzaSanitaria.Services
{
    public interface DataStore
    {
        Task<bool> LoginTokenAsync(Login login);
        Task<Iscritto> GetIscrittoAsync(bool forceRefresh = false);

        Task<IEnumerable<Prestazione>> GetPrestazioniAsync();

        bool AddRichiestaAsync(Richiesta richiesta);
        bool UpdateRichiestaAsync(Richiesta richiesta);
        Task<Richiesta> GetRichiestaAsync(int id);
        Task<IEnumerable<Richiesta>> GetRichiesteAsync();
    }
}

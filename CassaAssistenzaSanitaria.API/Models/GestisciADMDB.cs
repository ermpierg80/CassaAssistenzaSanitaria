using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class GestisciADMDB
    {
        public IConfiguration Configuration { get; }

        public GestisciADMDB(IConfiguration IConfig)
        {
            Configuration = IConfig;
            CultureInfo.CurrentCulture = new CultureInfo(Configuration.GetValue<string>("CurrentCulture"), false);
        }

        public string GetRichiesteFromUser(string CodiceFiscale)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaADMDbContext(Configuration.GetConnectionString("ADMConnection")))
                {
                    var query = from t in context.Richieste.Include("Tipologia").Include("Richiedente")
                                where t.Richiedente.CodiceFiscale.Equals(CodiceFiscale)
                                select t;

                    string output = "";

                    foreach (var t in query)
                    {
                        if (t != null)
                        {
                            output = t.ToString();
                        }
                    }
                    return output;
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}

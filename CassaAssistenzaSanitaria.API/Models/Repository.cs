using System;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class Repository
    {
        private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        public Repository(IConfiguration IConfig)
        {
            CultureInfo.CurrentCulture = new CultureInfo(IConfig.GetValue<string>("DBConf:CurrentCulture"), false);
            builder.DataSource = IConfig.GetValue<string>("DBConf:DataSource");
            builder.UserID = IConfig.GetValue<string>("DBConf:UserID");              
            builder.Password = IConfig.GetValue<string>("DBConf:Password");
            builder.InitialCatalog = IConfig.GetValue<string>("DBConf:InitialCatalog");
        }

        public string GetRichiesteFromUser(string user)
        {
            try
            {
                using (CassaAssistenzaSanitaria.API.Models.CassaAssistenzaContext context = new CassaAssistenzaSanitaria.API.Models.CassaAssistenzaContext(builder.ConnectionString))
                {
                    var query = from t in context.Richieste.Include("Tipologia").Include("Richiedente")
                                where t.Richiedente.Nome.Equals(user)
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

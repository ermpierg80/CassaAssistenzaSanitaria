using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using CassaAssistenzaSanitaria.API.Data;
using System.Security.Claims;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CassaAssistenzaSanitaria.API.Models
{
    public class GestisciSECDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<CassaAssistenzaSECDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "Ermanno.Piergiacomi@gmail.com",
                    UserName = "ErmPierg",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                userManager.CreateAsync(user, "Test@123");
            }
        }

        public static string RetrieveCodiceFiscale(IConfiguration Configuration, ClaimsPrincipal User)
        {
            string CodiceFiscale = "";
            string UserName = RetrieveUserName(User);

            try
            {
                using (SqlConnection connection = new SqlConnection(Configuration.GetConnectionString("SECConnection")))
                {
                    string sql = "SELECT CodiceFiscale FROM AspNetUsers WHERE UserName = @UserName";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@UserName";
                        param.Value = UserName;

                        command.Parameters.Add(param);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CodiceFiscale = reader.GetString(0);
                            }
                        }
                    }
                }
                return CodiceFiscale;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public static string RetrieveUserName(ClaimsPrincipal User)
        {
            String Nome = "";

            try
            {
                foreach (Claim info in User.Claims)
                {
                    if (info.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))
                    {
                        Nome = info.Value;
                        break;
                    }
                }

                return Nome;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public static List<string> RetrieveUserRoles(ClaimsPrincipal User)
        {
            List<string> Roles = new List<string>();
            try
            {
                foreach (Claim info in User.Claims)
                {
                    if (info.Type.Equals(ClaimTypes.Role))
                    {
                        Roles.Add(info.Value);
                    }
                }
                return Roles;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}

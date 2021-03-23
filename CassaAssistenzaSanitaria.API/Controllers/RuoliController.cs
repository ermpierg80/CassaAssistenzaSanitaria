using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CassaAssistenzaSanitaria.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using CassaAssistenzaSanitaria.API.Data;

namespace CassaAssistenzaSanitaria.API.Controllers
{
    [Route("api/[controller]")]
    public class RuoliController : Controller
    {
        private IConfiguration Configuration;
        private GestisciADMDB CassaAssistenzaDB;
        private log4net.ILog log;

        public RuoliController(IConfiguration IConf)
        {
            log = Logger.GetLogger(typeof(RuoliController));
            Configuration = IConf;
            CassaAssistenzaDB = new GestisciADMDB(Configuration);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<Ruolo> Get()
        {
            List <Ruolo> ruoli = new List<Ruolo>();

            try
            {
                if (User.IsInRole("Admin"))
                {
                    ruoli.Add(new Ruolo() { Info = "Admin"});
                }
                if (User.IsInRole("User"))
                {
                    ruoli.Add(new Ruolo() { Info = "User" });
                }
                return ruoli;
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
                return null;
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CassaAssistenzaSanitaria.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using CassaAssistenzaSanitaria.API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CassaAssistenzaSanitaria.API.Controllers
{
    [Route("api/[controller]")]
    public class RichiesteController : Controller
    {
        private IConfiguration Configuration;
        private GestisciADMDB CassaAssistenzaDB;
        private log4net.ILog log;

        public RichiesteController(IConfiguration IConf)
        {
            log = Logger.GetLogger(typeof(RichiesteController));
            Configuration = IConf;
            CassaAssistenzaDB = new GestisciADMDB(Configuration);
        }

        // GET: api/values
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<RichiestaOutput> Get()
        {
            try
            {
                return CassaAssistenzaDB.GetRichieste((User.IsInRole("Admin") ? "*" : GestisciSECDB.RetrieveCodiceFiscale(Configuration, User)));
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
                return null;
            }
        }

        // GET api/values/5
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public Richiesta Get(int id)
        {
            try
            {
                return CassaAssistenzaDB.GetRichiesta(id, (User.IsInRole("Admin") ? "*" : GestisciSECDB.RetrieveCodiceFiscale(Configuration, User)));
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
                return null;
            }
        }

        // POST api/values
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public void Post([FromBody] RichiestaModel richiesta)
        {
            try
            {
                CassaAssistenzaDB.AddRichiesta(richiesta, (User.IsInRole("Admin") ? "*" : GestisciSECDB.RetrieveCodiceFiscale(Configuration, User)));
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] RichiestaModel value)
        {
            try
            {
                if (id > 0 && value != null)
                {
                    CassaAssistenzaDB.UpdRichieste(id, value, (User.IsInRole("Admin") ? "*" : GestisciSECDB.RetrieveCodiceFiscale(Configuration, User)));
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    CassaAssistenzaDB.DelRichieste(id);
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }
    }
}

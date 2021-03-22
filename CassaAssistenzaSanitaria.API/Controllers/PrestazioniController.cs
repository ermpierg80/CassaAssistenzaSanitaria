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
    public class PrestazioniController : Controller
    {
        private IConfiguration Configuration;
        private GestisciADMDB CassaAssistenzaDB;
        private log4net.ILog log;

        public PrestazioniController(IConfiguration IConf)
        {
            log = Logger.GetLogger(typeof(PrestazioniController));
            Configuration = IConf;
            CassaAssistenzaDB = new GestisciADMDB(Configuration);
        }

        // GET: api/values
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<Prestazione> Get()
        {
            try
            {
                return CassaAssistenzaDB.GetPrestazioni();
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
        public Prestazione Get(int id)
        {
            try
            {
                if (id > 0)
                {
                    return CassaAssistenzaDB.GetPrestazione(id);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
                return null;
            }
        }

        // POST api/values
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post([FromBody] PrestazioneModel value)
        {
            try
            {
                if (value != null)
                {
                    CassaAssistenzaDB.AddPrestazione(value);
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PrestazioneModel value)
        {
            try
            {
                if (id > 0 && value != null)
                {
                    CassaAssistenzaDB.UpdPrestazione(id, value);
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
                    CassaAssistenzaDB.DelPrestazione(id);
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }
    }
}

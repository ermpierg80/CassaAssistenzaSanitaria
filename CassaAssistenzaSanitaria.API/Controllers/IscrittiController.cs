using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CassaAssistenzaSanitaria.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using CassaAssistenzaSanitaria.API.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CassaAssistenzaSanitaria.API.Controllers
{
    [Route("api/[controller]")]
    public class IscrittiController : Controller
    {
        private IConfiguration Configuration;
        private GestisciADMDB CassaAssistenzaDB;
        private log4net.ILog log;

        public IscrittiController(IConfiguration IConf)
        {
            log = Logger.GetLogger(typeof(IscrittiController));
            Configuration = IConf;
            CassaAssistenzaDB = new GestisciADMDB(Configuration);
        }

        // GET: api/values
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IEnumerable<Iscritto> Get()
        {
            try
            {
                return CassaAssistenzaDB.GetIscritti();
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
        public Iscritto Get(int id)
        {
            try
            {
                return CassaAssistenzaDB.GetIscritto((User.IsInRole("Admin") ? id : 0),
                ((User.IsInRole("Admin") && (id != 0)) ? "*" : GestisciSECDB.RetrieveCodiceFiscale(Configuration, User)));
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
        public void Post([FromBody] IscrittoModel value)
        {
            try
            {
                if (value != null)
                {
                    CassaAssistenzaDB.AddIscritto(value);
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
        public void Put(int id, [FromBody] IscrittoModel value)
        {
            try
            {
                if(id > 0 && value != null)
                {
                    CassaAssistenzaDB.UpdIscritto(id, value);
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
                    CassaAssistenzaDB.DelIscritto(id);
                }
            }
            catch (Exception e)
            {
                this.log.Error(e.ToString());
            }
        }
    }
}

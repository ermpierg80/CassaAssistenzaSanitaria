﻿using System;
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
            this.log = Logger.GetLogger(typeof(RichiesteController));
            this.Configuration = IConf;
            this.CassaAssistenzaDB = new GestisciADMDB(this.Configuration);
        }

        // GET: api/values
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

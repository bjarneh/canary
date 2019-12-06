// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppX.Models;
using WebAppX.DB;

/**
 * Only "controller" in this somewhat modified MVC Hello world App.
 *
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.Controllers
{

    [Route("api/[controller]")]
    public class CanaryController : Controller
    {

        private MySqlDB db = new MySqlDB(
                "Server=localhost;User ID=root;Password=passord;Database=canary");

        // GET api/canary
        [HttpGet]
        public IEnumerable<Sing> Get()
        {
            return db.AllSongs();
        }

        // GET api/canary/5
        [HttpGet("{id}")]
        public Sing Get(string id)
        {
            var song = db.OneSong(id);
            if( song != default(Sing) ){
                return song;
            }
            // Is this really the way to do it?
            Response.StatusCode = StatusCodes.Status404NotFound;
            return null;
        }

        // POST api/canary
        [HttpPost("{id}")]
        public void Post(string id)
        {
            var song = db.OneSong(id);
            if( song == default(Sing) ){
                var s = new Sing();
                s.Id = id;
                s.Last = DateTime.Now;
                s.Next = DateTime.Now.AddDays(7);
                db.Create(s);
            }else{
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        // PUT api/canary/5
        [HttpPut("{id}")]
        public void Put(String id)
        {
            var song = db.OneSong(id);
            if( song != default(Sing) ){
                song.Last = DateTime.Now;
                db.Update(song);
            }
        }

        // DELETE api/canary/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            db.Delete(id);
        }

    }
}

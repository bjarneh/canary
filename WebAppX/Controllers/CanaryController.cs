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

        private readonly Storage db;

        public CanaryController(Storage dbDep){
            db = dbDep;
        }


        // GET api/canary
        [HttpGet]
        public IEnumerable<Sing> Get()
        {
            return db.AllSongs();
        }


        // GET api/canary/some.job
        [HttpGet("{id}")]
        public Sing Get([FromRoute] string id)
        {
            var song = db.OneSong(id);
            if( song != default(Sing) ){
                return song;
            }
            // Is this really the way to do it?
            Response.StatusCode = StatusCodes.Status404NotFound;
            return null;
        }


        // POST api/canary/some.job/30/m
        [HttpPost("{id}/{num}/{hms}")]
        public void Post(
            [FromRoute] string id,
            [FromRoute] long num,
            [FromRoute] string hms)
        {
            if(hms != "h" &&
               hms != "m" &&
               hms != "s")
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
            }else{
                var song = new Sing();
                song.Id = id;
                song.Last = DateTime.Now;
                if(hms == "h"){
                    song.Next = DateTime.Now.AddHours(num);
                }else if(hms == "m"){
                    song.Next = DateTime.Now.AddMinutes(num);
                }else if(hms == "s"){
                    song.Next = DateTime.Now.AddSeconds(num);
                }else{
                    song.Next = song.Last;
                }
                song.IpAddr = Request.Host.Host;
                db.CreateOrUpdate(song);
            }
        }


        // PUT api/canary/some.job/30/m
        [HttpPut("{id}/{num}/{hms}")]
        public void Put(
            [FromRoute] string id,
            [FromRoute] long num,
            [FromRoute] string hms)
        {
            Post(id, num, hms);
        }


        // DELETE api/canary/some.job
        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            db.Delete(id);
        }

    }
}

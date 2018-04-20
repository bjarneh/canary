// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebAppX.Models;

/**
 * Only "controller" in this somewhat modified MVC Hello World App.
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
        Dictionary<string, Sing> songs = new Dictionary<string,Sing>(){
            { "id:1", new Sing("id:1") },
            { "id:2", new Sing("id:2") },
            { "id:3", new Sing("id:3") },
            { "id:4", new Sing("id:4") },
            { "id:5", new Sing("id:5") },
            { "id:6", new Sing("id:6") },
            { "id:7", new Sing("id:7") }
        };

        public Dictionary<string, Sing> Songs { get => songs; set => songs = value; }

        // GET api/canary
        [HttpGet]
        public IEnumerable<Sing> Get()
        {
            return Songs.Values;
        }

        // GET api/canary/5
        [HttpGet("{id}")]
        public Sing Get(String id)
        {
            if( songs.ContainsKey(id) )
            {
                return songs[id];
            }
            // Is this really the way to do it?
            Response.StatusCode = StatusCodes.Status404NotFound;
            return null;
        }

        // POST api/canary
        [HttpPost]
        public void Post([FromBody]Sing song)
        {
            songs[song.Id] = song;
        }

        // PUT api/canary/5
        [HttpPut("{id}")]
        public void Put(String id, [FromBody]Sing song)
        {
            songs[song.Id] = song;
        }

        // DELETE api/canary/5
        [HttpDelete("{id}")]
        public void Delete(String id)
        {
            songs.Remove(id);
        }
    }
}
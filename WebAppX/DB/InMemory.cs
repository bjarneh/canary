// Copyright (C) Bjarne Holen 2020. BSD compatible license.

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebAppX.Models;
using Dapper;
using System.Data;


/**
 * In memory object to store feedback.
 * 
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.DB
{
    public class InMemory : Storage {

        private readonly Dictionary<string, Sing> songs = new Dictionary<string, Sing>();
        
        public InMemory(){ }

        public IEnumerable<Sing> AllSongs(){
            return songs.Values;
        }

        public Sing OneSong(string id){
            if(songs.ContainsKey(id)){
                return songs[id];
            }
            return default(Sing);
        }

        public int Create(Sing sing){
            songs.Add(sing.Id, sing);
            return 0;
        }

        public int CreateOrUpdate(Sing sing){
            songs[sing.Id] = sing;
            return 0;
        }

        public int Update(Sing sing){
            songs[sing.Id] = sing;
            return 0;
        }

        public int Delete(string id){
            songs.Remove(id);
            return 0;
        }
    }
}

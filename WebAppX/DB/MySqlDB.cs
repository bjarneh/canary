
// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebAppX.Models;
using Dapper;
using System.Data;


/**
 * Just an object to store feedback in MySQL.
 * 
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.DB
{
    public class MySqlDB : Storage {
        
        public string ConnString { get; set; } = "Server=localhost;User ID=root;Password=passord;Database=canary";

        public MySqlDB(string _connStr){
            ConnString = _connStr;
        }
        
        public MySqlDB(){ }

        public IEnumerable<Sing> AllSongs(){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return AllSongs(conn);
            }
        }

        private IEnumerable<Sing> AllSongs(IDbConnection conn){
            return conn.Query<Sing>("SELECT * FROM sing");
        }


        public Sing OneSong(string id){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return OneSong(id, conn);
            }
        }

        private Sing OneSong(string id, IDbConnection conn){
            return conn.QuerySingleOrDefault<Sing>("SELECT * FROM sing WHERE id = @id", new { id = id });
        }


        public int Create(Sing sing){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return Create(sing, conn);
            }
        }

        private int Create(Sing sing, IDbConnection conn){
            return conn.Execute("INSERT INTO sing(id,last,next,ipaddr) VALUES(@id, @last, @next, @ipaddr) ", sing);
        }


        public int Update(Sing sing){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return Update(sing, conn);
            }
        }

        private int Update(Sing sing, IDbConnection conn){
            return conn.Execute("UPDATE sing SET last = @last, next = @next, ipaddr = @ipaddr WHERE id = @id", sing);
        }



        public int CreateOrUpdate(Sing sing){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                Sing song = OneSong(sing.Id, conn);
                if(song == null){
                    Create(song, conn);
                }
                return Update(song, conn);
            }
        }


        public int Delete(string id){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return conn.Execute("DELETE FROM sing WHERE id = @id", new {});
            }
        }

    }
}

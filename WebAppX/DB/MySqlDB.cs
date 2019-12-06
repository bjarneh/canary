
// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WebAppX.Models;
using Dapper;
using System.Data;


/**
 * Just an object to store feedback.
 * 
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.DB
{
    public class MySqlDB{
        
        public string ConnString { get; set; }

        public MySqlDB(string _connStr){
            ConnString = _connStr;
        }
        
        public MySqlDB(){
        }

        public IEnumerable<Sing> AllSongs(){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return conn.Query<Sing>("SELECT * FROM sing");
            }
        }

        public Sing OneSong(string id){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return conn.QuerySingleOrDefault<Sing>("SELECT * FROM sing WHERE id = @id", new { id = id });
            }
        }

        public int Create(Sing sing){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return conn.Execute("INSERT INTO sing(id,last,next) VALUES(@id, @last, @next) ", sing);
            }
        }

        public int Update(Sing sing){
            using ( IDbConnection conn = new MySqlConnection(this.ConnString) )
            {
                return conn.Execute("UPDATE sing SET last = @last, next = @next WHERE id = @id", sing);
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
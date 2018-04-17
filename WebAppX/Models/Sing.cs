// Copyright (C) Bjarne Holen 2018. BSD compatible license.
using System;
using System.Collections.Generic;

/**
 * Just an object to store feedback.
 * 
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.Models
{
    public class Sing {

        public Sing(){}
        public Sing(String _id){
            this.Id = _id;
        }

        public String Id { get; set; }
        public DateTime Last { get; set; }
        public DateTime Next { get; set; }
    }
}

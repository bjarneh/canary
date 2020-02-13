
// Copyright (C) Bjarne Holen 2020. BSD compatible license.
using System;
using System.Collections.Generic;
using System.Data;
using WebAppX.Models;


/**
 * The contract for a database / storage to be used.
 * 
 * @author  bjarneholen@gmail.com
 * @license BSD
 * @version 1.0
 */

namespace WebAppX.DB
{
    public interface Storage {
        
        IEnumerable<Sing> AllSongs();

        Sing OneSong(string id);

        int Create(Sing sing);

        int Update(Sing sing);

        int Delete(string id);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_database.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }




    }
}
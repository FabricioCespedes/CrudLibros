﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudLibros
{
    public static class Config
    {
        public static string getCadenaConexion
        {
            get { return Properties.Settings.Default.CadenaConexionINA; }
        }

    }
}

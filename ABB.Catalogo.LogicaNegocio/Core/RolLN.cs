﻿using ABB.Catalogo.Entidades.Core;
using ABB.Catalogo.AccesoDatos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class RolLN
    {
        public List<Rol> ListaRol()
        {
            try
            {
                RolDA roles = new RolDA();
                return roles.ListaRol();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}

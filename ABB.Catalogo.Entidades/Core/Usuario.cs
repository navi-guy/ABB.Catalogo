using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Catalogo.Entidades.Core
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string CodUsuario { get; set; }
        public string ClaveTxt { get; set; }
        public byte[] Clave { get; set; }
        public string Nombres { get; set; }
        public int IdRol { get; set; }
        //----Adicional
        public string DesRol { get; set; }


    }
}

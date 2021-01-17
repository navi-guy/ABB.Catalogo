using ABB.Catalogo.AccesoDatos.Core;
using ABB.Catalogo.Entidades.Base;
using ABB.Catalogo.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABB.Catalogo.LogicaNegocio.Core
{
    public class UsuarioApiLN : BaseLN
    {
        public UsuarioApi BuscaUsuarioApi(UsuarioApi PamUsuarioApi)
        {
            return new UsuarioApiDA().BuscaUsuarioApi(PamUsuarioApi);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de Web_service_estados
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Web_service_estados : System.Web.Services.WebService
    {

        [WebMethod]

        public ModeloDatos.Estado MostrarEstados(int id_estado)
        {
            return NegocioElevenShop.Negocio_Estado.GetEstado(id_estado);
        }
    }
}

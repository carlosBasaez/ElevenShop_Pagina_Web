using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ModeloDatos;
using NegocioElevenShop;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de WebService_Bodeguero
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Bodeguero : System.Web.Services.WebService
    {

        [WebMethod]
        public bool insert(string rut)
        {
            Bodeguero bodeguero = new Bodeguero();
            bodeguero.Rut = rut;

            return Negocio_Bodeguero.insert(bodeguero);
        }

        [WebMethod]
        public bool delete(string rut)
        {
            return Negocio_Bodeguero.delete(rut);
        }

        [WebMethod]
        public Bodeguero get(string rut)
        {
            return Negocio_Bodeguero.get(rut);
        }

        [WebMethod]
        public List<Bodeguero> listar()
        {
            return Negocio_Bodeguero.list();
        }

        [WebMethod]
        public List<Usuario> listarUsuario()
        {
            return Negocio_Bodeguero.listUsuariosBodeguero();
        }

        [WebMethod]
        public bool exist(string rut)
        {
            return Negocio_Bodeguero.exist(rut);
        }

    }
}

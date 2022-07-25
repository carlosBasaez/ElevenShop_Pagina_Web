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
    /// Descripción breve de WebService_Despachador
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Despachador : System.Web.Services.WebService
    {

        [WebMethod]
        public bool insert(string rut)
        {
            Despachador despachador = new Despachador();
            despachador.Rut = rut;

            return Negocio_Despachador.insert(despachador);
        }

        [WebMethod]
        public bool delete(string rut)
        {
            return Negocio_Despachador.delete(rut);
        }

        [WebMethod]
        public Despachador get(string rut)
        {
            return Negocio_Despachador.get(rut);
        }

        [WebMethod]
        public List<Despachador> listar()
        {
            return Negocio_Despachador.list();
        }

        [WebMethod]
        public List<Usuario> listarUsuario()
        {
            return Negocio_Despachador.listUsuariosDespachador();
        }

        [WebMethod]
        public bool exist(string rut)
        {
            return Negocio_Despachador.exist(rut);
        }

    }
}

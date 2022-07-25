using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NegocioElevenShop;
using ModeloDatos;

namespace WebServiceShop
{
    /// <summary>
    /// Descripción breve de WebService_Administrador
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Administrador : System.Web.Services.WebService
    {

        [WebMethod]
        public bool insert(string rut)
        {
            Administrador admin = new Administrador();
            admin.Rut = rut;

            return Negocio_Administrador.insert(admin);
        }

        [WebMethod]
        public bool delete(string rut)
        {
            return Negocio_Administrador.delete(rut);
        }

        [WebMethod]
        public Administrador get(string rut)
        {
            return Negocio_Administrador.get(rut);
        }

        [WebMethod]
        public List<Administrador> listar()
        {
            return Negocio_Administrador.list();
        }

        [WebMethod]
        public List<Usuario> listarUsuario()
        {
            return Negocio_Administrador.listUsuariosAdmin();
        }

        [WebMethod]
        public bool exist(string rut)
        {
            return Negocio_Administrador.exist(rut);
        }
    }
}

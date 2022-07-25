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
    /// Descripción breve de WebService_Usuario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Usuario : System.Web.Services.WebService
    {

        [WebMethod]
        public bool insertUsuario(string nombre, string correo, string rut, string pass, int telefono)
        {
            Usuario user = new Usuario();
            user.nombrecompleto = nombre;
            user.email = correo;
            user.rut = rut;
            user.pass = pass;
            user.telefono = telefono;
            bool insertado = Negocio_Usuario.insertarUsuario(user);
            if (insertado)
            {
                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                servicio_correo.SendUsuarioNuevo(rut, correo);
            }
            return insertado;
        }

        [WebMethod]
        public bool updateUsuario(string nombre, string correo, string rut, int telefono)
        {
            Usuario user = new Usuario();
            user.nombrecompleto = nombre;
            user.email = correo;
            user.rut = rut;
            user.pass = "te quiero pipo";
            user.telefono = telefono;
            bool actualizado = Negocio_Usuario.updateUsuario(user);
            if (actualizado)
            {
                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                servicio_correo.SendActualizacionUsuario(rut, correo);
            }
            return actualizado;
        }

        [WebMethod]
        public bool NewChangePass(string rut)
        {
            bool estado = true;

            Usuario user = Negocio_Usuario.getUsuario(rut);
            if (user == null) return false;

            //Quitar llave anterior
            if (Negocio_PassToken.Exist(rut))
            {
                Negocio_PassToken.Delete(rut);
            }

            //Agregar nueva llave
            var passToken = new PassToken();
            passToken.Rut = rut;
            passToken.Token = Utilities.GetRandomString().ToUpper();
            estado = Negocio_PassToken.Create(passToken);
            
            //Enviar Correo de Llave
            if (estado)
            {
                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                estado = servicio_correo.SendNewCambioContraseña(rut, user.email, passToken.Token);
            }

            return estado;
        }

        [WebMethod]
        public bool ChangePass(string token, string pass)
        {
            PassToken passToken = Negocio_PassToken.GetToken(token.ToUpper());
            
            if (passToken == null) return false;

            Negocio_PassToken.Delete(passToken.Rut);

            Usuario usuario = Negocio_Usuario.getUsuario(passToken.Rut);
            usuario.pass = pass;
            
            bool estado = Negocio_Cliente.actualizarpass(usuario);

            if (estado)
            {
                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                estado = servicio_correo.SendCambioContraseña(usuario.rut, usuario.email);
            }

            return estado;
        }

        [WebMethod]
        public bool deleteUsuario(string rut)
        {
            return Negocio_Usuario.deleteUsuario(rut);
        }

        [WebMethod]
        public List<Usuario> listar()
        {
            return Negocio_Usuario.list();
        }

        [WebMethod]
        public Usuario getUsuario(string rut)
        {
            return Negocio_Usuario.getUsuario(rut);
        }

        [WebMethod]
        public bool existUsuario(string rut)
        {
            return Negocio_Usuario.existUsuario(rut);
        }

        [WebMethod]
        public Usuario getUsuarioSesion(string rut, string pass)
        {
            return Negocio_Usuario.getUsuario(rut, pass);
        }

        [WebMethod]
        public bool existUsuarioSesion(string rut, string pass)
        {
            return Negocio_Usuario.existUsuario(rut, pass);
        }

        [WebMethod]
        public bool tipoCliente(string rut)
        {
            return Negocio_Cliente.existCliente(rut);
        }

        [WebMethod]
        public bool tipoAdministrador(string rut)
        {
            return Negocio_Administrador.exist(rut);
        }

        [WebMethod]
        public bool tipoBodeguero(string rut)
        {
            return Negocio_Bodeguero.exist(rut);
        }

        [WebMethod]
        public bool tipoDespachador(string rut)
        {
            return Negocio_Despachador.exist(rut);
        }

    }
}

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
    /// Descripción breve de WebService_cliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService_cliente : System.Web.Services.WebService
    {

        [WebMethod]
        public string insertar_usuario(string nombre_completo, string email, string rut, string pass, int telefono, string direccion, string ciudad, int comuna)
        {
            ModeloDatos.Usuario usuario = new ModeloDatos.Usuario();
            usuario.nombrecompleto = nombre_completo;
            usuario.email = email;
            usuario.rut = rut;
            usuario.pass = pass;
            usuario.telefono = telefono;

            ModeloDatos.Cliente cliente = new ModeloDatos.Cliente();
            cliente.rut_cliente = rut;
            cliente.direccion = direccion;
            cliente.ciudad = ciudad;
            cliente.comuna = comuna;
            if (NegocioElevenShop.Negocio_Cliente.existCliente(rut) == false)
            {
                if (!NegocioElevenShop.Negocio_Usuario.existUsuario(rut))
                {
                    if (!NegocioElevenShop.Negocio_Usuario.insertarUsuario(usuario))
                        return "usuario_no_creado";
                }

                if (NegocioElevenShop.Negocio_Cliente.InsertarCliente(cliente))
                {
                    ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                    servicio_correo.SendUsuarioNuevo(rut, email);
                    return "cliente_creado";
                }
                else
                {
                    return "cliente_no_creado";
                }
            }
            return "existe_cliente";
        }

        [WebMethod]
        public string editar_pass (string rut, string pass)
        {
            if (NegocioElevenShop.Negocio_Cliente.existCliente(rut) == true)
            {
                ModeloDatos.Usuario usuario = new ModeloDatos.Usuario();
                usuario.rut = rut;
                usuario.pass = pass;

                NegocioElevenShop.Negocio_Cliente.actualizarpass(usuario);

                ServiceCorreo.WebService_CorreoSoapClient servicio_correo = new ServiceCorreo.WebService_CorreoSoapClient();
                servicio_correo.SendCambioContraseña(rut, Negocio_Usuario.getUsuario(rut).email);

                return "contraseña actualizada";
            }
            else
            {
                return "Contraseña no actualizada, cliente no existe";
            }            
        }

        [WebMethod]
        public bool insert(string rut, string direccion, string ciudad, int comuna)
        {
            ModeloDatos.Cliente cliente = new ModeloDatos.Cliente();
            cliente.rut_cliente = rut;
            cliente.direccion = direccion;
            cliente.comuna = comuna;
            cliente.ciudad = ciudad;
            return NegocioElevenShop.Negocio_Cliente.InsertarCliente(cliente);
        }

        [WebMethod]
        public bool update(string rut, string direccion, string ciudad, int comuna)
        {
            ModeloDatos.Cliente cliente = new ModeloDatos.Cliente();
            cliente.rut_cliente = rut;
            cliente.direccion = direccion;
            cliente.comuna = comuna;
            cliente.ciudad = ciudad;
            return NegocioElevenShop.Negocio_Cliente.updateCliente(cliente);
        }

        [WebMethod]
        public bool delete(string rut)
        {
            return NegocioElevenShop.Negocio_Cliente.deleteCliente(rut);
        }

        [WebMethod]
        public Cliente get(string rut)
        {
            return Negocio_Cliente.getCliente(rut);
        }

        [WebMethod]
        public bool exist(string rut)
        {
            return NegocioElevenShop.Negocio_Cliente.existCliente(rut);
        }
    }
}

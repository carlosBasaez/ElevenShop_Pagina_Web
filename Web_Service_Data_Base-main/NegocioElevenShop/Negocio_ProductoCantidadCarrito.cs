using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionOracle;
using ModeloDatos;

namespace NegocioElevenShop
{
    public class Negocio_ProductoCantidadCarrito
    {
        public static List<ProductoCantidadCarrito> Lista(int id_carrito)
        {
            List<ProductoCantidadCarrito> lista = new List<ProductoCantidadCarrito>();
            ConexionBD bd = new ConexionBD();
            string query = $"select pc.idcarrito, p.idproducto, p.nombre, pc.cantidad, p.valor from producto_cantidad pc join producto p on (pc.idproducto = p.idproducto) where pc.idcarrito = {id_carrito}";
            List<object[]> select = bd.Query(query);

            try
            {
                ProductoCantidadCarrito pcc;
                foreach (var row in select)
                {
                    pcc = new ProductoCantidadCarrito();
                    pcc.IdCarrito = int.Parse(row[0].ToString());
                    pcc.IdProducto = int.Parse(row[1].ToString());
                    pcc.Nombre = row[2].ToString();
                    pcc.Cantidad = int.Parse(row[3].ToString());
                    pcc.Valor = int.Parse(row[4].ToString());
                    lista.Add(pcc);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(">> Exception: " + ex.Message);
                lista = new List<ProductoCantidadCarrito>();
            }

            return lista;
        }

        public static int total_carrito(int id_carrito)
        {
            List<ProductoCantidadCarrito> list = Lista(id_carrito);
            int valor = 0;
            foreach (var item in list)
            {
                valor += item.Cantidad * item.Valor;
            }
            return valor;
        }
    }
}

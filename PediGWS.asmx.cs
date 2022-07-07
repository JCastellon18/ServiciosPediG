using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ServiciosPediG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServiciosPediG
{
    /// <summary>
    /// Descripción breve de PediGWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PediGWS : System.Web.Services.WebService
    {

        PediGModelContainer db = new PediGModelContainer();
        ApplicationDbContext context = new ApplicationDbContext();

        public class ResultRegister
        {
            public bool Band { get; set; }
            public string Mensaje { get; set; }
        }

        [WebMethod]
        public ResultRegister Register(string nombre, string nombreUsuario , string email, string Pass)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ResultRegister Value = new ResultRegister();

            User Existe = db.Users.Where(x => x.Email == email).FirstOrDefault();

            if (Existe == null)
            {
                var user = new ApplicationUser();
                user.UserName = email;
                user.Email = email;
                string userPWD = Pass;

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role User    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "User");

                    User item = new User();
                    item.Nombre = nombre;
                    item.NombreUsuario = nombreUsuario;
                    item.Email = email;
                    item.Contrasenia = Pass;
                    db.Users.Add(item);
                    db.SaveChanges();
                    Value.Band = true;
                    Value.Mensaje = "Usuario registrado con exito";
                }
                else
                {
                    Value.Band = false;
                    Value.Mensaje = "Ocurrio un error al crear usuario";
                }
            }
            else
            {
                Value.Band = false;
                Value.Mensaje = "Ya existe un usuario con este correo";
            }

            return Value;
        }
        [WebMethod]
        public bool Login(string user, string pass)
        {
            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta,
            //cambie a shouldLockout: true
            // var result = SignInManager.PasswordSignInAsync(user, pass, false, shouldLockout: false);
            var result = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>().PasswordSignIn(user, pass, false, false);
            if (result == SignInStatus.Success)
            {
                return true;
            }
            else
                return false;
        }

        [WebMethod]
        public string CambiarContrasenia(string email, string currentPass, string newPass)
        {
            ApplicationDbContext SecurityDB = new ApplicationDbContext(); // Acceso a BD de seguridad de usaurio
            ApplicationUserManager UserManager = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>(); // Permite el acceso a la configuracion de la gestion de usuario

            if (!string.IsNullOrEmpty(email))
            {
                var User = SecurityDB.Users.FirstOrDefault(it => it.Email == email);

                if (User != null)
                {
                    var result = UserManager.ChangePassword(User.Id, currentPass, newPass);

                    if (result.Succeeded)
                        return "true";
                    else
                        return "false";
                }
                return "No existe un Usuario con ese correo Electronico..";
            }
            return "Ingrese un Correo";
        }
        [WebMethod]
        public bool AddCliente(string nombres, string apellidos, int numTelf, string correo, string user)
        {
            Cliente item = new Cliente();
            item.Nombres = nombres;
            item.Apellidos = apellidos;
            item.NumTelf = numTelf;
            item.Correo = correo;
            item.User = user;
            db.Clientes.Add(item);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
                return false;
        }
        [WebMethod]
        public bool UpdateCliente(int Id, string nombres, string apellidos, int numTelf, string correo, string user)
        {
            var item = db.Clientes.Find(Id);
            if (item != null)
            {
                item.Nombres = nombres;
                item.Apellidos = apellidos;
                item.NumTelf = numTelf;
                item.Correo = correo;
                item.User = user;

                db.Entry(item).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        [WebMethod]
        public bool DeleteCliente(int Id)
        {
            var item = db.Clientes.Find(Id);
            if (item != null)
            {
                if (item.Facturas.Count > 0)
                {
                    return false;
                }
                else
                {
                    db.Clientes.Remove(item);
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }
        [WebMethod]
        public bool AddArticulo(string nombreArticulo, string descripcion, byte[] img, double precio, int cantidad, int categoria, string user, ArticuloSW x)
        {
            Articulo item = new Articulo();
            item.NombreArticulo = nombreArticulo;
            item.Descripcion = descripcion;
            item.Img = img;
            item.Precio = precio;
            item.Cantidad = cantidad;
            item.CategoriaId = categoria;
            item.User = user;

            db.Articulos.Add(item);
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
                return false;
        }
        [WebMethod]
        public bool UpdateArticulo(int Id, string nombreArticulo, string descripcion, byte[]img, double precio, int cantidad, int categoriaId, string user)
        {
            var item = db.Articulos.Find(Id);
            if (item != null)
            {
                item.NombreArticulo = nombreArticulo;
                item.Descripcion = descripcion;
                item.Img = img;
                item.Precio = precio;            
                item.Cantidad = cantidad;              
                item.CategoriaId = categoriaId;
                item.User = user;

                db.Entry(item).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        [WebMethod]
        public bool DeleteArticulo(int Id)
        {
            var item = db.Articulos.Find(Id);
            if (item != null)
            {
                if (item.DetalleFacturas.Count > 0)
                {
                    return false;
                }
                else
                {
                    db.Articulos.Remove(item);
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }
        //public bool AddFactura(int numFactura, DateTime fecha, double totalNeto, int tipoPagoId, int tipoEntegaId, int clienteId, string direccionPedido, int estadoFacturaId, DateTime fechaEntregaPedido, string user, int articuloId, int cantidad, double subtotal)
        //{
        //    Factura item = new Factura();
        //    DetalleFactura det = new DetalleFactura();
        //    Articulo art = new Articulo();
        //    item.NumeroFactura = numFactura;
        //    item.FechaFactura = fecha;
        //    item.TotalNeto = totalNeto;
        //    item.TipoDePagoId = tipoPagoId;
        //    item.TipoEntregaId = tipoEntegaId;
        //    item.ClienteId = clienteId;
        //    item.DireccionPedido = direccionPedido;
        //    item.EstadoFacturaId = estadoFacturaId;
        //    item.FechaEntregaPedido = fechaEntregaPedido;            
        //    db.Facturas.Add(item);

        //    if (db.SaveChanges() > 0)
        //    {
        //        return true;
        //    }
        //    else       
        //        return false;

        //    det.ArticuloId = articuloId;
        //    det.Cantidad = cantidad;
        //}
        [WebMethod]
        public List<ArticuloSW> getArticuloSW(string email)
        {
            return db.Articulos.Where(x => x.User == email).Select(x => new ArticuloSW()
            {
                Id = x.Id,
                nombreArticulo = x.NombreArticulo,
                descripcion = x.Descripcion,
                img = x.Img,
                precio = x.Precio,
                cantidad = x.Cantidad,
                categoriaId = x.Categoria.NombreCategoria,              
            }).ToList();
        }

        [WebMethod]
        public List<ClienteSW> getClienteSW(string email)
        {
            return db.Clientes.Where(x => x.User == email).Select(x => new ClienteSW()
            {
                Id = x.Id,
                nombres = x.Nombres,
                apellidos = x.Apellidos,
                numTelf = (int)x.NumTelf,
                correo = x.Correo,
            }).ToList();
        }
        [WebMethod]
        public List<DetalleFactSW> getDetalleFactSW(int nfact)
        {
            return db.DetalleFacturas.Where(x => x.FacturaId == nfact).Select(x => new DetalleFactSW()
            {
                Id = x.Id,
                precioUnitario = x.PrecionUnitario,
                cantidad = x.Cantidad,
                subTotal = x.Subtotal,
                facturaId = x.Factura.NumeroFactura,
                articuloId = x.Articulo.NombreArticulo
            }).ToList();
        }
        
        public class ArticuloSW
        {
            public int Id { get; set; }
            public string nombreArticulo { get; set; }
            public string descripcion { get; set; }
            public byte[] img { get; set; }
            public double precio { get; set; }
            public int cantidad { get; set; }
            public string categoriaId { get; set; }
        }
       
        public class ClienteSW
        {
            public int Id { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }          
            public int numTelf { get; set; }
            public string correo { get; set; }
        }
        public class FacturaSW
        {
            public int Id { get; set; }
            public int numFactura { get; set; }
            public DateTime fechaFactura { get; set; }
            public double totalNeto { get; set; }
            public string típoPagoId { get; set; }
            public string tipoEntregaId{ get; set; }
            public string clienteId { get; set; }
            public string direccionPedido { get; set; }
            public string estadoFacturaId { get; set; }
            public DateTime fechaEntregaPedido { get; set; }
        }
        public class DetalleFactSW
        {
            public int Id { get; set; }
            public double precioUnitario { get; set; }
            public double cantidad { get; set; }
            public double subTotal { get; set; }
            public int facturaId { get; set; }
            public string articuloId { get; set; }
        }       
    }
}
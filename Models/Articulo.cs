//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiciosPediG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Articulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articulo()
        {
            this.DetalleFacturas = new HashSet<DetalleFactura>();
        }
    
        public int Id { get; set; }
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public byte[] Img { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int CategoriaId { get; set; }
        public string User { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}

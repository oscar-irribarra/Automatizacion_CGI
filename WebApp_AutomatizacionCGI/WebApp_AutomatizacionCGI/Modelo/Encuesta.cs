//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp_AutomatizacionCGI.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Encuesta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Encuesta()
        {
            this.DetalleRespuesta = new HashSet<DetalleRespuesta>();
        }
    
        public int ID_Encuesta { get; set; }
        public int ID_Pad { get; set; }
        public string Detalle { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleRespuesta> DetalleRespuesta { get; set; }
        public virtual Pad Pad { get; set; }
    }
}
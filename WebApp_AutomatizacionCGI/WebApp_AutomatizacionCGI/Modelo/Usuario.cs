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
    
    public partial class Usuario
    {
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public int ID_Estado { get; set; }
    
        public virtual Estado Estado { get; set; }
    }
}

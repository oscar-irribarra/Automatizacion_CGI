﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bd_entities : DbContext
    {
        public bd_entities()
            : base("name=bd_entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Curso_Docente> Curso_Docente { get; set; }
        public virtual DbSet<Docente> Docente { get; set; }
        public virtual DbSet<Encargado> Encargado { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Pad> Pad { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Encuesta> Encuesta { get; set; }
        public virtual DbSet<Pregunta> Pregunta { get; set; }
        public virtual DbSet<Respuestas> Respuestas { get; set; }
    }
}

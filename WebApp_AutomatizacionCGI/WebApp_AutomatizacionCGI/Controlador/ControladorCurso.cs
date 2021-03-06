﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp_AutomatizacionCGI.Modelo;

namespace WebApp_AutomatizacionCGI.Controlador
{
    public class ControladorCurso
    {
        bd_entities contexto = new bd_entities();

        public List<Curso> listaCursos()
        {
            try
            {
                return contexto.Curso.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool addCursos(Curso nuevo)
        {
            try
            {
                contexto.Curso.Add(nuevo);
                return contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ActualizarCurso(Curso nuevo)
        {
            try
            {
                Curso original = new Curso();
                original = contexto.Curso.Find(nuevo.ID_Curso);
                original.Rut_Encargado = nuevo.Rut_Encargado;
                original.Detallecurso = nuevo.Detallecurso;
                original.ID_Estado = nuevo.ID_Estado;
                original.Usuario = nuevo.Usuario;
                original.Contraseña = nuevo.Contraseña;

                return contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<object> listaCurso_Pad()
        {
            try
            {
                var consulta = from c in contexto.Curso
                               join e in contexto.Encargado on c.Rut_Encargado equals e.Rut
                               join est in contexto.Estado on c.ID_Estado equals est.ID_Estado
                               select new { c.ID_Curso, c.Rut_Encargado, e.Nombre, e.Apellido, c.Detallecurso, c.ID_Estado, est.Detalle, c.Usuario, c.Contraseña };

                return consulta.ToList<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool validarCurso(String usuario, String pass)
        {
            try
            {
                var consulta = from c in contexto.Curso
                               where c.Usuario.Equals(usuario) && c.Contraseña.Equals(pass)
                               select c;
                bool valido = consulta.Count() == 1;
                if (valido == true)
                {
                    int estado_curso = consulta.First().ID_Estado;
                    if (estado_curso == 1)
                    {

                    }
                    else
                    {
                        valido = false;
                    }
                }
                return valido;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<object> Lista_Buscar_Curso(string rut)
        {
            try
            {
                var consulta = from c in contexto.Curso
                               join e in contexto.Encargado on c.Rut_Encargado equals e.Rut
                               join est in contexto.Estado on c.ID_Estado equals est.ID_Estado
                               where e.Codigo == rut
                               select new { c.ID_Curso, c.Rut_Encargado, e.Nombre, e.Apellido, c.Detallecurso, c.ID_Estado, est.Detalle, c.Usuario, c.Contraseña };

                return consulta.ToList<object>();
            }
            catch (Exception)
            {
                return null;
            }
        }





    }
}
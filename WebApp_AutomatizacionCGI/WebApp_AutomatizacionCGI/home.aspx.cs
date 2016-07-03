﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using WebApp_AutomatizacionCGI.Controlador;
using WebApp_AutomatizacionCGI.Modelo;

namespace WebApp_AutomatizacionCGI
{
    public partial class home : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MultiView1.ActiveViewIndex = 3;
                MostrarDocentes();

                
                MostrarCursos(); //view add cursos

                Mostrar_AsignarDocentes(); //mostrar gridview asignar docentes
            }
        }

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            //volver a view 0
        }

        //--------------------------------------------------------vista docentes--------------------------------------------------------

        public void MostrarDocentes()
        {
            ControladorDocente control = new ControladorDocente();
            GridView_docentes.DataSource = control.listaDocentes();
            GridView_docentes.DataBind();
        }
            

        protected void btn_addDocente_Click(object sender, EventArgs e)
        {
            ControladorDocente control = new ControladorDocente();

            String nombre = txt_nombreDocente.Text;
            String rut = txt_rutDocente.Text +"-"+ txt_digitoDocente.Text;
            String apellido = txt_apellidoDocente.Text;
            DateTime fecha = Convert.ToDateTime(txt_fechaingresoDocente.Text);
            String correo = txt_correoDocente.Text;
            String codigo = rut;
            int id_Estado = 1;

            Docente nuevo = new Docente
            {
                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Fecha_Ingreso = fecha,
                Correo = correo,
                ID_Estado = id_Estado,
                Codigo = codigo

            };

            if (control.addDocentes(nuevo))
            {
                MostrarDocentes();
            }
        }

        protected void GridView_docentes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView_docentes.EditIndex = e.NewEditIndex;
            MostrarDocentes();          
        }

        protected void GridView_docentes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //valor anterior
           string rut = "";
            rut = GridView_docentes.DataKeys[e.RowIndex].Values[0].ToString();

            //valores nuevos
            TextBox txtnombreDoc = (TextBox)GridView_docentes.Rows[e.RowIndex].FindControl("TextBox1_nombreDocente");
            TextBox txtapellidoDoc = (TextBox)GridView_docentes.Rows[e.RowIndex].FindControl("TextBox2_apellidoDocente");
            TextBox txtcorreoDoc = (TextBox)GridView_docentes.Rows[e.RowIndex].FindControl("TextBox3_correoDocente");

            Docente nuevo = new Docente
            {
                Rut = rut,
                Nombre = txtnombreDoc.Text,
                Apellido = txtapellidoDoc.Text,
                Correo = txtcorreoDoc.Text
            };

            ControladorDocente control = new ControladorDocente();

            control.ActualizarDocente(nuevo);
            GridView_docentes.EditIndex = -1;
            MostrarDocentes();

        }

        protected void GridView_docentes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_docentes.EditIndex = -1;
            MostrarDocentes();
        }

        protected void GridView_docentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_docentes.PageIndex = e.NewPageIndex;
            MostrarDocentes();
        }

        //--------------------------------------------------------vista cursos--------------------------------------------------------

        protected void GridView_cursos_PageIndexChanging(object sender, GridViewPageEventArgs e)  //paginacion gridview cursos
        {
            GridView_cursos.PageIndex = e.NewPageIndex;
        }

        public void MostrarCursos()
        {
            ControladorCurso control = new ControladorCurso();
            GridView_cursos.DataSource = control.listaCurso_Pad();
            GridView_cursos.DataBind();
        }

        protected void Link_CursoNuevo_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1_cursonuevo.Show();
            lbCurso.Visible = false;
            txt_RutEncargardo_Curso.Text = "";
            txt_detalleCurso.Text = "";            
        }

        protected void Link_GuardarCurso_Click(object sender, EventArgs e)
        {
            ControladorCurso control = new ControladorCurso();

            ControladorEncargado controlEnc = new ControladorEncargado();

            string rut = txt_RutEncargardo_Curso.Text;
            string detalle = txt_detalleCurso.Text;

            if (rut == "" || detalle == "")
            {    
                lbCurso.Text = "rellene los campos";
                txt_RutEncargardo_Curso.Text = rut;
                txt_detalleCurso.Text = detalle;
                lbCurso.Visible = true;
                ModalPopupExtender1_cursonuevo.Show();
            }
            else
            {
                //validar que exista rut de encargado   
                int aux = controlEnc.listaBuscarEncargados(rut).Count;                          

                if (aux == 0)
                {
                    lb_encargadoNoencontrado.Text = "El encargado que Ingreso no existe <br/> Desea crear uno nuevo?";
                    ModalPopupExtender2_ConfirmarEncargado.Show();
                }
                else
                {
                    Curso nuevo = new Curso
                    {
                        Rut_Encargado = rut,
                        Detalle = detalle,
                        ID_Estado = 1

                    };

                    if (control.addCursos(nuevo))
                    {
                        MostrarCursos();
                    }


                    lbCurso.Visible = false;
                    txt_RutEncargardo_Curso.Text = "";
                    txt_detalleCurso.Text = "";
                }               
            }
        }

        protected void Link_si_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3_encargadonuevo.Show();
        }

        protected void Link_GuardarEncargado_Click(object sender, EventArgs e)
        {
            ControladorEncargado control = new ControladorEncargado();

            string rut = txt_RutEncargardo.Text;
            string nombre = txt_NombreEncargado.Text;
            string apellido = txt_ApellidoEncargado.Text;
            string correo = txt_CorreoEncargado.Text;
            string codigo = rut;

            Encargado nuevo = new Encargado
            {
                Rut = rut,
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                ID_Estado = 1,
                Codigo = codigo

            };

            if (control.addEncargados(nuevo))
            {
                ModalPopupExtender1_cursonuevo.Show();
                txt_RutEncargardo_Curso.Text = rut;
            }
        }
             
        protected void GridView_cursos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbcodigoasignar = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label1");
            lb_idcurso.Text = lbcodigoasignar.Text;
            MostrarPad_Curso();
        }

        public void MostrarPad_Curso()
        {
            int id_curso=0;
            int.TryParse(lb_idcurso.Text, out id_curso);
            
            ControladorPad control = new ControladorPad();
            GridView_detallePad.DataSource = control.listaPad_Cursos(id_curso);
            GridView_detallePad.DataBind();
        }

        protected void Link_ModalPad_Click(object sender, EventArgs e)
        {            
            ModalPopupExtender4_detallecurso.Show();
        }

        protected void Link_NuevoPad_Click(object sender, EventArgs e)
        {
            ModalPopupExtender5_padnuevo.Show();
        }

        protected void Link_GuardarPad_Click(object sender, EventArgs e)
        {
            ControladorPad control = new ControladorPad();
            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);
            
            string sala = txt_numerosala.Text;
            string coffe = txt_numerosalacoffe.Text;
            string hora_inicio = txt_horainicio.Text+":"+txt_minutoinicio.Text;
            string hora_fin = txt_horafin.Text+":"+txt_minutofin.Text;
            DateTime fecha = Convert.ToDateTime(txt_fechapad.Text);
            int id_estado = 1;

            Pad nuevo = new Pad
            {
                ID_Curso = id_curso,
                Sala = sala,
                Sala_Coffe = coffe,
                Hora_Inicio = hora_inicio,
                Hora_Termino = hora_fin,
                Fecha = fecha,
                ID_Estado = id_estado

            };

            if (control.addPad(nuevo))
            {
                MostrarPad_Curso();
                ModalPopupExtender4_detallecurso.Show();
            }
        }

        public void Mostrar_AsignarDocentes()
        {
            ControladorDocente control = new ControladorDocente();

            GridView_asignardocentes.DataSource = control.listaAsignar_Docentes();
            GridView_asignardocentes.DataBind();

        }
        
        protected void Link_viewAsignarDocentes_Curso_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;            
        }

        protected void Link_volverviewcursos_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
        }

        protected void GridView_asignardocentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {           
            GridView_asignardocentes.PageIndex = e.NewPageIndex;            
            Mostrar_AsignarDocentes();
        }

     
    }
}
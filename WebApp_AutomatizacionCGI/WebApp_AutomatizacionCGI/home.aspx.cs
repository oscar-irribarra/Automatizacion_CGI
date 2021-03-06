﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using WebApp_AutomatizacionCGI.Controlador;
using WebApp_AutomatizacionCGI.Modelo;
using System.Web.Security;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace WebApp_AutomatizacionCGI
{
    public partial class home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                MultiView1.ActiveViewIndex = 0;
                //VISTA DOCENTES
                MostrarDocentes();
                MostrarEstado_Docentes();
                //VISTA ENCARGADOS
                MostrarEncargados();
                MostrarEstado_Encargados();
                //VISTA CURSOS
                MostrarCursos();
                MostrarEstado_Encargado_Cursos();

                MostrarEstado_Pad();
                MostrarEstado_Cursos();

                Mostrar_AsignarDocentes(); //VISTA CURSO_ASIGNAR DOCENTE
                //VISTA USUARIO
                MostrarUsuarios();
                MostrarTipoUsuario();
                MostrarEstadoUsuario();

                MostrarDatosUsuarios();
                cargarReportesEncuestas();
               


            }
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.Link_ExportarAsistenciaAExcel);
            scriptManager.RegisterPostBackControl(this.Link_ExportarAsistenciaAPDF);
            scriptManager.RegisterPostBackControl(this.Link_ExportarEncuestasaPDF);
            scriptManager.RegisterPostBackControl(this.Link_ExportarEncuestasaExcel);
            

        }

        public void MostrarDatosUsuarios()
        {
            ControladorUsuario control = new ControladorUsuario();
            try
            {
                lb_NombreUsuario_Logeado.Text = control.DevolverNombreUsuario(Session["IDUsuario"].ToString()) + " " + control.DevolverApellidoUsuario(Session["IDUsuario"].ToString());
                lb_fechadeAcceso.Text = "<b>Ultimo Acceso:</b> " + DateTime.Today.ToShortDateString();
            }
            catch (Exception) { }
        }

        //MENU DE NAVEGACION

        protected void Link_VistaDocentes_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            MostrarDocentes();
            txt_BuscarDocente.Text = "";
        }

        protected void Link_VistaEncargados_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MostrarEncargados();
            txt_BuscarEncargado.Text = "";
        }

        protected void Link_VistaCursos_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            MostrarCursos();
            txt_BuscarCurso.Text = "";
        }

        protected void Link_volverviewcursos_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            
            lb_UsuariosYaAsignado.Text = "";
        }

        protected void Link_viewAsignarDocentes_Curso_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
            Mostrar_AsignarDocentes();
            txt_Buscar_DocenteAsignar.Text = "";            
        }

        protected void Link_VistaUsuarios_Click(object sender, EventArgs e)
        {
            txt_nickname.Text = "";
            txt_password.Text = "";
            MultiView1.ActiveViewIndex = 5;
        }

        protected void Link_CerrarSession_Click(object sender, EventArgs e)
        {
            Session.Abandon();

            FormsAuthentication.SignOut();

            FormsAuthentication.RedirectToLoginPage();
        }


        //METODOS GENERALES

        protected void btn_volver_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        public void MostrarEstado_Docentes()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoDocente.DataSource = control.listaEstado();
            cb_EstadoDocente.DataTextField = "Detalle";
            cb_EstadoDocente.DataValueField = "ID_Estado";
            cb_EstadoDocente.DataBind();
            cb_EstadoDocente.SelectedIndex = 0;
        }

        public void MostrarEstado_Encargados()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoEncargado.DataSource = control.listaEstado();
            cb_EstadoEncargado.DataTextField = "Detalle";
            cb_EstadoEncargado.DataValueField = "ID_Estado";
            cb_EstadoEncargado.DataBind();
            cb_EstadoEncargado.SelectedIndex = 0;
        }

        public void MostrarEstado_Encargado_Cursos()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoEncargadoCurso.DataSource = control.listaEstado();
            cb_EstadoEncargadoCurso.DataTextField = "Detalle";
            cb_EstadoEncargadoCurso.DataValueField = "ID_Estado";
            cb_EstadoEncargadoCurso.DataBind();
            cb_EstadoEncargadoCurso.SelectedIndex = 0;
        }

        public void MostrarEstado_Cursos()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoCurso.DataSource = control.listaEstado();
            cb_EstadoCurso.DataTextField = "Detalle";
            cb_EstadoCurso.DataValueField = "ID_Estado";
            cb_EstadoCurso.DataBind();
            cb_EstadoCurso.SelectedIndex = 0;
        }

        public void MostrarEstado_Pad()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoPad.DataSource = control.listaEstado();
            cb_EstadoPad.DataTextField = "Detalle";
            cb_EstadoPad.DataValueField = "ID_Estado";
            cb_EstadoPad.DataBind();
            cb_EstadoPad.SelectedIndex = 0;
        }

        public void MostrarEstadoUsuario()
        {
            ControladorEstado control = new ControladorEstado();

            cb_EstadoUsuario.DataSource = control.listaEstado();
            cb_EstadoUsuario.DataTextField = "Detalle";
            cb_EstadoUsuario.DataValueField = "ID_Estado";
            cb_EstadoUsuario.DataBind();
            cb_EstadoUsuario.SelectedIndex = 0;
        }

        public void MostrarTipoUsuario()
        {
            ControladorEstado control = new ControladorEstado();

            List<String> tipo = new List<String>();

            tipo.Add("root");
            tipo.Add("normal");

            cb_TipoUsuario.DataSource = tipo;
            cb_TipoUsuario.DataBind();
        }


        //VISTA DOCENTES

        public void MostrarDocentes()
        {
            ControladorDocente control = new ControladorDocente();
            GridView_docentes.DataSource = control.listaGridView_Docentes();
            GridView_docentes.DataBind();
        }

        protected void Link_AbrirModalDocente_Click(object sender, EventArgs e)
        {
            ModalPopupExtender0_ModalDocente.Show();


            txt_rutDocente.Enabled = true;


            cb_EstadoDocente.Enabled = false;
            cb_EstadoDocente.SelectedValue = "1";
            txt_rutDocente.Text = "";

            txt_nombreDocente.Text = "";
            txt_apellidoDocente.Text = "";
            txt_correoDocente.Text = "";
            txt_fechaingresoDocente.Text = "";
            lb_AvisoDocente.Text = "";

            Link_addDocente.Visible = true;
            Link_EditarDocente.Visible = false;
        }

        protected void Link_addDocente_Click(object sender, EventArgs e)
        {
            ControladorDocente control = new ControladorDocente();

            String nombre = txt_nombreDocente.Text;
            String rut = txt_rutDocente.Text;
            String apellido = txt_apellidoDocente.Text;

            String correo = txt_correoDocente.Text;

            string codigo = "";
            codigo = rut.ToUpper();
            codigo = codigo.Replace(".", "");
            codigo = codigo.Replace("-", "");
            codigo = codigo.Replace("_", "");


            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            int id_Estado = 0;
            int.TryParse(cb_EstadoDocente.SelectedValue, out id_Estado);

            if (txt_nombreDocente.Text == "..-" || rut.Length < 11 || txt_rutDocente.Text == "" || txt_apellidoDocente.Text == "" || txt_fechaingresoDocente.Text == "" ||
                txt_correoDocente.Text == "")
            {
                lb_AvisoDocente.Text = "Complete todo el Formulario";
                ModalPopupExtender0_ModalDocente.Show(); //asdsa
            }
            else
            {
                lb_AvisoDocente.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {

                    lb_AvisoDocente.Text = "";
                    DateTime fecha = Convert.ToDateTime(txt_fechaingresoDocente.Text);
                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoDocente.Text = "";

                        int aux = control.BuscarDocentesRepetidos(rut).Count;
                        if (aux == 0)
                        {
                            lb_AvisoDocente.Text = "";
                            if (Validar.validarEmail(correo))
                            {
                                lb_AvisoDocente.Text = "";

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
                            else
                            {
                                lb_AvisoDocente.Text = "Correo no Valido";
                                ModalPopupExtender0_ModalDocente.Show();
                            }
                        }
                        else
                        {
                            lb_AvisoDocente.Text = "El Rut " + rut + " Ya se encuentra registrado!";
                            ModalPopupExtender0_ModalDocente.Show();
                        }

                    }
                    else
                    {
                        lb_AvisoDocente.Text = "Rut no Valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender0_ModalDocente.Show();
                    }

                }
                else
                {
                    lb_AvisoDocente.Text = "Rut no Valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender0_ModalDocente.Show();
                }
            }

        }

        protected void GridView_docentes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)  //editar controles visibles entre los modal pop up
        {
            Label lbcodigoDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label1");
            Label lbNombreDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label2");
            Label lbApellidoDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label3");
            Label lbCorreoDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label4");
            Label lbFechaIngresoDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label5");
            Label lbEstadoDocente = (Label)GridView_docentes.Rows[e.NewSelectedIndex].FindControl("Label7");

            String fecha = Convert.ToString(lbFechaIngresoDocente.Text);
            String rut = Convert.ToString(lbcodigoDocente.Text);

            txt_rutDocente.Text = rut;
            txt_rutDocente.Enabled = false;

            cb_EstadoDocente.Enabled = true;

            Link_addDocente.Visible = false;
            Link_EditarDocente.Visible = true;

            txt_nombreDocente.Text = lbNombreDocente.Text;
            txt_apellidoDocente.Text = lbApellidoDocente.Text;
            txt_correoDocente.Text = lbCorreoDocente.Text;
            cb_EstadoDocente.SelectedValue = lbEstadoDocente.Text;
            txt_fechaingresoDocente.Text = fecha.Substring(0, 10);

            //generar barcode

            LabelKit.BarcodeGenerator code = new LabelKit.BarcodeGenerator();
            System.Drawing.Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1, 1, PixelFormat.Format32bppArgb);

            g = Graphics.FromImage(bmp);
            code.DrawCode128(g, rut, 0, 0).Save(Server.MapPath("./barcodes/" + rut + ".png"), ImageFormat.Png);
            Image_codigo.ImageUrl = "./barcodes/" + rut + ".png";


        }

        protected void Link_ModificarDocente_Click(object sender, EventArgs e)
        {
            lb_AvisoDocente.Text = "";
            ModalPopupExtender0_ModalDocente.Show();
        }

        protected void Link_EditarDocente_Click(object sender, EventArgs e)
        {
            ControladorDocente control = new ControladorDocente();
            int id_estado = 0;
            int.TryParse(cb_EstadoDocente.SelectedValue, out id_estado);
            DateTime fecha = Convert.ToDateTime(txt_fechaingresoDocente.Text);

            if (txt_nombreDocente.Text == "" || txt_apellidoDocente.Text == "" || txt_fechaingresoDocente.Text == "" ||
             txt_correoDocente.Text == "")
            {
                lb_AvisoDocente.Text = "Complete todo el formulario";
                ModalPopupExtender0_ModalDocente.Show(); //asdsa
            }
            else
            {
                lb_AvisoDocente.Text = "";
                if (Validar.validarEmail(txt_correoDocente.Text))
                {
                    lb_AvisoDocente.Text = "";
                    Docente nuevo = new Docente
                    {
                        Rut = txt_rutDocente.Text,
                        Nombre = txt_nombreDocente.Text,
                        Apellido = txt_apellidoDocente.Text,
                        Correo = txt_correoDocente.Text,
                        Fecha_Ingreso = fecha,
                        ID_Estado = id_estado

                    };
                    if (control.ActualizarDocente(nuevo))
                    {
                        MostrarDocentes();
                    }

                }
                else
                {
                    lb_AvisoDocente.Text = "Correo no valido";
                    ModalPopupExtender0_ModalDocente.Show();
                }

            }
        }



        protected void Link_AbrirModalCodigoDocente_Click(object sender, EventArgs e)
        {
            ModalPopupExtender_Barcode.Show();
        }



        protected void GridView_docentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_docentes.PageIndex = e.NewPageIndex;
            MostrarDocentes();
        }


        //VISTA ENCARGADOS

        public void MostrarEncargados()
        {
            ControladorEncargado control = new ControladorEncargado();
            GridView_Encargados.DataSource = control.listaGridview_Encargados();
            GridView_Encargados.DataBind();
        }

        protected void Link_AbrirModalEncargado_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3_encargadonuevo.Show();

            txt_RutEncargardo.Enabled = true;
            lb_AvisoEncargado.Text = "";

            cb_EstadoEncargado.Enabled = false;
            cb_EstadoEncargado.SelectedValue = "1";
            Link_EditarEncargado.Visible = false;

            Link_GuardarEncargado_VistaEncargado.Visible = true;

            txt_NombreEncargado.Text = "";
            txt_ApellidoEncargado.Text = "";
            txt_CorreoEncargado.Text = "";
            txt_RutEncargardo.Text = "";

        }

        protected void GridView_Encargados_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbcodigoEncargado = (Label)GridView_Encargados.Rows[e.NewSelectedIndex].FindControl("Label1");
            Label lbNombreEncargado = (Label)GridView_Encargados.Rows[e.NewSelectedIndex].FindControl("Label2");
            Label lbApellidoEncargado = (Label)GridView_Encargados.Rows[e.NewSelectedIndex].FindControl("Label3");
            Label lbCorreoEncargado = (Label)GridView_Encargados.Rows[e.NewSelectedIndex].FindControl("Label4");

            Label lbEstadoEncargado = (Label)GridView_Encargados.Rows[e.NewSelectedIndex].FindControl("Label6");


            String rut = Convert.ToString(lbcodigoEncargado.Text);

            txt_RutEncargardo.Text = rut;

            txt_RutEncargardo.Enabled = false;


            cb_EstadoEncargado.Enabled = true;

            Link_GuardarEncargado_VistaEncargado.Visible = false;

            Link_EditarEncargado.Visible = true;

            txt_NombreEncargado.Text = lbNombreEncargado.Text;
            txt_ApellidoEncargado.Text = lbApellidoEncargado.Text;
            txt_CorreoEncargado.Text = lbCorreoEncargado.Text;

            cb_EstadoEncargado.SelectedValue = lbEstadoEncargado.Text;

            LabelKit.BarcodeGenerator code = new LabelKit.BarcodeGenerator();
            System.Drawing.Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1, 1, PixelFormat.Format32bppArgb);

            g = Graphics.FromImage(bmp);
            code.DrawCode128(g, rut, 0, 0).Save(Server.MapPath("./barcodes/" + rut + ".png"), ImageFormat.Png);
            Image_BarcodeEncargado.ImageUrl = "./barcodes/" + rut + ".png";


        }

        protected void Link_AbrirModalCodigoEncargado_Click(object sender, EventArgs e)
        {
            ModalPopupExtender_BadCodeEncargado.Show();
        }

        protected void Link_ModificarEncargado_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3_encargadonuevo.Show();
        }


        protected void Link_EditarEncargado_Click(object sender, EventArgs e)
        {
            ControladorEncargado control = new ControladorEncargado();
            int id_estado = 0;
            int.TryParse(cb_EstadoEncargado.SelectedValue, out id_estado);
            string correo = txt_CorreoEncargado.Text;
            if (txt_NombreEncargado.Text == "" || txt_ApellidoEncargado.Text == "" || txt_CorreoEncargado.Text == "")
            {
                lb_AvisoEncargado.Text = "Complete Todo el Formulario";
                ModalPopupExtender3_encargadonuevo.Show();
            }
            else
            {
                lb_AvisoEncargado.Text = "";
                if (Validar.validarEmail(correo))
                {
                    lb_AvisoEncargado.Text = "";
                    Encargado nuevo = new Encargado
                    {
                        Rut = txt_RutEncargardo.Text,
                        Nombre = txt_NombreEncargado.Text,
                        Apellido = txt_ApellidoEncargado.Text,
                        Correo = correo,
                        ID_Estado = id_estado

                    };
                    if (control.ActualizarEncargado(nuevo))
                    {
                        MostrarEncargados();
                    }
                }
                else
                {
                    lb_AvisoEncargado.Text = "Correo no valido. Ejemplo inacap@inacap.cl";
                    ModalPopupExtender3_encargadonuevo.Show();
                }
            }
        }


        protected void Link_GuardarEncargado_VistaEncargado_Click(object sender, EventArgs e)
        {
            ControladorEncargado control = new ControladorEncargado();

            int id_Estado = 0;
            int.TryParse(cb_EstadoEncargado.SelectedValue, out id_Estado);


            string nombre = txt_NombreEncargado.Text;
            string apellido = txt_ApellidoEncargado.Text;
            string correo = txt_CorreoEncargado.Text;

            string rut = txt_RutEncargardo.Text;
            string codigo = "";
            codigo = rut.ToUpper();
            codigo = codigo.Replace(".", "");
            codigo = codigo.Replace("-", "");
            codigo = codigo.Replace("_", "");


            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_RutEncargardo.Text == "..-" || rut.Length < 11 || txt_NombreEncargado.Text == "" || txt_ApellidoEncargado.Text == "" ||
                txt_CorreoEncargado.Text == "")
            {
                lb_AvisoEncargado.Text = "Complete Todo el Formulario";
                ModalPopupExtender3_encargadonuevo.Show();
            }
            else
            {

                lb_AvisoEncargado.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoEncargado.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoEncargado.Text = "";
                        int aux = control.BuscarEncargadosRepetidos(rut).Count;

                        if (aux == 0)
                        {
                            lb_AvisoEncargado.Text = "";
                            if (Validar.validarEmail(correo))
                            {
                                lb_AvisoEncargado.Text = "";
                                Encargado nuevo = new Encargado
                                {
                                    Rut = rut,
                                    Nombre = nombre,
                                    Apellido = apellido,
                                    Correo = correo,
                                    ID_Estado = id_Estado,
                                    Codigo = codigo

                                };

                                if (control.addEncargados(nuevo))
                                {
                                    MostrarEncargados();
                                }
                            }
                            else
                            {
                                lb_AvisoEncargado.Text = "Correo no valido. Ejemplo inacap@inacap.cl";
                                ModalPopupExtender3_encargadonuevo.Show();
                            }
                        }
                        else
                        {
                            lb_AvisoEncargado.Text = "El Rut " + rut + " Ya se encuentra registrado!";
                            ModalPopupExtender3_encargadonuevo.Show();
                        }

                    }
                    else
                    {
                        lb_AvisoEncargado.Text = "Rut no valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender3_encargadonuevo.Show();
                    }
                }
                else
                {
                    lb_AvisoEncargado.Text = "Rut no valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender3_encargadonuevo.Show();
                }

            }

        }




        protected void GridView_Encargados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Encargados.PageIndex = e.NewPageIndex;
            MostrarEncargados();
        }

        //VISA CURSOS

        public void MostrarCursos()
        {
            ControladorCurso control = new ControladorCurso();
            GridView_cursos.DataSource = control.listaCurso_Pad();
            GridView_cursos.DataBind();
        }

        protected void Link_AbrirModalCurso_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1_cursonuevo.Show();
            Link_GuardarCurso.Visible = true;
            Link_EditarCurso.Visible = false;
            lbCurso.Visible = false;
            cb_EstadoCurso.Enabled = false;
            cb_EstadoCurso.SelectedValue = "1";
            txt_RutEncargardo_Curso.Text = "";
            txt_detalleCurso.Text = "";
            txt_UsuarioCurso.Text = "";
            txt_ContraseñaCurso.Text = "";
        }

        protected void GridView_cursos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbCodigoCurso = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label1");
            lb_idcurso.Text = lbCodigoCurso.Text;
            MostrarPad_Curso();

            Label lbRutEncargado = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label2");
            Label lbDetalleCurso = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label5");
            Label lbEstadoCurso = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label6");
            Label lbUsuarioCurso = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label8");
            Label lbContraseñaCurso = (Label)GridView_cursos.Rows[e.NewSelectedIndex].FindControl("Label9");

            Link_GuardarCurso.Visible = false;
            Link_EditarCurso.Visible = true;
            txt_RutEncargardo_Curso.Text = lbRutEncargado.Text;
            txt_detalleCurso.Text = lbDetalleCurso.Text;
            txt_UsuarioCurso.Text = lbUsuarioCurso.Text;
            txt_ContraseñaCurso.Text = lbContraseñaCurso.Text;
            cb_EstadoCurso.SelectedValue = lbEstadoCurso.Text;

            ControladorCursoDocente control = new ControladorCursoDocente();
            int curso = 0;
            int.TryParse(lbCodigoCurso.Text, out curso);
            GridView_DocentesAsignados.DataSource = control.ListaDocentesAsignados(curso);
            GridView_DocentesAsignados.DataBind();

        }





        protected void Link_ModificarCurso_Click(object sender, EventArgs e)
        {
            Link_GuardarCurso.Visible = false;
            Link_EditarCurso.Visible = true;
            cb_EstadoCurso.Enabled = true;
            ModalPopupExtender1_cursonuevo.Show();
        }

        protected void Link_EditarCurso_Click(object sender, EventArgs e)
        {
            ControladorCurso control = new ControladorCurso();
            ControladorEncargado controlEnc = new ControladorEncargado();

            int id_estado = 0;
            int.TryParse(cb_EstadoCurso.SelectedValue, out id_estado);

            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);

            string rut = txt_RutEncargardo_Curso.Text;

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_RutEncargardo_Curso.Text == "" || txt_detalleCurso.Text == "" || txt_ContraseñaCurso.Text == "" || txt_UsuarioCurso.Text == "")
            {
                lb_AvisoCurso.Text = "Complete Todo el Formulario";
                ModalPopupExtender1_cursonuevo.Show();  // Agregar mensaje
            }
            else
            {
                lb_AvisoCurso.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoCurso.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoCurso.Text = "";
                        int aux = controlEnc.listaBuscarEncargados(rut).Count;

                        if (aux == 0)
                        {
                            lb_encargadoNoencontrado.Text = "El relator que Ingreso no existe <br/> Desea crear uno nuevo?";
                            ModalPopupExtender2_ConfirmarEncargado.Show();
                        }
                        else
                        {
                            lb_encargadoNoencontrado.Text = "";
                            Curso nuevo = new Curso
                            {
                                ID_Curso = id_curso,
                                Rut_Encargado = txt_RutEncargardo_Curso.Text,
                                Detallecurso = txt_detalleCurso.Text,
                                Usuario = txt_UsuarioCurso.Text,
                                Contraseña = txt_ContraseñaCurso.Text,
                                ID_Estado = id_estado

                            };
                            if (control.ActualizarCurso(nuevo))
                            {
                                MostrarCursos();
                            }
                        }
                    }
                    else
                    {
                        lb_AvisoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender1_cursonuevo.Show();
                    }

                }
                else
                {
                    lb_AvisoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender1_cursonuevo.Show();
                }
            }

        }


        protected void Link_GuardarCurso_Click(object sender, EventArgs e)
        {
            ControladorCurso control = new ControladorCurso();

            ControladorEncargado controlEnc = new ControladorEncargado();

            string rut = txt_RutEncargardo_Curso.Text;
            string detalle = txt_detalleCurso.Text;
            string usuario = txt_UsuarioCurso.Text;
            string contraseña = txt_ContraseñaCurso.Text;

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_RutEncargardo_Curso.Text == "..-" || rut.Length < 11 || detalle == "" || txt_ContraseñaCurso.Text == "" || txt_UsuarioCurso.Text == "")
            {
                lb_AvisoCurso.Text = "Complete Todo el Formulario";

                ModalPopupExtender1_cursonuevo.Show();
            }
            else
            {
                lb_AvisoCurso.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoCurso.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoCurso.Text = "";

                        int aux = controlEnc.listaBuscarEncargados(rut).Count;

                        if (aux == 0)
                        {
                            lb_encargadoNoencontrado.Text = "El relator que Ingreso no existe <br/> Desea crear uno nuevo?";
                            ModalPopupExtender2_ConfirmarEncargado.Show();
                        }
                        else
                        {

                            lb_AvisoCurso.Text = "";
                            Curso nuevo = new Curso
                            {
                                Rut_Encargado = rut,
                                Detallecurso = detalle,
                                Usuario = usuario,
                                Contraseña = contraseña,
                                ID_Estado = 1

                            };

                            if (control.addCursos(nuevo))
                            {
                                MostrarCursos();
                            }


                            lbCurso.Visible = false;
                            txt_RutEncargardo_Curso.Text = "";
                            txt_detalleCurso.Text = "";
                            txt_UsuarioCurso.Text = "";
                            txt_ContraseñaCurso.Text = "";

                        }
                    }
                    else
                    {
                        lb_AvisoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender1_cursonuevo.Show();
                    }
                }
                else
                {
                    lb_AvisoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender1_cursonuevo.Show();
                }
            }
        }


        protected void Link_si_Click(object sender, EventArgs e) //modal creacion encargado en caso de no encontrar rut;
        {
            txt_RutEncargardoCurso.Text = txt_RutEncargardo_Curso.Text; //pasar el rut no registrador al formulario de registro
            ModalPopupExtender4_EncargadoVistaCurso.Show();
            txt_NombreEncargadoCurso.Text = "";
            txt_ApellidoEncargadoCurso.Text = "";
            txt_CorreoEncargadoCurso.Text = "";

            lb_AvisoEncargadoCurso.Text = "";
        }

        protected void Link_GuardarEncargadoCurso_Click(object sender, EventArgs e)
        {
            ControladorEncargado control = new ControladorEncargado();

            int id_Estado = 0;
            int.TryParse(cb_EstadoEncargado.SelectedValue, out id_Estado);

            string rut = txt_RutEncargardoCurso.Text;
            string nombre = txt_NombreEncargadoCurso.Text;
            string apellido = txt_ApellidoEncargadoCurso.Text;
            string correo = txt_CorreoEncargadoCurso.Text;

            string codigo = "";
            codigo = rut.ToUpper();
            codigo = codigo.Replace(".", "");
            codigo = codigo.Replace("-", "");
            codigo = codigo.Replace("_", "");

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_NombreEncargadoCurso.Text == "" || txt_ApellidoEncargadoCurso.Text == "" || txt_CorreoEncargadoCurso.Text == "")
            {
                lb_AvisoEncargadoCurso.Text = "Complete Todo el Formulario";
                ModalPopupExtender4_EncargadoVistaCurso.Show();
            }
            else
            {
                lb_AvisoEncargadoCurso.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoEncargadoCurso.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoEncargadoCurso.Text = "";
                        int aux = control.BuscarEncargadosRepetidos(rut).Count;
                        if (aux == 0)
                        {
                            lb_AvisoEncargadoCurso.Text = "";
                            if (Validar.validarEmail(correo))
                            {
                                lb_AvisoEncargadoCurso.Text = "";
                                Encargado nuevo = new Encargado
                                {
                                    Rut = rut,
                                    Nombre = nombre,
                                    Apellido = apellido,
                                    Correo = correo,
                                    ID_Estado = id_Estado,
                                    Codigo = codigo

                                };

                                if (control.addEncargados(nuevo))
                                {
                                    ModalPopupExtender1_cursonuevo.Show();
                                    txt_RutEncargardo_Curso.Text = rut;
                                }
                            }
                            else
                            {
                                lb_AvisoEncargadoCurso.Text = "Correo no valido. Ejemplo inacap@inacap.cl";
                                ModalPopupExtender4_EncargadoVistaCurso.Show();
                            }
                        }
                        else
                        {
                            lb_AvisoEncargadoCurso.Text = "El Rut " + rut + " Ya se encuentra registrado!";
                            ModalPopupExtender4_EncargadoVistaCurso.Show();
                        }

                    }
                    else
                    {
                        lb_AvisoEncargadoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender4_EncargadoVistaCurso.Show();
                    }
                }
                else
                {
                    lb_AvisoEncargadoCurso.Text = "Rut no valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender4_EncargadoVistaCurso.Show();
                }

            }
        }


        protected void GridView_cursos_PageIndexChanging(object sender, GridViewPageEventArgs e)  //paginacion gridview cursos
        {
            GridView_cursos.PageIndex = e.NewPageIndex;
            MostrarCursos();
        }


        //PAD

        public void MostrarPad_Curso()
        {
            int id_curso = 0;
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
            Link_EditarPad.Visible = false;
            Link_GuardarPad.Visible = true;
            cb_EstadoPad.Enabled = false;
            cb_EstadoPad.SelectedValue = "1";
            Link_fechaPad.Visible = true;

            txt_SalaPad.Text = "";
            txt_SalaCoffe.Text = "";
            txt_horainicioPad.Text = "";
            txt_horafinPad.Text = "";
            txt_fechapad.Text = "";

        }

        protected void GridView_detallePad_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbCodigoPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label1");
            Label lbSalaPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label3");
            Label lbSalaCoffePad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label4");
            Label lbHoraInicioPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label5");
            Label lbHoraTerminoPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label6");
            Label lbFechaPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label7");
            Label lbEstadoPad = (Label)GridView_detallePad.Rows[e.NewSelectedIndex].FindControl("Label8");

            String fecha = Convert.ToString(lbFechaPad.Text);

            txt_rutDocente.Enabled = false;


            cb_EstadoDocente.Enabled = true;
            Link_GuardarPad.Visible = false;
            Link_EditarPad.Visible = true;
            Link_addDocente.Visible = false;

            Link_EditarDocente.Visible = true;

            txt_SalaPad.Text = lbSalaPad.Text;
            txt_SalaCoffe.Text = lbSalaCoffePad.Text;
            txt_horainicioPad.Text = lbHoraInicioPad.Text;
            txt_horafinPad.Text = lbHoraTerminoPad.Text;
            txt_fechapad.Text = fecha.Substring(0, 10);
            cb_EstadoPad.SelectedValue = lbEstadoPad.Text;
            lb_codigoPad.Text = lbCodigoPad.Text;
        }
        protected void Link_ModificarPad_Click(object sender, EventArgs e)
        {
            ModalPopupExtender5_padnuevo.Show();
            cb_EstadoPad.Enabled = true;
            Link_fechaPad.Visible = false;
            lb_avisoPAD.Text = "";
        }

        protected void Link_EditarPad_Click(object sender, EventArgs e)
        {
            ControladorPad control = new ControladorPad();
            int id_estado = 0;
            int.TryParse(cb_EstadoPad.SelectedValue, out id_estado);

            int id_pad = 0;
            int.TryParse(lb_codigoPad.Text, out id_pad);


            if (txt_SalaPad.Text == "" || txt_SalaCoffe.Text == "" || txt_fechapad.Text == "" || txt_horainicioPad.Text == "::" ||
                txt_horafinPad.Text == "::")
            {
                lb_avisoPAD.Text = "Complete Todo el Formulario";
                ModalPopupExtender5_padnuevo.Show();
            }
            else
            {
                lb_avisoPAD.Text = "";
                if (!(txt_horainicioPad.Text == txt_horafinPad.Text || txt_horainicioPad.Text == "00:00:00" || txt_horafinPad.Text == "00:00:00"))
                {
                    lb_avisoPAD.Text = "";
                    DateTime fecha = Convert.ToDateTime(txt_fechapad.Text);

                    TimeSpan hinicio;
                    TimeSpan.TryParse(txt_horainicioPad.Text, out hinicio);

                    TimeSpan htermino;
                    TimeSpan.TryParse(txt_horafinPad.Text, out htermino);


                    Pad nuevo = new Pad
                    {
                        ID_Pad = id_pad,
                        Sala = txt_SalaPad.Text,
                        Sala_Coffe = txt_SalaCoffe.Text,
                        Hora_Inicio = hinicio,
                        Hora_Termino = htermino,
                        Fecha = fecha,
                        ID_Estado = id_estado

                    };
                    if (control.ActualizarPad(nuevo))
                    {
                        MostrarPad_Curso();
                        ModalPopupExtender4_detallecurso.Show();
                    }
                }
                else
                {
                    lb_avisoPAD.Text = "Error, Hora de inicio y termino no pueden ser iguales";
                    ModalPopupExtender5_padnuevo.Show();
                }


            }


        }


        protected void Link_GuardarPad_Click(object sender, EventArgs e)
        {
            ControladorPad control = new ControladorPad();
            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);

            string sala = txt_SalaPad.Text;
            string coffe = txt_SalaCoffe.Text;

            string hora_inicio = txt_horainicioPad.Text;
            string hora_fin = txt_horafinPad.Text;

            if (txt_SalaPad.Text == "" || txt_SalaCoffe.Text == "" || txt_horainicioPad.Text == "::" || txt_horainicioPad.Text == "::" ||
                txt_fechapad.Text == "")
            {
                lb_avisoPAD.Text = "Complete todo el formulario";
                ModalPopupExtender5_padnuevo.Show();

            }
            else
            {
                lb_avisoPAD.Text = "";
                if (!(txt_horainicioPad.Text == txt_horafinPad.Text || txt_horainicioPad.Text == "00:00:00" || txt_horafinPad.Text == "00:00:00"))
                {
                    lb_avisoPAD.Text = "";
                    DateTime fecha = Convert.ToDateTime(txt_fechapad.Text);
                    int id_estado = 1;

                    TimeSpan hinicio;
                    TimeSpan.TryParse(txt_horainicioPad.Text, out hinicio);

                    TimeSpan htermino;
                    TimeSpan.TryParse(txt_horafinPad.Text, out htermino);

                    int aux = control.listaBuscarPadDia_curso(fecha, id_curso).Count;
                    if (aux == 0)
                    {
                        lb_avisoPAD.Text = "";
                        Pad nuevo = new Pad
                        {
                            ID_Curso = id_curso,
                            Sala = sala,
                            Sala_Coffe = coffe,
                            Hora_Inicio = hinicio,
                            Hora_Termino = htermino,
                            Fecha = fecha,
                            ID_Estado = id_estado

                        };

                        if (control.addPad(nuevo))
                        {
                            MostrarPad_Curso();
                            ModalPopupExtender4_detallecurso.Show();
                        }
                    }
                    else
                    {
                        lb_avisoPAD.Text = "Ya existe pad para el dia " + fecha.ToShortDateString() + " en este curso";
                        ModalPopupExtender5_padnuevo.Show();
                    }
                }
                else
                {
                    lb_avisoPAD.Text = "Error, Hora de inicio y termino no pueden ser iguales";
                    ModalPopupExtender5_padnuevo.Show();
                }

            }
        }


        //ASIGNAR DOCENTES

        public void Mostrar_AsignarDocentes()
        {
            ControladorDocente control = new ControladorDocente();

            GridView_asignardocentes.DataSource = control.listaAsignar_Docentes();
            GridView_asignardocentes.DataBind();
        }



        protected void GridView_asignardocentes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_asignardocentes.PageIndex = e.NewPageIndex;
            Mostrar_AsignarDocentes();
        }

        protected void GridView_asignardocentes_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbcodigoDocente = (Label)GridView_asignardocentes.Rows[e.NewSelectedIndex].FindControl("Label1");

            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);
            string rut = lbcodigoDocente.Text;

            ControladorCursoDocente control = new ControladorCursoDocente();

            int aux = control.listaBuscarCurso_Docente(id_curso, rut).Count;
            
            if (aux == 0)
            {
                lb_UsuariosYaAsignado.Text = "";
                Curso_Docente nuevo = new Curso_Docente
                {
                    ID_Curso = id_curso,
                    Rut_Docente = rut
                };

                if (control.addCursoDocente(nuevo))
                {                   
                    GridView_DocentesAsignados.DataSource = control.ListaDocentesAsignados(id_curso);
                    GridView_DocentesAsignados.DataBind();
                }
            }
            else
            {
                lb_UsuariosYaAsignado.Text = "El Docente " + rut + " Ya ha sido asignado a este curso";
            }
        }



        // VISTA USUARIOS

        protected void Link_IngresarUsuario_Click(object sender, EventArgs e)
        {
            ControladorUsuario control = new ControladorUsuario();

            string nickname = txt_nickname.Text;
            string password = txt_password.Text;
            if (txt_nickname.Text == "" || txt_password.Text == "")
            {
                lb_accesonopermitido.Text = "Ingrese sus datos";
            }
            else
            {
                lb_accesonopermitido.Text = "";
                if (control.validarUsuario_root(nickname, password))
                {
                    MultiView1.ActiveViewIndex = 6;
                    lb_accesonopermitido.Text = "";
                }
                else
                {
                    lb_accesonopermitido.Text = "acceso no permitido";
                }
            }
        }

        public void MostrarUsuarios()
        {
            ControladorUsuario control = new ControladorUsuario();

            GridView_Usuarios.DataSource = control.listaUsuarios();
            GridView_Usuarios.DataBind();
        }

        protected void GridView_Usuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_Usuarios.PageIndex = e.NewPageIndex;
            MostrarUsuarios();
        }

        protected void GridView_Usuarios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Label lbcodigoUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label1");
            Label lbnombreUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label2");
            Label lbapellidoUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label3");
            Label lbnicknameUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label4");
            Label lbpasswordUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label7");
            Label lbtipoUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label5");
            Label lbestadoUsuario = (Label)GridView_Usuarios.Rows[e.NewSelectedIndex].FindControl("Label6");

            txt_RutUsuario.Text = lbcodigoUsuario.Text;
            txt_NombreUsuario.Text = lbnombreUsuario.Text;
            txt_ApellidoUsuario.Text = lbapellidoUsuario.Text;
            txt_NicknameUsuario.Text = lbnicknameUsuario.Text;
            txt_PasswordUsuario.Text = lbpasswordUsuario.Text;

            cb_TipoUsuario.SelectedValue = lbtipoUsuario.Text;
            cb_EstadoUsuario.SelectedValue = lbestadoUsuario.Text;

            if (lbcodigoUsuario.Text == "00.000.000-0")
            {
                txt_NombreUsuario.Enabled = false;
                txt_ApellidoUsuario.Enabled = false;
                txt_NicknameUsuario.Enabled = false;

                cb_TipoUsuario.Enabled = false;
                cb_EstadoUsuario.Enabled = false;
            }
            else
            {
                txt_NombreUsuario.Enabled = true;
                txt_ApellidoUsuario.Enabled = true;
                txt_NicknameUsuario.Enabled = true;

                cb_TipoUsuario.Enabled = true;
                cb_EstadoUsuario.Enabled = true;
            }
        }

        protected void Link_EditarUsuario_Click(object sender, EventArgs e)
        {
            ControladorUsuario control = new ControladorUsuario();
            int id_estado = 0;
            int.TryParse(cb_EstadoUsuario.SelectedValue, out id_estado);
            string tipo = cb_TipoUsuario.SelectedValue;

            if (txt_NombreUsuario.Text == "" || txt_ApellidoUsuario.Text == "" || txt_NicknameUsuario.Text == "" ||
                txt_PasswordUsuario.Text == "")
            {
                lb_AvisoUsuario.Text = "Complete Todo el Formulario";
            }
            else
            {
                lb_AvisoUsuario.Text = "";

                lb_AvisoUsuario.Text = "";

                Usuario nuevo = new Usuario
                {
                    Rut = txt_RutUsuario.Text,
                    Nombre = txt_NombreUsuario.Text,
                    Apellido = txt_ApellidoUsuario.Text,
                    Nickname = txt_NicknameUsuario.Text,
                    Password = txt_PasswordUsuario.Text,
                    Tipo = tipo,
                    ID_Estado = id_estado

                };
                if (control.ActualizarUsuario(nuevo))
                {
                    MostrarUsuarios();
                }
            }
        }

        protected void Link_GuardarUsuario_Click(object sender, EventArgs e)
        {
            ControladorUsuario control = new ControladorUsuario();

            int id_Estado = 0;
            int.TryParse(cb_EstadoUsuario.SelectedValue, out id_Estado);

            string rut = txt_RutUsuario.Text;
            string nombre = txt_NombreUsuario.Text;
            string apellido = txt_ApellidoUsuario.Text;
            string nickname = txt_NicknameUsuario.Text;
            string password = txt_PasswordUsuario.Text;
            string tipo = cb_TipoUsuario.SelectedValue;

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_RutUsuario.Text == "" || rut.Length < 11 || txt_NombreUsuario.Text == "" || txt_ApellidoUsuario.Text == "" ||
                txt_NicknameUsuario.Text == "" || txt_PasswordUsuario.Text == "")
            {
                lb_AvisoUsuario.Text = "Complete Todo el Formulario";
                ModalPopupExtender_Usuario.Show();
            }
            else
            {
                lb_AvisoUsuario.Text = "";
                int aux_nick = control.Buscar_nickname(txt_NicknameUsuario.Text).Count;

                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoUsuario.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        int aux_rut = control.Buscar_Rut(rut).Count;
                        lb_AvisoUsuario.Text = "";
                        if (aux_nick == 0)
                        {
                            lb_AvisoUsuario.Text = "";
                            if (aux_rut == 0)
                            {
                                lb_AvisoUsuario.Text = "";

                                Usuario nuevo = new Usuario
                                {
                                    Rut = rut,
                                    Nombre = nombre,
                                    Apellido = apellido,
                                    Nickname = nickname,
                                    Password = password,
                                    Tipo = tipo,
                                    ID_Estado = id_Estado

                                };

                                if (control.addUsuario(nuevo))
                                {
                                    MostrarUsuarios();
                                }
                            }
                            else
                            {
                                lb_AvisoUsuario.Text = "El Rut " + rut + " Ya se encuentra registrado!";
                                ModalPopupExtender_Usuario.Show();
                            }

                        }
                        else
                        {
                            lb_AvisoUsuario.Text = "Nickname ingresado no se encuetra disponible";
                            ModalPopupExtender_Usuario.Show();

                        }

                    }
                    else
                    {
                        lb_AvisoUsuario.Text = "Rut no valido. Ejemplo 11.111.111-1";
                        ModalPopupExtender_Usuario.Show();
                    }
                }
                else
                {
                    lb_AvisoUsuario.Text = "Rut no valido. Ejemplo 11.111.111-1";
                    ModalPopupExtender_Usuario.Show();
                }

            }

        }
        protected void Link_AbrirModalUsuario_Click(object sender, EventArgs e)
        {
            Link_EditarUsuario.Visible = false;
            Link_GuardarUsuario.Visible = true;
            txt_RutUsuario.Enabled = true;
            cb_EstadoUsuario.Enabled = false;

            txt_RutUsuario.Text = "";
            txt_NombreUsuario.Text = "";
            txt_ApellidoUsuario.Text = "";
            txt_NicknameUsuario.Text = "";
            txt_PasswordUsuario.Text = "";
            cb_TipoUsuario.SelectedValue = "normal";
            cb_EstadoUsuario.SelectedValue = "1";
            lb_AvisoUsuario.Text = "";
            txt_NicknameUsuario.Enabled = true;
            ModalPopupExtender_Usuario.Show();
        }

        protected void Link_AbrirModalModificarUsuario_Click(object sender, EventArgs e)
        {
            Link_EditarUsuario.Visible = true;
            Link_GuardarUsuario.Visible = false;
            cb_EstadoUsuario.Enabled = true;
            txt_RutUsuario.Enabled = false;
            lb_AvisoUsuario.Text = "";
            ModalPopupExtender_Usuario.Show();
            txt_NicknameUsuario.Enabled = false;

        }


        protected void Link_BuscarDocente_Click(object sender, EventArgs e)
        {
            ControladorDocente control = new ControladorDocente();
            string rut = txt_BuscarDocente.Text;
            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");
            if (txt_BuscarDocente.Text == "" || rut.Length < 8)
            {
                lb_AvisoBusqueda_Docente.Text = "";
                MostrarDocentes();
            }
            else
            {

                lb_AvisoBusqueda_Docente.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoBusqueda_Docente.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        int aux = control.Lista_Buscar_Docentes(rut).Count;
                        lb_AvisoBusqueda_Docente.Text = "";
                        if (aux == 0)
                        {
                            lb_AvisoBusqueda_Docente.Text = "Docente con Rut " + rut + " No se encuentra registrado";
                            MostrarDocentes();
                        }
                        else
                        {
                            txt_BuscarDocente.Text = "";
                            lb_AvisoBusqueda_Docente.Text = "";
                            GridView_docentes.DataSource = control.Lista_Buscar_Docentes(rut);
                            GridView_docentes.DataBind();
                        }
                    }
                    else
                    {
                        lb_AvisoBusqueda_Docente.Text = "Rut no valido";
                    }
                }
                else
                {
                    lb_AvisoBusqueda_Docente.Text = "Rut no valido";
                }

            }
        }


        protected void Link_BuscarEncargado_Click(object sender, EventArgs e)
        {
            ControladorEncargado control = new ControladorEncargado();
            String rut = txt_BuscarEncargado.Text;
            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");

            if (txt_BuscarEncargado.Text == "" || rut.Length < 8)
            {
                lb_AvisoBusqueda_Encargado.Text = "";
                MostrarEncargados();
            }
            else
            {

                lb_AvisoBusqueda_Encargado.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoBusqueda_Encargado.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        int aux = control.Lista_Buscar_Encargados(rut).Count;
                        lb_AvisoBusqueda_Encargado.Text = "";
                        if (aux == 0)
                        {
                            lb_AvisoBusqueda_Encargado.Text = "Relator con Rut " + rut + " No se encuentra registrado";
                            MostrarEncargados();
                        }
                        else
                        {
                            txt_BuscarEncargado.Text = "";
                            lb_AvisoBusqueda_Encargado.Text = "";
                            GridView_Encargados.DataSource = control.Lista_Buscar_Encargados(rut);
                            GridView_Encargados.DataBind();
                        }
                    }
                    else
                    {
                        lb_AvisoBusqueda_Encargado.Text = "Rut no valido";
                    }

                }
                else
                {
                    lb_AvisoBusqueda_Encargado.Text = "Rut no valido";
                }

            }

        }

        protected void Link_BuscarCurso_Click(object sender, EventArgs e)
        {
            ControladorCurso control = new ControladorCurso();
            String rut = txt_BuscarCurso.Text;

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");

            if (txt_BuscarCurso.Text == "" || rut.Length < 8)
            {
                lb_AvisoBusqueda_Cursos.Text = "";
                MostrarCursos();
            }
            else
            {

                lb_AvisoBusqueda_Cursos.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoBusqueda_Cursos.Text = "";
                    if (Validar.validarRut(rut))
                    {
                        int aux = control.Lista_Buscar_Curso(rut).Count;
                        lb_AvisoBusqueda_Cursos.Text = "";
                        if (aux == 0)
                        {
                            lb_AvisoBusqueda_Cursos.Text = "Curso con Relator Rut " + rut + " No se encuentra registrado";
                            MostrarCursos();
                        }
                        else
                        {
                            txt_BuscarCurso.Text = "";
                            lb_AvisoBusqueda_Cursos.Text = "";
                            GridView_cursos.DataSource = control.Lista_Buscar_Curso(rut);
                            GridView_cursos.DataBind();
                        }
                    }
                    else
                    {
                        lb_AvisoBusqueda_Cursos.Text = "Rut no valido";
                    }

                }
                else
                {
                    lb_AvisoBusqueda_Cursos.Text = "Rut no valido";
                }

            }
        }

        protected void Link_BuscarDocenteAsignar_Click(object sender, EventArgs e)
        {
            ControladorDocente control = new ControladorDocente();
            String rut = txt_Buscar_DocenteAsignar.Text;

            rut = rut.ToUpper();
            rut = rut.Replace("_", "");
            rut = rut.Replace(",", ".");

            if (txt_Buscar_DocenteAsignar.Text == "" || rut.Length < 8)
            {
                lb_AvisoBusqueda_AsignarDocente.Text = "";
                Mostrar_AsignarDocentes();
            }
            else
            {

                lb_AvisoBusqueda_AsignarDocente.Text = "";
                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoBusqueda_AsignarDocente.Text = "";
                    if (Validar.validarRut(rut))
                    {

                        int aux = control.Lista_Buscar_AsignarDocentes(rut).Count;
                        lb_AvisoBusqueda_AsignarDocente.Text = "";
                        if (aux == 0)
                        {
                            lb_AvisoBusqueda_AsignarDocente.Text = "Docente Rut " + rut + " No se encuentra registrado ó no se encuentra Activo";
                            Mostrar_AsignarDocentes();
                        }
                        else
                        {
                            txt_Buscar_DocenteAsignar.Text = "";
                            lb_AvisoBusqueda_AsignarDocente.Text = "";
                            GridView_asignardocentes.DataSource = control.Lista_Buscar_AsignarDocentes(rut);
                            GridView_asignardocentes.DataBind();
                        }
                    }
                    else
                    {
                        lb_AvisoBusqueda_AsignarDocente.Text = "Rut no valido";
                    }
                }
                else
                {
                    lb_AvisoBusqueda_AsignarDocente.Text = "Rut no valido";
                }

            }
        }
        protected void Link_VistaReportes_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 7;
            MostrarAsistencia();
        }

        protected void Link_ReportesEncuestas_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 8;
        }



        public void MostrarAsistencia()
        {
            ControladorAsistencia control = new ControladorAsistencia();
            GridView_ReporteAsistencia.DataSource = control.Lista_MostrarAsistencia();
            GridView_ReporteAsistencia.DataBind();
            
        }

        protected void Link_ExportarAsistenciaAExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                // Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                if (txt_FechaBusqueda.Text == "")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte Asistencia " + txt_RutBusqueda.Text + ".xls");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte Asistencia " + txt_FechaBusqueda.Text + ".xls");
                }
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Charset = "65001";
                byte[] b = new byte[] {
             0xef,
             0xbb,
             0xbf
              };
                Response.BinaryWrite(b);
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    GridView_ReporteAsistencia.HeaderRow.BackColor = Color.Red;
                    foreach (TableCell cell in GridView_ReporteAsistencia.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView_ReporteAsistencia.HeaderStyle.BackColor;
                    }

                    foreach (GridViewRow row in GridView_ReporteAsistencia.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView_ReporteAsistencia.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView_ReporteAsistencia.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }
                    GridView_ReporteAsistencia.RenderControl(hw);

                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                txt_FechaBusqueda.Text = "";
                txt_RutBusqueda.Text = "";
            }
            catch (Exception)
            {
                lb_AvisoBusquedaReporteAsistencia.Text = "No se ha encontrado asistencia registrada";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.        
        }

        protected void Link_ExportarAsistenciaAPDF_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                if (txt_FechaBusqueda.Text == "")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte Asistencia " + txt_RutBusqueda.Text + ".pdf");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename=Reporte Asistencia " + txt_FechaBusqueda.Text + ".pdf");
                }

                Response.Charset = "";

                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    this.Panel_reporteasistencia.RenderControl(hw);


                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.Write(pdfDoc);
                    Response.End();
                }
                txt_FechaBusqueda.Text = "";
                txt_RutBusqueda.Text = "";
            }
            catch (Exception)
            {
                lb_AvisoBusquedaReporteAsistencia.Text = "No se ha encontrado asistencia registrada";
            }
            
        }

        protected void Link_BuscarAsistenciaXfecha_Click(object sender, EventArgs e)
        {
            txt_RutBusqueda.Text = "";
            ControladorAsistencia control = new ControladorAsistencia();

            if (txt_FechaBusqueda.Text == "")
            {
                MostrarAsistencia();

            }
            else
            {
                DateTime fecha = Convert.ToDateTime(txt_FechaBusqueda.Text);

                lb_AvisoBusquedaReporteAsistencia.Text = "";
                int aux = control.Lista_AsistenciaXFecha(fecha).Count;
                if (aux != 0)
                {
                    lb_AvisoBusquedaReporteAsistencia.Text = "";
                    
                    GridView_ReporteAsistencia.DataSource = control.Lista_AsistenciaXFecha(fecha);
                    GridView_ReporteAsistencia.DataBind();
                }
                else
                {
                    MostrarAsistencia();
                    lb_AvisoBusquedaReporteAsistencia.Text = "No se realizo ningun curso el dia" + fecha;
                }


            }
        }

        protected void Link_BuscarAsistenciaXrut_Click(object sender, EventArgs e)
        {
            txt_FechaBusqueda.Text = "";
            ControladorAsistencia control = new ControladorAsistencia();
            string rut = txt_RutBusqueda.Text;
            rut = rut.ToUpper();
            if (txt_RutBusqueda.Text == "" || rut.Length < 8)
            {
                MostrarAsistencia();

            }
            else
            {


                string pCodHomolog = rut;
                int contador = pCodHomolog.ToArray().Where(lr => lr.Equals('K')).Count();
                if (contador <= 1)
                {
                    lb_AvisoBusquedaReporteAsistencia.Text = "";

                    if (Validar.validarRut(rut))
                    {
                        lb_AvisoBusquedaReporteAsistencia.Text = "";
                        int aux = control.Lista_AsistenciaXRut(rut).Count;
                        if (aux != 0)
                        {
                            lb_AvisoBusquedaReporteAsistencia.Text = "";
                            
                            GridView_ReporteAsistencia.DataSource = control.Lista_AsistenciaXRut(rut);
                            GridView_ReporteAsistencia.DataBind();
                        }
                        else
                        {
                            MostrarAsistencia();
                            lb_AvisoBusquedaReporteAsistencia.Text = "Docente no encontrado";
                        }

                    }
                    else
                    {
                        lb_AvisoBusquedaReporteAsistencia.Text = "Rut no valido";
                    }
                }
                else
                {
                    lb_AvisoBusquedaReporteAsistencia.Text = "Rut no valido";
                }
            }
        }

        protected void GridView_ReporteAsistencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_ReporteAsistencia.PageIndex = e.NewPageIndex;
            MostrarAsistencia();
        }




        public void cargarReportesEncuestas()
        {
            bd_entities contexto = new bd_entities();
            var consulta = from p in contexto.Pregunta
                           orderby p.ID_Pregunta
                           select p;


            List<Resultado> res = new List<Resultado>();

            for (int i = 1; i <= 15; i++)
            {

                res.Add(respEnc(i + ""));
            }
            GridView1.DataSource = res;
            GridView1.DataBind();
            try {
                DateTime fecha = Convert.ToDateTime(txt_FechaBusquedaEncuestas.Text);
                ControladorEncuestas control = new ControladorEncuestas();
                GridView_Comentario.DataSource = control.listaComentarios(fecha);
                GridView_Comentario.DataBind();

            } catch (Exception) { }
            


        }

        protected void Link_buscarENcuestas_Click(object sender, EventArgs e)
        {
            cargarReportesEncuestas();          

        }

        public Resultado respEnc(string idP)
        {
            try
            {


                bd_entities contexto = new bd_entities();

                DateTime fecha = Convert.ToDateTime(txt_FechaBusquedaEncuestas.Text);

                var consultaP1 = from p in contexto.Pregunta
                                 join r in contexto.Respuestas on p.ID_Pregunta equals r.ID_Pregunta
                                 join c in contexto.Curso on r.ID_Curso equals c.ID_Curso
                                 join pd in contexto.Pad on c.ID_Curso equals pd.ID_Curso
                                 where p.ID_Pregunta == idP && pd.Fecha == fecha
                                 select new Resultado
                                 {
                                     pregunta = p.Pregunta1,
                                     resultado1 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("1") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado2 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("2") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado3 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("3") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado4 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("4") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado5 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("5") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado6 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("6") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado7 = (from rp in contexto.Respuestas where rp.Respuesta.Equals("7") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado8 = (from rp in contexto.Respuestas where rp.Respuesta.Contains("SI") && rp.ID_Pregunta.Equals(idP) select rp).Count(),
                                     resultado9 = (from rp in contexto.Respuestas where rp.Respuesta.Contains("NO") && rp.ID_Pregunta.Equals(idP) select rp).Count()
                                 };

                return consultaP1.ToList<Resultado>().ElementAt(0);

            }
            catch (Exception)
            {

                return null;
            }


        }

        

        protected void Link_ExportarEncuestasaPDF_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Reporte Encuestas Fecha " + txt_FechaBusquedaEncuestas.Text + ".pdf");
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                this.Panel1_reportesEncuestas.RenderControl(hw);


                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
        }

        protected void Link_ExportarEncuestasaExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Reporte Encuestas Fecha " + txt_FechaBusquedaEncuestas.Text + ".xls");
           
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "65001";
            byte[] b = new byte[] {
                     0xef,
                      0xbb,
                     0xbf
                  };
            Response.BinaryWrite(b);

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                
                GridView1.HeaderRow.BackColor = Color.Red;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }

                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }
                GridView1.RenderControl(hw);               
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();

            }
        }

        protected void GridView_DocentesAsignados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView_DocentesAsignados.PageIndex = e.NewPageIndex;
            ControladorCursoDocente control = new ControladorCursoDocente();
            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);

            GridView_DocentesAsignados.DataSource = control.ListaDocentesAsignados(id_curso);
            GridView_DocentesAsignados.DataBind();
        }

        protected void GridView_DocentesAsignados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lb1 = (Label)GridView_DocentesAsignados.Rows[e.RowIndex].FindControl("Label1");
            lb_UsuariosYaAsignado.Text = "";
            int id_curso = 0;
            int.TryParse(lb_idcurso.Text, out id_curso);

            int id = 0;
            int.TryParse(lb1.Text, out id);
            ControladorCursoDocente control = new ControladorCursoDocente();

            if (control.EliminarAsignacion(id))
            {
               
                GridView_DocentesAsignados.DataSource = control.ListaDocentesAsignados(id_curso);
                GridView_DocentesAsignados.DataBind();
            }
            
        }
    }
}
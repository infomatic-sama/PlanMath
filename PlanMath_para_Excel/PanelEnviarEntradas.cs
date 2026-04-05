using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static PlanMath_para_Excel.Entradas;
using static PlanMath_para_Excel.PlanMath_Excel;

namespace PlanMath_para_Excel
{
    public partial class PanelEnviarEntradas : UserControl
    {
        public int IndiceEjecucionSeleccionada = -1;
        public bool ActualizarEjecucionSeleccionada = false;
        bool modoManual;
        public bool ModoManual
        {
            get
            {
                return modoManual;
            }
            set
            {
                if (value)
                {
                    panelSeleccionarCeldas.Visible = true;
                    panelCeldasSeleccionadasAutomaticamente.Visible = false;
                }
                else
                {
                    panelSeleccionarCeldas.Visible = false;
                    panelCeldasSeleccionadasAutomaticamente.Visible = true;
                }
                modoManual = value;
            }
        }
        public SeleccionInstancia SeleccionInstancia { get; set; }
        public EnvioEntradaDesdeEjecucion EjecucionSeleccionada
        {
            get
            {
                return (EnvioEntradaDesdeEjecucion)ejecucionSeleccionada.SelectedItem;
            }
        }
        public bool EsEntradaTextosInformacion { get; set; }
        public PanelEnviarEntradas()
        {
            InitializeComponent();
            mensajeErrorSeleccion.Text = string.Empty;
        }

        private void ejecucionSeleccionada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModoManual)
            {
                if (SeleccionInstancia.SeleccionandoCeldas)
                {
                    btnActivarDesactivarSeleccionandoCeldas_Click(this, e);
                }
            }

            ComboBox combobox = (ComboBox)sender;
            IndiceEjecucionSeleccionada = combobox.SelectedIndex;

            if (IndiceEjecucionSeleccionada > -1)
            {
                EnvioEntradaDesdeEjecucion ejecucionSeleccionada = (EnvioEntradaDesdeEjecucion)combobox.SelectedItem;
                txtLibro.Text = ejecucionSeleccionada.DefinicionEntrada.Libro;
                txtHoja.Text = ejecucionSeleccionada.DefinicionEntrada.Hoja;
                txtCeldas.Text = ejecucionSeleccionada.DefinicionEntrada.Celdas;
                txtTipoEntrada.Text = (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.Numero) ? "Variable de número" :
                    (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.ConjuntoNumeros) ? "Vector de números" :
                    (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.TextosInformacion) ? "Vector de cadenas de texto" : string.Empty;
                txtArchivoPlanMath.Text = ejecucionSeleccionada.DefinicionEntrada.ArchivoPlanMath;
                txtCalculo.Text = ejecucionSeleccionada.DefinicionEntrada.NombreCalculo;
                txtEntrada.Text = ejecucionSeleccionada.DefinicionEntrada.Nombre;
                PlanMath_Excel.SeleccionarCeldas_DefinicionEjecucion(ejecucionSeleccionada.DefinicionEntrada);
                iconoEnvioOk.Visible = false;
                lblEnvio.Visible = false;

                if (ModoManual)
                {
                    SeleccionInstancia.EntradaActual.Tipo = ejecucionSeleccionada.DefinicionEntrada.Tipo;

                    if (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.ConjuntoNumeros)
                        establecerTextosNumeros.Visible = true;
                    else
                        establecerTextosNumeros.Visible = false;

                    if (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.TextosInformacion)
                        EsEntradaTextosInformacion = true;
                    else
                        EsEntradaTextosInformacion = false;
                }
                else
                {
                    establecerTextosNumeros.Visible = false;
                    EsEntradaTextosInformacion = false;
                }
            }
            else
            {
                txtLibro.Text = string.Empty;
                txtHoja.Text = string.Empty;
                txtCeldas.Text = string.Empty;
                txtTipoEntrada.Text = string.Empty;
                txtArchivoPlanMath.Text = string.Empty;
                txtCalculo.Text = string.Empty;
                txtEntrada.Text = string.Empty;
                lblNombresDescripciones.Text = string.Empty;
                establecerTextosNumeros.Visible = false;
            }
        }

        private void btnEjecucionAnterior_Click(object sender, EventArgs e)
        {
            if (IndiceEjecucionSeleccionada > 0)
            {
                IndiceEjecucionSeleccionada--;
                ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
                iconoEnvioOk.Visible = false;
                lblEnvio.Visible = false;
            }
        }

        private void btnEjecucionSiguiente_Click(object sender, EventArgs e)
        {
            if (IndiceEjecucionSeleccionada < PlanMath_Excel.Ejecuciones_EnvioEntradas.
                Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) - 1)
            {
                IndiceEjecucionSeleccionada++;
                ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
                iconoEnvioOk.Visible = false;
                lblEnvio.Visible = false;
            }
        }

        private void btnEnviarEntrada_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia.SeleccionandoCeldas)
            {
                MessageBox.Show("Termina de seleccionar las celdas antes de enviar la variable o vector de entrada a PlanMath.");
            }
            else
            {
                if (IndiceEjecucionSeleccionada > -1 && ejecucionSeleccionada.SelectedItem != null)
                {
                    EnvioEntradaDesdeEjecucion ejecucion = (EnvioEntradaDesdeEjecucion)ejecucionSeleccionada.SelectedItem;
                    Entrada_Desde_Excel celdasSeleccionadasObj = null;

                    if (ModoManual)
                    {
                        //celdasSeleccionadas_Ejecucion.EntradaManual = ejecucion.DefinicionEntrada.EntradaManual;
                        //celdasSeleccionadas_Ejecucion.ReemplazarCeldas = ejecucion.DefinicionEntrada.ReemplazarCeldas;
                        //celdasSeleccionadas_Ejecucion.NombreCalculo = ejecucion.DefinicionEntrada.NombreCalculo;
                        //celdasSeleccionadas_Ejecucion.EsRutaLocal_Excel = ejecucion.DefinicionEntrada.EsRutaLocal_Excel;
                        //celdasSeleccionadas_Ejecucion.ArchivoPlanMath = ejecucion.DefinicionEntrada.ArchivoPlanMath;
                        //celdasSeleccionadas_Ejecucion.Asignaciones_TextosInformacion = ejecucion.DefinicionEntrada.Asignaciones_TextosInformacion;
                        //celdasSeleccionadas_Ejecucion.Nombre = ejecucion.DefinicionEntrada.Nombre;
                        //celdasSeleccionadas_Ejecucion.Tipo = ejecucion.DefinicionEntrada.Tipo;
                        //celdasSeleccionadas_Ejecucion.URLOffice_Original = ejecucion.DefinicionEntrada.URLOffice_Original;

                        //celdasSeleccionadas_Ejecucion.Libro = Globals.PlanMath_Excel.Application.ActiveWorkbook.FullName;
                        //celdasSeleccionadas_Ejecucion.Hoja = Globals.PlanMath_Excel.Application.ActiveSheet;
                        //celdasSeleccionadas_Ejecucion.Celdas = Globals.PlanMath_Excel.Application.ActiveCell.AddressLocal;
                        //celdasSeleccionadas = celdasSeleccionadas_Ejecucion;
                        celdasSeleccionadasObj = SeleccionInstancia.EntradaActual;

                        celdasSeleccionadasObj.Hoja = hojaSeleccionada.Text;
                        celdasSeleccionadasObj.Celdas = celdasSeleccionadas.Text;
                        celdasSeleccionadasObj.Nombre = ejecucion.DefinicionEntrada.Nombre;
                        celdasSeleccionadasObj.Asignaciones_TextosInformacion.AddRange(
                            SeleccionInstancia.AsignacionesTextosInformacion.Select
                            (i => new AsignacionTextoInformacion()
                            {
                                CeldaNumero = i.Celda_Numero.Celda.Address.ToString(),
                                CeldaTextoInformacion = i.Celda_TextoInformacion.Celda.Address.ToString()
                            }));
                    }
                    else
                    {
                        celdasSeleccionadasObj = ejecucion.DefinicionEntrada;
                    }

                    PlanMath_Excel.SeleccionarCeldas_DefinicionEjecucion(celdasSeleccionadasObj);

                    if (celdasSeleccionadasObj.Tipo == TipoEntrada.Numero)
                    {
                        bool encontrado = false;
                        double numero = PlanMath_Excel.ObtenerEntrada_En_Excel(celdasSeleccionadasObj, ref encontrado, null);

                        if (encontrado)
                        {
                            EnviarNumeroEntrada_APlanMath(numero, ejecucion.ID_Ejecucion, celdasSeleccionadasObj.Nombre);
                        }
                    }
                    else if (celdasSeleccionadasObj.Tipo == TipoEntrada.ConjuntoNumeros)
                    {
                        bool encontrado = false;
                        List<PlanMath_Excel.NumeroObtenido> numeros = PlanMath_Excel.ObtenerEntrada_ConjuntoNumeros_En_Excel(celdasSeleccionadasObj, ref encontrado, null);

                        if (encontrado)
                        {
                            EnviarConjuntoNumerosEntrada_APlanMath(numeros, ejecucion.ID_Ejecucion, ejecucion.DefinicionEntrada.Nombre);
                        }
                    }
                    else if (celdasSeleccionadasObj.Tipo == TipoEntrada.TextosInformacion)
                    {
                        bool encontrado = false;
                        List<List<string>> textos = PlanMath_Excel.ObtenerEntrada_ConjuntoTextosInformacion_En_Excel(celdasSeleccionadasObj, ref encontrado, string.Empty);

                        if (encontrado)
                        {
                            EnviarConjuntoTextosInformacionEntrada_APlanMath(textos, ejecucion.ID_Ejecucion, celdasSeleccionadasObj.Nombre);
                        }
                    }

                    btnQuitarEjecucion_Click(this, e);

                    SeleccionInstancia.AsignacionesTextosInformacion.Clear();
                    //SeleccionInstancia.Celdas.Clear();
                    //SeleccionInstancia.ColumnasNumeros.Clear();
                    SeleccionInstancia.EntradaActual = new Entrada_Desde_Excel();
                    SeleccionInstancia.Hoja = null;
                    SeleccionInstancia.TextosInformacion.Clear();
                    SeleccionInstancia.NumerosSeleccionados.Clear();

                    iconoEnvioOk.Visible = true;
                    lblEnvio.Text = "Variable o vector de entrada enviada a la ejecución de PlanMath.";
                    lblEnvio.Visible = true;
                }
            }
        }

        private void EnviarNumeroEntrada_APlanMath(double numero, string ID_Ejecucion, string nombreEntrada)
        {
            string rutaArchivo = PlanMath_Excel.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion + "\\" + ID_Ejecucion + "-" + nombreEntrada + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteElementString("Numero", numero.ToString());
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
        }

        private void EnviarConjuntoNumerosEntrada_APlanMath(List<PlanMath_Excel.NumeroObtenido> numeros, string ID_Ejecucion, string nombreEntrada)
        {
            string rutaArchivo = PlanMath_Excel.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion + "\\" + ID_Ejecucion + "-" + nombreEntrada + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteStartElement("Numeros");
            DataContractSerializer objeto = new DataContractSerializer(typeof(List<PlanMath_Excel.NumeroObtenido>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(escribirArchivo, numeros);
            escribirArchivo.WriteEndElement();
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
        }

        private void EnviarConjuntoTextosInformacionEntrada_APlanMath(List<List<string>> numeros, string ID_Ejecucion, string nombreEntrada)
        {
            string rutaArchivo = PlanMath_Excel.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion + "\\" + ID_Ejecucion + "-" + nombreEntrada + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteStartElement("TextosInformacion");
            DataContractSerializer objeto = new DataContractSerializer(typeof(List<List<string>>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(escribirArchivo, numeros);
            escribirArchivo.WriteEndElement();
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
        }

        private void procesoActualizarEjecucionSeleccionada_Tick(object sender, EventArgs e)
        {
            if (ActualizarEjecucionSeleccionada)
            {
                //if (IndiceEjecucionSeleccionada == -1)
                //{
                ejecucionSeleccionada.SelectedItem = null;

                ejecucionSeleccionada.DataSource = null;
                ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";
                                
                ejecucionSeleccionada.DataSource = PlanMath_Excel.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList();

                if (PlanMath_Excel.Ejecuciones_EnvioEntradas.
                    Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) > 0)
                {
                    IndiceEjecucionSeleccionada = PlanMath_Excel.Ejecuciones_EnvioEntradas
                        .Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) - 1;
                    ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
                }
                //}

                ActualizarEjecucionSeleccionada = false;
            }
        }

        private void btnQuitarEjecucion_Click(object sender, EventArgs e)
        {
            EnvioEntradaDesdeEjecucion ejecucion = (EnvioEntradaDesdeEjecucion)ejecucionSeleccionada.SelectedItem;

            if (ejecucion != null)
            {
                File.Delete(ejecucion.RutaArchivo);

                int indice = IndiceEjecucionSeleccionada;

                ejecucionSeleccionada.DataSource = null;
                ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";

                var elemento = PlanMath_Excel.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList().ElementAt(indice);

                PlanMath_Excel.Ejecuciones_EnvioEntradas.Remove(elemento);

                ejecucionSeleccionada.DataSource = PlanMath_Excel.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList();

                IndiceEjecucionSeleccionada = PlanMath_Excel.Ejecuciones_EnvioEntradas
                    .Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) - 1;
                ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
            }
        }

        private void PanelEnviarEntradas_Load(object sender, EventArgs e)
        {
            if (Globals.PlanMath_Excel.EsModoOscuro())
            {
                Globals.PlanMath_Excel.ColorearControles(this);
            }
        }

        private void btnActivarDesactivarSeleccionandoCeldas_Click(object sender, EventArgs e)
        {
            if (!SeleccionInstancia.SeleccionandoCeldas)
            {
                SeleccionInstancia.SeleccionandoCeldas = true;
                btnActivarDesactivarSeleccionandoCeldas.Text = "Seleccionando celdas...\nDetener seleccionar celdas";
                btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._04;
                Globals.PlanMath_Excel.PlanMath_Excel_SheetSelectionChange(
                     Globals.PlanMath_Excel.Application.ActiveSheet,
                      Globals.PlanMath_Excel.Application.Selection);
            }
            else
            {
                SeleccionInstancia.SeleccionandoCeldas = false;
                btnActivarDesactivarSeleccionandoCeldas.Text = "Comenzar a seleccionar celdas";
                btnActivarDesactivarSeleccionandoCeldas.Image = global::PlanMath_para_Excel.Properties.Resources._03;
            }
        }

        private void establecerTextosNumeros_Click(object sender, EventArgs e)
        {
            AsignacionManual_NumerosTextosInformacion asignacion = new AsignacionManual_NumerosTextosInformacion();
            asignacion.SeleccionInstancia = SeleccionInstancia;
            asignacion.ShowDialog();
        }

        private void lblEnvio_Click(object sender, EventArgs e)
        {

        }

        private void iconoEnvioOk_Click(object sender, EventArgs e)
        {

        }
    }
}

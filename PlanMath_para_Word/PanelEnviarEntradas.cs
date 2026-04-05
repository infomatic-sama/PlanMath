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
using static PlanMath_para_Word.Entradas;
using static PlanMath_para_Word.PlanMath_Word;
using static System.Net.Mime.MediaTypeNames;

namespace PlanMath_para_Word
{
    public partial class PanelEnviarEntradas : UserControl
    {
        public int IndiceEjecucionSeleccionada = -1;
        int indiceTextoSeleccionado = -1;
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
                    panelSeleccionarTextos.Visible = true;
                    panelTextosSeleccionadosAutomaticamente.Visible = false;
                }
                else
                {
                    panelSeleccionarTextos.Visible = false;
                    panelTextosSeleccionadosAutomaticamente.Visible = true;
                }
                modoManual = value;
            }
        }
        public SeleccionInstancia SeleccionInstancia { get; set; }
        public PanelEnviarEntradas()
        {
            InitializeComponent();
        }

        private void ejecucionSeleccionada_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            IndiceEjecucionSeleccionada = combobox.SelectedIndex;

            if (IndiceEjecucionSeleccionada > -1)
            {
                EnvioEntradaDesdeEjecucion ejecucionSeleccionada = (EnvioEntradaDesdeEjecucion)combobox.SelectedItem;
                txtLibro.Text = ejecucionSeleccionada.DefinicionEntrada.RutaDocumento;
                txtTipoEntrada.Text = (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.Numero) ? "Variable de número" :
                    (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.ConjuntoNumeros) ? "Vector de números" :
                    (ejecucionSeleccionada.DefinicionEntrada.Tipo == TipoEntrada.TextosInformacion) ? "Vector de cadenas de texto" : string.Empty;
                txtArchivoPlanMath.Text = ejecucionSeleccionada.DefinicionEntrada.ArchivoPlanMath;
                txtCalculo.Text = ejecucionSeleccionada.DefinicionEntrada.NombreCalculo;
                txtEntrada.Text = ejecucionSeleccionada.DefinicionEntrada.Nombre;
                LlenarTextos(ejecucionSeleccionada.DefinicionEntrada);
                indiceTextoSeleccionado = 0;

                if(cmbTextos.Items.Count > 0)
                    cmbTextos.SelectedIndex = indiceTextoSeleccionado;

                iconoEnvioOk.Visible = false;
                lblEnvio.Visible = false;
            }
            else
            {
                indiceTextoSeleccionado = -1;
                cmbTextos.Items.Clear();
                txtLibro.Text = string.Empty;
                txtTipoEntrada.Text = string.Empty;
                txtArchivoPlanMath.Text = string.Empty;
                txtCalculo.Text = string.Empty;
                txtEntrada.Text = string.Empty;
            }
        }

        private void LlenarTextos(Entrada_Desde_Word entrada)
        {
            foreach(var itemTexto in entrada.TextosBusqueda)
            {
                string texto = "Número de página: " + itemTexto.NumeroPagina.ToString() + " " +
                    "Número de Fila: " + itemTexto.NumeroFila.ToString() + " " +
                    "Posición de texto inicial: " + itemTexto.CaracterInicial.ToString() + " " +
                    "Posición de texto final: " + itemTexto.CaracterFinal.ToString();

                cmbTextos.Items.Add(texto);
            }
        }

        private void cmbTextos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ejecucionSeleccionada.SelectedItem != null)
            {
                if (this.ejecucionSeleccionada.SelectedIndex != -1)
                {
                    EnvioEntradaDesdeEjecucion ejecucionSeleccionada = (EnvioEntradaDesdeEjecucion)this.ejecucionSeleccionada.SelectedItem;
                    PlanMath_Word.SeleccionarTextos_DefinicionEjecucion(ejecucionSeleccionada.DefinicionEntrada, cmbTextos.SelectedIndex);
                }
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
            if (IndiceEjecucionSeleccionada < PlanMath_Word.Ejecuciones_EnvioEntradas.
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
            if (SeleccionInstancia.SeleccionandoTextos)
            {
                MessageBox.Show("Termina de seleccionar los textos antes de enviar la entrada a PlanMath.");
            }
            else
            {
                if (IndiceEjecucionSeleccionada > -1 && ejecucionSeleccionada.SelectedItem != null)
                {
                    EnvioEntradaDesdeEjecucion ejecucion = (EnvioEntradaDesdeEjecucion)ejecucionSeleccionada.SelectedItem;
                    Entrada_Desde_Word textosSeleccionados = null;

                    if (ModoManual)
                    {
                        textosSeleccionados = SeleccionInstancia.EntradaActual;

                        foreach (var itemTexto in SeleccionInstancia.TextosBusqueda)
                        {
                            Entradas.TextoBusqueda_DocumentoWord texto = new Entradas.TextoBusqueda_DocumentoWord();
                            texto.NumeroFila = itemTexto.NumeroFila;
                            texto.NumeroPagina = itemTexto.NumeroPagina;
                            texto.CaracterInicial = itemTexto.CaracterInicial;
                            texto.CaracterFinal = itemTexto.CaracterFinal;
                            textosSeleccionados.TextosBusqueda.Add(texto);
                        }

                        textosSeleccionados.Nombre = ejecucion.DefinicionEntrada.Nombre;
                    }
                    else
                    {
                        textosSeleccionados = ejecucion.DefinicionEntrada;
                    }

                    int indiceTexto = 0;
                    List<string> textos = new List<string>();

                    foreach (var itemTexto in textosSeleccionados.TextosBusqueda)
                    {
                        PlanMath_Word.SeleccionarTextos_DefinicionEjecucion(textosSeleccionados, indiceTexto);
                        textos.Add(PlanMath_para_Word.Globals.PlanMath_Word.Application.Selection.Text);
                        indiceTexto++;
                    }

                    EnviarConjuntoTextos_APlanMath(textos, ejecucion.ID_Ejecucion, textosSeleccionados.Nombre);

                    btnQuitarEjecucion_Click(this, e);

                    SeleccionInstancia.RangosSeleccionados.Clear();
                    SeleccionInstancia.SeleccionesTextos.Clear();
                    SeleccionInstancia.TextosBusqueda.Clear();
                    SeleccionInstancia.EntradaActual = new Entrada_Desde_Word();

                    iconoEnvioOk.Visible = true;
                    lblEnvio.Text = "Variable o vector de entrada enviada a la ejecución de PlanMath.";
                    lblEnvio.Visible = true;
                }
            }
        }

        private void EnviarConjuntoTextos_APlanMath(List<string> numeros, string ID_Ejecucion, string nombreEntrada)
        {
            string rutaArchivo = PlanMath_Word.RutaCarpeta_PlanMath_EnvioEntradas_Ejecucion + "\\" + ID_Ejecucion + "-" + nombreEntrada + ".dat";
            XmlWriter escribirArchivo = XmlWriter.Create(rutaArchivo);
            escribirArchivo.WriteStartElement("EnvioEntrada");
            escribirArchivo.WriteStartElement("Numeros");
            DataContractSerializer objeto = new DataContractSerializer(typeof(List<string>), new DataContractSerializerSettings() { PreserveObjectReferences = true });
            objeto.WriteObject(escribirArchivo, numeros);
            escribirArchivo.WriteEndElement();
            escribirArchivo.WriteEndElement();
            escribirArchivo.Close();
        }

        private void procesoActualizarEjecucionSeleccionada_Tick(object sender, EventArgs e)
        {
            if (ActualizarEjecucionSeleccionada)
            {
                ejecucionSeleccionada.SelectedItem = null;

                ejecucionSeleccionada.DataSource = null;
                ejecucionSeleccionada.DisplayMember = "Texto_Ejecucion";

                ejecucionSeleccionada.DataSource = PlanMath_Word.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList(); ;

                if (PlanMath_Word.Ejecuciones_EnvioEntradas.
                    Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) > 0)
                {
                    IndiceEjecucionSeleccionada = PlanMath_Word.Ejecuciones_EnvioEntradas.
                        Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) - 1;
                    ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
                }

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

                var elemento = PlanMath_Word.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList().ElementAt(indice);

                PlanMath_Word.Ejecuciones_EnvioEntradas.Remove(elemento);

                ejecucionSeleccionada.DataSource = PlanMath_Word.Ejecuciones_EnvioEntradas
                    .Where(i => i.DefinicionEntrada.EntradaManual == ModoManual).ToList();

                IndiceEjecucionSeleccionada = PlanMath_Word.Ejecuciones_EnvioEntradas.
                    Count(i => i.DefinicionEntrada.EntradaManual == ModoManual) - 1;
                ejecucionSeleccionada.SelectedIndex = IndiceEjecucionSeleccionada;
            }
        }

        private void btnTextoAnterior_Click(object sender, EventArgs e)
        {
            if (IndiceEjecucionSeleccionada > -1)
            {
                if (indiceTextoSeleccionado > 0)
                {
                    indiceTextoSeleccionado--;
                    cmbTextos.SelectedIndex = indiceTextoSeleccionado;
                    iconoEnvioOk.Visible = false;
                    lblEnvio.Visible = false;
                }
            }
        }

        private void btnTextoSiguiente_Click(object sender, EventArgs e)
        {
            if (IndiceEjecucionSeleccionada > -1)
            {
                EnvioEntradaDesdeEjecucion ejecucion = (EnvioEntradaDesdeEjecucion)ejecucionSeleccionada.SelectedItem;

                if (ejecucion != null)
                {
                    if (indiceTextoSeleccionado < ejecucion.DefinicionEntrada.TextosBusqueda.Count - 1)
                    {
                        indiceTextoSeleccionado++;
                        cmbTextos.SelectedIndex = indiceTextoSeleccionado;
                        iconoEnvioOk.Visible = false;
                        lblEnvio.Visible = false;
                    }
                }
            }
        }

        private void PanelEnviarEntradas_Load(object sender, EventArgs e)
        {
            if (Globals.PlanMath_Word.EsModoOscuro())
            {
                Globals.PlanMath_Word.ColorearControles(this);
            }
        }

        private void btnActivarDesactivarSeleccionandoTextos_Click(object sender, EventArgs e)
        {
            if (!SeleccionInstancia.SeleccionandoTextos)
            {
                SeleccionInstancia.SeleccionandoTextos = true;
                btnActivarDesactivarSeleccionandoTextos.Text = "Seleccionando textos...\nDetener seleccionar textos";
                btnActivarDesactivarSeleccionandoTextos.Image = global::PlanMath_para_Word.Properties.Resources._02;
                Globals.PlanMath_Word.PlanMath_Word_WindowSelectionChange(
                     Globals.PlanMath_Word.Application.Selection);
            }
            else
            {
                SeleccionInstancia.SeleccionandoTextos = false;
                btnActivarDesactivarSeleccionandoTextos.Text = "Comenzar a seleccionar textos";
                btnActivarDesactivarSeleccionandoTextos.Image = global::PlanMath_para_Word.Properties.Resources._01;
            }
        }

        private void establecerTextosEnviar_Click(object sender, EventArgs e)
        {
            AsignacionManual_ListaTextos asignacion = new AsignacionManual_ListaTextos();
            asignacion.SeleccionInstancia = SeleccionInstancia;
            asignacion.ShowDialog();
        }

        private void agregarTexto_Click(object sender, EventArgs e)
        {
            if (SeleccionInstancia != null)
            {
                foreach (var itemSeleccion in SeleccionInstancia.SeleccionesTextos)
                {
                    var texto = SeleccionInstancia.TextosBusqueda.Where(i => i.CaracterInicial == itemSeleccion.CaracterInicial &&
                    i.CaracterFinal == itemSeleccion.CaracterFinal &&
                    i.NumeroPagina == itemSeleccion.NumeroPagina &&
                    i.NumeroFila == itemSeleccion.NumeroFila).FirstOrDefault();

                    if (texto == null)
                    {
                        SeleccionInstancia.TextosBusqueda.Add(new TextoBusqueda_Instancia()
                        {
                            CaracterFinal = itemSeleccion.CaracterFinal,
                            CaracterInicial = itemSeleccion.CaracterInicial,
                            NumeroFila = itemSeleccion.NumeroFila,
                            NumeroPagina = itemSeleccion.NumeroPagina,
                            Texto = itemSeleccion.TextoActual
                        });

                        //string[] subTextos = new string[3];
                        //subTextos[0] = itemSeleccion.TextoActual;
                        //subTextos[1] = SeleccionInstancia.TextosBusqueda.Last().NumeroPagina.ToString();
                        //subTextos[2] = SeleccionInstancia.TextosBusqueda.Last().NumeroFila.ToString();

                        //ListViewItem item = new ListViewItem(subTextos);
                        //item.Tag = SeleccionInstancia.TextosBusqueda.Last();

                        //listaTextos.Items.Add(item);

                        //panelDefinicionEntradas.iconoEnvioOk.Visible = false;
                        //panelDefinicionEntradas.lblEnvio.Visible = false;
                    }
                }
            }
        }

        private void ejecucionSeleccionada_Click(object sender, EventArgs e)
        {
            if (ModoManual)
            {
                if (SeleccionInstancia.SeleccionandoTextos)
                {
                    btnActivarDesactivarSeleccionandoTextos_Click(this, e);
                }
            }
        }
    }
}

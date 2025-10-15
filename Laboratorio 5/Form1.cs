using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_5
{
    public partial class Form1 : Form
    {
        double platino = 0;
        double VIP = 0;
        double general = 0;
        double priceEstacionamiento;
        double priceEntradas;
        double subtotal1, subtotal2, SPAC, ITBMS;

        public Form1()
        {
            InitializeComponent();
            nUPestacionamientoEntradas.Visible = false;

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            validarFactura();
            string nombre = textNombre.Text.Trim();
            
            Cliente cliente = new Cliente(nombre);

            if (rbPlatino.Checked) platino = 10;
            if (rbVIP.Checked) VIP = 20;
            if (rbGeneral.Checked) general = 120;

            Entradas entradas = new Entradas(platino,VIP,general);
            lblNombre.Text = "Nombre: " + textNombre.Text;


            resumenDeFactura();
        }
        //Boton de limpiar Factura y datos del Usuario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textNombre.Clear();
            lblNombre.Text = "";
            lblEntradas.Text = "";
            lblCantidad.Text = "";
            lblCantidadEstacionam.Text = "";
            PrecioEstacionamiento.Text = "";
            precioEntrada.Text = "";
            lblSubtotal1.Text = "";
            lblSubtotal2.Text = "";
            lblSPAC.Text = "";
            lblITBMS.Text = "";
            lblTOTAL.Text = "";
            lblEstacionamiento.Text = ".";
            validarFactura();
            btnRegistrar.Enabled = true;
            
        }

        private void validarFactura()
        {
            if (lblNombre.Text != "")
            {
                btnRegistrar.Enabled = false;
            }
        }

        private void textNombre_Validating(object sender, CancelEventArgs e)
        {
            if (sender is Control c)
            {
                if (string.IsNullOrWhiteSpace(c.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(c, "Campo requerido.");
                }
                else
                {
                    errorProvider1.SetError(c, "");
                }
            }
        }

        private void nombreKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            bool letra = char.IsLetter(e.KeyChar);
            bool espacio = char.IsWhiteSpace(e.KeyChar);
            bool guion = e.KeyChar == '-' || e.KeyChar == '/';

            if (!(letra || espacio || guion))
            {
                e.Handled = true;
            }
        }

        private void textNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }


        //calculos de factura y validaciones de Radio Buttons



        private void resumenDeFactura()
        {
            int cantidadEntradas = Convert.ToInt32(entradas.Value);

            if (rbPlatino.Checked)
            {
                if (!int.TryParse(entradasP.Text, out int disponibles))
                    disponibles = 0; 

                disponibles -= cantidadEntradas;
                if (disponibles < 0) disponibles = 0;

                rbPlatino.Enabled = disponibles > 0;
                if (disponibles == 0 && rbPlatino.Checked)
                {
                    rbPlatino.Checked = false;
                }

                precioEntrada.Text = (priceEntradas = (cantidadEntradas * 150)).ToString()+"$";
                lblEntradas.Text = "Platino";
                lblCantidad.Text = "X"+ cantidadEntradas.ToString();
                entradasP.Text = disponibles.ToString();
            }

            if (rbVIP.Checked)
            {
                if (!int.TryParse(entradasV.Text, out int disponibles))
                    disponibles = 0;

                disponibles -= cantidadEntradas;
                if (disponibles < 0) disponibles = 0;

                rbVIP.Enabled = disponibles > 0;
                if (disponibles == 0 && rbVIP.Checked)
                {
                    rbVIP.Checked = false;
                }

                precioEntrada.Text = (priceEntradas=(cantidadEntradas * 100)).ToString() + "$";
                lblEntradas.Text = "VIP";
                lblCantidad.Text = "X" + cantidadEntradas.ToString();
                entradasV.Text = disponibles.ToString();
            }

            if (rbGeneral.Checked)
            {
                if (!int.TryParse(entradasG.Text, out int disponibles))
                    disponibles = 0;

                disponibles -= cantidadEntradas;
                if (disponibles < 0) disponibles = 0;

                rbGeneral.Enabled = disponibles > 0;
                if (disponibles == 0 && rbGeneral.Checked)
                {
                    rbGeneral.Checked = false;
                }

                precioEntrada.Text = (priceEntradas=(cantidadEntradas * 50)).ToString() + "$";
                lblEntradas.Text = "General";
                lblCantidad.Text = "X" + cantidadEntradas.ToString();
                entradasG.Text = disponibles.ToString();
            }

            //ESTACIONAMIENTO
            int cantidadEstacionamientos = Convert.ToInt32(nUPestacionamientoEntradas.Value);
            priceEstacionamiento = cantidadEstacionamientos * 25;
            lblEstacionamiento.Text = "Estacionamiento";
            if (rbSEstacionamiento.Checked == true) { lblCantidadEstacionam.Text = "X" + nUPestacionamientoEntradas.Text; PrecioEstacionamiento.Text = Convert.ToString(priceEstacionamiento) + "$"; }
            if (rbNestacionamiento.Checked == true) { lblCantidadEstacionam.Text = "X0"; PrecioEstacionamiento.Text = "0$"; }

            //SUBTOTAL1
            subtotal1 = priceEstacionamiento + priceEntradas;
            lblSubtotal1.Text = Convert.ToString(subtotal1) + "$";
            //SPAC 5%
            SPAC = priceEntradas * 0.05;
            lblSPAC.Text = Convert.ToString(SPAC) + "$";
            //SUBTOTAL2
            subtotal2 = subtotal1 + SPAC;
            lblSubtotal2.Text = Convert.ToString(subtotal2) + "$";
            //ITBMS 7%
            ITBMS = subtotal2 * 0.07;
            lblITBMS.Text = Convert.ToString(ITBMS) + "$";
            //Total a pagar
            lblTOTAL.Text = Convert.ToString(subtotal2 + ITBMS) + "$";


            variableGuardar.Text += "Nombre: "+ textNombre.Text + " | " + "Tipo de Entrada: " + lblEntradas.Text + " | " + lblCantidad.Text + " | "+"Estacionamiento: "+ lblCantidadEstacionam.Text+ " | " +"Total: "+ lblTOTAL.Text + Environment.NewLine;

        }
        // hola
        

        private void rbSEstacionamiento_CheckedChanged(object sender, EventArgs e)
        {
            nUPestacionamientoEntradas.Visible = true;
        }

        private void rbNestacionamiento_CheckedChanged(object sender, EventArgs e)
        {
            nUPestacionamientoEntradas.Visible = false;

        }

        
    }
}
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
        public Form1()
        {
            InitializeComponent();
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
            precioEntrada.Text = "";
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

        //calculos de factura y validaciones de Radio Buttons

        private void resumenDeFactura()
        {
            int cantidad = Convert.ToInt32(entradas.Value);

            if (rbPlatino.Checked)
            {
                if (!int.TryParse(entradasP.Text, out int disponibles))
                    disponibles = 0; 

                disponibles -= cantidad;
                if (disponibles < 0) disponibles = 0;

                rbPlatino.Enabled = disponibles > 0;
                if (disponibles == 0 && rbPlatino.Checked)
                {
                    rbPlatino.Checked = false;
                }

                precioEntrada.Text = (cantidad * 150).ToString()+"$";
                lblEntradas.Text = "Platino X"+cantidad.ToString();
                entradasP.Text = disponibles.ToString();
            }

            if (rbVIP.Checked)
            {
                if (!int.TryParse(entradasV.Text, out int disponibles))
                    disponibles = 0;

                disponibles -= cantidad;
                if (disponibles < 0) disponibles = 0;

                rbVIP.Enabled = disponibles > 0;
                if (disponibles == 0 && rbVIP.Checked)
                {
                    rbVIP.Checked = false;
                }

                precioEntrada.Text = (cantidad * 100).ToString() + "$";
                lblEntradas.Text = "VIP X" + cantidad.ToString();
                entradasV.Text = disponibles.ToString();
            }

            if (rbGeneral.Checked)
            {
                if (!int.TryParse(entradasG.Text, out int disponibles))
                    disponibles = 0;

                disponibles -= cantidad;
                if (disponibles < 0) disponibles = 0;

                rbGeneral.Enabled = disponibles > 0;
                if (disponibles == 0 && rbGeneral.Checked)
                {
                    rbGeneral.Checked = false;
                }

                precioEntrada.Text = (cantidad * 50).ToString() + "$";
                lblEntradas.Text = "General X" + cantidad.ToString();
                entradasG.Text = disponibles.ToString();
            }


        }

    }
}
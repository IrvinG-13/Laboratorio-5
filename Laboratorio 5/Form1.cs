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
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            validarFactura();
            string nombre = textNombre.Text.Trim();
            Cliente cliente = new Cliente(nombre);

            lblNombre.Text = "Nombre: " + textNombre.Text;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textNombre.Clear();
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
    }
}

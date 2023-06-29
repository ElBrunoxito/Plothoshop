using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoMatematicaComputacional
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inicio P = new Inicio();
            this.Visible = false;
            P.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //vERIFICAR
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Complete los campos", "Error");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Contraseñas no coinciden", "Error");
            }
            else
            {
                Verificacion v = new Verificacion();

                if (v.Registro(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Se registro exitosamente");
                    Inicio N = new Inicio();
                    this.Visible = false;
                    N.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Ocurrio un error");                    
                }



            }

        }
    }
}

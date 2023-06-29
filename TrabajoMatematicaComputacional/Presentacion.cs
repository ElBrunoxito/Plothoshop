using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace TrabajoMatematicaComputacional
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();           
        }
        private void Presentacion_Load(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)

        {
            Integrantes i = new Integrantes();
            this.Visible = false;              
            i.Show();  
        }

        //Aprobar usuario y contraseña
        private void button1_Click(object sender, EventArgs e)
        {
            Buttonuno();                   
        }

        private void Buttonuno()
        {            
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Complete los campos", "Error");
            }
            else
            {
                Verificacion v = new Verificacion();
                if (v.Ingresa(textBox1.Text, textBox2.Text))
                {
                    Form1 N = new Form1(textBox1.Text);
                    this.Visible = false;
                    N.ShowDialog();
                }
                else
                {
                    DialogResult r = MessageBox.Show("Debes crearte una cuenta", "Error", MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    if (r == DialogResult.Yes)
                    {
                        Registro R = new Registro();
                        this.Visible = false;
                        R.ShowDialog();
                    }

                }



            }
        }

        private void TeclasPresionadas(object sender, KeyEventArgs e)
        {

        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Presentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            Buttonuno();
        }

      

    }
}

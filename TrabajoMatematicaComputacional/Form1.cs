using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;



namespace TrabajoMatematicaComputacional
{
    partial class Form1 : Form
    {
        Bitmap Original;        
        //Transformaciones Histograma;
        XmlDocument xd = new XmlDocument();
        string[] listComboBox = { "Expandir", "Ecualizar", "Convertir a grises" };

        public Form1(string Usuario)
        {            
            InitializeComponent();
            this.menuStrip1.Items[0].Text = Usuario;           
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string i in listComboBox)
                this.comboBox1.Items.Add(i);                      
        }

        
        //Otras opciones
        private void button2_Click(object sender, EventArgs e)
        {
               
        }      
        private void chart1_Click(object sender, EventArgs e)
        {

        }       
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }       
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {         
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;                     
        }                               
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void CompletoPictureBox_2(object sender, AsyncCompletedEventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Cargar fondo de pantalla
        private void fondoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.colorDialog1.ShowDialog();
                Color c = colorDialog1.Color;
                panel5.BackColor = c;
                this.BackColor = c;
            }catch(Exception)
            {
                
            }

        }

        private void guardarImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Borrar la Serie de Histograma
        private void BorrarChart(Chart c)
        {
            c.Series["Intensidades"].Points.Clear();
        }


        //Seleccion del combobox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //var funciones = new List

            //if (comboBox1.SelectedItem == comboBox1.Items[0])OriginalH();

            {
                if (comboBox1.SelectedItem == comboBox1.Items[0]) { 
                    ExpandirH();
                    //ExpandirA();
                }
                if (comboBox1.SelectedItem == comboBox1.Items[1])
                {
                    EcualzarA();
                    //EcualizarH();
                }
              
                
                if (comboBox1.SelectedItem == comboBox1.Items[2]) BlancoNegro();

                pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }



        }
        //Graficar Histograma original
        private void OriginalH()
        {
            try
            {

                BorrarChart(chart1);
                Transformaciones Histograma = new Transformaciones(Original);
                int[] HistogramaOriginal = Histograma.histogramaOriginal;
                Color[] colores = Histograma.coloresO;
                            
                for (int i = 0; i < 256; i++)
                {
                    chart1.Series["Intensidades"].Points.Add(HistogramaOriginal[i]);
                    chart1.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                    chart1.Series["Intensidades"].Points[i].LegendText = i.ToString();
                    chart1.Series["Intensidades"].Points[i].Color = colores[i];
                   
                }
                chart1.Visible = true;
            }
            catch (Exception )
            {
                MessageBox.Show("Error :(");
            }
        }
        //Graficar Histograma expandido
        private void ExpandirH()
        {
            try
            {
                BorrarChart(chart2);
                Transformaciones Histograma = new Transformaciones(Original);
                pictureBox2.Image = Histograma.ExpandirHistograma(0, 256);
                Color[] colores = Histograma.coloresT;
                int[] HistogramaExpandido = Histograma.histogramaT;
                for (int i = 0; i < 256; i++)
                {
                    chart2.Series["Intensidades"].Points.Add(HistogramaExpandido[i]);
                    chart2.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                    chart2.Series["Intensidades"].Points[i].LegendText = i.ToString();
                    chart2.Series["Intensidades"].Points[i].Color = colores[i];
                }
                chart2.Visible = true;
                
            }
            catch 
            {
                MessageBox.Show("Error :(");
            }
        }
        private void ExpandirA(){
            try
            {
                BorrarChart(chart2);
                Transformaciones Histograma = new Transformaciones(Original);
                pictureBox2.Image = Histograma.ExpandirProfesional();
                Color[] colores = Histograma.coloresT;
                int[] HistogramaExpandido = Histograma.histogramaT;
                for (int i = 0; i < 256; i++)
                {
                    chart2.Series["Intensidades"].Points.Add(HistogramaExpandido[i]);
                    chart2.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                    chart2.Series["Intensidades"].Points[i].LegendText = i.ToString();
                    chart2.Series["Intensidades"].Points[i].Color = colores[i];
                }
                chart2.Visible = true;
            } catch 
            {
                MessageBox.Show("Error :(");
            }
}
        //Graficar Histograma ecualizado
        private void EcualizarH()
        {
            try
            {
                BorrarChart(chart2);                
                Transformaciones Histograma = new Transformaciones(Original);
                pictureBox2.Image = Histograma.EcualizarHistograma();
                Color[] colores = Histograma.coloresT;
                int[] HistogramaEcualizado = Histograma.histogramaT;
                
                for (int i = 0; i < HistogramaEcualizado.Length; i++)
                {
                    chart2.Series["Intensidades"].Points.Add(HistogramaEcualizado[i]);
                    chart2.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                    chart2.Series["Intensidades"].Points[i].LegendText = i.ToString();
                    chart2.Series["Intensidades"].Points[i].Color = colores[i];
                }                               
                chart2.Visible = true;               
            }
            catch (Exception )
            {
                MessageBox.Show("Error :(");
            }
        }      
        private void EcualzarA()
        {
            BorrarChart(chart2);
            Transformaciones Histograma = new Transformaciones(Original);
            pictureBox2.Image = Histograma.EcualizadoProfesional();
            Color[] colores = Histograma.coloresT;
            int[] HistogramaEcualizado = Histograma.histogramaT;
            for (int i = 0; i < HistogramaEcualizado.Length; i++)
            {
                chart2.Series["Intensidades"].Points.Add(HistogramaEcualizado[i]);
                chart2.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                chart2.Series["Intensidades"].Points[i].LegendText = i.ToString();
                chart2.Series["Intensidades"].Points[i].Color = colores[i];
            }
            chart2.Visible = true;

        }
        //Graficar Histograma Blanco y Negro
        private void BlancoNegro()
        {
            try
            {
                BorrarChart(chart2);
                Transformaciones Histograma = new Transformaciones(Original);
                pictureBox2.Image = Histograma.BlancoYNegro();
                Color[] colores = Histograma.coloresT;
                int[] HistogramaBN = Histograma.histogramaT;
                for (int i = 0; i < HistogramaBN.Length; i++)
                {
                    chart2.Series["Intensidades"].Points.Add(HistogramaBN[i]);
                    chart2.Series["Intensidades"].Points[i].AxisLabel = i.ToString();
                    chart2.Series["Intensidades"].Points[i].LegendText = i.ToString();
                    chart2.Series["Intensidades"].Points[i].Color = colores[i];
                }
                chart2.Visible = true;
            }
            catch (Exception )
            {
                MessageBox.Show("Error :(");
            }
        }




        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }





        //Opcines para cargar imagen
        private void button1_Click(object sender, EventArgs e)
        {
            Cargar();
        }
        private void abrirImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cargar();
        }
        //Opciones para guardar imagen
        private void button2_Click_1(object sender, EventArgs e)
        {
            Guardar();
        }
        private void guardarImagenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Guardar();
        }






        //Funcion Guardar Imagen
        private void Guardar()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Imagenes | *.png;*.jpg";
                sfd.Title = "Guardar Imagen";
                System.Drawing.Image img = pictureBox2.Image;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    img.Save(sfd.FileName);
                }                                                             
            }
            catch (Exception )
            {

            }

        }
        //Funcion Cargar Imagen
        private void Cargar()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagenes | *.png;*.jpg;*.jfif;*.jpeg";
            ofd.Title = "Abrir Imagen";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string Ruta = ofd.FileName;
                Original = new Bitmap(Ruta);

                pictureBox1.Image = Original;
                


                //MessageBox.Show(Histograma.min.ToString() + " ," + Histograma.max.ToString());

                MessageBox.Show("Imagen correctamente cargada");
                //Histograma = new Transformaciones(Original);
                OriginalH();
                Transformaciones s = new Transformaciones(Original);
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta opcion no esta disponible :(");
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inicio P = new Inicio();
            this.Visible = false;            
            P.Show();
        }

        private void miCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Esta opcion no esta disponible :(");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

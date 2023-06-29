using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;


namespace TrabajoMatematicaComputacional
{
    partial class Transformaciones
    {                
        public int[] histogramaOriginal = new int[256];
        public int[] histogramaT = new int[256];
        private int[] mapeo = new int[256];
        //*****
        public int[,] RGB = new int[3, 256];
        //*****

        public Color[] coloresO = new Color[256];
        public Color[] coloresT = new Color[256];


        private long MN;
        private float[] hi = new float[256];
        private int[] sk = new int[256];
        private int L = 255;
        //Imagen original;
        private Bitmap B;
        
        int w , h ;
        public int min = 0, max = 0;
        private int s1,s2;
          
        public Transformaciones(Bitmap B)
        {
            Matrices m = new Matrices();
            histogramaOriginal = m.getValuesBitmap(B);
            MN = B.Width * B.Height;
            coloresO = m.colore;
            this.B = B;
            MinandMax();
            w = B.Width;                
            h = B.Height;

            rmapeo();
            for (int i = 0; i < 256; i++)
            {
                //long aux =  histogramaOriginal[i] / MN;
                hi[i] = (((float)histogramaOriginal[i]) / ((float)MN));
            }
            float Suma = hi[0];
            for (int i = 0; i < 256; i++)
            {
                Suma += hi[i];
                sk[i] = (int)(Math.Round(Suma * L));

            }

        }   
        //ExpandirHistograma             
        public Bitmap ExpandirHistograma(int s1, int s2)
        {
            this.s1 = s1;
            this.s2 = s2;
            Bitmap r = new Bitmap(w, h);
            Matrices m = new Matrices();
            
            int re = 255, ve = 255, ae = 255;
          
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Color co = B.GetPixel(i, j);
                    int ro = co.R;
                    int vo = co.G;
                    int ao = co.B;



                    funtions a;
                    re = a.FuncionEspecial(Tr(ro));
                    ve = a.FuncionEspecial(Tr(vo));
                    ae = a.FuncionEspecial(Tr(ao));
                    //int Gray = a.FuncionEspecial(Tr((ro + vo + ao) / 3));
                    Color ce = Color.FromArgb(re, ve, ae);
                    //Color ce = Color.FromArgb(Gray, Gray, Gray);
                 
                    r.SetPixel(i, j, ce);
                }
            }
            
            
            histogramaT = m.getValuesBitmap(r);
            coloresT = m.colore;
           
            return r;
        }
        public Bitmap ExpandirProfesional()
        {
            Matrices m = new Matrices();
            ResizeBilinear filtro = new ResizeBilinear(B.Width, B.Height);
            Bitmap r = filtro.Apply(B);
            histogramaT = m.getValuesBitmap(r);
            coloresT = m.colore;
            return r;
        }

        //Funcion Recta
        private int Tr(int r)
        { 
            double m = (s2 - s1) / (max - min);           
            double b = s1 - (min * m);       
            return (int)(Math.Round((r * m) + b));
        }
        //Ecualizar Histograma
        public Bitmap EcualizarHistograma()
        {
            Bitmap r = new Bitmap(w, h);
            Matrices m = new Matrices();
                    
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Color co = B.GetPixel(i, j);
                    int ro = co.R;
                    int vo = co.G;
                    int ao = co.B;


                    funtions a;
                    int  re = a.FuncionEspecial(ECEcual(ro));
                    int ve = a.FuncionEspecial(ECEcual(vo));
                    int ae = a.FuncionEspecial(ECEcual(ao));

                    
                    Color ce = Color.FromArgb(re, ve, ae);

                    // Asignar el nuevo color en la imagen expandida
                    r.SetPixel(i, j, ce);
                    
                }
            }

            histogramaT = m.getValuesBitmap(r);
            coloresT = m.colore;
            return r;

        }          
        public Bitmap EcualizadoProfesional()
        {
            Matrices m = new Matrices();
            HistogramEqualization he = new HistogramEqualization();
            Bitmap r =he.Apply(B);
            histogramaT = m.getValuesBitmap(r);
            coloresT = m.colore;
            return r;
            
        }

        private int ECEcual(int R)
        {
            return sk[R];
        }
        public Bitmap BlancoYNegro()
        {
            
            Bitmap Resultado = new Bitmap(w, h);
            Matrices m = new Matrices();
            Color A, N;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    A = B.GetPixel(i, j);
                    int P = (A.R + A.G + A.B) / 3;
                    N = Color.FromArgb(P, P, P);

                    Resultado.SetPixel(i, j, N);


                }
            }
            histogramaT = m.getValuesBitmap(Resultado);
            coloresT = m.colore;
            return Resultado;
        }
        private void MinandMax()
        {
            max = histogramaOriginal[histogramaOriginal.Length - 1];
            min = histogramaOriginal[0];

            for (int i = 0; i < histogramaOriginal.Length; i++)
            {
                if (histogramaOriginal[i] != 0)
                {
                    min = i;
                    break;
                }

            }
            for (int i = histogramaOriginal.Length - 1; i >= 0; i--)
            {
                if (histogramaOriginal[i] != 0)
                {
                    max = i;
                    break;
                }

            }
        }


        public bool EsColor()
        {
            bool Au = false ;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Color A = B.GetPixel(i, j);
                    int P = (A.R + A.G + A.B) / 3;
                    if (P == A.R && P == A.B && P == A.B) 
                        Au= true;
                }
            }
            return Au;
        }
        private void rmapeo()
        {
            for (int i = 0; i < 256; i++)
            {
                mapeo[i] = (int)(((double)(i - min) / (max - min)) * 255);
            }
        }

        private int Fe(int c)
        {
            int nuevaIntensidad = mapeo[c];
            return nuevaIntensidad;
        }
     
    }
    struct funtions{
   
        public int FuncionEspecial(int tr)
        {

            if (tr > 255)
                return 255;
            else if (tr < 0)
                return 0;
            else
                return tr;
        }

     
    }
   
}

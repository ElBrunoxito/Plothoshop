using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TrabajoMatematicaComputacional
{
    class Matrices
    {
        
        public int[] Values = new int[256];

        public Color[] colore = new Color[256];

        public Matrices()
        {
            RellenarIntensidades();
            //getValuesBitmap(B);
            
        }
        public int[] getValuesBitmap(Bitmap B)
        {
            for (int i = 0; i < B.Width; i++)
            {
                for (int j = 0; j < B.Height; j++)
                {
                    Color c = B.GetPixel(i, j);

                    int r = c.R;
                    int g = c.G;
                    int b = c.B;
                    //int Gris = (int)((r* 0.299 + g* 0.587  + b* 0.114));
                    int Gris = (int)((r + g + b) / 3);
                    Values[Gris]++;
                    colore[Gris] = c;

                }

            }

            return Values;

        }      
        
        private void RellenarIntensidades()
        {
            for (int i = 0; i < 256; i++)
            {                                                   
                Values[i] = 0;
                colore[i] = Color.White; 

            }
        }          
    }
}

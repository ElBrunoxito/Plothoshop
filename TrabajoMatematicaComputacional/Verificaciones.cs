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
    class Verificacion
    {
        string Server = "Server=DESKTOP-MFFVEI8";
        string DataBase = "database=BD_Computacional";

        public bool Ingresa(string U, string C) {
            bool Aux = false;
            string Dates = "SELECT usuario, contraseña FROM Usuarios";
            try
            {
                SqlConnection conexion = new SqlConnection(Server + " ; " + DataBase + " ; " + "integrated security = true");
                using (SqlCommand sqlCommand = new SqlCommand(Dates, conexion))
                {
                    //Abrir Conexion
                    conexion.Open();
                    //Leer Comandos sql
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string usuario= reader.GetString(0);
                            string contrasena = reader.GetString(1);

                            if (usuario.Equals(U) && contrasena.Equals(C))
                            {
                                Aux = true;
                                break;
                            }
                        }
                    }
                }

            }catch(SqlException se)
            {
                MessageBox.Show("ERROR: " + se.ToString());
            }
            return Aux;
        }

        public bool Registro(string U, string C)
        {
            bool Aux = false;
            string Query = "INSERT INTO Usuarios(usuario, contraseña) VALUES(@Usuario, @Contraseña)";

            try
            {
                using (SqlConnection sqlConnection1 = new SqlConnection(Server + ";" + DataBase + ";" + "integrated security=true"))
                {
                    sqlConnection1.Open();
                    using (SqlCommand cmd = new SqlCommand(Query, sqlConnection1))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", U);
                        cmd.Parameters.AddWithValue("@Contraseña", C);

                        cmd.ExecuteNonQuery();
                        Aux = true;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }



            return Aux;
        }

        
       


    }


}
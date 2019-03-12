using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacial
{

    class Conexion
    {
        //funcion que conecta a la base de datos, previamente se busca ubicacion del archivo de base de datos
        public OleDbConnection Obtener_conexion()
        {
            OleDbConnection conexion = new OleDbConnection();
            try
            {
                // Path de la aplicación, ahi guardamos la base de datos
                String path = Environment.CurrentDirectory;

                conexion.ConnectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + path + "/bd.accdb;Persist Security Info = False";
                conexion.Open();
            }
            catch (OleDbException e)
            {
                MessageBox.Show("No se encuentra archivo!");
                Process.GetCurrentProcess().Kill();
            }
            return conexion;
        }

        public void Cerrar_conexion(OleDbConnection conexion)
        {
            conexion.Close();
        }

        //ejecuta comando sql
        public void Sql_command(OleDbConnection conexion, String a)
        {
            //se guarda en tabla productos los datos ingresados
            String comando2 = string.Format(a);
            OleDbCommand command2 = new OleDbCommand(comando2, conexion);
            command2.ExecuteNonQuery();
        }
    }


   


}

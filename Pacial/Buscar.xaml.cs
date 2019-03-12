using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pacial
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public static string seleccion_de_nombre;

        public Window1()
        {
            InitializeComponent();

            Conexion cn = new Conexion();
            OleDbConnection conexion = cn.Obtener_conexion();

            OleDbDataAdapter da = new OleDbDataAdapter("SELECT productos.nombre as Nombre,  productos.descripcion as Descripcion, " +
                "productos.precio_compra as PrecioDeCompra, productos.precio_venta as PrecioDeVenta, pais.pais as Pais FROM productos" +
                " left join pais on productos.Idpais = pais.Idpais order by productos.nombre asc", conexion);

            DataSet ds = new DataSet();
            da.Fill(ds);
            cn.Cerrar_conexion(conexion);
            datagrid.ItemsSource = ds.Tables[0].DefaultView;

        }


        private void btn_buscar(object sender, RoutedEventArgs e)
        {
            Conexion cn = new Conexion();
            OleDbConnection conexion = cn.Obtener_conexion();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT productos.nombre, productos.descripcion, " +
                "productos.precio_compra, productos.precio_venta, pais.pais FROM productos" +
                " left join pais on productos.Idpais = pais.Idpais where productos.nombre like" + "'%" + txtbox.Text + "%' order by productos.nombre asc", conexion);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cn.Cerrar_conexion(conexion);
            datagrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void Seleccion_Click(object sender, MouseButtonEventArgs e)
        {
           DataRowView elemento_seleccionado = datagrid.SelectedItem as DataRowView;
           seleccion_de_nombre = Convert.ToString(elemento_seleccionado["nombre"]);

            MainWindow MW= new MainWindow();
            MW.Show();
            this.Close();
        }

        private void btn_cancelar(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }
    }
}

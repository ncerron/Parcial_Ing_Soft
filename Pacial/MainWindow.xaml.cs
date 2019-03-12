using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows;

namespace Pacial
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet ds;
        OleDbDataAdapter da;
        DataRow filas;
        int cantidad_filas; 
        int cont = 0; 
        int cont_posicion;
        public MainWindow()
        {

            InitializeComponent();

            //si la variable de la ventana buscar tiene algun contenido en la variable Window1.seleccion_de_nombre, se carga el 
            // registro correspondiente
            if (!string.IsNullOrEmpty(Window1.seleccion_de_nombre))
            {
                Buscar_posicon();
                cont = cont_posicion;
                Cargar_datos();
            }
            Cargar_datos();
        }
        
        private void Cargar_datos()
        {
            Conexion cn = new Conexion();
            OleDbConnection conexion = cn.Obtener_conexion();

            string cmd = "SELECT *, pais FROM productos" +
              " left join pais on productos.Idpais = pais.Idpais";
            da = new OleDbDataAdapter(cmd, conexion);
            ds = new DataSet();
            da.Fill(ds);
            cantidad_filas = ds.Tables[0].Rows.Count;
            Navegar_registros();
            cn.Cerrar_conexion(conexion);
        }

        private void Navegar_registros()
        {
            filas = ds.Tables[0].Rows[cont];
            nombre.Text = filas.ItemArray.GetValue(2).ToString();
            descripcion.Text = filas.ItemArray.GetValue(3).ToString();
            precio_compra.Text = filas.ItemArray.GetValue(4).ToString();
            precio_venta.Text = filas.ItemArray.GetValue(5).ToString();
            origen.Text = filas.ItemArray.GetValue(0).ToString();
        }

        //se usa para obtener posicion del registro elegido del formulario buscar y poder cargarlo luego
        //en el formulario MainWindow
        private void Buscar_posicon()
        {
            int cont_p = 0;
            Conexion cn = new Conexion();
            OleDbConnection conexion = cn.Obtener_conexion();

            //seleccion de campos de tablas a mostrar en el formulario
            string cmd = "SELECT productos.nombre,  productos.descripcion, " +
                "productos.precio_compra, productos.precio_venta, pais.pais FROM productos" +
                " left join pais on productos.Idpais = pais.Idpais";
            
            da = new OleDbDataAdapter(cmd, conexion);
            ds = new DataSet();
            da.Fill(ds);
            
            foreach (DataRow fila in ds.Tables[0].Rows)
            {
                if (Convert.ToString(fila["nombre"])==(Window1.seleccion_de_nombre))
                {
                    cont_posicion = cont_p;
                }
                cont_p++;
            }
            
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            //limpieza de los textBox
            nombre.Text = "";
            descripcion.Text = "";
            precio_compra.Text = "";
            precio_venta.Text = "";
            origen.Text = "";
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            //busca las ventanas abiertas en la aplicacion y chequea que no este abierta window1
            if (!Application.Current.Windows.OfType<Window1>().Any())
            {
                Window1 ventana_bucar = new Window1();
                ventana_bucar.Show();
            }
            Window1.seleccion_de_nombre = nombre.Text;
            this.Hide();
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)

        {
            if (!nombre.Text.Equals("") && !descripcion.Text.Equals("") && !precio_compra.Text.Equals("") && !precio_venta.Text.Equals("") && !origen.Text.Equals(""))
            {
                if (origen.Text.ToLower().Equals("china") || origen.Text.ToLower().Equals("corea") || origen.Text.ToLower().Equals("india") || origen.Text.ToLower().Equals("argentina"))
                {
                    if ((IsNumeric(precio_compra.Text) == true) && (IsNumeric(precio_venta.Text) == true))
                    {
                        Conexion cn = new Conexion();
                        OleDbConnection conexion = cn.Obtener_conexion();

                        //se busca id de pais del producto ingresado
                        int consulta = Id_pais(conexion);

                        //buscar producto en base de datos
                        string nombre_existente = Buscar_si_existe_producto(conexion, consulta);

                        if (string.IsNullOrEmpty(nombre_existente))
                        {
                            //ingreso de los datos ingresados a la base de datos
                            string a = string.Format("INSERT INTO productos(nombre, descripcion, precio_compra, precio_venta, Idpais)" +
                            " VALUES('{0}','{1}','{2}','{3}','{4}')", nombre.Text, descripcion.Text, decimal.Parse(precio_compra.Text),
                            decimal.Parse(precio_venta.Text), consulta);

                            cn.Sql_command(conexion, a);

                            cn.Cerrar_conexion(conexion);
                            MessageBox.Show("Registro Guardado!");

                            cont = cantidad_filas;  
                            Cargar_datos();
                        }
                        else
                            MessageBox.Show("Existe el producto en la base de datos");
                    }
                    else
                        MessageBox.Show("Revise los datos ingresados, debe ingresar un numero!");
                }
                else MessageBox.Show("No es un dato de origen válido");
            }
            else  MessageBox.Show("Existen campos vacios");

        }
        //busca producto comparando nombre, descripcon y pais
        private string Buscar_si_existe_producto(OleDbConnection conexion, int consulta)
        {
            string buscar_si_existe_producto = string.Format("SELECT * FROM productos WHERE (productos.nombre LIKE" +
                " '{0}') and (productos.descripcion LIKE '{1}') and (productos.Idpais LIKE '{2}')",
                nombre.Text, descripcion.Text, consulta);

            OleDbCommand command = new OleDbCommand(buscar_si_existe_producto, conexion);
            string nombre_existente = Convert.ToString(command.ExecuteScalar());
            return nombre_existente;
        }

        #region navegar registros

        //navegamos al primer registro 
        private void Primero_Click(object sender, RoutedEventArgs e)
        {
            if (cont != 0)
            {
                cont = 0;
                Navegar_registros();
            }
        }

        //navegamos al ultimo registro
        private void Ultimo_Click(object sender, RoutedEventArgs e)
        {
            if (cont != cantidad_filas -1)
            {
                cont = cantidad_filas - 1;
                Navegar_registros();
            }
        }

        //avanzamos un registro
        private void Avanzar_Click(object sender, RoutedEventArgs e)
        {
            if (cont != cantidad_filas - 1)
            {
                cont++;
                Navegar_registros();
            }
            else MessageBox.Show("No hay mas registros!");
        }

        //retrocedemos un registro
        private void Retroceder_Click(object sender, RoutedEventArgs e)
        {
            if (cont > 0)
            {
                cont--;
                Navegar_registros();
            }
            else  MessageBox.Show("Primer registro!");
        }

        #endregion

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {  
            if (!nombre.Text.Equals("") && !descripcion.Text.Equals("") && !precio_compra.Text.Equals("") &&
                !precio_venta.Text.Equals("") && !origen.Text.Equals(""))
            {
                if (origen.Text.ToLower().Equals("china") || origen.Text.ToLower().Equals("corea") || 
                    origen.Text.ToLower().Equals("india") || origen.Text.ToLower().Equals("argentina"))
                {
                    if ((IsNumeric(precio_compra.Text) == true) && (IsNumeric(precio_venta.Text) == true))
                    {
                        Conexion cn = new Conexion();
                        OleDbConnection conexion = cn.Obtener_conexion();

                        //se busca id de pais del producto ingresado
                        int consulta = Id_pais(conexion);

                        using (var cmd = new OleDbCommand("UPDATE productos SET nombre = @nombre, descripcion = @descripcion, " +
                            "precio_compra= @precio_compra,precio_venta=@precio_venta, Idpais=@Idpais WHERE Idroducto = @Idroducto", conexion))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombre.Text);
                            cmd.Parameters.AddWithValue("@descripcion", descripcion.Text);
                            cmd.Parameters.AddWithValue("@precio_compra", Convert.ToDouble(precio_compra.Text));
                            cmd.Parameters.AddWithValue("@precio_venta", Convert.ToDouble(precio_venta.Text));
                            cmd.Parameters.AddWithValue("@Idpais", consulta);
                            cmd.Parameters.AddWithValue("@Idroducto", filas.ItemArray.GetValue(1));

                            cmd.ExecuteNonQuery();
                        }
                        cn.Cerrar_conexion(conexion);
                        MessageBox.Show("Registro Modificado!");
                        Cargar_datos();
                    }
                    else
                        MessageBox.Show("Revise los datos ingresados, debe ingresar un numero!");
                }
                else MessageBox.Show("No es un dato de origen válido");
            }
            else MessageBox.Show("Existen campos vacios");
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!nombre.Text.Equals("") && !descripcion.Text.Equals("") && !precio_compra.Text.Equals("") &&
                !precio_venta.Text.Equals("") && !origen.Text.Equals(""))
            {
                Conexion cn = new Conexion();
                OleDbConnection conexion = cn.Obtener_conexion();

                string a = string.Format("DELETE from productos WHERE Idroducto = {0}", filas.ItemArray.GetValue(1));
                cn.Sql_command(conexion, a);
                cn.Cerrar_conexion(conexion);
                MessageBox.Show("Registro Eliminado!");

                if (cont != 0)
                {
                    cont--;
                }
                Cargar_datos();
            }
            else MessageBox.Show("Debe seleccionar un producto");
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
           // Configure the message box to be displayed
            string messageBoxText = "  ¿Desea salir?";
            string caption = "Mensaje";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    App.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        //busca idpais segun el txtbox origen del producto
        private int Id_pais(OleDbConnection conexion)
        {
            string comando = string.Format("SELECT distinct Idpais FROM pais WHERE pais = " +
                "(SELECT pais FROM pais where pais.pais LIKE '%{0}%')", origen.Text);
            OleDbCommand command = new OleDbCommand(comando, conexion);
            int consulta = Convert.ToInt32(command.ExecuteScalar());
            return consulta;
        }
        //devuelve verdadero si el string es un numero
        public bool IsNumeric(string str)
        {
            double _val;
            bool valor = double.TryParse(str, out _val);
            return valor;
        }
    
    }
}

using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace InventoryTrackerFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlConnection connection;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void connect()
        {
            string connectionString = @"Data Source=WOOTBOOK-WINDOW\SQLEXPRESS;Initial Catalog=InventoryTracker;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            this.connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM AppUser", connection);

            this.connection.Open();
            MessageBox.Show("Connection succeeded");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.connect();

            try
            {
                string sql = "SELECT * FROM AppUser";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dr = command.ExecuteReader();

                SqlDataAdapter adapter = new SqlDataAdapter();

                //check if there are records
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //display retrieved record (first column only/string value)
                        Console.WriteLine(dr.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
                dr.Close();
                command.Dispose();
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }

            this.connection.Close();

            Console.WriteLine(this.textBoxName);
            Console.WriteLine(this.textBoxLastName);
        }
    }
}

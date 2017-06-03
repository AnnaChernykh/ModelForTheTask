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
using System.Data.OleDb;


namespace ModelForTheTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            TaskScheme Scheme = new TaskScheme(); 
            InitializeComponent();
            Scheme.Number = 1;
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\Task.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindID = myOleDbConnection.CreateCommand();
            FindID.CommandText = string.Format("SELECT Number, ID FROM Scheme WHERE Number=" + Scheme.Number);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader1 = FindID.ExecuteReader();
            myOleDbDataReader1.Read();
            Scheme.ID = Convert.ToInt32(myOleDbDataReader1["ID"]);
            myOleDbDataReader1.Close();
            myOleDbConnection.Close();
            MessageBox.Show(Scheme.Number + " " + Scheme.ID);
        }
    }
}

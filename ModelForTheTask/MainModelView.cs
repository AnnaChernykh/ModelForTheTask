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

    public class ComposeTask
    {
        public void Compose()
        {

            TaskScheme Scheme = new TaskScheme();
            Scheme.Number = 1;
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\Task.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindID = myOleDbConnection.CreateCommand();
            FindID.CommandText = string.Format("SELECT Number, ID FROM Scheme WHERE Number=" + Scheme.Number);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader = FindID.ExecuteReader();
            myOleDbDataReader.Read();
            Scheme.ID = Convert.ToInt32(myOleDbDataReader["ID"]);
            myOleDbDataReader.Close();

            TaskParametrs Param = new TaskParametrs();
            Param.Number = 2;
            OleDbCommand FindParam = myOleDbConnection.CreateCommand();
            FindParam.CommandText = string.Format("SELECT Number, GPower FROM Param WHERE Number=" + Param.Number);
            OleDbDataReader myOleDbDataReader1 = FindParam.ExecuteReader();
            myOleDbDataReader1.Read();
            TaskParametrs.Generator.GPower = Convert.ToInt32(myOleDbDataReader1["GPower"]);
            myOleDbDataReader1.Close();
            myOleDbConnection.Close();
            MessageBox.Show(Scheme.Number + " " + Scheme.ID + "\n" + Param.Number + " " + TaskParametrs.Generator.GPower);
        }
    }
}




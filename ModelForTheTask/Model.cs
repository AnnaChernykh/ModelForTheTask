using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;

namespace ModelForTheTask
{
    public class Scheme
    {
        public static int Number;
        public static int ID;


        public int SchemeNumber
        {
            get { return Number; }
            set { Number = value; }
        }
        public int SchemeID
        {
            get { return ID; }
            set { ID = value; }
        }
        public void SchemeCompose(int SchemeNumber)
        {
            //Model Scheme = new Model();
            //SchemeNumber = 1;
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\Task.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindID = myOleDbConnection.CreateCommand();
            FindID.CommandText = string.Format("SELECT Number, ID FROM Scheme WHERE Number=" + SchemeNumber);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader = FindID.ExecuteReader();
            myOleDbDataReader.Read();
            SchemeID = Convert.ToInt32(myOleDbDataReader["ID"]);
            myOleDbDataReader.Close();
            myOleDbConnection.Close();

           
        }
    }

    public class TaskParametrs
    {
        
        public int Number;
        public int Power;
        public double CosF;
        public double Voltage;
        public double SupertrancRes;
        public double Res;
        public double TimeConst;

        public int ParamNumber
        {
            get { return Number; }
            set { Number = value; }
        }
        
        public int GPower
        {
             get { return Power; }
             set { Power = value; }
        }
        public double GCosF
        {
             get { return CosF; }
             set { CosF = value; }
        }
        public double GVoltage
        {
             get { return Voltage; }
             set { Voltage = value; }
        }
        public double GSupertrancRes
        {
            get { return SupertrancRes; }
            set { SupertrancRes = value; }
        }
        public double GRes
        {
            get { return Res; }
            set { Res = value; }
        }
        public double GTimeConst
        {
            get { return TimeConst; }
            set { TimeConst = value; }
        }

        public void FindGenerator(int ParamNumber)
        {
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\Task.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindParam = myOleDbConnection.CreateCommand();
            FindParam.CommandText = string.Format("SELECT * FROM ParamGen WHERE Number=" + ParamNumber);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader1 = FindParam.ExecuteReader();
            myOleDbDataReader1.Read();
            GPower = Convert.ToInt32(myOleDbDataReader1["NomPower"]);
            GCosF= Convert.ToDouble(myOleDbDataReader1["cosjnom"]);
            GVoltage= Convert.ToDouble(myOleDbDataReader1["Unom"]);
            GSupertrancRes= Convert.ToDouble(myOleDbDataReader1["Xd"]);
            GRes= Convert.ToDouble(myOleDbDataReader1["X2"]);
            GTimeConst= Convert.ToDouble(myOleDbDataReader1["Ta"]);
            myOleDbDataReader1.Close();
            myOleDbConnection.Close();
            
        }
    }  
    public class Test
    {
        public void TestTask()
        { 
             Scheme Scheme = new Scheme();
             Scheme.SchemeNumber = 1;
             Scheme.SchemeCompose(1);
             TaskParametrs Generator = new TaskParametrs();
             Generator.Number = 2;
             Generator.FindGenerator(2);

            List<double> Gen = new List<double>();
            Gen.AddRange(new double[] { Scheme.SchemeID, Generator.GPower, Generator.GVoltage, Generator.GCosF, Generator.GSupertrancRes, Generator.GRes, Generator.GTimeConst});
            MessageBox.Show(Gen[0] + "\n"+Gen[1]+ "\n" + Gen[2]+ "\n" + Gen[3]+ "\n" + Gen[4] + "\n" + Gen[5] + "\n" + Gen[6]);
    }     
  }}


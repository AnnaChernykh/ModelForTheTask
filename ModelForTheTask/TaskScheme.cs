using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;

namespace ModelForTheTask
{
    public class TaskScheme 
    {
        public static int SchemeNumber;
        public static int SchemeID;

       
        public int Number
        {
            get { return  SchemeNumber; }
            set { SchemeNumber=value; }
        }
        public int ID
        {
            get { return SchemeID; }
            set { SchemeID=value; }
        }
     }

    public class TaskParametrs
    {
        public int ParamNumber;
        public int Number
        {
            get { return ParamNumber; }
            set { ParamNumber = value; }
        }
        Generator Gen = new Generator();
        public class Generator
        {
            public static int GPower;
            public static double GCosF;
            public static double GVoltage;
            public int Power
            {
                get { return GPower; }
                set { GPower = value; }
            }
            public double CosF
            {
                get { return GCosF; }
                set { GCosF = value; }
            }
            public double Voltage
            {
                get { return GVoltage; }
                set { GVoltage = value; }
            }
        }
        
       

    }
}

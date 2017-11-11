using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;

namespace ModelForTheTask
{
    
    public class Element
    {

        public ElementType Type { get; set; } = ElementType.Unknown;
        public string Name;
        public string TypeM;

        // словарик для хранения параметров элемента
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        // перечисление, что бы знать какой тип у элемента
        public enum ElementType
        {
            Unknown, // неопределенный
            G, // генератор
            T, // трансформатор
            W, // линия
            LR // реактор
        }
    }

    // и отдельный класс для задания
    public class Task
    {
        // со списком элементов
        public List<Element> Elements { get; set; } = new List<Element>();

        
    }

    public static class DataBaseService
    {
        

        public static Task ReadElement(int ParamNumber, Element.ElementType type, string tablename)
        {
            Task task = new Task();
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\ExamTask.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindElement = myOleDbConnection.CreateCommand();
            FindElement.CommandText = string.Format("SELECT * FROM "+ tablename +" WHERE Number=" + ParamNumber);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader1 = FindElement.ExecuteReader();
            myOleDbDataReader1.Read();
            string fieldname;
            for (int i = 1; i < myOleDbDataReader1.FieldCount; i++)
            {
                fieldname = myOleDbDataReader1.GetName(i);
                if (myOleDbDataReader1[fieldname].ToString() != "")
                {
                    Element element = new Element();
                    element.Type = type;
                    element.Name = fieldname;
                    element.TypeM = myOleDbDataReader1[fieldname].ToString();
                    task.Elements.Add(element);
                }

            }
            foreach (Element el in task.Elements)
            {
                myOleDbDataReader1.Close();
                OleDbCommand FindParam = myOleDbConnection.CreateCommand();
                FindParam.CommandText = string.Format("SELECT * FROM "+type+" WHERE ID='" + el.TypeM+"'");
                OleDbDataReader myOleDbDataReader2 = FindParam.ExecuteReader();
                myOleDbDataReader2.Read();
                for (int i = 1; i < myOleDbDataReader2.FieldCount; i++)
                {
                    fieldname = myOleDbDataReader2.GetName(i);
                    if (myOleDbDataReader2[fieldname].ToString() != "")
                    {
                        el.Parameters.Add(fieldname, myOleDbDataReader2[fieldname]);
                    }
                }
                myOleDbDataReader2.Close();
            }

            myOleDbConnection.Close();

            return task;
        }
    }
    public class Test
    {
        public void TestTask()
        {
            Task task = new Task();
            Task taskG = new Task();
            Task taskT = new Task();

            int ParamVariant = 12; //для каждой схемы 5 вариантов исходных параментров элементов 

            taskG = DataBaseService.ReadElement(ParamVariant, Element.ElementType.G, "VarGen");
            taskT= DataBaseService.ReadElement(ParamVariant, Element.ElementType.T, "VarTR");
            

            string s = "";

            foreach (Element el in taskT.Elements)
            {
                s += el.Type + ", ";
                s += el.Name + ", ";
                s += el.TypeM + ",";
                foreach (KeyValuePair<string, object> kvp in el.Parameters)
                {
                    s += kvp.Key + " = " + kvp.Value.ToString() + ",";
                }
                s +=  "\n";
            }

            MessageBox.Show(s);


        }
    }}


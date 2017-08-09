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
        public static Element ReadElement(int ParamNumber, Element.ElementType type, string tablename)
        {            
            Element element = new Element();
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Environment.CurrentDirectory + "\\Task.mdb";
            OleDbConnection myOleDbConnection = new OleDbConnection(connectionString);
            OleDbCommand FindParam = myOleDbConnection.CreateCommand();
            FindParam.CommandText = string.Format("SELECT * FROM "+tablename+" WHERE Number=" + ParamNumber);
            myOleDbConnection.Open();
            OleDbDataReader myOleDbDataReader1 = FindParam.ExecuteReader();
            myOleDbDataReader1.Read();
            string fieldname;
            for (int i = 0; i < myOleDbDataReader1.FieldCount; i++)
            {
                fieldname = myOleDbDataReader1.GetName(i);
                element.Parameters.Add(fieldname, myOleDbDataReader1[fieldname]);
            }

            myOleDbDataReader1.Close();
            myOleDbConnection.Close();
            return element;
        }
    }  
    public class Test
    {
        public void TestTask()
        {
            Task task = new Task();
            
            task.Elements.Add(DataBaseService.ReadElement(1,Element.ElementType.Unknown,"Scheme"));
            task.Elements.Add(DataBaseService.ReadElement(2, Element.ElementType.G, "ParamGen"));

            string s="";
            
            foreach (Element el in task.Elements)
            {
                s += el.Type+"\n";      
                foreach (KeyValuePair<string, object> kvp in el.Parameters)
                {
                    s += kvp.Key + " = " + kvp.Value.ToString() + "\n";
                }
            }
            MessageBox.Show(s);

           
    }     
  }}


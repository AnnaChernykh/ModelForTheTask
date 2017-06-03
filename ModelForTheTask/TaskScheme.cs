using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;

namespace ModelForTheTask
{
    public class TaskScheme : DependencyObject
    {
        public static readonly DependencyProperty SchemeNumber;
        public static readonly DependencyProperty SchemeID;

        static TaskScheme()
        {
            SchemeNumber = DependencyProperty.Register("Number", typeof(int), typeof(TaskScheme));
            SchemeID = DependencyProperty.Register("ID", typeof(int), typeof(TaskScheme));
        }
        public int Number
        {
            get { return (int)GetValue(SchemeNumber); }
            set { SetValue(SchemeNumber, value); }
        }
        public int ID
        {
            get { return (int)GetValue(SchemeID); }
            set { SetValue(SchemeID, value); }
        }
     }

    public class TaskParametrs:DependencyObject
    {
        public static readonly DependencyProperty Generator;
        public static readonly DependencyProperty Transformer;
    }
}

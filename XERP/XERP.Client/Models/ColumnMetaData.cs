using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XERP.Client.Models
{//pasting in to a grid from a spread sheet is column fixed...
    //this meta data class will allow the view model to understand the column order
    //and interpret the Clipbaord data assign to the appropriate strongly typed List property value...
    public class ColumnMetaData
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _order;
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
    TODO:
        - make seting the data robust
*/

namespace StatsAppUI.Table
{
    internal class DataItem
    {
        /* This class holds a single piece of data that occupies one cell in a
           data table. 
           
           The DataItem can have an 'empty' state, whereby there is not data contained. It is IMPORTANT to recognise
           that null will be returned is m_isEmpty = true. This must be accounted for, otherwise a null reference 
           exception will be thrown. */
        private object  m_data;
        private bool    m_isEmpty;


        public object Data { get { return m_data; } set { this.SetData(value); } }

        public DataItem(object data)
        {
            /* Create a new DataItem. param data is the piece of data. If null, the DataItem is set
               to the 'empty' state. */
            this.SetData(data);
        }

        private void SetData(object data)
        {
            /* Change the data in m_data. If null, the DataItem is set to the 'empty' state. */
            m_data = data;
            m_isEmpty = (m_data == null) ? true : false;
        }

        public static DataItem Parse(object input)
        {
            return new DataItem(input);
        }
    }
}

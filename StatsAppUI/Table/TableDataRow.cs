using System.Collections.Generic;
using System;

namespace StatsAppUI.Table
{
    internal class TableDataRow
    {
        /* This class holds many DataItems on the same row of a table.
        
           The DataRow can have an 'empty' state, whereby there is not data contained. IMPORTANT, if 
           the m_items index the user is accessing through the Items property is empty, it will be null.
           
           Also, do not use foreach as a null reference exception will be thrown if the state is empty.
           This will be fixed in the future. */
        private List<DataItem>  m_items;
        private bool            m_isEmpty;


        public TableDataRow()
        {
            /* Create an new DataRow with the empty state set to true. */
            m_items = new List<DataItem>();
            m_isEmpty = true;
        }

        public TableDataRow(params DataItem[] items)
        {
            m_items = new List<DataItem>();
            m_items.AddRange(items);
            m_isEmpty = false;
        }

        public void Add(DataItem item)
        {
            m_items.Add(item);
            if (m_isEmpty)
                m_isEmpty = false;
        }

        public void Add(params DataItem[] items)
        {
            m_items.AddRange(items);
            if (m_isEmpty)
                m_isEmpty = false;
        }

        public void Clear()
        {
            /* Remove all items from the data row, and set the state to empty. */
            m_items.Clear();
            m_isEmpty = true;
        }

        public object[] ToItemArray()
        {
            /* Return a single-dimensional array of data from the DataItems in the DataRow. */
            object[] items = new object[m_items.Count];
            for (int i = 0; i < m_items.Count; i++)
            {
                items[i] = m_items[i].Data;
            }
            return items;
        }

        public List<DataItem> Items { get { return m_items; } }
        public bool Empty { get { return m_isEmpty; } }

        public static int[] ItemArrayAsInt(object[] arr)
        {
            int[] casted = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                casted[i] = (int)arr[i];
            }
            return casted;
        }

        public static object[] JoinItemArrays(object[] arr1, object[] arr2)
        {
            /* Concatenate 2 ItemArrays. */
            object[] joined = new object[arr1.Length + arr2.Length];
            arr1.CopyTo(joined, 0);
            arr2.CopyTo(joined, arr1.Length);
            return joined;
        }
    }
}

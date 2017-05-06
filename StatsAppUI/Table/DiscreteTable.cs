using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace StatsAppUI.Table
{
    internal class DiscreteTable : BaseTable
    {
        /* This table can hold discrete pieces of data. It contains two columns, the first one is the label
           which describes the piece of data, the second column holds the piece of data. */
        public DiscreteTable(string name, Control.ControlCollection parentControls, DockStyle dockStyle)
            : base(name, parentControls, dockStyle)
        {
            AddColumns("Label", "Data");
        }

        public void SetLabelColumn(string columnName)
        {
            UpdateColumn(Columns[0].HeaderText, columnName);
        }
        public void SetDataColumn(string columnName)
        {
            UpdateColumn(Columns[1].HeaderText, columnName);
        }

        public override TableDataRow[] GetData()
        {
            /* Return an array of DataRows from the discrete table. If there is an error when getting the data from a row, 
               the user is asked whether they want to continue without that row in the data. */
            List<TableDataRow> drlist = new List<TableDataRow>();
            TableDataRow tempdr = new TableDataRow();

            for (int i = 0; i < base.RowCount; i++)
            {
                tempdr = base.GetEntireRow(i, true);
                if (tempdr == null)
                {
                    DialogResult result = MessageBox.Show("There is an error in the data of row " + (i + 1) + ". Would you like to continue?",
                                                          "Invalid Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No)
                        // user does not want to continue    
                        return null;

                    if (result == DialogResult.Yes)
                    {
                        // user does want to continue so add an empty data row
                        drlist.Add(new TableDataRow());
                        continue;
                    }
                }

                drlist.Add(tempdr);
            }

            return drlist.ToArray();
        }
    }
}

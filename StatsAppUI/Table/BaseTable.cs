using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace StatsAppUI.Table
{
    internal class BaseTable
    {
        /* This is the base class for all tables displayed on the ui, e.g. contingency tables. */
        protected  DataGridView                m_tableView;


        public BaseTable(string name, Control.ControlCollection parentControls, DockStyle dockStyle)
        {
            /* Create a DataGridView control. */
            m_tableView = new DataGridView()
            {
                Dock = dockStyle,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Name = name,
                MultiSelect = false
            };

            parentControls.Add(m_tableView);

            m_tableView.CellMouseClick += new DataGridViewCellMouseEventHandler(this.HandleCellMouseClick);

            m_tableView.Hide();
        }

        public void Show() { m_tableView.Show(); }
        public void Hide() { m_tableView.Hide(); }

        public void AddColumns(params string[] headers)
        {
            /* Add multiple columns to the DataGridViewControl. */
            List<DataGridViewTextBoxColumn> cols = new List<DataGridViewTextBoxColumn>();
            foreach (string header in headers)
            {
                cols.Add(
                    new DataGridViewTextBoxColumn()
                    {
                        HeaderText = header,
                        Name = header
                    }
                );
            }
            m_tableView.Columns.AddRange(cols.ToArray());
        }
        public void AddColumn(string header)
        {
            /* Add a single column to the DataGridView control. */
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn()
            {
                HeaderText = header,
                Name = header
            };
            m_tableView.Columns.Add(col);
        }
        protected void UpdateColumn(string columnName, string newHeader)
        {
            /* Update the column of a given column name. Note, this removes all data from that column, so should be called at 
               the start of the program. */
            int index = m_tableView.Columns[columnName].Index;
            m_tableView.Columns.RemoveAt(index);
            m_tableView.Columns.Insert(
                    index,
                    new DataGridViewColumn()
                    {
                        HeaderText = newHeader,
                        Name = newHeader,
                        CellTemplate = new DataGridViewTextBoxCell()
                    }
            );
        }

        public virtual TableDataRow[] GetData()
        {
            /* When implemented, this method allows the user to retrieve the data entered into the table. 
               When not implemented, null is returned. */
            return null;
        }

        protected TableDataRow GetEntireRow(int rowIndex, bool skipFirstColumn = false)
        {
            /* Return a row data, with each column being an element in the returned array. Return null, 
               if there is a problem with what is being parsed. The list of problems that can arise are:
                   - no data found in a cell. */
            if (!ValidateRow(rowIndex, skipFirstColumn))
                return null;

            TableDataRow dr = new TableDataRow();
            for (int i = (skipFirstColumn) ? 1 : 0; i < m_tableView.Rows[rowIndex].Cells.Count; i++)
            {
                // start at 1 to skip the first column as it is the label column, so any data is not relevant
                if (CanParseToInt(m_tableView.Rows[rowIndex].Cells[i].Value.ToString()))
                    // add as int if it can
                    dr.Add(DataItem.Parse(Int32.Parse(m_tableView.Rows[rowIndex].Cells[i].Value.ToString())));
                else
                    // if all other data types fail, add as string
                    dr.Add(DataItem.Parse(m_tableView.Rows[rowIndex].Cells[i].Value.ToString()));
            }

            return dr;
        }

        protected TableDataRow GetEntireColumn(int colIndex)
        {
            /* Return the data of from an entire column. Return null if there is a problem with what is being parsed.
              The lsit of problems that can arise are:
                - no data found in a cell. */
            if (!ValidateColumn(colIndex))
                return null;

            TableDataRow data = new TableDataRow();
            for (int i = 0; i < this.RowCount; i++)
            {
                data.Add(DataItem.Parse(m_tableView.Rows[i].Cells[colIndex]));
            }

            return data;
        }

        private bool ValidateColumn(int index)
        {
            /* Ensure all data inputted on a given column is complete and in the correct format. Return true if it is,
               return false if it is not. 
               param index: the index of the column to be validated. */
            for (int i = 0; i < this.RowCount; i++)
            {
                if (!CellContainsData(m_tableView.Rows[i].Cells[index]))
                    return false;
            }
            return true;
        }

        private bool ValidateRow(int index, bool skipFirstColumn = false)
        {
            /* Ensure all data inputted on a given row is complete and in the correct format. Return true if it is,
               return false if it is not.*/
            for (int i = (skipFirstColumn) ? 1 : 0; i < m_tableView.Rows[index].Cells.Count; i++)
            {
                if (!this.RowFull(m_tableView.Rows[index]))
                {
                    MessageBox.Show("Data entered in row " + (index + 1) + " is not valid", "Invalid Row", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return false;
                }
            }

            return true;
        }

        public int Width { get { return m_tableView.Width; } set { m_tableView.Width = value; } }
        public int Height { get { return m_tableView.Height; } set { m_tableView.Height = value; } }
        public int X { get { return m_tableView.Location.X; } set { m_tableView.Location = new Point(value, m_tableView.Location.Y); } }
        public int Y { get { return m_tableView.Location.Y; } set { m_tableView.Location = new Point(m_tableView.Location.Y, value); } }
        public int RowCount
        {
            /* Get the number of rows in the DataGridViewControl. Note, the empty bottom row is not included
               in the amount. */
            get
            {
                return m_tableView.Rows.Count - 1;
            }
        }
        public int ColumnCount
        {
            /* Get the number of columns in the DataGridViewControl. */
            get
            {
                return m_tableView.ColumnCount;
            }
        }
        protected DataGridViewColumnCollection Columns { get { return m_tableView.Columns; } }

        private void HandleCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /* Add a blank row to the bottom of the table when a cell on the current bottom row is clicked. 
               However, only add a row if there is data in the row above the clicked row. */
            if (e.Button == MouseButtons.Left)
            {
                // validation is not needed as mutli select is turned off, and since this method 
                // is called when a cell is clicked, there is always a selected cell.
                DataGridViewRow selectedRow = m_tableView.Rows[e.RowIndex];

                if (selectedRow.Index == 0 && m_tableView.Rows.Count == 1)
                {
                    this.AppendRow();
                    return;
                }
                else if (selectedRow.Index == 0)
                {
                    // stop method to stop index out of bounds exception when checking if the row
                    // above contains any data.
                    return;
                }

                // check if row above selected row contains data
                if (this.RowContainsData(m_tableView.Rows[e.RowIndex - 1]) && (selectedRow.Index + 1) == m_tableView.Rows.Count)
                {
                    this.AppendRow();
                    return;
                }
            }
        }

        public bool CellContainsData(DataGridViewCell cell)
        {
            /* Return true if a cell contains data, return false if it doesn't. */
            if (cell.Value == null)
                return false;

            if (cell.Value != null)
            {
                if (String.IsNullOrWhiteSpace(cell.Value.ToString()) && String.IsNullOrEmpty(cell.Value.ToString()))
                    return false;
            }

            return true;
        }

        public bool RowContainsData(DataGridViewRow row)
        {
            /* Return true if any of a row's cells contains some sort of data. */
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value != null)
                {
                    if (!String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) && !String.IsNullOrEmpty(row.Cells[i].Value.ToString()))
                        return true;
                }
            }
            return false;
        }

        public bool RowFull(DataGridViewRow row)
        {
            /* Return true if all of a row's cells contains data. Return false if at least
               one cell is empty. */
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value == null)
                {
                    return false;
                }

                if (row.Cells[i].Value != null)
                {
                    if (String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) && String.IsNullOrEmpty(row.Cells[i].Value.ToString()))
                        return false;
                }
            }
            return true;
        }

        private bool CanParseToInt(string input)
        {
            try
            {
                Int32.Parse(input);
            }
            catch
            {
                return false;
            }
            return true;
        }

        protected void AppendRow()
        {
            /* Add a blank row to the bottom of the table. */
            m_tableView.Rows.Add();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using StatsAppUI.Table;
using StatsAppUI.Controls;

namespace StatsAppUI
{
    public partial class MainForm : Form
    {
        private DiscreteTable discreteTable;

        private TreeViewHandle  m_treeViewHandle;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            discreteTable = new DiscreteTable("Test Discrete Table", this.Controls, DockStyle.Left);
            discreteTable.SetLabelColumn("Label Column");
            discreteTable.SetDataColumn("Data Column");
            discreteTable.Show();

            LoadTreeView();
        }

        private void LoadTreeView()
        {
            m_treeViewHandle = new TreeViewHandle(this.Width - 300, 0, 300, this.Height);
            m_treeViewHandle.AddToControls(this.Controls);

            m_treeViewHandle.AddRootNode("basic", "Basic Stats");
            m_treeViewHandle.AddNode("basic", "mean", "Mean: ");
            m_treeViewHandle.AddNode("basic", "median", "Median: ");
            m_treeViewHandle.AddNode("basic", "mode", "Mode: ");
            m_treeViewHandle.AddNode("basic", "range", "Range");
            m_treeViewHandle.AddNode("basic", "lower_quartile", "Q1");
            m_treeViewHandle.AddNode("basic", "upper_quartile", "Q3");
            m_treeViewHandle.AddNode("basic", "interquartile_range", "IQR");
        }

        private void evaluateDiscreteTableButton_Click(object sender, EventArgs e)
        {
            //TableDataRow[] data = discreteTable.GetData();
            TestDiscreteTableGetData();
        }

        #region Tests
        private void TestDiscreteTableGetData()
        {
            TableDataRow[] data = discreteTable.GetData();

            if (data == null)
                return;

            Console.WriteLine("---- Begin Discrete Table GetData() Test ----\n");
            Console.WriteLine("Data Row Count: " + data.Length + "\n");

            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Row : " + (i + 1) + " | Empty : " + data[i].Empty);
                for (int j = 0; j < data[i].Items.Count; j++)
                {
                    if (data[i].Items[j] != null)
                    {
                        Console.WriteLine("\tData : " + data[i].Items[j].Data.ToString());
                        Console.WriteLine("\tType : " + data[i].Items[j].Data.GetType());
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
            }

            // put data from each caloumn into separate item arrays
            object[] itemarr = new object[0];
            object[] tempitemarr;
            for (int i = 0; i < data.Length; i++)
            {
                tempitemarr = data[i].ToItemArray();
                itemarr = TableDataRow.JoinItemArrays(itemarr, tempitemarr);
            }
            int[] arr = TableDataRow.ItemArrayAsInt(itemarr);

            Console.WriteLine("Int32 Array Length: " + arr.Length);
            Console.WriteLine("Mean: " + CoreImports.Mean(arr, (UInt32)arr.Length));
            Console.WriteLine("Median: " + CoreImports.Median(arr, (UInt32)arr.Length));
            Console.WriteLine("Mode: " + CoreImports.Mode(arr, (UInt32)arr.Length));
            Console.WriteLine("Range: " + CoreImports.Range(arr, (UInt32)arr.Length));
            Console.WriteLine("Upper Quartile: " + CoreImports.UpperQuartile(arr, (UInt32)arr.Length));
            Console.WriteLine("Lower Quartile: " + CoreImports.LowerQuartile(arr, (UInt32)arr.Length));

            m_treeViewHandle.UpdateNodeText("basic", "mean", "Mean: " + CoreImports.Mean(arr, (UInt32)arr.Length).ToString());
            m_treeViewHandle.UpdateNodeText("basic", "median", "Median: " + CoreImports.Median(arr, (UInt32)arr.Length).ToString());
            m_treeViewHandle.UpdateNodeText("basic", "mode", "Mode: " + CoreImports.Mode(arr, (UInt32)arr.Length).ToString());
            m_treeViewHandle.UpdateNodeText("basic", "range", "Range: " + CoreImports.Range(arr, (UInt32)arr.Length).ToString());
            m_treeViewHandle.UpdateNodeText("basic", "lower_quartile", "Q1: " + CoreImports.LowerQuartile(arr, (UInt32)arr.Length).ToString());
            m_treeViewHandle.UpdateNodeText("basic", "upper_quartile", "Q3: " + CoreImports.UpperQuartile(arr, (UInt32)arr.Length).ToString());

            Console.WriteLine("---- End Discrete Table GetData Test ----");
        }
        #endregion
    }
}

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace StatsAppUI.Controls
{
    class TreeViewHandle
    {
        /* This is an abstraction layer to the treeview control.*/
        private TreeView m_treeView;

        public void GetChildren(TreeNode root, ref List<TreeNode> children)
        {
            /* Recursively adds all child nodes of the root node. Note, the root node
            is added as the first element in the list. */
            children.Add(root);
            foreach (TreeNode n in root.Nodes)
                GetChildren(n, ref children);
        }

        public void SetAfterSelectHandler(Action<TreeNode> handler)
        {
            /* AfterSelect event is called when the user releases a mouse click. */
            m_treeView.AfterSelect += (sender, EventArgs) => { handler(EventArgs.Node); };
        }

        public void AddRootNode(string key, string text)
        {
            if (m_treeView.Nodes.ContainsKey(key))
                throw new Exception("Duplicate root node.");
            m_treeView.Nodes.Add(key, text);
        }

        public void AddNode(string parentKey, string key, string text)
        {
            /* Add a child node to a parent node. */
            TreeNode[] nodes = m_treeView.Nodes.Find(parentKey, true);
            if (nodes.Length > 0)
            {
                nodes[0].Nodes.Add(key, text);
            }
            else
                throw new Exception("No parent key - " + parentKey + " - found.");
        }

        public void UpdateNodeText(string rootKey, string key, string newText)
        {
            TreeNode[] nodes = m_treeView.Nodes.Find(rootKey, true);
            if (nodes.Length > 0)
                nodes[0].Nodes[key].Text = newText;
        }

        public void AddToControls(Control.ControlCollection controls)
        {
            /* Add to a parent control. */
            controls.Add(m_treeView);
        }

        public TreeViewHandle(int x, int y, int width, int height)
        {
            m_treeView = new TreeView();
            m_treeView.Location = new Point(x, y);
            m_treeView.Size = new Size(width, height);
        }
    }
}

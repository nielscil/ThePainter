using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest
{
    public class PainterTreeView : TreeView
    {

        public delegate void SelectionChangedEventHandler(object sender, EventArgs e);
        private event SelectionChangedEventHandler _selectionChanged;
        public event SelectionChangedEventHandler SelectionChanged
        {
            add
            {
                _selectionChanged += value;
            }
            remove
            {
                _selectionChanged -= value;
            }
        }

        private bool IsCtrlShift
        {
            get
            {
                Keys key = Control.ModifierKeys;
                return key == Keys.Control || key == Keys.Shift;
            }
        }

        public List<PainterTreeNode> SelectedItems { get; private set; } = new List<PainterTreeNode>();
        public List<PainterTreeNode> Items
        {
            get
            {
                List<PainterTreeNode> list = new List<PainterTreeNode>();

                foreach(var item in Nodes)
                {
                    if(item is PainterTreeNode)
                    {
                        list.Add(item as PainterTreeNode);
                    }
                }

                return list;
            }
        }

        public Color SelectionColor { get; set; } = Color.Blue;

        public PainterTreeView()
        {
            SetEvents();
        }

        private void SetEvents()
        {
            BeforeSelect += PainterTreeView_BeforeSelect;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
            {
                ReleaseEvents();
            }
        }

        private void OnSelectionChanged()
        {
            _selectionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ReleaseEvents()
        {
            BeforeSelect -= PainterTreeView_BeforeSelect;
        }

        private void PainterTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {

            if(e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if(!IsCtrlShift)
                {
                    RemoveSelection();
                }

                AddToSelection(e.Node);
                OnSelectionChanged();
            }
        }

        private void RemoveSelection()
        {
            foreach(var item in SelectedItems)
            {
                item.BackColor = DefaultBackColor;
            }
            SelectedItems.Clear();
        }

        private void AddToSelection(TreeNode node)
        {
            if (node is PainterTreeNode)
            {
                SelectedItems.Add(node as PainterTreeNode);
                node.BackColor = SelectionColor;
            }
        }
    }
}

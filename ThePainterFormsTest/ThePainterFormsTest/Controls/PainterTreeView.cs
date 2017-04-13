using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest.Controls
{
    /// <summary>
    /// Custom treeview for multiselect
    /// </summary>
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

        public ObservableCollection<PainterTreeNode> SelectedItems { get; private set; } = new ObservableCollection<PainterTreeNode>();
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

        public Color SelectionColor { get; set; } = Color.FromArgb(51,153,255);
        public Color SelectionForeColor { get; set; } = Color.White;

        /// <summary>
        /// Constructor
        /// </summary>
        public PainterTreeView()
        {
            SetEvents();
        }

        /// <summary>
        /// Sets the needed event handlers
        /// </summary>
        private void SetEvents()
        {
            BeforeSelect += PainterTreeView_BeforeSelect;
            AfterSelect += PainterTreeView_AfterSelect;
            SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
        }

        /// <summary>
        /// Dispose override for removing the event handlers
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
            {
                ReleaseEvents();
            }
        }

        /// <summary>
        /// On selection changed event trigger
        /// </summary>
        private void OnSelectionChanged()
        {
            _selectionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Releases event handlers
        /// </summary>
        private void ReleaseEvents()
        {
            BeforeSelect -= PainterTreeView_BeforeSelect;
            AfterSelect -= PainterTreeView_AfterSelect;
            SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;
        }

        private bool _usingSelect = false;

        /// <summary>
        /// Handles before select event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PainterTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            _usingSelect = true;
            if(e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            {
                if(!IsCtrlShift)
                {
                    RemoveSelection();
                }

                if(IsInSelection(e.Node))
                {
                    RemoveFromSelection(e.Node);
                }
                else
                {
                    AddToSelection(e.Node);
                }
                
                OnSelectionChanged();
            }
            _usingSelect = false;
        }

        /// <summary>
        /// Handles after select event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PainterTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = null;
        }

        /// <summary>
        /// Handles on collection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(!_usingSelect)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var item in e.NewItems)
                    {
                        SetSelectedColor(item as TreeNode);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var item in e.OldItems)
                    {
                        SetNormalColor(item as TreeNode);
                    }
                }
            }
        }

        /// <summary>
        /// Remove selection
        /// </summary>
        private void RemoveSelection()
        {
            foreach(var item in SelectedItems)
            {
                item.BackColor = DefaultBackColor;
                item.ForeColor = DefaultForeColor;
            }
            SelectedItems.Clear();
        }

        /// <summary>
        /// check if node is in selection
        /// </summary>
        /// <param name="node">node</param>
        /// <returns></returns>
        private bool IsInSelection(TreeNode node)
        {
            return node is PainterTreeNode && SelectedItems.Contains(node as PainterTreeNode);
        }

        /// <summary>
        /// Adds node to selection
        /// </summary>
        /// <param name="node">node</param>
        private void AddToSelection(TreeNode node)
        {
            if (node is PainterTreeNode)
            {
                SelectedItems.Add(node as PainterTreeNode);
                SetSelectedColor(node);
            }
        }

        /// <summary>
        /// Set selection color
        /// </summary>
        /// <param name="node"></param>
        private void SetSelectedColor(TreeNode node)
        {
            node.BackColor = SelectionColor;
            node.ForeColor = SelectionForeColor;
        }

        /// <summary>
        /// Removes node form selection
        /// </summary>
        /// <param name="node">node</param>
        private void RemoveFromSelection(TreeNode node)
        {
            if(node is PainterTreeNode)
            {
                SelectedItems.Remove(node as PainterTreeNode);
                SetNormalColor(node);
            }
        }

        /// <summary>
        /// Set colors of node to normal colors
        /// </summary>
        /// <param name="node">node</param>
        private void SetNormalColor(TreeNode node)
        {
            node.BackColor = DefaultBackColor;
            node.ForeColor = DefaultForeColor;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest
{
    /// <summary>
    /// Extention class for treeNodecollection
    /// </summary>
    public static class TreeNodeCollectionExtension
    {
        /// <summary>
        /// Checks if node is in collection
        /// </summary>
        /// <param name="coll">treenode collection</param>
        /// <param name="node">node</param>
        /// <returns></returns>
        public static bool IsInCollection(this TreeNodeCollection coll,TreeNode node)
        {
            foreach(TreeNode item in coll)
            {
                if (item == node)
                {
                    return true;
                }
                    
                if(item.Nodes.Count > 0)
                {
                    bool hasNode = item.Nodes.IsInCollection(node);

                    if (hasNode)
                    {
                        return true;
                    }  
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest
{
    public static class TreeNodeCollectionExtension
    {
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

using fCommon.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ffwebAdminUI
{
    public class TreeBuilder
    { 

        #region "Settings tree"
        //public void PopulateTreeView(TreeView tree, List<SettingsGroup> settingsGroup)
        //{
        //    try
        //    {
        //        Item first = new Item
        //        {
        //            ItemID = 0,
        //            Text = "Settings",
        //            Payload = null
        //        };

        //        List<Item> i = (from s in settingsGroup
        //                        select new Item
        //                        {
        //                            ItemID = s.Id,
        //                            ParentID = s.Parent,
        //                            Text = s.GroupName,
        //                            Payload = s.Settings
        //                        }).ToList();

        //        List<Item> all = new List<Item>();
        //        all.AddRange(i);

        //        PopulateTreeViewEnumerable(tree, all);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.ShowError(ex);
        //    }
        //}
        #endregion "Settings tree"

        #region "generic tree"
        public void PopulateTreeViewEnumerable(TreeView tree, IEnumerable<Item> items)
        {
            try
            {
                Dictionary<int, Tuple<Item, TreeNode>> allNodes = new Dictionary<int, Tuple<Item, TreeNode>>();

                foreach (var item in items)
                {
                    var node = CreateTreeNode(item);
                    allNodes.Add(item.ItemID, Tuple.New(item, node));
                }

                foreach (var kvp in allNodes)
                {
                    //kvp.Value.First.ParentID != null || 
                    if (kvp.Value.First.ParentID != 0)
                    {
                        allNodes[kvp.Value.First.ParentID].Second.Nodes.Add(kvp.Value.Second);
                    }
                    else
                    {
                        tree.Nodes.Add(kvp.Value.Second);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "generic tree"

        private TreeNode CreateTreeNode(Item item)
        {
            var node = new TreeNode();
            node.Text = item.ItemID + "-" + item.Text;
            node.Name = item.ItemID.ToString();
            return node;
        }

    }

    public class Item
    {
        public int ItemID { get; set; }
        public int ParentID { get; set; }
        public string Text { get; set; }
        public object Payload { get; set; }
    }

    public class Tuple<T>
    {
        public Tuple(T first)
        {
            First = first;
        }

        public T First { get; set; }
    }

    public class Tuple<T, T2> : Tuple<T>
    {
        public Tuple(T first, T2 second)
            : base(first)
        {
            Second = second;
        }

        public T2 Second { get; set; }
    }

    public static class Tuple
    {
        //Allows Tuple.New(1, "2") instead of new Tuple<int, string>(1, "2")
        public static Tuple<T1> New<T1>(T1 t1)
        {
            return new Tuple<T1>(t1);
        }

        public static Tuple<T1, T2> New<T1, T2>(T1 t1, T2 t2)
        {
            return new Tuple<T1, T2>(t1, t2);
        }
    }


}

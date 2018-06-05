using System.Collections.Generic;
using System.Linq;
using KS.Core.Model.ContentManagement;
using KS.Model.Base;

namespace KS.Business.Security.Base
{
    public class BaseBiz
    {


        //protected void BuildTree<T,TU>(T node, List<T> newTree, List<T> baseTree) where T:ITree<TU>
        //{
        //    var currentNode = node;

        //    do
        //    {
        //        if (!newTree.Exists(t => t.Id == currentNode.Id))
        //            newTree.Add(currentNode);
        //        else
        //            break;
        //        var previousNode = currentNode;
        //        if(currentNode.ParentId != null)
        //        currentNode = baseTree.Find(t => t.Id == currentNode.ParentId);
        //        if (currentNode == null)
        //        {
        //            previousNode.ParentId = null;
        //            break;
        //        }
        //        if (currentNode.ParentId != null) continue;
        //        {
        //            if (!newTree.Exists(t => t.Id == currentNode.Id))
        //                newTree.Add(currentNode);
        //        }
        //    } while (currentNode.ParentId != null);


        //}

        //protected List<T> RemoveEmptyParentNode<T>(List<T> tree) where T:ITree<T>
        //{
        //    foreach (var menu in tree.Where(menu => menu.IsLeaf == false).Where(menu => !tree.Exists(t => t.ParentId == menu.Id)))
        //    {
        //        menu.Status = -1;
        //    }
        //    return tree.Where(t => t.Status != -1).ToList();
        //}

        //protected List<T> RemoveEmptyParentNode<T, TU>(List<T> tree) where T : ITree<TU>
        //{
        //    foreach (var menu in tree.Where(menu => menu.IsLeaf == false).Where(menu => !tree.Exists(t => t.ParentId == menu.Id)))
        //    {
        //        menu.Status = -1;
        //    }
        //    return tree.Where(t => t.Status != -1).ToList();
        //}

    }
}
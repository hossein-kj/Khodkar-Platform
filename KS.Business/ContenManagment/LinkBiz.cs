using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using KS.Business.Security;
using KS.Model.ContentManagement;
using Newtonsoft.Json.Linq;
using EntityFramework.Extensions;
using KS.Business.ContenManagment.Base;
using KS.Business.Security.Base;
using KS.Core.Exceptions;
using KS.Core.GlobalVarioable;
using KS.Core.Utility;
using KS.Core.Localization;
using KS.Core.Security;
using KS.DataAccess.Contexts.Base;

namespace KS.Business.ContenManagment
{
    public class LinkBiz:BaseBiz, ILinkBiz
    {

        private readonly IContentManagementContext _contentManagementContext;


        public LinkBiz(IContentManagementContext contentManagementContext)
             : base()
        {

            _contentManagementContext = contentManagementContext;
        }


        //public async Task<IList<LinkObjective>> GetAllLinkAsync(IEnumerable<string> accessRoles = null, IEnumerable<string> denyRoles = null)
        //{
        //    //var ts = _securityContext.ApplicationTrees
        //    // .Include(x => x.Offspring.Select(y => y.Offspring))
        //    // .SingleOrDefault(x => x.Id == 72);
        //    //ts = ts;
        //    var lang = Setting.Language;
        //    var baseTree = await _contentManagementContext.Links.Where(ln => ln.Language == lang).AsNoTracking().ToListAsync();
        //    var newTree = new List<LinkObjective>();
        //    foreach (var node in baseTree)
        //    {
        //        if (node.IsLeaf)
        //        {
        //            if (accessRoles == null && denyRoles == null)
        //                BuildTree<LinkObjective, LinkObjective>(node, newTree, baseTree);
        //            else if (accessRoles != null && denyRoles != null)
        //            {
        //                if (accessRoles.Any(r => r == node.ViewRoleId.ToString()) && !denyRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                {
        //                    BuildTree<LinkObjective, LinkObjective>(node, newTree, baseTree);
        //                }
        //            }
        //            else if (denyRoles == null)
        //            {
        //                if (accessRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                {
        //                    BuildTree<LinkObjective, LinkObjective>(node, newTree, baseTree);
        //                }
        //            }
        //            else if (accessRoles == null)
        //            {
        //                if (!denyRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                {
        //                    BuildTree<LinkObjective, LinkObjective>(node, newTree, baseTree);
        //                }
        //            }

        //        }


        //        if (node.ParentId == null)
        //        {
        //            if (node.IsLeaf)
        //            {

        //                if (accessRoles == null && denyRoles == null)
        //                {
        //                    if (!newTree.Exists(t => t.Id == node.Id))
        //                        newTree.Add(node);
        //                }
        //                else if (accessRoles != null && denyRoles != null)
        //                {
        //                    if (accessRoles.Any(r => r == node.ViewRoleId.ToString()) && !denyRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                    {
        //                        if (!newTree.Exists(t => t.Id == node.Id))
        //                            newTree.Add(node);
        //                    }
        //                }
        //                else if (denyRoles == null)
        //                {
        //                    if (accessRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                    {
        //                        if (!newTree.Exists(t => t.Id == node.Id))
        //                            newTree.Add(node);
        //                    }
        //                }
        //                else if (accessRoles == null)
        //                {
        //                    if (!denyRoles.Any(r => r == node.ViewRoleId.ToString()))
        //                    {
        //                        if (!newTree.Exists(t => t.Id == node.Id))
        //                            newTree.Add(node);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (!newTree.Exists(t => t.Id == node.Id))
        //                    newTree.Add(node);
        //            }
        //        }
        //    }
        //    return RemoveEmptyParentNode<LinkObjective, LinkObjective>(newTree);
        //}

        #region [SaveLink..]
        public async Task<Link> Save(JObject data)
        {
            dynamic linkDto = data;
            
            string oldUrl = "";

            int? linkId = linkDto.Id;

            
            var link = new Link()
            {
                Id = linkId == null ? 0 : linkDto.Id
            };
            var currentLink = await _contentManagementContext.Links.AsNoTracking().SingleOrDefaultAsync(ln => ln.Id == link.Id);

            try
            {
                
                if (currentLink == null)
                    throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.LinkNotFound));
                oldUrl = currentLink.Url;
                link = currentLink;
                link.RowVersion = linkDto.RowVersion;
                _contentManagementContext.Links.Attach(link);
            }
            catch (Exception)
            {
                link = new Link();
                _contentManagementContext.Links.Add(link);
            }

       
            string linkUrl = linkDto.Url;
            if (linkUrl.IndexOf(Helper.RootUrl, StringComparison.Ordinal) != 0)
                linkUrl = Helper.RootUrl + linkUrl;
            if (linkUrl.LastIndexOf(Helper.RootUrl, StringComparison.Ordinal) == linkUrl.Length - 1)
                linkUrl = linkUrl.Substring(0, linkUrl.Length - 1);
            try
            {
                int parentId = linkDto.ParentId;
                if (currentLink?.ParentId != parentId)
                {

                    var parentCode = await _contentManagementContext.Links.SingleOrDefaultAsync(md => md.Id == parentId);
                    if (parentCode == null)
                        throw new KhodkarInvalidException(
                            LanguageManager.ToAsErrorMessage(ExceptionKey.ParentRecordNotFound));
                    AuthorizeManager.CheckParentNodeModifyAccessForAddingChildNode(parentCode, parentCode.Id);
                }
                link.ParentId = parentId;
            }
            catch (KhodkarInvalidException)
            {

                throw;
            }
            catch (Exception)
            {

                link.ParentId = null;
            }
            link.Text = linkDto.Text;
            link.TransactionCode = linkDto.TransactionCode;
            link.Html = linkDto.Html;
            link.Action = linkDto.Action;
            link.Url = linkUrl;

            var repeatedLink = await _contentManagementContext.Links.Where(sr => sr.Url == link.Url).CountAsync();
         
            if ((repeatedLink > 0 && oldUrl == "") || (repeatedLink > 1 && oldUrl == ""))
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.RepeatedValue, link.Url));
          

            try
            {
                link.Order = linkDto.Order;
            }
            catch (Exception)
            {
                link.Order = 1;
            }
            link.IsLeaf = linkDto.IsLeaf;
            link.Language = linkDto.Language;

            //if(service.IsLeaf)

            if (currentLink != null)
            {
                link.ViewRoleId = currentLink.ViewRoleId;
                link.ModifyRoleId = currentLink.ModifyRoleId;
                link.AccessRoleId = currentLink.AccessRoleId;
            }

            AuthorizeManager.SetAndCheckModifyAndAccessRole(link, linkDto);

            link.Status = linkDto.Status;
            link.IsMobile = linkDto.IsMobile;
            link.TypeId = linkDto.TypeId;

            await _contentManagementContext.SaveChangesAsync();
            if (oldUrl != "")
                _contentManagementContext.WebPages.Where(wp => wp.Url == oldUrl).Update(wp => new WebPage() { Url = linkUrl });
            return link;
        }
        #endregion [SaveLink...]

        public async Task<bool> Delete(JObject data)
        {
            dynamic linkData = data;
            int id;

            try
            {
                id = linkData.Id;
            }
            catch (Exception)
            {
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.FieldMustBeNumeric, "Link Id"));

            }
            var link = await _contentManagementContext.Links.SingleOrDefaultAsync(md => md.Id == id)
                ;
     
            if (link == null)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.LinkNotFound));

            AuthorizeManager.SetAndCheckModifyAndAccessRole(link, null, false);


            var useCount = await _contentManagementContext.WebPages.Where(wp => wp.Url == link.Url)
                 .CountAsync();

            if (useCount > 0)
                throw new KhodkarInvalidException(LanguageManager.ToAsErrorMessage(ExceptionKey.InUseItem, link.Text));
         

            _contentManagementContext.Links.Remove(link);

            await _contentManagementContext.SaveChangesAsync();
            return true;
        }
    }
}

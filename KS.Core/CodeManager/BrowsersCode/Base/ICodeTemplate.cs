using KS.Core.Model;
using KS.Core.Model.Develop;
using KS.Core.Model.Security;

namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public interface ICodeTemplate
    {
        string DependencyArray { get; }
        string DependencyKey { get; }
        string DependencyKeyArray { get; }
        string DependencyKeyEnd { get; }
        string DependencyKeyStart { get; }
        string DependencyTemplate { get; }
        string JavaScript { get; }
        string JavaScriptRefrencesEnd { get; }
        string JavaScriptRefrencesStart { get; }
        string NewModule { get; }
        string PageId { get; }
        string PageParams { get; }
        string PlaceHolder { get; }
        string ScriptsPath { get; }
        string ServicePrefix { get; }
        string Style { get; }
        string StyleRefrencesEnd { get; }
        string StyleRefrencesStart { get; }
        string StylesPath { get; }
        string Template { get; }
        string Title { get; }
        string MetaTags { get; }
        string MetaTagsStart { get; }
        string MetaTagsEnd { get; }

        string GetScriptDebugPathByDebugId(DebugUser debug);
    }
}
using KS.Core.GlobalVarioable;
using KS.Core.Model;
using KS.Core.Model.Develop;
using KS.Core.Model.Security;

namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public abstract class BaseCodeTemplate
    {
        public virtual string Template => "@asTemplate";
        public virtual string JavaScript => "@asJavaScript";
        public virtual string PageParams => "@asPageParams";
        public virtual string PageId => "@asPageId";
        public virtual string Style => "@asStyle";
        public virtual string ScriptsPath => "@asScriptsPath@";
        public virtual string StylesPath => "@asStylesPath@";
        public virtual string Title => "@asTitle";

        public virtual string PlaceHolder => "@asPlaceHolder";
        public virtual string ServicePrefix => "$.asUrls";

        public virtual string JavaScriptRefrencesStart => "@asJavaScriptRefrencesStart@";
        public virtual string JavaScriptRefrencesEnd => "@asJavaScriptRefrencesEnd@";
        public virtual string StyleRefrencesStart => "@asStyleRefrencesStart@";
        public virtual string StyleRefrencesEnd => "@asStyleRefrencesEnd@";
        public virtual string DependencyKey => "@asDependencyKey@";
        public virtual string DependencyKeyStart => @"/*" + DependencyKey + "@Start*/";
        public virtual string DependencyKeyEnd => @"/*" + DependencyKey + "@End*/";
        public virtual string DependencyArray => "@asDependencyArray@";
        public virtual string DependencyKeyArray => "@asDependencyKeyArray@";
        public virtual string NewModule => "/*@asNewModule@*/";

        public virtual string DependencyTemplate => DependencyKeyStart +
                                                   " function " + DependencyKey + "() { " +
                                                   " return [" + DependencyArray + "]" +
                                                   ".concat(" + DependencyKeyArray + "); " +
                                                   " } " +
                                                   " if (source.indexOf('" + DependencyKey + "') >= 0) {" +
                                                   " addDependency(@asDependencyKey@()); " +
                                                   "} " +
                                                   DependencyKeyEnd;
public virtual string GetScriptDebugPathByDebugId(DebugUser debug)
        {
            var debugId = debug == null ? "" : Config.DebugIdSign + "/" +  debug.Guid + "/";
            return Config.ScriptDebugPath + debugId;
        }
    }
}

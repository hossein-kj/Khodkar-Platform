using System;
using System.Text;
using EcmaScript.NET;
using KS.Core.Exceptions;
using KS.Core.Localization;
using Microsoft.Ajax.Utilities;

namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public abstract class BaseCompressManager
    {
        protected readonly Minifier Minifier;
        protected readonly Yahoo.Yui.Compressor.JavaScriptCompressor JavaScriptCompressor;

        protected BaseCompressManager()
        {
            Minifier = new Microsoft.Ajax.Utilities.Minifier();
            JavaScriptCompressor = new Yahoo.Yui.Compressor.JavaScriptCompressor { Encoding = Encoding.UTF8,ObfuscateJavascript=false };
            //_cssCompressor = new Yahoo.Yui.Compressor.CssCompressor() { RemoveComments=true};
        }
        public virtual string CompressJavaScript(string js,string sourceName)
        {
            try
            {
                return Minifier.MinifyJavaScript(js, new CodeSettings()
                {
                    //MinifyCode = true,
                    ////OutputMode = OutputMode.SingleLine,
                    ////CollapseToLiteral = true,
                    ////EvalTreatment = EvalTreatment.MakeAllSafe,
                    ////IndentSize = 0,
                    ////InlineSafeStrings = true,
                    ////LocalRenaming = LocalRenaming.CrunchAll,
                    ////PreserveFunctionNames = false,
                    ////RemoveFunctionExpressionNames = true,
                    RemoveUnneededCode = true,
                    StripDebugStatements = true,
                    //AllowEmbeddedAspNetBlocks = true
                    PreserveImportantComments = false
                }); 
            }
            catch (EcmaScriptRuntimeException ex)
            {
                var message = ex.Message + " SourceName : " + sourceName + " LineSource : " + ex.LineSource +
                              " LineNumber : " + ex.LineNumber +
                              " ColumnNumber : " + ex.ColumnNumber;

                //Elmah.ErrorSignal.FromCurrentContext().Raise(new BundleCompileException(
                //    ex.Message + " SourceName : " + sourceName
                //    + " LineSource : " + ex.LineSource +
                //    " LineNumber : " + ex.LineNumber +
                //    " ColumnNumber : " + ex.ColumnNumber));
                throw new EcmaScriptRuntimeException(LanguageManager.ToAsErrorMessage(message: message));
            }

        }

        public virtual string CompressCss(string css)
        {
            return Minifier.MinifyStyleSheet(css, new CssSettings()
            {
                MinifyExpressions = true,
                //OutputMode = OutputMode.SingleLine,
                //IndentSize = 0,
                //AllowEmbeddedAspNetBlocks = true,
                CommentMode = CssComment.Hacks

            });
        }

        public virtual bool CheckJavaScriptCode(string js, string sourceName)
        {

            try
            {
                if(!string.IsNullOrEmpty(js))
                JavaScriptCompressor.Compress(js);
                return true;
            }
            catch (EcmaScriptRuntimeException ex)
            {
                var message = ex.Message + " LineSource : " + ex.LineSource +
                              " LineNumber : " + ex.LineNumber +
                              " ColumnNumber : " + ex.ColumnNumber;
                //Elmah.ErrorSignal.FromCurrentContext().Raise(new BundleCompileException(
                //    ex.Message + " SourceName : " + sourceName
                //    + " LineSource : " + ex.LineSource +
                //    " LineNumber : " + ex.LineNumber +
                //    " ColumnNumber : " + ex.ColumnNumber));
                throw new EcmaScriptRuntimeException(LanguageManager.ToAsErrorMessage(message:message));
            }
        }
    }
}

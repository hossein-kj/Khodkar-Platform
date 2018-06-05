namespace KS.Core.CodeManager.BrowsersCode.Base
{
    public interface ICompressManager
    {
        bool CheckJavaScriptCode(string js, string sourceName);
        string CompressCss(string css);
        string CompressJavaScript(string js, string sourceName);
    }
}
using System;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class BundleCompileException : Exception
    {
         public BundleCompileException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         //public BundleCompileException() : base(Translation.ToAsErrorMessage(ExceptionKey.PageNotFound)) { }
         ////public PageNotFoundException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.PageNotFoundException)) { }
    }
}

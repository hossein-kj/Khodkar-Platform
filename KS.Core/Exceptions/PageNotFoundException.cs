using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class PageNotFoundException: Exception
    {
         public PageNotFoundException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         public PageNotFoundException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.PageNotFound)) { }
         //public PageNotFoundException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.PageNotFoundException)) { }
    }
}

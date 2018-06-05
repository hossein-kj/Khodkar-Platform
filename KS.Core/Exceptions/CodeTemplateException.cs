using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class CodeTemplateException : Exception
    {
         public CodeTemplateException(string message) : base(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeTemplateException,message)) { }
         //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
         public CodeTemplateException()
             : base(LanguageManager.ToAsErrorMessage(ExceptionKey.CodeTemplateException)) { }
    }
}

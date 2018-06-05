using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class DefaultsLanguageFoundException: Exception
    {
         public DefaultsLanguageFoundException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         //public DefaultsLanguageFoundException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.DefaultsLanguageFoundException)) { }
         public DefaultsLanguageFoundException() : 
             base(LanguageManager.ToAsErrorMessage(ExceptionKey.DefaultsLanguageFoundException)) { }
    }
}

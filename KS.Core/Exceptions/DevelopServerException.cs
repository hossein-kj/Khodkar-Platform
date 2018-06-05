using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class DevelopServerException : Exception
    {
         public DevelopServerException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
         public DevelopServerException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.DevelopServerException)) { }
    }
}

using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class InvalidLoginException: Exception
    {
         public InvalidLoginException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
         public InvalidLoginException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidLogin)) { }
    }
}

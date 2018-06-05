using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class CheckOutCodeException : Exception
    {
         public CheckOutCodeException(string message) : base(LanguageManager.ToAsErrorMessage(null,message)) { }
         //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
         public CheckOutCodeException(string message="",bool onlyDataBaseMessage=true) : base(
             onlyDataBaseMessage ? LanguageManager.ToAsErrorMessage(ExceptionKey.CheckOutCode): LanguageManager.ToAsErrorMessage(ExceptionKey.CheckOutCode, message) ) { }
    }
}

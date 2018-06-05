using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class DllAlreadyCompiledException : Exception
    {
        public DllAlreadyCompiledException(string message) : base(LanguageManager.ToAsErrorMessage(ExceptionKey.DllAlreadyCompiled,message)) { }
        //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
        public DllAlreadyCompiledException()
             : base(LanguageManager.ToAsErrorMessage(ExceptionKey.DllAlreadyCompiled)) { }
    }
}

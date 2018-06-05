using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class DataConcurrencyException: Exception
    {
         public DataConcurrencyException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
         //public DataConcurrencyException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.DataConcurrencyException)) { }
         public DataConcurrencyException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.DataConcurrencyException)) { }
    }
}

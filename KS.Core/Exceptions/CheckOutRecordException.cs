using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class CheckOutRecordException : Exception
    {
        public string ModifyUser { get; set; }

        public CheckOutRecordException(string message)
            : base(LanguageManager.ToAsErrorMessage(ExceptionKey.CheckOutRecord, message))
        {
            ModifyUser = message;
        }
         //public InvalidLoginException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidLogin)) { }
         public CheckOutRecordException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.CheckOutRecord)) { }
    }
}

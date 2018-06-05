using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
    [Serializable]
    public sealed class InvalidAccessTreeDirectoryRoleException : Exception
    {
        public InvalidAccessTreeDirectoryRoleException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
        //public InvalidAccessTreeDirectoryRoleException() : 
        //    base(Helper.ToAsErrorMessage(Resources.ErrorMessages.InvalidAccessTreeDirectoryRoleException)) { }
        public InvalidAccessTreeDirectoryRoleException() :
            base(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidAccessTreeDirectoryRoleException))
        {

        }
    }
}

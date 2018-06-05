using System;
using KS.Core.GlobalVarioable;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
    [Serializable]
    public sealed class PageTemporaryInaccessibleException : Exception
    {
        public PageTemporaryInaccessibleException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
        public PageTemporaryInaccessibleException() : base(LanguageManager.ToAsErrorMessage(ExceptionKey.PageTemporaryInaccessible))
        { }
        //public PageNotFoundException() : base(Helper.ToAsErrorMessage(Resources.ErrorMessages.PageNotFoundException)) { }
    }
}

using System;
using KS.Core.Utility;
using KS.Core.Localization;

namespace KS.Core.Exceptions
{
     [Serializable]
    public sealed class KhodkarInvalidException : Exception
    {
         public KhodkarInvalidException(string message) : base(LanguageManager.ToAsErrorMessage(message: message)) { }
    }
}

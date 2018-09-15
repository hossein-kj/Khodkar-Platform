using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KS.Business.Security;
using KS.Core.GlobalVarioable;
using Microsoft.AspNet.Identity;
using KS.Core.Localization;
//using Resources;

namespace KS.Business.Localization
{
    /// <summary>
    ///     Validates users before they are saved
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class LocalizeUserValidator<TUser, TKey> : IIdentityValidator<TUser>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="manager"></param>
        public LocalizeUserValidator(ApplicationUserManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            AllowOnlyAlphanumericUserNames = true;
            Manager = manager;
        }

        public void Initialize()
        {
            AllowOnlyAlphanumericUserNames = false;
            RequireUniqueEmail = true;
        }

        /// <summary>
        ///     Only allow [A-Za-z0-9@_] in UserNames
        /// </summary>
        public bool AllowOnlyAlphanumericUserNames { get; set; }

        /// <summary>
        ///     If set, enforces that emails are non empty, valid, and unique
        /// </summary>
        public bool RequireUniqueEmail { get; set; }

        private ApplicationUserManager Manager { get; set; }
        public int RequiredLength { get { return 8; } }

        /// <summary>
        ///     Validates a user before saving
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            var errors = new List<string>();
            await ValidateUserName(item, errors).WithCurrentCulture();
            if (RequireUniqueEmail)
            {
                await ValidateEmailAsync(item, errors).WithCurrentCulture();
            }
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }
            return IdentityResult.Success;
        }

        private async Task ValidateUserName(TUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {

                errors.Add(LanguageManager.ToAsErrorMessage(
                        message:
                        string.Format(LanguageManager.GetException(ExceptionKey.PropertyTooShort), "User Name", RequiredLength)));
            }
            else if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, @"^[A-Za-z0-9@_\.]+$"))
            {
                // If any characters are not letters or digits, its an illegal user name
            

                errors.Add(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidUserName));
            }
            else
            {
                var owner = await Manager.FindByNameAsync(user.UserName).WithCurrentCulture();
                if (owner != null && !Object.Equals(owner.Id, user.Id))
                {
                    errors.Add(LanguageManager.ToAsErrorMessage(ExceptionKey.DuplicateName));
                }
            }
        }

        // make sure email is not empty, valid, and unique
        private async Task ValidateEmailAsync(TUser user, List<string> errors)
        {
            var email = user.UserName;
            if (string.IsNullOrWhiteSpace(email))
            {

                errors.Add(LanguageManager.ToAsErrorMessage(
        message:
        string.Format(LanguageManager.GetException(ExceptionKey.PropertyTooShort), "Email", RequiredLength)));
                return;
            }
            try
            {
                var m = new MailAddress(email);
            }
            catch (FormatException)
            {
                errors.Add(LanguageManager.ToAsErrorMessage(ExceptionKey.InvalidEmail));
               
                return;
            }
            var owner = await Manager.FindByEmailAsync(email).WithCurrentCulture();
            if (owner != null && !Object.Equals(owner.Id, user.Id))
            {
                errors.Add(LanguageManager.ToAsErrorMessage(ExceptionKey.DuplicateEmail));

            }
        }
    }
}
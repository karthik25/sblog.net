#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using sBlog.Net.Abstract;
using sBlog.Net.Domain.Utilities;
using sBlog.Net.FluentExtensions;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Generators;
using sBlog.Net.Infrastructure;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Models.Account;
using sBlog.Net.Services;

namespace sBlog.Net.Controllers
{
    public class AccountController : BlogController
    {
        private readonly IUser _userRepository;
        private readonly IRole _roleRepository;

        public AccountController(IUser userRepository, ISettings settingsRepository, IRole roleRepository)
            : base(settingsRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            ExpectedMasterName = string.Empty;
        }

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToRoute("AdminIndex");
            }

            var logOnModel = new LogOnModel { Title = SettingsRepository.BlogName };
            return View(logOnModel);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);

                    var userEntity = SetupFormsAuthTicket(model.UserName, model.RememberMe);

                    if (userEntity.LastLoginDate == null)
                    {
                        return RedirectToRoute("AdminUpdateProfile");
                    }
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToRoute("AdminIndex");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            model.Title = SettingsRepository.BlogName;
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            if (Request.IsAuthenticated)
            {
                var userIdentity = GetUserId();
                _userRepository.SetOneTimeToken(userIdentity, null);
                _userRepository.UpdateLastLoginDate(userIdentity);
                FormsService.SignOut();
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Add()
        {
            var newUserModel = new NewUserModel
                {
                    Title = SettingsRepository.BlogName,
                    RoleId = 2
                };
            return View(newUserModel);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult Add(NewUserModel newUserModel)
        {
            var urlFormat = string.Empty;
            var createStatus = true;

            if (ModelState.IsValid)
            {
                var userActivationKey = HashExtensions.GetMD5Hash(string.Format("{0}-{1}-{2}", newUserModel.UserDisplayName, newUserModel.UserEmailAddress, DateTime.Now));
                createStatus = _userRepository.AddUser(newUserModel.UserEmailAddress, newUserModel.UserDisplayName, userActivationKey);

                if (createStatus)
                {
                    var newUser = _userRepository.GetUserNameByEmail(newUserModel.UserEmailAddress);
                    _roleRepository.AddRoleForUser(newUser.UserID, newUserModel.RoleId);

                    urlFormat = string.Format("{0}account/register?newUserTicket={1}", GetRootUrl(), userActivationKey);
                    var status = Emailer.SendMessage(SettingsRepository.BlogAdminEmailAddress, newUserModel.UserEmailAddress,
                                                     string.Format("Join {0}", SettingsRepository.BlogName),
                                                     string.Format(NewUserEmailMeassage, newUserModel.UserDisplayName,
                                                                   SettingsRepository.BlogName, urlFormat,
                                                                   SettingsRepository.BlogName));

                    if (status)
                    {
                        return RedirectToRoute("AdminUserManagement");
                    }
                }
            }

            newUserModel.Title = SettingsRepository.BlogName;
            var errorMessage = createStatus
                            ? "Unable to send an email because the emailing service failed. Please send the following url to the user " +
                              urlFormat
                            : "Creation/update of new user failed. Check the email address entered.";
            ModelState.AddModelError("", errorMessage);
            return View(newUserModel);
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register(string newUserTicket)
        {
            var userEntity = GetNewUserIDByTicket(newUserTicket);
            if (userEntity == null || Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var registerModel = new RegisterModel
            {
                Title = SettingsRepository.BlogName,
                NewUserTicket = newUserTicket,
                UserID = userEntity.UserID,
                UserDisplayName = userEntity.UserDisplayName
            };
            return View(registerModel);
        }

        private UserEntity GetNewUserIDByTicket(string newUserTicket)
        {
            if (string.IsNullOrEmpty(newUserTicket))
                return null;

            return _userRepository.GetAllUsers()
                                  .ToList()
                                  .SingleOrDefault(u => u.ActivationKey == newUserTicket);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid && ValidateNewUser(model))
            {
                // Attempt to register the user
                var createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    return RedirectToAction("LogOn", "Account");
                }
                ModelState.AddModelError("", createStatus.ErrorCodeToString());
            }

            var userEntity = GetNewUserIDByTicket(model.NewUserTicket);
            if (userEntity != null)
            {
                model.UserDisplayName = userEntity.UserDisplayName;
            }
            model.Title = SettingsRepository.BlogName;
            return View(model);
        }

        private bool ValidateNewUser(RegisterModel model)
        {
            var userEntity = GetNewUserIDByTicket(model.NewUserTicket);
            if (userEntity != null)
            {
                if (userEntity.UserEmailAddress == model.Email && userEntity.UserID == model.UserID)
                    return true;
            }

            ModelState.AddModelError("", "Unable to validate the information you entered");
            return false;
        }

        [HttpPost]
        public string ForgotPassword(string emailAddress)
        {
            var verificationCode = _userRepository.ForgotPassword(emailAddress);
            var urlFormat = "{0}account/resetpassword?ticket={1}";

            if (!string.IsNullOrEmpty(verificationCode))
            {
                urlFormat = string.Format(urlFormat, GetRootUrl(), verificationCode);
                var status = Emailer.SendMessage(SettingsRepository.BlogAdminEmailAddress, emailAddress,
                                                 string.Format("Request to reset your password :: {0}", SettingsRepository.BlogName),
                                                 string.Format(EmailMeassage, urlFormat, SettingsRepository.BlogName));
                return !status ? "unable to process your request. please try again later..." : "successfully reset your password. please check your email in a few minutes...";
            }
            return "unable to find a user with the email address. please check the email address entered...";
        }

        public ActionResult ResetPassword(string ticket)
        {
            if (string.IsNullOrEmpty(ticket))
            {
                return RedirectToRoute("Default");
            }
            var model = new ResetPasswordModel { VerificationCode = ticket, Title = SettingsRepository.BlogName };
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordModel model, string ticket)
        {
            if (string.IsNullOrEmpty(ticket))
            {
                return RedirectToRoute("Default");
            }

            if (ModelState.IsValid)
            {
                var randomCode = RandomStringGenerator.RandomString();
                var encCode = TripleDES.EncryptString(randomCode);
                var hashedPassword = PasswordHelper.GenerateHashedPassword(model.Password, randomCode);
                if (_userRepository.ResetPassword(model.EmailAddress, model.VerificationCode, hashedPassword, encCode))
                {
                    return RedirectToRoute("AdminIndex");
                }
                ModelState.AddModelError("__FORM", "unable to reset your password");
            }

            model.Title = SettingsRepository.BlogName;
            return View(model);
        }

        [HttpPost]
        public string RequestAccount(RequestAccountModel requestAccountModel)
        {
            const string emailMessage = "Name: {0}<br/><br/>Email:{1}<br/><br/>Message:{2}";

            if (ModelState.IsValid)
            {
                var message = string.Format(emailMessage, requestAccountModel.AuthorName,
                                            requestAccountModel.AuthorEmail, requestAccountModel.AuthorMessage);

                var status = Emailer.SendMessage(SettingsRepository.BlogAdminEmailAddress, SettingsRepository.BlogAdminEmailAddress,
                                                 "Request for an account", message);
                return status ? "request for being an author sent. please check your email address if it has been approved."
                              : "unable to process your request. please try again later.";
            }

            return "some information entered is invalid. please try again.";
        }

        private UserEntity SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            var userEntity = _userRepository.GetUserObjByUserName(userName);
            var userId = userEntity.UserID;
            var token = RandomStringGenerator.RandomString(8);
            _userRepository.SetOneTimeToken(userId, token);
            var userData = string.Format("{0}:{1}", userId.ToString(CultureInfo.InvariantCulture),
                                                       token);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                                        userName, // user name
                                                        DateTime.Now,             //creation
                                                        DateTime.Now.AddMinutes(30), //Expiration
                                                        persistanceFlag, //Persistent
                                                        userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return userEntity;
        }

        private const string EmailMeassage = @"Click <a href=""{0}"">here</a> to reset your password.<br />
                                                   If you have issues visiting the password reset page, copy and paste the following url
                                                   <br /><br />{0}<br/><br/>Thanks,<br/>Admin<br/>{1}";

        private const string NewUserEmailMeassage = @"Hello {0}! You are invited by the admin of {1} to function as an author.
                                                    Click <a href=""{2}"">here</a> to register if you are interested.<br />
                                                   If you have issues visiting the registration page, copy and paste the following url
                                                   <br /><br />{2}<br/><br/>Thanks,<br/>Admin<br/>{3}";
    }
}

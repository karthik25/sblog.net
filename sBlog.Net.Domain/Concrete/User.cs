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
/* User.cs 
 * 
 * This class extends the DefaultDisposable class,
 * Which implements the IDisposable interface for this class.
 * 
 * If you modify the class to add more disposable managed
 * resources, you can remove DefaultDisposable and implement
 * the Dispose() method yourself
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using sBlog.Net.Domain.Interfaces;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Utilities;

namespace sBlog.Net.Domain.Concrete
{
    public class User : DefaultDisposable, IUser
    {
        private readonly Table<UserEntity> _usersTable;

        public User()
        {
            _usersTable = context.GetTable<UserEntity>();
        }

        public UserEntity GetUserObjByUserID(int userID)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userID);
            return user;
        }

        public UserEntity GetUserObjByUserName(string userName, string passWord)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserName == userName && u.Password == passWord);
            return user;
        }

        public UserEntity GetUserNameByEmail(string emailAddress)
        {
            var userObj = _usersTable.SingleOrDefault(u => u.UserEmailAddress == emailAddress);
            return userObj;
        }

        public UserEntity GetUserObjByUserName(string userName)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserName == userName);
            return user;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return _usersTable.AsEnumerable();
        }

        public bool AddUser(string emailAddress, string displayName, string userActivationTicket)
        {
            var status = false;
            var item = _usersTable.SingleOrDefault(u => u.UserEmailAddress == emailAddress);

            if (item == null)
            {
                var user = new UserEntity
                    {
                        UserName = string.Empty,
                        UserDisplayName = displayName,
                        Password = string.Empty,
                        UserCode = string.Empty,
                        UserEmailAddress = emailAddress,
                        UserActiveStatus = null,
                        ActivationKey = userActivationTicket
                    };

                _usersTable.InsertOnSubmit(user);
                context.SubmitChanges();
                status = true;
            }
            else
            {
                if (item.UserID != 1)
                {
                    item.UserDisplayName = displayName;
                    item.ActivationKey = userActivationTicket;
                    context.SubmitChanges();
                    status = true;
                }
            }

            return status;
        }

        public int RegisterUser(UserEntity userObj)
        {
            var user = new UserEntity
                           {
                               UserName = userObj.UserName,
                               UserDisplayName = userObj.UserName,
                               Password = userObj.Password,
                               UserCode = userObj.UserCode,
                               UserEmailAddress = userObj.UserEmailAddress,
                               UserActiveStatus = 1
                           };

            _usersTable.InsertOnSubmit(user);
            context.SubmitChanges();

            return user.UserID;
        }

        public string GetOneTimeToken(int userId)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userId);
            return user != null ? user.OneTimeToken : null;
        }

        public void SetOneTimeToken(int userId, string oneTimeToken)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                user.OneTimeToken = oneTimeToken;
                context.SubmitChanges();
            }
        }

        public void UpdateUser(UserEntity userEntity)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userEntity.UserID);
            if (user != null)
            {
                user.Password = userEntity.Password;
                user.UserCode = userEntity.UserCode;
                context.SubmitChanges();
            }
        }

        public bool UpdateProfile(UserEntity userEntity)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userEntity.UserID);
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.UserName))
                    user.UserName = userEntity.UserName;

                user.UserEmailAddress = userEntity.UserEmailAddress;
                user.UserDisplayName = userEntity.UserDisplayName;

                if (userEntity.Password != null && userEntity.UserCode != null)
                {
                    user.Password = userEntity.Password;
                    user.UserCode = userEntity.UserCode;
                }

                if (userEntity.UserActiveStatus.HasValue)
                    user.UserActiveStatus = userEntity.UserActiveStatus;

                user.ActivationKey = string.Empty;

                user.UserSite = userEntity.UserSite;

                context.SubmitChanges();
                return true;
            }
            return false;
        }

        public string ForgotPassword(string emailAddress)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserEmailAddress == emailAddress);
            if (user != null)
            {
                var userString = string.Format("{0}-{1}-{2}-{3}", user.UserName, user.Password, user.UserEmailAddress, DateTime.Now.ToString());
                var verificationCode = HashExtensions.GetMD5Hash(userString);
                user.ActivationKey = verificationCode;
                context.SubmitChanges();
                return verificationCode;
            }
            return string.Empty;
        }

        public bool ResetPassword(string emailAddress, string verificationCode, string newPassword, string userCode)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserEmailAddress == emailAddress && u.ActivationKey == verificationCode);
            if (user != null)
            {
                user.ActivationKey = string.Empty;
                user.Password = newPassword;
                user.UserCode = userCode;
                context.SubmitChanges();
                return true;
            }
            return false;
        }

        public void UpdateLastLoginDate(int userID)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userID);
            if (user != null)
            {
                user.LastLoginDate = DateTime.Now;
                context.SubmitChanges();
            }
        }

        public void DeleteUser(int userID)
        {
            var user = _usersTable.SingleOrDefault(u => u.UserID == userID);
            if (user != null)
            {
                _usersTable.DeleteOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public bool ToggleUserActiveStatus(int userId, bool activate)
        {
            var status = activate ? 1 : 0;
            var user = _usersTable.SingleOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                user.UserActiveStatus = status;                
                context.SubmitChanges();
                return true;
            }
            return false;
        }

        ~User()
        {
            Dispose(false);
        }
    }
}

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
using System.Collections.Generic;
using sBlog.Net.Domain.Entities;

namespace sBlog.Net.Domain.Interfaces
{
    public interface IUser : IDisposable
    {
        UserEntity GetUserObjByUserID(int userID);
        UserEntity GetUserObjByUserName(string userName, string passWord);
        UserEntity GetUserNameByEmail(string emailAddress);
        UserEntity GetUserObjByUserName(string userName);
        IEnumerable<UserEntity> GetAllUsers();
        bool AddUser(string emailAddress, string displayName, string userActivationTicket);
        int RegisterUser(UserEntity userObj);
        string GetOneTimeToken(int userId);
        void SetOneTimeToken(int userId, string oneTimeToken);
        void UpdateUser(UserEntity userEntity);
        string ForgotPassword(string emailAddress);
        bool ResetPassword(string emailAddress, string verificationCode, string newPassword, string userCode);
        bool UpdateProfile(UserEntity userEntity);
        void UpdateLastLoginDate(int userID);
        void DeleteUser(int userID);
        bool ToggleUserActiveStatus(int userId, bool activate);
    }
}

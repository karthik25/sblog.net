using System;
using System.Collections.Generic;
using System.Linq;
using sBlog.Net.Domain.Entities;
using sBlog.Net.Domain.Interfaces;

namespace sBlog.Net.Tests.MockObjects
{
    public class MockUser : IUser
    {
        private readonly List<UserEntity> _usersTable;

        public MockUser()
        {
            _usersTable = GetMockUsers();
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
            throw new NotImplementedException();
        }

        public int RegisterUser(UserEntity userObj)
        {
            throw new NotImplementedException();
        }

        public string GetOneTimeToken(int userId)
        {
            throw new NotImplementedException();
        }

        public void SetOneTimeToken(int userId, string oneTimeToken)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public string ForgotPassword(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool ResetPassword(string emailAddress, string verificationCode, string newPassword, string userCode)
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfile(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }

        public void UpdateLastLoginDate(int userID)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userID)
        {
            throw new NotImplementedException();
        }

        private static List<UserEntity> GetMockUsers()
        {
            return new List<UserEntity>
                       {
                           new UserEntity { UserID = 1, UserName = "admin", UserDisplayName = "Admin", UserActiveStatus = 1 },
                           new UserEntity { UserID = 2, UserName = "karthik", UserDisplayName = "Karthik", UserActiveStatus = 1  },
                           new UserEntity { UserID = 3, UserName = "testuser01", UserDisplayName = "Test User 01", UserActiveStatus = 1  },
                           new UserEntity { UserID = 4, UserName = "testuser02", UserDisplayName = "Test User 02", UserActiveStatus = 1  },
                           new UserEntity { UserID = 5, UserName = "testuser03", UserDisplayName = "Test User 03", UserActiveStatus = 1  },
                           new UserEntity { UserID = 6, UserName = "testuser04", UserDisplayName = "Test User 04", UserActiveStatus = 1  }
                       };
        }

        public bool ToggleUserActiveStatus(int userId, bool activate)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}

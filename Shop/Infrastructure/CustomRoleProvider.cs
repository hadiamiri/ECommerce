using System;
using System.Linq;
using System.Web.Security;
using Shop.Models;

namespace Shop.Infrastructure
{
    public class CustomRoleProvider : RoleProvider
    {

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName == username);
                if (user != null)
                {
                    return user.Role.Name.Equals(roleName);
                }
                return false;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new Context())
            {
                var user = context.Users.FirstOrDefault(x => x.UserName.Equals(username));

                if (user != null) return new[] { user.Role.Name };
            }

            return new string[] { };
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
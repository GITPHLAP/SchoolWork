using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecureLoginWebAPI.Models
{
    public class ApplicationUser
    {
		private int id;
		private string username;
		private string normalizedusername;
		private string email;
		private string normalizedemail;
        private bool emailconfirmed;
		private string passwordhash;
        private string securityStamp;
        private string concurrencyStamp;
        private string phonenumber;
		private bool phonenumberconfirmed;
		private bool twofactorenabled;
        private DateTimeOffset? lockoutEnd;
        private bool lockoutEnable;
        private int accessFailedCount;
        private bool deleted;
        private string authenticatorKey;
        private IList<RecoverCode> recoveryCodes;


        public ApplicationUser()
        {
        }

        public ApplicationUser(int id, string username, string normalizedusername, string email,
            string normalizedemail, bool emailconfirmed, string passwordhash, string securityStamp,
            string concurrencyStamp, string phonenumber, bool phonenumberconfirmed, bool twofactorenabled,
            DateTimeOffset? lockoutEnd, bool lockoutEnable, int accessFailedCount, bool deleted, string authenticatorKey)
        {
            this.id = id;
            this.username = username;
            this.normalizedusername = normalizedusername;
            this.email = email;
            this.normalizedemail = normalizedemail;
            this.emailconfirmed = emailconfirmed;
            this.passwordhash = passwordhash;
            this.securityStamp = securityStamp;
            this.concurrencyStamp = concurrencyStamp;
            this.phonenumber = phonenumber;
            this.phonenumberconfirmed = phonenumberconfirmed;
            this.twofactorenabled = twofactorenabled;
            this.lockoutEnd = lockoutEnd;
            this.lockoutEnable = lockoutEnable;
            this.accessFailedCount = accessFailedCount;
            this.deleted = deleted;
            this.authenticatorKey = authenticatorKey;

        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Normalizedusername { get => normalizedusername; set => normalizedusername = value; }
        public string Email { get => email; set => email = value; }
        public string Normalizedemail { get => normalizedemail; set => normalizedemail = value; }
        public bool Emailconfirmed { get => emailconfirmed; set => emailconfirmed = value; }
        public string Passwordhash { get => passwordhash; set => passwordhash = value; }
        public string SecurityStamp { get => securityStamp; set => securityStamp = value; }
        public string ConcurrencyStamp { get => concurrencyStamp; set => concurrencyStamp = value; }
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }
        public bool Phonenumberconfirmed { get => phonenumberconfirmed; set => phonenumberconfirmed = value; }
        public bool Twofactorenabled { get => twofactorenabled; set => twofactorenabled = value; }
        public DateTimeOffset? LockoutEnd { get => lockoutEnd; set => lockoutEnd = value; }
        public bool LockoutEnable { get => lockoutEnable; set => lockoutEnable = value; }
        public int AccessFailedCount { get => accessFailedCount; set => accessFailedCount = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string AuthenticatorKey { get => authenticatorKey; set => authenticatorKey = value; }
        public IList<RecoverCode> RecoveryCodes { get => recoveryCodes; set => recoveryCodes = value; }
    }
}

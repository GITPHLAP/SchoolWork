using NpgsqlTypes;
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
		private string phonenumber;
		private bool phonenumberconfirmed;
		private bool twofactorenabled;

        public ApplicationUser()
        {
        }

        public ApplicationUser(int id, string username, string normalizedusername, string email, string normalizedemail, bool emailconfirmed, string passwordhash, string phonenumber, bool phonenumberconfirmed, bool twofactorenabled)
        {
            this.id = id;
            this.username = username;
            this.normalizedusername = normalizedusername;
            this.email = email;
            this.normalizedemail = normalizedemail;
            this.emailconfirmed = emailconfirmed;
            this.passwordhash = passwordhash;
            this.phonenumber = phonenumber;
            this.phonenumberconfirmed = phonenumberconfirmed;
            this.twofactorenabled = twofactorenabled;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Normalizedusername { get => normalizedusername; set => normalizedusername = value; }
        public string Email { get => email; set => email = value; }
        public string Normalizedemail { get => normalizedemail; set => normalizedemail = value; }
        
        public bool Emailconfirmed { get => emailconfirmed; set => emailconfirmed = value; }
        public string Passwordhash { get => passwordhash; set => passwordhash = value; }
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }
        public bool Phonenumberconfirmed { get => phonenumberconfirmed; set => phonenumberconfirmed = value; }
        public bool Twofactorenabled { get => twofactorenabled; set => twofactorenabled = value; }
    }
}

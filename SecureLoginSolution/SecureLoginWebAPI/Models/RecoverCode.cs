namespace SecureLoginWebAPI.Models
{
    public class RecoverCode
    {
        private string code;
        private bool redeemed;

        public RecoverCode(string code, bool redeemed)
        {
            this.Code = code;
            this.Redeemed = redeemed;
        }

        public string Code { get => code; set => code = value; }
        public bool Redeemed { get => redeemed; set => redeemed = value; }
    }
}

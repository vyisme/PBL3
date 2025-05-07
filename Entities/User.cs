namespace PBL3.Entities
{
    public class User : Account
    {
        //danh sách các tài khoản ngân hàng
        public DateTime NS { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public User()
        {
            BankAccounts = new HashSet<BankAccount>();
        }   

    }
}

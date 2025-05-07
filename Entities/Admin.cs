namespace PBL3.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Linq;

    public class Admin : Account
    {

        //public override void DisplayInfo()
        //{
        //    // Không in ra console nếu không xử lý sự kiện
        //    // Có thể trả về chuỗi nếu cần hiển thị ngoài
        //}

        //public override string GetData()
        //{
        //    var sb = new StringBuilder();
        //    sb.Append("Admin|")
        //      .Append(name).Append("|")
        //      .Append(username).Append("|")
        //      .Append(password);
        //    return sb.ToString();
        //}

        // Other methods remain unchanged


        // Các chức năng quản trị chỉ khai báo, xử lý sẽ viết ở nơi khác
        public void FreezeBankAccount(List<BankAccount> bankAccounts)
        {
            // Xử lý logic ở lớp dịch vụ riêng nếu cần
        }

        public void UnfreezeBankAccount(List<BankAccount> bankAccounts)
        {
            // Xử lý logic ở lớp dịch vụ riêng nếu cần
        }

        public void ViewBankAccountInfo(List<BankAccount> bankAccounts)
        {
            // Không in ở đây
        }

        public void DeleteBankAccount(List<BankAccount> bankAccounts)
        {
            // Xử lý ngoài
        }

        public void ViewUserInfo(List<Account> accounts)
        {
            // Xử lý ngoài
        }

        public void ViewUserTransactionHistory(List<Account> accounts)
        {
            // Xử lý ngoài
        }

        public void DeleteUser(List<Account> accounts)
        {
            // Xử lý ngoài
        }

        public void DisplayActions(List<Account> accounts, List<BankAccount> bankAccounts)
        {
            // Hiển thị giao diện → xử lý bên ngoài (UI/service)
        }
    }

}


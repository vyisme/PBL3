using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Entities
{
    public enum TransactionType
    {
        Deposit,   // Nạp tiền
        Withdrawal, // Rút tiền
        Transfer   // Chuyển khoản
    }
    public class Trans
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong TransactionId { get; set; } 
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now; 

        [Required]
        public TransactionType Type { get; set; } // Sử dụng enum để xác định loại giao dịch

        // Khóa ngoại
        [ForeignKey("FromAccount")]
        public int FromAccountId { get; set; }

        [ForeignKey("ToAccount")]
        public int ToAccountId { get; set; }

        public double Amount { get; set; }

        // Mối quan hệ với các đối tượng BankAccount
        public virtual BankAccount FromAccount { get; set; }
        public virtual BankAccount ToAccount { get; set; }
        public Trans(int fromAccountId, int toAccountId, double amount, TransactionType type)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền phải lớn hơn 0", nameof(amount));

            if (fromAccountId == toAccountId)
                throw new ArgumentException("Tài khoản gửi và tài khoản nhận không thể giống nhau", nameof(fromAccountId));

            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
            Type = type;
            TransactionDate = DateTime.Now; // Gán thời gian hiện tại
            TransactionId = GenerateUniqueTransactionId(); // Tạo ID giao dịch duy nhất
        }

        // Hàm tạo TransactionId duy nhất
        public static ulong GenerateUniqueTransactionId()
        {
            // Cách tạo ID duy nhất bằng cách kết hợp ticks với một số ngẫu nhiên
            return (ulong)(DateTime.Now.Ticks + new Random().Next(1, 1000)) % ulong.MaxValue;
        }
    }
}

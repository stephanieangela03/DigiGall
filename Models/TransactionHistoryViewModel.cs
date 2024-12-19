namespace DigiGall.Models
{
    public class TransactionHistoryViewModel
    {
        public IEnumerable<Transaksi> Transaksis { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}

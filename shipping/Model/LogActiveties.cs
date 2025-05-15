using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class LogActiveties
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public string UserName { get; set; } = default!;
        public string Action {  get; set; } = default!;
    }
}

namespace Library3.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

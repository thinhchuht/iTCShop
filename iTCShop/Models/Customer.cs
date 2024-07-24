namespace iTCShop.Models
{
    public class Customer
    {
        public string        ID          { get; set; }
        public string        Name        { get; set; }
        public string        Email       { get; set; }
        public string        UserName    { get; set; }
        public string        Password    { get; set; }
        public string        Phone       { get; set; }
        public string        Address     { get; set; }
        public DateTime      DateOfBirth { get; set; }
        public int AuthId    { get;      set; }
        public AuthorizeUser Auth        { get; set; }
        public List<Order>   Orders      { get; set; }

        public Customer(string name, string email, string userName, string password, string phone, string address, DateTime dateOfBirth)
        {
            ID = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            UserName = userName;
            Password = password;
            Phone = phone;
            Address = address;
            DateOfBirth = dateOfBirth;
            AuthId = 2;
        }
    }
}

namespace iTCShop.Models
{
    public class Admin
    {
        public string        ID       { get; set; }
        public string        UserName { get; set; }
        public string        Password { get; set; }
        public int           AuthID   { get; set; }
        public AuthorizeUser Auth     { get; set; }
    }
}

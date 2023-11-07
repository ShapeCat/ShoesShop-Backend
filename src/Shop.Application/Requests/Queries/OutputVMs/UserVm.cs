namespace ShoesShop.Application.Requests.Queries.OutputVMs
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public AdressVm Adress { get; set; }
    }
}

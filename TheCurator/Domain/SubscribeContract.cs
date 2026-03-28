namespace Domain
{
    public class SubscribeContract
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public SubscribeContract(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}

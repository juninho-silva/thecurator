namespace Infra.Data
{
    public interface ISubscriberRepository
    {
        Task<Subscriber?> FindByEmailAsync(string email);
        Task<Subscriber?> FindByTokenAsync(string token);
        Task InsertAsync(Subscriber subscriber);
        Task ReactivateAsync(Guid id, string name);
        Task DeactivateAsync(Guid id);
        Task<IEnumerable<Subscriber>> GetActiveAsync();
    }
}

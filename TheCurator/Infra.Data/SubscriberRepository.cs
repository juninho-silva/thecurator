using System.Data;
using Dapper;
using Infra.Data;

public class SubscriberRepository(IDbConnection db) : ISubscriberRepository
{
    public Task<Subscriber?> FindByEmailAsync(string email) =>
        db.QueryFirstOrDefaultAsync<Subscriber>(
            "SELECT * FROM subscribers WHERE email = @Email", new { Email = email });

    public Task<Subscriber?> FindByTokenAsync(string token) =>
        db.QueryFirstOrDefaultAsync<Subscriber>(
            "SELECT * FROM subscribers WHERE unsubscribe_token = @Token", new { Token = token });

    public Task InsertAsync(Subscriber s) =>
        db.ExecuteAsync("""
            INSERT INTO subscribers (id, name, email, unsubscribe_token, active, created_at, updated_at)
            VALUES (@Id, @Name, @Email, @UnsubscribeToken, @Active, @CreatedAt, @UpdatedAt)
            """, s);

    public Task ReactivateAsync(Guid id, string name) =>
        db.ExecuteAsync("""
            UPDATE subscribers SET active = TRUE, name = @Name, updated_at = NOW()
            WHERE id = @Id
            """, new { Id = id, Name = name });

    public Task DeactivateAsync(Guid id) =>
        db.ExecuteAsync("""
            UPDATE subscribers SET active = FALSE, updated_at = NOW()
            WHERE id = @Id
            """, new { Id = id });

    public Task<IEnumerable<Subscriber>> GetActiveAsync() =>
        db.QueryAsync<Subscriber>("SELECT * FROM subscribers WHERE active = TRUE");
}
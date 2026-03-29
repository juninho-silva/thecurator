using System.Data;
using Dapper;
using Infra.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infra.Data
{
    public static class DependencyInjection
    {
        public static void AddInfraData(this IServiceCollection services, IConfiguration configuration)
        {
            SqlMapper.SetTypeMap(typeof(Subscriber), new ColumnAttributeTypeMapper<Subscriber>());
            services.AddScoped<IDbConnection>(sp =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return new NpgsqlConnection(connectionString);
            });
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        }
    }
}

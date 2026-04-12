using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper;

namespace Infra.Data.Extensions
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
        : base(
        [
            new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
                type.GetProperties().FirstOrDefault(prop =>
                    prop.GetCustomAttributes<ColumnAttribute>(inherit: false)
                        .Any(attr => attr.Name == columnName)
                )),
            new DefaultTypeMap(typeof(T))
        ])
        { }
    }

    public class FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers) : SqlMapper.ITypeMap
    {
        public ConstructorInfo? FindConstructor(string[] names, Type[] types) =>
            mappers.Select(m => m.FindConstructor(names, types)).FirstOrDefault(r => r != null);

        public ConstructorInfo? FindExplicitConstructor() =>
            mappers.Select(m => m.FindExplicitConstructor()).FirstOrDefault(r => r != null);

        public SqlMapper.IMemberMap? GetConstructorParameter(ConstructorInfo constructor, string columnName) =>
            mappers.Select(m => m.GetConstructorParameter(constructor, columnName)).FirstOrDefault(r => r != null);

        public SqlMapper.IMemberMap? GetMember(string columnName) =>
            mappers.Select(m => m.GetMember(columnName)).FirstOrDefault(r => r != null);
    }
}

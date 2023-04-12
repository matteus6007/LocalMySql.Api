namespace LocalMySql.Api.Domain.Services
{
    public interface IMapper<in TSource, out TDest>
    {
        TDest Map(TSource data);
    }
}

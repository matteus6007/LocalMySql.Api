namespace LocalMySql.Api.Infrastructure.Configuration
{
    public class DatabaseOptions
    {
        public string Server { get; set; }

        public int? Port { get; set; }

        public string Schema { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}

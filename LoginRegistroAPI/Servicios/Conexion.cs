namespace LoginRegistroAPI.Servicios
{
    public class Conexion
    {
        private readonly string cadenaSQL = string.Empty;

        public Conexion()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public string GetCadenaSQL()
        {
            return cadenaSQL;
        }
    }

}


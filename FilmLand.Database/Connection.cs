namespace FilmLand.Database
{
    public static class Connection
    {
        public static string FilmLand()
        {
            var lines = File.ReadAllLines("../FilmLand_API/Database.txt");
            var connectionString = "data source=" + lines[0] + ";initial catalog=" + lines[1] + ";persist security info=True;user id=" + lines[2] + ";password=" + lines[3] + ";MultipleActiveResultSets=True;TrustServerCertificate=True;";
            return connectionString;
        }
    }
}

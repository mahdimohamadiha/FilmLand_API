namespace FilmLand.Database
{
    public static class Connection
    {
        public static string FilmLand()
        {
            var lines = File.ReadAllLines("../FilmLand_API/Database.txt");
            var connectionString = "Server="+ lines[0] +";Database="+ lines[1] +";TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true;";
            return connectionString;
        }
    }
}

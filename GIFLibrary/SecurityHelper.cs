using BCrypt.Net;
namespace GIFLibrary
{
    // A class to encrypt and dicrypt
    public class SecurityHelper
    {
        public static string GeneratePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);   //Not good enough, since if someone uses the same password it'll have the same hash
        }

        public static bool VerifyPassword(string password, string passwordHash) 
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }

        public static string GetDBConnectionString()
        {
            string connString = "Server = (localdb)\\MSSQLocalDB; Database = DoneInAGiffy; Trusted_Connection = true;";
            return connString;
        }
    }
}

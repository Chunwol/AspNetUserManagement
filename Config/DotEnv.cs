namespace AspNetUserManagement.Config
{
    public class DotEnv
    {
        public static string DatabaseIp
        {
            get { return DotNetEnv.Env.GetString("DATABASE_IP", "NULL"); }
        }

        public static string DatabaseName
        {
            get { return DotNetEnv.Env.GetString("DATABASE_NAME"); }
        }

        public static string DatabaseId
        {
            get { return DotNetEnv.Env.GetString("DATABASE_ID"); }
        }

        public static string DatabasePassword
        {
            get { return DotNetEnv.Env.GetString("DATABASE_PW"); }
        }
    }
}
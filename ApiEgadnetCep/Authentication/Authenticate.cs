namespace ApiEgadnetCep.Authentication
{
    public static class Authenticate
    {
        public static bool Authorize(User user)
        {
            if (user.Email == "root@gmail.com" && user.Password == "root123") 
                return true;
            return false;
        }
    }
}

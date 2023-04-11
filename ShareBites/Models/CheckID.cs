namespace ShareBites.Models
{
    public static class CheckID
    {

        public static string IDExists(ShareBitesContext ctx, string Username)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(Username))
            {
                var user = ctx.UserLogins.FirstOrDefault(
                    s => s.Username.ToLower() == Username.ToLower());
                if (user != null)
                    msg = $"username {Username} already in use.";
            }
            return msg;
        }

    }
}

namespace DataAccessLayer
{
    public static class AppUserClass
    {
        private static string appUserId;

        public static string AppUserId
        {
            get
            {
                return appUserId;
            }
            set
            {
                appUserId = value;
            }
        } // end property AppUserId
    } // end class AppUser
} // end namespace

using DataLayer;

namespace ServiceLayer
{
    public static class ContextGenerator
    {
        private static DiceMatchDbContext dbContext;
        private static MatchContext matchContext;
        private static CharacterContext characterContext;
        private static UserContext userContext;

        public static DiceMatchDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                SetDbContext();
            }
            return dbContext;
        }

        public static void SetDbContext()
        {
            dbContext = new DiceMatchDbContext();
        }

        public static MatchContext GetMatchesContext()
        {
            if (matchContext == null)
            {
                SetMatchesContext();
            }
            return matchContext;
        }

        public static void SetMatchesContext()
        {
            matchContext = new MatchContext(GetDbContext());
        }

        public static CharacterContext GetCharactersContext()
        {
            if (characterContext == null)
            {
                SetCharactersContext();
            }

            return characterContext;
        }

        public static void SetCharactersContext()
        {
            characterContext = new CharacterContext(GetDbContext());
        }

        public static UserContext GetUsersContext()
        {
            if (userContext == null)
            {
                SetUsersContext();
            }

            return userContext;
        }

        public static void SetUsersContext()
        {
            userContext = new UserContext(GetDbContext());
        }
    }
}

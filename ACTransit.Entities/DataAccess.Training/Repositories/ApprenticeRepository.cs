namespace ACTransit.DataAccess.Training.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ApprenticeRepository<T> : BaseRepository<T> where T:class, new()
    {
        public ApprenticeRepository() { }
        public ApprenticeRepository(string currentUserName)
        {
            CurrentUserName = currentUserName;
        }

    }
    
}

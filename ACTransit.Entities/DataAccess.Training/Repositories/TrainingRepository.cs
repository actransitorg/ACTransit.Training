namespace ACTransit.DataAccess.Training.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainingRepository<T> : BaseRepository<T> where T:class, new()
    {
        public TrainingRepository() { }
        public TrainingRepository(string currentUserName)
        {
            CurrentUserName = currentUserName;
        }
    }
    
}

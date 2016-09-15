using ACTransit.Framework.DataAccess;

namespace ACTransit.DataAccess.Training.Repositories
{
    public abstract class BaseRepository<T> : RepositoryBase<T, TrainingEntities> where T : class, new()
    {      
        protected BaseRepository()
            : this(new TrainingEntities()){}

        protected BaseRepository(TrainingEntities context)
        {            
        }

        


        protected TrainingEntities CreateContext()
        {
            return new TrainingEntities();
        }     
    }
}
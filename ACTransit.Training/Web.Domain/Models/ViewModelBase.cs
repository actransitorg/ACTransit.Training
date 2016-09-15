namespace ACTransit.Training.Web.Domain.Models
{
    public abstract class ViewModelBase
    {
        protected ViewModelBase(ViewModelState state )
        {
            State = state;
        }
        protected ViewModelBase() : this(ViewModelState.UnChanged) { }

        public ViewModelState State { get; set; }
    }

    public enum ViewModelState
    {
        New=0
        ,Edit=1
        ,UnChanged=2
    }
}

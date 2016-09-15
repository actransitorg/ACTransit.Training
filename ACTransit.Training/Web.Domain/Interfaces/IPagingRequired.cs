namespace ACTransit.Training.Web.Domain.Interfaces
{
    public interface IPagingRequired
    {
        int RowsPerPage { get; set; }
        int SkipRows { get; set; }
        long TotalRows { get; set; }
    }
}

namespace books_management.Utils;

public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalResults { get; set; }
}
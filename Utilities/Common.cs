namespace books_management.Utils;


// Pagination
public class PagedResponse<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalResults { get; set; }
}


// Response
public class Response
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public Response()
    {
        IsSuccess = false;
    }
}
namespace HospitalSystem.Application.Models;

public sealed class PaginatedList<T>
{
    public PaginatedList(
        IReadOnlyCollection<T> items,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(pageNumber),
                "Page number must be greater than zero.");

        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(pageSize),
                "Page size must be greater than zero.");

        if (totalCount < 0)
            throw new ArgumentOutOfRangeException(
                nameof(totalCount),
                "Total count cannot be negative.");

        Items = items ?? throw new ArgumentNullException(nameof(items));
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public IReadOnlyCollection<T> Items { get; }

    public int TotalCount { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public int TotalPages =>
        (int)Math.Ceiling(
            TotalCount / (double)PageSize);

    public bool HasPreviousPage =>
        PageNumber > 1;

    public bool HasNextPage =>
        PageNumber < TotalPages;
}

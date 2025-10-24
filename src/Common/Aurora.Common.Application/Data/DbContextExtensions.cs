namespace Aurora.Common.Application.Data;

public static class DbContextExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PagedViewRequest viewRequest,
        CancellationToken cancellationToken)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(viewRequest);

        int totalItems = await query.CountAsync(cancellationToken);

        List<T> items = await query
            .Skip((viewRequest.PageIndex - 1) * viewRequest.PageSize)
            .Take(viewRequest.PageSize)
            .ToListAsync(cancellationToken);

        int currentPage = totalItems > 0 ? viewRequest.PageIndex : 0;
        int totalPages = (int)Math.Ceiling(totalItems / (double)viewRequest.PageSize);

        ArgumentOutOfRangeException.ThrowIfLessThan(totalPages, currentPage);

        return new PagedResult<T>(
            items,
            totalItems,
            currentPage,
            totalPages);
    }
}
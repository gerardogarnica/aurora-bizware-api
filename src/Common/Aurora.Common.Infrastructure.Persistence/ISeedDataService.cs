using Microsoft.EntityFrameworkCore;

namespace Aurora.Common.Infrastructure.Persistence;

public interface ISeedDataService<in T> where T : DbContext
{
    void Seed(T context);
}
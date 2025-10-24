using Microsoft.AspNetCore.Routing;

namespace Aurora.Common.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
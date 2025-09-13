using Mapster;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Mapping;
public static class IssueMapsterConfiguration
{
    public static void Map()
    {
        TypeAdapterConfig<IssueDataObjectForUpdate, Issue>
            .NewConfig()
            .Ignore(dest => dest.CreatedDate);
    }
}

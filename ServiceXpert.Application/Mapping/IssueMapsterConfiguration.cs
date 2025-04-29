using Mapster;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Mapping;
public static class IssueMapsterConfiguration
{
    public static void Map()
    {
#pragma warning disable CS8603 // Possible null reference return.
        TypeAdapterConfig<IssueDataObjectForCreate, Issue>
            .NewConfig()
            .Ignore(dest => dest.ModifyDate);
#pragma warning restore CS8603 // Possible null reference return.

        TypeAdapterConfig<IssueDataObjectForUpdate, Issue>
            .NewConfig()
            .Ignore(dest => dest.CreateDate);
    }
}

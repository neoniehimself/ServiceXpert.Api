using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;

namespace ServiceXpert.Application.Services.Concretes.Issues;
internal class IssueCommentService : ServiceBase<Guid, IssueComment, IssueCommentDataObject>, IIssueCommentService
{
    private readonly IIssueCommentRepository issueCommentRepository;

    public IssueCommentService(IMapper mapper, IIssueCommentRepository issueCommentRepository) : base(mapper, issueCommentRepository)
    {
        this.issueCommentRepository = issueCommentRepository;
    }

    public async Task<ServiceResult<IEnumerable<IssueCommentDataObject>>> GetAllByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        var includeExpressions = new IncludeExpressions<IssueComment>()
        {
            c => c.CreatedByUser!,
            c => c.CreatedByUser!.SecurityProfile!
        };

        var comments = await this.issueCommentRepository.GetAllAsync(c => c.IssueId == IssueUtil.GetIdFromKey(issueKey), new IncludeOptions<IssueComment>(includeExpressions), cancellationToken);
        var commentsToReturn = comments.Adapt<ICollection<IssueCommentDataObject>>();

        return ServiceResult<IEnumerable<IssueCommentDataObject>>.Ok(commentsToReturn);
    }
}

using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;

namespace ServiceXpert.Application.Services.Concretes.Issues;

internal class IssueCommentService : ServiceBase<Guid, IssueComment, IssueCommentDataObject>, IIssueCommentService
{
    private readonly IIssueCommentRepository commentRepository;

    public IssueCommentService(IMapper mapper, IIssueCommentRepository commentRepository) : base(mapper, commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<ServiceResult<IEnumerable<IssueCommentDataObject>>> GetAllByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default)
    {
        var includeExpressions = new IncludeExpressions<IssueComment>()
        {
            c => c.CreatedByUser!,
            c => c.CreatedByUser!.SecurityProfile!
        };

        var comments = await this.commentRepository.GetAllAsync(
            new FilterOption<IssueComment>(c => c.IssueId == IssueUtil.GetIdFromKey(issueKey)),
            new IncludeOption<IssueComment>(includeExpressions),
            cancellationToken);

        var commentsToReturn = comments.Adapt<ICollection<IssueCommentDataObject>>();
        return ServiceResult<IEnumerable<IssueCommentDataObject>>.Ok(commentsToReturn);
    }
}

using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Concretes;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;

namespace ServiceXpert.Application.Services.Concretes.Issues;
public class CommentService : ServiceBase<Guid, IssueComment, IssueCommentDataObject>, IIssueCommentService
{
    private readonly IIssueCommentRepository commentRepository;

    public CommentService(IMapper mapper, IIssueCommentRepository commentRepository) : base(mapper, commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<ServiceResult<IEnumerable<IssueCommentDataObject>>> GetAllByIssueKeyAsync(string issueKey)
    {
        var comments = await this.commentRepository.GetAllAsync(c => c.IssueId == IssueUtil.GetIdFromKey(issueKey), new IncludeOptions<IssueComment>(c => c.CreatedByUser!));
        var commentsToReturn = comments.Adapt<ICollection<IssueCommentDataObject>>();

        return ServiceResult<IEnumerable<IssueCommentDataObject>>.Ok(commentsToReturn);
    }
}

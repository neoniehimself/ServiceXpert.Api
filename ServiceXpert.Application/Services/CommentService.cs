using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Repositories.Issues;

namespace ServiceXpert.Application.Services;
public class CommentService : ServiceBase<Guid, IssueComment, CommentDataObject>, ICommentService
{
    private readonly IIssueCommentRepository commentRepository;

    public CommentService(IIssueCommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<Result<IEnumerable<CommentDataObject>>> GetAllByIssueKeyAsync(int issueId)
    {
        var comments = await this.commentRepository.GetAllAsync(c => c.IssueId == issueId, new IncludeOptions<IssueComment>(c => c.CreatedByUser!));
        var commentsToReturn = comments.Adapt<ICollection<CommentDataObject>>();

        return Result<IEnumerable<CommentDataObject>>.Ok(commentsToReturn);
    }
}

using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;

namespace ServiceXpert.Application.Services;
public class CommentService : ServiceBase<Guid, Comment, CommentDataObject>, ICommentService
{
    private readonly ICommentRepository commentRepository;

    public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<Result<IEnumerable<CommentDataObject>>> GetAllByIssueKeyAsync(int issueId)
    {
        var comments = await this.commentRepository.GetAllAsync(c => c.IssueId == issueId, new IncludeOptions<Comment>(c => c.CreatedByUser!));
        var commentsToReturn = comments.Adapt<ICollection<CommentDataObject>>();

        return Result<IEnumerable<CommentDataObject>>.Ok(commentsToReturn);
    }
}

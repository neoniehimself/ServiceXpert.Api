using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;

namespace ServiceXpert.Application.Services;
public class CommentService : ServiceBase<Guid, Comment, CommentDataObject>, ICommentService
{
    private readonly ICommentRepository commentRepository;

    public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentDataObject>> GetAllByIssueKeyAsync(int issueId)
    {
        var comments = await this.commentRepository.GetAllAsync(c => c.IssueId == issueId);
        return comments.Adapt<ICollection<CommentDataObject>>();
    }
}

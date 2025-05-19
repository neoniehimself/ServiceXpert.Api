using MapsterMapper;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;

namespace ServiceXpert.Application.Services;
public class CommentService : ServiceBase<Guid, Comment, CommentDataObject>, ICommentService
{
    public CommentService(ICommentRepository commentRepository, IMapper mapper) : base(commentRepository, mapper)
    {
    }
}

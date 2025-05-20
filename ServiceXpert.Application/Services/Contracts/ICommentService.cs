using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Services.Contracts;
public interface ICommentService : IServiceBase<Guid, Comment, CommentDataObject>
{
}

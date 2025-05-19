using ServiceXpert.Application.DataObjects;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Services.Contracts;
public interface ICommentService : IServiceBase<Guid, Comment, CommentDataObject>
{
}

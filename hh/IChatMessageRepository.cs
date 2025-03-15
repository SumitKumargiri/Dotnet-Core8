using App.EnglishBuddy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Repositories
{
    public interface IChatMessageRepository : IBaseRepository<ChatMessageinfo>
    {
        Task<ChatMessageinfo?> GetByConnectionIdAsync(Guid connectionId, CancellationToken cancellationToken);
        Task AddAsync(ChatMessageinfo entity, CancellationToken cancellationToken);
        Task <List<ChatMessageinfo>>GetMessagesAsync(Guid senderId, Guid receiverId, Guid connectionId, CancellationToken cancellationToken);

        Task<List<ChatMessageinfo>> GetMessagesByUsernameAsync(string sendername, CancellationToken cancellationToken);
        Task<List<ChatMessageinfo>> GetMessagesByReceivernameAsync(string sendername, string receivername, CancellationToken cancellationToken);

    }
}

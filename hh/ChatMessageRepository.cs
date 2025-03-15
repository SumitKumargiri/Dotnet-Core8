using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Application.Services.Interface;
using App.EnglishBuddy.Domain.Entities;
using App.EnglishBuddy.Infrastructure.Context;
using Google;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace App.EnglishBuddy.Infrastructure.Repositories
{
    public class ChatMessageRepository : BaseRepository<ChatMessageinfo>,IChatMessageRepository
    {
        private readonly EnglishBuddyDbContext _context;

        public ChatMessageRepository(EnglishBuddyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ChatMessageinfo?> GetByConnectionIdAsync(Guid connectionId, CancellationToken cancellationToken)
        {
            return await _context.ChatMessages.FirstOrDefaultAsync(c => c.ConnectionId == connectionId, cancellationToken);
        }

        public async Task AddAsync(ChatMessageinfo entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.ChatMessages.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<ChatMessageinfo>> GetMessagesAsync(Guid senderId, Guid receiverId, Guid connectionId, CancellationToken cancellationToken)
        {
            return await _context.ChatMessages.Where(c => (c.SenderId == senderId && c.ReceiverId == receiverId && c.ConnectionId==connectionId) || (c.SenderId == receiverId && c.ReceiverId == senderId && c.ConnectionId==connectionId))
                                                .OrderBy(c => c.CreatedDate).ToListAsync(cancellationToken);
        }



        public async Task<List<ChatMessageinfo>> GetMessagesByUsernameAsync(string sendername, CancellationToken cancellationToken)
        {
            var query = _context.ChatMessages.Where(c => c.SenderName == sendername);

            //if (connectionId.HasValue)
            //{
            //    query = query.Where(c => c.ConnectionId == connectionId.Value);
            //}

            return await query.OrderBy(c => c.CreatedDate).ToListAsync(cancellationToken);
        }

        public async Task<List<ChatMessageinfo>> GetMessagesByReceivernameAsync(string sendername, string receivername, CancellationToken cancellationToken)
        {
            var query = _context.ChatMessages.Where(c => (c.ReceiverName == receivername && c.SenderName==sendername) || (c.ReceiverName==sendername && c.SenderName==receivername));
            return await query.OrderBy(c => c.CreatedDate).ToListAsync(cancellationToken);
        }


    }
}

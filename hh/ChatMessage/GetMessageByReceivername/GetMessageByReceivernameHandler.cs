using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessageByUsername;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetMessageByReceivername
{
    public class GetMessageByReceivernameHandler : IRequestHandler<GetMessageByReceivernameRequest, List<ChatMessageinfo>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public GetMessageByReceivernameHandler(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<List<ChatMessageinfo>> Handle(GetMessageByReceivernameRequest request, CancellationToken cancellationToken)
        {
            return await _chatMessageRepository.GetMessagesByReceivernameAsync(request.SenderName, request.ReceiverName, cancellationToken);
        }
    }
}

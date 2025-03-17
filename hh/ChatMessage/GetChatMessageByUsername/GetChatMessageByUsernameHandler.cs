using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessages;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessageByUsername
{
    public class GetChatMessageByUsernameHandler : IRequestHandler<GetChatMessageByUsernameRequest, List<ChatMessageinfo>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public GetChatMessageByUsernameHandler(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<List<ChatMessageinfo>> Handle(GetChatMessageByUsernameRequest request, CancellationToken cancellationToken)
        {
            return await _chatMessageRepository.GetMessagesByUsernameAsync(request.SenderName, cancellationToken);
        }

    }
}

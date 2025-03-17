using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.CreateChatMessages;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

public class ChatMessageHandler : IRequestHandler<ChatMessageRequest, ChatMessageResponse>
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatMessageHandler(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatMessageResponse> Handle(ChatMessageRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Message))
        {
            return new ChatMessageResponse { Status = "Error", Message = "Message content is required" };
        }

        var chatMessage = new ChatMessageinfo
        {
            SenderName = request.SenderName,
            ReceiverName = request.ReceiverName,
            SenderId = request.SenderId,
            ReceiverId = request.ReceiverId,
            Message = request.Message,
            ConnectionId = request.ConnectionId,
            //Time = DateTime.Now,
            CreatedDate = DateTime.Now,
            Createdby = request.Createdby,
            UpdateDate = DateTime.Now,
            Updatedby = request.Updatedby,
            IsActive = request.IsActive

        };

        await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);

        return new ChatMessageResponse { Status = "Success", Message = "Message sent successfully" };
    }
}

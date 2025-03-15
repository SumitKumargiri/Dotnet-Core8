using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.CreateChatMessages;
using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessageByUsername;
using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessages;
using App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetMessageByReceivername;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.EnglishBuddy.API.Controllers
{
    [ApiController]
    [Route("chat")]
    public class ChatMessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatMessageController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost("send")]
        public async Task<ActionResult<ChatMessageResponse>> SendMessage([FromBody] ChatMessageRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("messages")]
        public async Task<ActionResult<List<ChatMessageinfo>>> GetMessages([FromQuery] Guid senderId, [FromQuery] Guid receiverId, [FromQuery] Guid connectionId, CancellationToken cancellationToken)
        {
            var query = new GetChatMessagesRequest { SenderId = senderId, ReceiverId = receiverId, ConnectionId=connectionId };
            var messages = await _mediator.Send(query, cancellationToken);
            return Ok(messages);
        }


        [HttpGet("getbyusername")]
        public async Task<ActionResult<List<ChatMessageinfo>>> GetMessages([FromQuery] string sendername,CancellationToken cancellationToken)
        {
            var query = new GetChatMessageByUsernameRequest
            {
                SenderName = sendername,
                //ConnectionId = connectionId ?? Guid.Empty
            };

            var messages = await _mediator.Send(query, cancellationToken);
            return Ok(messages);
        }


        [HttpGet("getbyreceivername")]
        public async Task<ActionResult<List<ChatMessageinfo>>> GetMessage([FromQuery] string sendername, [FromQuery] string receivername, CancellationToken cancellationToken)
        {
            var query = new GetMessageByReceivernameRequest
            {
                SenderName = sendername,
                ReceiverName = receivername,
            };

            var messages = await _mediator.Send(query, cancellationToken);
            return Ok(messages);
        }

    }
}

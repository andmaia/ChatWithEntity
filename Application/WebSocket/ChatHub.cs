using Application.Crosscuting.DTO.Message;
using Application.Crosscuting.DTO.TalkToUser;
using Application.Domain.Service;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Application.WebSocket
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task JoinRoom(TalkToUserResponse talkToUserResponse)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, talkToUserResponse.IdTalk);

            var oldMessages = await _messageService.GetAllbyTalk(talkToUserResponse.IdTalk);
            await Clients.Caller.SendAsync("ReceiveOldMessages", oldMessages);
        }

        public async Task SendMessage(MessageRequest messageRequest)
        {
            // Salvar a mensagem no banco de dados
            var message = await _messageService.CreateMessage(messageRequest);

            // Envie a mensagem para o grupo associado ao talkId
            await Clients.Group(messageRequest.TalkId).SendAsync("ReceiveMessage", message);
        }

        // Outros métodos para lidar com a saída do usuário, desconexão, etc. (não mostrados aqui)
    }
}
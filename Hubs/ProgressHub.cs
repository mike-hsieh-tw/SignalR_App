using Microsoft.AspNetCore.SignalR;

namespace SignalR_App.Hubs
{   

    public class ProgressHub : Hub
    {
        /// <summary>
        /// 連線事件
        /// 方法 OnConnectedAsync() 是每一個用戶連線後，都會觸發的事件，每次連線會得到 Context.ConnectionId，我們就把 Context.ConnectionId 回傳到前端。
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            // 回傳連線ID
            await Clients.Client(Context.ConnectionId).SendAsync("SetHubConnId", Context.ConnectionId);

            await base.OnConnectedAsync();
        }
    }
}

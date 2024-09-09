using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Home.Client.Services.Interfaces
{
    public interface IMqttService
    {
        Task SendMessageAsync(string topic, string msg);
    }
}

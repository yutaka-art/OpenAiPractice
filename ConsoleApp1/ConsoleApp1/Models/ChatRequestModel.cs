using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class MessageObject
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class ChatRequestModel
    {
        public List<MessageObject> Messages { get; set; }
        public int PastMessages { get; set; }
        public double Temperature { get; set; }
        public double TopP { get; set; }
        public int FrequencyPenalty { get; set; }
        public int PresencePenalty { get; set; }
        public int MaxTokens { get; set; }
        public object Stop { get; set; } // nullを許容するためにobject型にしています
    }

}

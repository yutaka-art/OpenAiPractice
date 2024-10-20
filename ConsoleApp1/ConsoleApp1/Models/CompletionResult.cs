using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class CompletionResult
    {
        public string? id { get; set; }
        public string? created { get; set; }
        public string? model { get; set; }
        public Choice[]? choices { get; set; }
        public Usage? usage { get; set; }


        public class Choice
        {
            public string? text { get; set; }
        }

        public class Usage
        {
            public int? completion_tokens { get; set; }
            public int? prompt_tokens { get; set; }
            public int? total_tokens { get; set; }
        }

    }
}

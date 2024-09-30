using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;
using System.ClientModel;

namespace ConsoleApp1
{
    internal class Program
    {
        // Azure OpenAI Service のエンドポイント
        const string OpenAIEndpoint = "https://oai-playground-osa-002.openai.azure.com/";
        // 使いたいモデルのデプロイ名
        const string ModelDeployName = "SampleChatModel01";
        // Azure OpenAI Service の API キー
        const string ApiKey = "ceaedacb49c64657a502f1082abc8725"; // ここに取得した API キーを入力

        public static async Task Main(string[] args)
        {
            await Chapter03_14_01();
        }

        private static void Chapter03_07()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var response = client.GetChatClient(ModelDeployName).CompleteChatAsync(prompt).Result;

            Console.Write("Result: ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_08_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var response = client.GetChatClient(ModelDeployName).CompleteChatAsync(prompt).Result;

                    Console.Write("Result: ");
                    Console.WriteLine(response.Value);
                }
            }
        }

        private static void Chapter03_08_02()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    var response = client.GetChatClient(ModelDeployName).CompleteChatAsync(chatHistory).Result;
                    chatHistory.Add(ChatMessage.CreateAssistantMessage(response.Value.Content.Last().Text));

                    Console.Write("Result: ");
                    Console.WriteLine(response.Value.Content.Last().Text);
                }
            }
        }

        private static void Chapter03_09_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateSystemMessage("あなたがフランス語翻訳アシスタントです。ユーザーが入力した文をすべてフランス語にします。"));

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    var response = client.GetChatClient(ModelDeployName).CompleteChatAsync(chatHistory).Result;
                    chatHistory.Add(ChatMessage.CreateAssistantMessage(response.Value.Content.Last().Text));

                    Console.Write("Result: ");
                    Console.WriteLine(response.Value.Content.Last().Text);
                }
            }
        }

        private static void Chapter03_09_02()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateSystemMessage("あなたは武家アシスタントです。武家言葉で会話します。"));
            chatHistory.Add(ChatMessage.CreateUserMessage("こんにちは。あなたの名前は？"));
            chatHistory.Add(ChatMessage.CreateAssistantMessage("お初にお目にかかり申す。拙者、OpenAIと申すものでござる。"));

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    var response = client.GetChatClient(ModelDeployName).CompleteChatAsync(chatHistory).Result;

                    Console.Write("Result: ");
                    Console.WriteLine(response.Value.Content.Last().Text);
                }
            }
        }

        private static void Chapter03_10_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 100;
            options.StopSequences.Add("。");
            options.StopSequences.Add(".");

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    var response = client.GetChatClient(ModelDeployName)
                                .CompleteChatAsync(chatHistory, options).Result;
                    chatHistory.Add(ChatMessage.CreateAssistantMessage(response.Value.Content.Last().Text));

                    Console.Write("Result: ");
                    Console.WriteLine(response.Value.Content.Last().Text);
                }
            }
        }

        private static void Chapter03_11_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.Temperature = 1.0f;

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_11_02()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            // NucleusSamplingFactor＝TopP
            options.TopP = 1.0f;

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_12_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.FrequencyPenalty = 2.0f;

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_12_02()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.PresencePenalty = 2.0f;

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_12_03()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.LogitBiases.Add(13068, 10);
            options.LogitBiases.Add(new KeyValuePair<int, int>(14783, 12));

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static void Chapter03_12_04()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(ChatMessage.CreateUserMessage(prompt));

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 1000;
            options.LogitBiases.Add(13068, -100);
            options.LogitBiases.Add(new KeyValuePair<int, int>(14783, -100));

            var response = client.GetChatClient(ModelDeployName)
                        .CompleteChatAsync(chatHistory, options).Result;

            Console.Write("[Result] ");
            Console.WriteLine(response.Value);
        }

        private static async Task Chapter03_13_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();

            var options = new ChatCompletionOptions();


            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    var response = client.GetChatClient(ModelDeployName)
                            .CompleteChatStreamingAsync(chatHistory, options);

                    await foreach (var chunk in response)
                    {
                        foreach (var content in chunk.ContentUpdate)
                        {
                            Console.Write(content.Text);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        private static async Task Chapter03_14_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            var chatClient = client.GetChatClient(ModelDeployName);
            var chatHistory = new List<ChatMessage>();

            var options = new ChatCompletionOptions();

            bool flag = true;

            // チャットを開始
            while (flag)
            {
                Console.Write("prompt: ");
                string prompt = Console.ReadLine() ?? "Hello.";

                if (prompt == "")
                {
                    flag = false;
                }
                else
                {
                    var targetMessage = ChatMessage.CreateUserMessage(prompt);
                    chatHistory.Add(targetMessage);

                    AsyncCollectionResult<StreamingChatCompletionUpdate> response = client.GetChatClient(ModelDeployName)
                            .CompleteChatStreamingAsync(chatHistory, options);

                    await foreach (var chunk in response)
                    {
                        foreach (var content in chunk.ContentUpdate)
                        {
                            Console.Write(content.Text);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }


        private async Task Sample()
        {
            var systemPrompt = "あなたは会話友達です。";
            //var systemPrompt = "あなたはフランス語翻訳アシスタントです。ユーザーが入力した文を全てフランス語にします。";
            //var systemPrompt = "あなたは武士アシスタントです。武家言葉で会話します。";


            // OpenAI クライアントの初期化
            var openAIClient = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new AzureKeyCredential(ApiKey)
            );

            // チャットクライアントの取得
            var chatClient = openAIClient.GetChatClient(ModelDeployName);

            // システムメッセージの作成
            var systemMessage = ChatMessage.CreateSystemMessage(systemPrompt);
            //var w1 = ChatMessage.CreateUserMessage("こんにちは。あなたの名前は？");
            //var w2 = ChatMessage.CreateAssistantMessage("お初にお目にかかり申す。拙者、OpenAIと申すものでござる。");

            // チャット履歴リストの初期化
            var chatHistory = new List<ChatMessage>();
            chatHistory.Add(systemMessage);
            //chatHistory.Add(w1);
            //chatHistory.Add(w2);

            var options = new ChatCompletionOptions();
            options.MaxOutputTokenCount = 100;
            options.StopSequences.Add("。");
            options.StopSequences.Add(".");

            // チャットを開始
            while (true)
            {
                // ユーザー入力を待機
                Console.Write("AI >>> 質問をどうぞ。(終了は Ctrl+C)\n\nUser >>> ");
                var userInput = Console.ReadLine() ?? string.Empty;

                // ユーザー入力をまず会話履歴に追加
                {
                    var userGreetingMessage = ChatMessage.CreateUserMessage(userInput);
                    chatHistory.Add(userGreetingMessage);
                }

                // AI からの回答に時間がかかるので、ニューザー入力を受け付けたことを表示
                Console.WriteLine("\nSYSTEM >>> 質問を受け付けました。AI に問い合わせています......");

                // AI に会話履歴 (今回のユーザー質問も格納済み) 送信。回答を待機。回答を会話履歴に追加。回答をコンソール表示
                {
                    var response = await chatClient.CompleteChatAsync(chatHistory, options);
                    var aiMessage = response.Value.Content.Last().Text;

                    var assistantMessage = ChatMessage.CreateAssistantMessage(aiMessage);
                    chatHistory.Add(assistantMessage);
                    Console.WriteLine($"\nAI >>> {aiMessage}\n");
                }
            }
        }

    }
}

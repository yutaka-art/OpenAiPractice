using Azure.AI.OpenAI;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using OpenAI.Chat;
using OpenAI.Images;
using System.ClientModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ConsoleApp1
{
    internal class Program
    {
        // Azure OpenAI Service のエンドポイント
        const string OpenAIEndpoint = "https://oai-playground-osa-002.openai.azure.com/";
        const string OpenAIEndpoint_Full = "https://oai-playground-osa-002.openai.azure.com/openai/deployments/SampleChatModel01/completions?api-version=2024-09-01-preview";
        // 使いたいモデルのデプロイ名
        const string ModelDeployName = "SampleChatModel01";
        // Azure OpenAI Service の API キー
        const string ApiKey = "ceaedacb49c64657a502f1082abc8725"; // ここに取得した API キーを入力

        public static async Task Main(string[] args)
        {
            await Chapter06_08_01();
        }

        private static void Chapter03_07()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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
                new ApiKeyCredential(ApiKey)
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


        private static async Task Chapter04_03_01()
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
            );

            ImageClient client = azureClient.GetImageClient("Dalle3");
            ClientResult<GeneratedImage> imageResult = await client.GenerateImageAsync("The beginning of the world and the budgie in a Chojugiga style", new()
            {
                Quality = GeneratedImageQuality.Standard,
                Size = GeneratedImageSize.W1024xH1024,
                ResponseFormat = GeneratedImageFormat.Uri
            });

            // Image Generations responses provide URLs you can use to retrieve requested images
            GeneratedImage image = imageResult.Value;
            Console.WriteLine($"Image URL: {image.ImageUri}");

        }

        private static async Task Chapter04_08_03()
        {
            var azureClient = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
            );

            ImageClient client = azureClient.GetImageClient("Dalle3");
            ClientResult<GeneratedImage> imageResult = await client.GenerateImageAsync("cat", new()
            {
                Quality = GeneratedImageQuality.Standard,
                Size = GeneratedImageSize.W1024xH1024,
                ResponseFormat = GeneratedImageFormat.Bytes
            });

            // Image Generations responses provide URLs you can use to retrieve requested images
            GeneratedImage image = imageResult.Value;
            BinaryData bytes = image.ImageBytes;
            using FileStream stream = File.OpenWrite($"{Guid.NewGuid()}.png");
            bytes.ToStream().CopyTo(stream);

            Console.WriteLine($"Image URL: {image.ImageUri}");
        }

        private static async Task Chapter05_02_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            var response = client.GetEmbeddingClient("SampleEmbeddingModel01");

            //var values = await response.GenerateEmbeddingsAsync(new List<string> { prompt });
            //var values = await response.GenerateEmbeddingsAsync(new List<string> { "試しにEmbeddingしてみる。" });
            var values = response.GenerateEmbedding("試しにEmbeddingしてみる。");

            //var generateEmbeddingsResult = response.GenerateEmbeddings(["にゃーん"]);
            var embedding = values;

            var dataArray = embedding.Value.ToFloats().ToArray();
            foreach (var value in dataArray)
            {
                Console.WriteLine(value);
            }

        }


        private static async Task Chapter05_04_01()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
            );

            Console.Write("title: ");
            var title = Console.ReadLine() ?? "Hello.";
            Console.Write("description: ");
            var description = Console.ReadLine() ?? "Hello.";

            var response = client.GetEmbeddingClient("SampleEmbeddingModel01");

            var values = response.GenerateEmbedding(description);
            var embed = values.Value.ToFloats().ToArray();
            var embedString = new System.Text.StringBuilder();

            embedString.Append("[");
            foreach (var value in embed)
            {
                embedString.Append($"{value},");
            }
            // 最後のカンマを削除
            embedString.Length--;
            embedString.Append("]");

            Console.Write(embedString.ToString());

            var desctop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fpath = Path.Combine(desctop, "output.csv");

            try
            {
                using (StreamWriter writer = new StreamWriter(fpath, true, System.Text.Encoding.UTF8, 20000))
                {
                    // embedStringをダブルクオートで囲む
                    var data = $"{title},{description},\"{embedString.ToString()}\"";
                    writer.Write(data);
                    writer.WriteLine();
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"CSVファイル読み込みエラー：{ex.Message}");
            }
        }


        private static async Task Chapter05_05_02()
        {
            var client = new AzureOpenAIClient(
                new Uri(OpenAIEndpoint),
                new ApiKeyCredential(ApiKey)
            );

            Console.Write("prompt: ");
            var prompt = Console.ReadLine() ?? "Hello.";

            var response = client.GetEmbeddingClient("SampleEmbeddingModel01");

            var values = response.GenerateEmbedding(prompt);
            float[]? embedded_data = values.Value.ToFloats().ToArray();
            var embedString = new System.Text.StringBuilder();

            embedString.Append("[");
            foreach (var value in embedded_data)
            {
                embedString.Append($"{value},");
            }
            // 最後のカンマを削除
            embedString.Length--;
            embedString.Append("]");

            //Console.Write(embedString.ToString());

            var desctop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var fpath = Path.Combine(desctop, "output.csv");

            // データ保管用のリストの用意
            var embedding_data = new List<EmbeddingData>();

            try
            {
                using (StreamReader reader = new StreamReader(fpath, System.Text.Encoding.UTF8))
                {
                    string? line;
                    _= reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        // 行データをカンマで分割しJSONフォーマットにする
                        var arr = line.Split(',');

                        // embedding 部分の前後のダブルクォートを削除し、JSONで正しく解釈されるようにする
                        var target = String.Join(",", arr.Skip(2)).Trim('"');
                        var json_str = $"{{\"title\":\"{arr[0]}\",\"description\":\"{arr[1]}\",\"embedding\":{target} }}";

                        // JSONデータからEmbeddingDataを生成
                        var data = JsonConvert.DeserializeObject<EmbeddingData>(json_str);
                        if (data != null)
                        {
                            embedding_data.Add(data);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"CSVファイル読み込みエラー：{ex.Message}");
            }

            // もっとも値の高いデータを保管しておく変数
            EmbeddingData? select = null;
            double last_CS = 0;

            // コサイン類似度を計算する
            foreach (EmbeddingData embeded_item in embedding_data)
            {
                Double CS = GetCosineSimilarity(embedded_data, embeded_item.embedding);
                if (CS > last_CS)
                {
                    last_CS = CS;
                    select = embeded_item;
                }
            }

            // 結果を表示
            Console.Write("Result: ");
            if (select != null)
            {
                Console.WriteLine(select.title);
            }
            else
            {
                Console.WriteLine("Not Found");
            }
        }

        private static double GetCosineSimilarity(float[]? vector1, float[]? vector2)
        {
            if (vector1 == null || vector2 == null)
            {
                return 0;
            }

            int count = vector1.Count() < vector2.Count() ? vector1.Count() : vector2.Count();
            double dot = 0.0d;
            double mag1 = 0.0d;
            double mag2 = 0.0d;

            for (int n = 0; n < count; n++)
            {
                dot += vector1[n] * vector2[n];
                mag1 += Math.Pow(vector1[n], 2);
                mag2 += Math.Pow(vector2[n], 2);
            }
            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }


        private static async Task Chapter06_08_01()
        {
            Console.Write("prompt: ");
            string prompt = Console.ReadLine() ?? "Hello.";

            // HttpClientの作成
            var httpClient = new HttpClient();
            // ヘッダ情報の設定
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("UTF-8"));
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ja-JP"));
            httpClient.DefaultRequestHeaders.Add("api-key", ApiKey);

            // ボディコンテンツの作成
            //var requestBody = new ChatRequestModel
            //{
            //    Messages = new List<MessageObject>
            //    {
            //        new MessageObject
            //        {
            //            Role = "user",
            //            Content = prompt
            //        }
            //    },
            //    PastMessages = 10,
            //    Temperature = 0.7,
            //    TopP = 0.95,
            //    FrequencyPenalty = 0,
            //    PresencePenalty = 0,
            //    MaxTokens = 800,
            //    Stop = null
            //};

            var requestBody = new
            {
                prompt = prompt,
                max_tokens = 100,
            };

            // JsonContentの作成 (Content-Typeはここで自動的に設定される)
            var json_content = JsonContent.Create(requestBody);
            try
            {
                // END_POINTにPOSTアクセスする
                using (var response = await httpClient.PostAsync(OpenAIEndpoint_Full, json_content))
                {
                    // ステータスコードが200番台以外の場合はエラー
                    response.EnsureSuccessStatusCode();

                    // 応答からテキストを取り出す
                    var result = await response.Content.ReadAsStringAsync();
                    // JSONテキストをオブジェクトに変換
                    CompletionResult? json_data = JsonConvert.DeserializeObject<CompletionResult>(result);
                    // 応答のテキストを表示
                    Console.WriteLine(json_data?.choices?[0].text);
                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTPリクエストエラー：{ex.Message}");
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
                new ApiKeyCredential(ApiKey)
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

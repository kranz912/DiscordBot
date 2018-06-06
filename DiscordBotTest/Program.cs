using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using DiscordBotTest.MemeGenerator;
namespace DiscordBotTest
{
    class Program
    {
        private DiscordSocketClient _client;
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;
            _client.UserJoined += AnnounceJoinedUser;
            _client.UserLeft += AnnounceUserLeft;
            await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("discordtoken"));
            await _client.StartAsync();
            
            await Task.Delay(-1);

        }
        public Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
        public Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser} is connected");
           
            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;
            if(message.Content == "!pogisikranz")
            {
                await message.Channel.SendMessageAsync("tama!");
            }
            if(message.Content == "!roll" || message.Content =="!dice")
            {
                await message.Channel.SendMessageAsync($"{message.Author.Username} rolled {Games.Dice.roll()}");
            }
            if (message.Content.Contains("!kiss"))
            {
                var users = message.MentionedUsers;
                foreach(var u in users)
                {
                    await message.Channel.SendMessageAsync($"{message.Author.Username} kissed {u.Mention}");
                }
                
            }
            if (message.Content.Contains("!slap"))
            {
                var users = message.MentionedUsers;
                foreach (var u in users)
                {
                    await message.Channel.SendMessageAsync($"Sinampal ni {message.Author.Username} si {u.Mention} sa mukha ");
                }
            }
            if(message.Content == "!meme")
            {
                var res = Generator.GetFromApiAsync("https://api.imgflip.com/get_memes");
                Console.WriteLine(res.Result.data.memes[0].url);
                Random random = new Random();
                int r = random.Next(0, 100);
                await Generator.downloadfile(res.Result.data.memes[r].url);
                await message.Channel.SendFileAsync("temp.jpg",res.Result.data.memes[r].name);

            }
        }
        public async Task AnnounceJoinedUser (SocketGuildUser user)
        {
            var channel = _client.GetChannel(330240114921963520) as SocketTextChannel;
            await channel.SendMessageAsync($"Welcome sa bikini gaming {user.Mention}. Maligo ka everyday!");
        }
        public async Task AnnounceUserLeft(SocketGuildUser user)
        {
            var channel = _client.GetChannel(330240114921963520) as SocketTextChannel;
            await channel.SendMessageAsync($"Bye bye {user.Mention}. Balik ka ha! Maligo ka everyday!");

        }
       
    }
}

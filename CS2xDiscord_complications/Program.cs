using CS2xDiscord_complications;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using WindowsInput;

class Program
{
    DiscordSocketClient client;
    private InputSimulator sim = new();
    
    private bool isWaitingForChoise = false;
    private int SecsForAllEvents = 15;
    private int SecsForCooldown = 15;
    private int HolderSize = 2;

    private ulong HolderMsgID = 0;
    private ulong EventMsgID = 0;
    
    private List<Event> Holder = new ();

    static void Main(string[] args)
    {
        Console.WriteLine("Выберите команду (1 или 2):");
        char i = Console.ReadLine().ToString()[0];

        if (i == '1')
        {
            new Program().MainAsync("here discord bot token")
                .GetAwaiter().GetResult();
        }else if (i == '2')
        {
            new Program().MainAsync("here discord bot token for team 2")
                .GetAwaiter().GetResult();
        }
    }

    private async Task MainAsync(string token_)
    {
        client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.All,
            UseInteractionSnowflakeDate = false
        });
        
        client.Log += Log;
        client.ButtonExecuted += btnExec;
        client.MessageReceived += msgRecived;

        var token = token_;

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        Console.ReadLine();

        await client.StopAsync();
        await client.LogoutAsync();
    }

    private Task msgRecived(SocketMessage arg)
    {
        if (arg.Content == "test")
        {
            var btnBuilder = new ComponentBuilder();
            var emBuilder = new EmbedBuilder();
            int chosed_events = 0;
            List<string> ids_of_events = new List<string>();
            while (chosed_events < 4)
            {
                foreach (var event_ in Events.GetEvents())
                {
                    if (!ids_of_events.Contains(event_.id))
                    {
                        int rand = new Random().Next(0, 100);
                        if (event_.lvlOfRarity == 0 && rand > 50)
                        {
                            btnBuilder.WithButton(event_.Name, event_.id);
                            emBuilder.WithDescription(emBuilder.Description +
                                                      $"\n{event_.Name} - {event_.Description}");
                            chosed_events++;
                            ids_of_events.Add(event_.id);
                        }
                        else if (event_.lvlOfRarity == 1 && rand > 75)
                        {
                            btnBuilder.WithButton(event_.Name, event_.id);
                            emBuilder.WithDescription(emBuilder.Description +
                                                      $"\n{event_.Name} - {event_.Description}");
                            chosed_events++;
                            ids_of_events.Add(event_.id);
                        }
                        else if (event_.lvlOfRarity == 2 && rand > 85)
                        {
                            btnBuilder.WithButton(event_.Name, event_.id);
                            emBuilder.WithDescription(emBuilder.Description +
                                                      $"\n{event_.Name} - {event_.Description}");
                            chosed_events++;
                            ids_of_events.Add(event_.id);
                        }
                        else if (event_.lvlOfRarity == 3 && rand > 95)
                        {
                            btnBuilder.WithButton(event_.Name, event_.id);
                            emBuilder.WithDescription(emBuilder.Description +
                                                      $"\n{event_.Name} - {event_.Description}");
                            chosed_events++;
                            ids_of_events.Add(event_.id);
                        }
                    }
                }
            }

            var emBuilder2 = new EmbedBuilder()
                .WithTitle("Холдер. Начало игры")
                .WithDescription("Здесь будут описания ваших карт")
                .WithColor(Color.Green);
            
            emBuilder.WithTitle("Дроп, выберите ивент");
            emBuilder.WithColor(Color.Blue);
            
            HolderMsgID = arg.Channel.SendMessageAsync("", embed: emBuilder2.Build()).GetAwaiter().GetResult().Id;
            EventMsgID = arg.Channel.SendMessageAsync(" ", embed: emBuilder.Build(), components: btnBuilder.Build()).GetAwaiter().GetResult().Id;
        }
        return Task.CompletedTask;
    }

    private async Task btnExec(SocketMessageComponent arg)
    {
        await arg.DeferAsync();
        if (Events.GetEvents().FirstOrDefault(e => "use_"+e.id == arg.Data.CustomId) != null)
        {
            //await arg.FollowupAsync();
            var e = Events.GetEvents().FirstOrDefault(e => "use_"+e.id == arg.Data.CustomId);
            if (arg.Data.CustomId == "use_moreholdsize")
            {
                HolderSize += 1;
            }
            else if (arg.Data.CustomId == "use_srochnyi_zakaz")
            {
                if (SecsForCooldown >= 5)
                {
                    SecsForCooldown -= 5;
                }
            }
            else
            {
                e?.ExecThread();
            }
            
            int index = Holder.FindIndex(i => i.id == e.id);
            if (index != -1)
            {
                // Удаление элемента по найденному индексу
                Holder.RemoveAt(index);
            }
            
            var btnBuilder = new ComponentBuilder();
            var emBuilder = new EmbedBuilder();

            foreach (var ev in Holder)
            {
                emBuilder.WithDescription(emBuilder.Description + $"\n{ev.Name} - {ev.Description}");
                btnBuilder.WithButton(ev.Name, "use_"+ev.id);
            }

            emBuilder.WithTitle($"Холдер. Изспользованно {Holder.Count} из {HolderSize}");
            emBuilder.WithColor(Color.Green);

            await arg.Message.ModifyAsync(m =>
            {
                m.Embed = emBuilder.Build();
                m.Components = btnBuilder.Build();
                m.Content = "";
            });
        }
        else if (Events.GetEvents().FirstOrDefault(e => e.id == arg.Data.CustomId) != null)
        {
            var e = Events.GetEvents().FirstOrDefault(e => e.id == arg.Data.CustomId);
            if (Holder.Count < HolderSize)
            {
                if (!Holder.Contains(e))
                {
                    Holder.Add(e);
                }

                if (true)
                {
                    var btnBuilder = new ComponentBuilder();
                    var emBuilder = new EmbedBuilder();

                    foreach (var ev in Holder)
                    {
                        emBuilder.WithDescription(emBuilder.Description + $"\n{ev.Name} - {ev.Description}");
                        btnBuilder.WithButton(ev.Name, "use_"+ev.id);
                    }

                    emBuilder.WithTitle($"Холдер. Изспользованно {Holder.Count} из {HolderSize}");
                    emBuilder.WithColor(Color.Green);
                    
                    //TODO: При одинаковых предметах ерноеться
                    var msg = await arg.Message.Channel.GetMessageAsync(HolderMsgID) as RestUserMessage;
                    await msg!.ModifyAsync(m =>
                    {
                        m.Embed = emBuilder.Build();
                        m.Components = btnBuilder.Build();
                        m.Content = "";
                    });
                }

                //await arg.FollowupAsync();
                if (true)
                {
                    new Thread(() =>
                    {
                        try
                        {
                            int i = SecsForCooldown;

                            arg.Message.DeleteAsync().GetAwaiter().GetResult();

                            var msg = arg.Channel.SendMessageAsync("Ожидайте").GetAwaiter().GetResult();
                            while (i > 0)
                            {
                                msg.ModifyAsync(m => m.Content = $"До следующего дропа осталось **{i} сек**");
                                Thread.Sleep(1000);
                                i--;
                            }

                            msg.DeleteAsync();

                            var btnBuilder = new ComponentBuilder();
                            var emBuilder = new EmbedBuilder();
                            int chosed_events = 0;
                            List<string> ids_of_events = new List<string>();
                            while (chosed_events < 4)
                            {
                                foreach (var event_ in Events.GetEvents())
                                {
                                    if (!ids_of_events.Contains(event_.id))
                                    {
                                        if (event_.lvlOfRarity == 0 && new Random().Next(0, 100) > 50)
                                        {
                                            btnBuilder.WithButton(event_.Name, event_.id);
                                            emBuilder.WithDescription(emBuilder.Description +
                                                                      $"\n{event_.Name} - {event_.Description}");
                                            chosed_events++;
                                            ids_of_events.Add(event_.id);
                                        }
                                        else if (event_.lvlOfRarity == 1 && new Random().Next(0, 100) > 75)
                                        {
                                            btnBuilder.WithButton(event_.Name, event_.id);
                                            emBuilder.WithDescription(emBuilder.Description +
                                                                      $"\n{event_.Name} - {event_.Description}");
                                            chosed_events++;
                                            ids_of_events.Add(event_.id);
                                        }
                                        else if (event_.lvlOfRarity == 2 && new Random().Next(0, 100) > 85)
                                        {
                                            btnBuilder.WithButton(event_.Name, event_.id);
                                            emBuilder.WithDescription(emBuilder.Description +
                                                                      $"\n{event_.Name} - {event_.Description}");
                                            chosed_events++;
                                            ids_of_events.Add(event_.id);
                                        }
                                        else if (event_.lvlOfRarity == 3 && new Random().Next(0, 100) > 95)
                                        {
                                            btnBuilder.WithButton(event_.Name, event_.id);
                                            emBuilder.WithDescription(emBuilder.Description +
                                                                      $"\n{event_.Name} - {event_.Description}");
                                            chosed_events++;
                                            ids_of_events.Add(event_.id);
                                        }
                                    }
                                }
                            }

                            emBuilder.WithTitle("Дроп, выберите ивент");
                            emBuilder.WithColor(Color.Blue);

                            EventMsgID = arg.Channel
                                .SendMessageAsync("", embed: emBuilder.Build(), components: btnBuilder.Build())
                                .GetAwaiter().GetResult().Id;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Environment.Exit(-52);
                        }
                    }).Start();
                }
            }
        }
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}

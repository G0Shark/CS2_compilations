using Discord;

namespace CS2xDiscord_complications;

public class Event
{
    public string Name { get; set; }
    public string id { get; set; }
    public string Description { get; set; }
    public int lvlOfRarity { get; set; }
    public int EmojiId { get; set; }
    public Thread WorkingThread { get; set; }
    
    public void ExecThread()
    {
        WorkingThread.Start();
    }
}
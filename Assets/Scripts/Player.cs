public class Player
{
    public string Name;

    public Player(string name)
    {
        Name = name;
        Brain.ins.EventHandler._playerCreatedEvent.Invoke(this);
    }
}
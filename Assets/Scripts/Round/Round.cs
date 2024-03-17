using System.Collections.Generic;

public enum RoundState
{
    New,
    InProgress,
    Pass,
    Fail
};

public class Round
{
    public Player Player;
    public RoundState State = RoundState.New;
    public List<Block> Blocks = new();
    public float Height = 20;
    public float StartHeight = 0;
    public readonly int Ante;
    public readonly int Session;

    public Round(int ante, int session)
    {
        Ante = ante;
        Session = session;

        Height = Ante switch
        {
            2 => 20,
            >= 3 => 30,
            _ => Height
        };
    }
    
    public void DestroyBlocks()
    {
        foreach (var block in Blocks)
        {
            block.Destroy();
        }
    }
    public void FreezeBlocks()
    {
        foreach (var block in Blocks)
        {
            block.FreezeBlock();
        }
    }

}
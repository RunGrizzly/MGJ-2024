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

    public void CompleteRound(RoundState state)
    {
        State = state;
    }
}
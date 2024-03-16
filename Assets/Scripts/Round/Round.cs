using System.Collections.Generic;
using UnityEngine;

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
    public float Height = 3;
    public float StartHeight = 0;
    public readonly int Ante;

    public Round(int ante)
    {
        Ante = ante;
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
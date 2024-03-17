using System.Collections.Generic;
using UnityEngine;

public enum RoundState
{
    New,
    InProgress,
    Pass,
    Fail,
    Lost
};

public class Round
{
    public Player Player;
    public RoundState State = RoundState.New;
    public List<Block> Blocks = new();
    public float Height = 3;
    public float StartHeight = 0;
    public readonly int Ante;
    public readonly int Session;

    public Round(int ante, int session)
    {
        Ante = ante;
        Session = session;

        // Height = Ante switch
        // {
        //     2 => 20,
        //     >= 3 => 30,
        //     _ => Height
        // };
    }

    public void DestroyBlocks()
    {
        foreach (var block in Blocks)
        {
            block.Destroy();
            // var rb = block.GetRigidbody();
            // rb.isKinematic = false;
            // var direction = rb.gameObject.transform.position - Camera.main.transform.position;
            // rb.AddForce(direction.normalized * -5000f);
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
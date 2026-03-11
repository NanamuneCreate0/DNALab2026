using UnityEngine;

public class ConsumeCell : ICreatureCell
{
    private Creature owner;

    private const float CELL_SIZE = 0.2f;
    private const float CONSUME_RANGE = 0.5f;

    public float CellSize => CELL_SIZE;

    public void Initialize(Creature creature)
    {
        owner = creature;
    }

    public void Tick()
    {
        TryConsume();
    }


    private void TryConsume()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            owner.transform.position,
            CONSUME_RANGE
        );

        foreach (var hit in hits)
        {
            Mana mana = hit.GetComponent<Mana>();

            if (mana == null) continue;

            owner.ChangeEnergy(mana.Energy);
            mana.ExcuteDie();
            break;
        }
    }
}

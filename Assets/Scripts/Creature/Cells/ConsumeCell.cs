using UnityEngine;

public class ConsumeCell : CreatureCell
{
    public override float CellSize => 0.2f;
    private Creature ownerCreature;
    private const float CONSUME_RANGE = 0.5f;

    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
    }

    public override void Tick()
    {
        TryConsume();
    }


    private void TryConsume()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            ownerCreature.transform.position,
            CONSUME_RANGE
        );

        foreach (var hit in hits)
        {
            Mana mana = hit.GetComponent<Mana>();

            if (mana == null) continue;

            ownerCreature.ChangeEnergy(mana.Energy);
            mana.ExcuteDie();
            break;
        }
    }
    public override void OnAging()
    {
    }
}

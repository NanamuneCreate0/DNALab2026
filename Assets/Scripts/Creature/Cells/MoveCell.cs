using UnityEngine;

public class MoveCell : CreatureCell
{
    public override float CellSize => 1f;

    private Creature ownerCreature;

    const float Straightness = 2f;
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
    }

    public override void Tick()
    {
        var motivations = ownerCreature.Memory.MoveMotivations;
        if (motivations.Count == 0) return;

        Vector2 move = Vector2.zero;
        for (int i = 0; i < motivations.Count; i++)
        {
            var m = motivations[i];

            float weight = Mathf.Pow(m.priority, Straightness);

            move += m.direction * weight;
        }
        if (move == Vector2.zero) return;
        move.Normalize();
        ownerCreature.AddAcceleration(move * ownerCreature.Speed);
    }
    public override void OnAging()
    {
    }
}
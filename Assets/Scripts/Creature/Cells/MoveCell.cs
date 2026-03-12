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
        var motivations = ownerCreature.Memory.CurrentMoveMotivations;
        if (motivations.Count == 0) return;

        Vector2 move = Vector2.zero;
        for (int i = 0; i < motivations.Count; i++)
        {
            var m = motivations[i];
            float weight = Mathf.Sign(m.priority) * Mathf.Pow(Mathf.Abs(m.priority), Straightness);
            move += m.dir * weight;
        }
        if (move == Vector2.zero) return;
        move.Normalize();
        ownerCreature.AddAcceleration(move * ownerCreature.Speed);
    }
    public override void OnAging()
    {
    }
}
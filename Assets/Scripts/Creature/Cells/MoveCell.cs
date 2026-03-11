using UnityEngine;

public class MoveCell : ICreatureCell
{
    public float CellSize => 1f;

    private Creature owner;

    const float Straightness = 2f;
    public void Initialize(Creature creature)
    {
        owner = creature;
    }

    public void Tick()
    {
        var motivations = owner.Memory.MoveMotivations;
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
        owner.AddAcceleration(move * owner.Acceleration);
    }
}
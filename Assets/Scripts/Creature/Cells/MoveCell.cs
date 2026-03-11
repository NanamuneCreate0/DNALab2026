using UnityEngine;

public class MoveCell : ICreatureCell
{
    public float CellSize => 1f;

    private Creature owner;
    private float speed = 3f;
    const float StopDistance = 0.3f;
    public void Initialize(Creature creature)
    {
        owner = creature;
    }

    public void Tick()
    {
        WorldObject target = owner.Memory.VisibleTarget;
        //‘ÎÛ‚ª‚¢‚È‚¯‚ê‚Îreturn
        if (target == null) { return; }
        //‘ÎÛ‚ªŠù‚É—×Ú‚È‚çreturn
        float dist = Vector2.Distance(owner.transform.position, target.Transform.position);
        if (dist < StopDistance) return;
        //‘ÎÛ‚Ì•ûŒü‚Éi‚Ş
        Vector3 dir = (target.Transform.position - owner.transform.position).normalized;
        owner.transform.position += dir * speed * SimulationTime.DeltaTime;
    }
}
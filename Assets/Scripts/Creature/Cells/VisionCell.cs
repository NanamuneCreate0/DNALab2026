using UnityEngine;

public class VisionCell : CreatureCell
{
    const float CellSizeValue = 1.0f;
    const float ViewRadius = 5f;

    private Creature ownerCreature;
    private float scanTimer;

    private static Collider2D[] results = new Collider2D[32];

    public override float CellSize => CellSizeValue;

    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;

        // ƒXƒLƒƒƒ“•ªŽU
        scanTimer = Random.Range(0.2f, 0.3f);
    }

    public override void Tick()
    {
        scanTimer += SimulationTime.DeltaTime;

        if (scanTimer < 0.2f) return;
        scanTimer = 0f;

        Scan();
    }

    private void Scan()
    {
        var motivations = ownerCreature.Memory.MoveMotivations;
        motivations.Clear();

        int count = Physics2D.OverlapCircleNonAlloc(
            ownerCreature.transform.position,
            ownerCreature.ViewRange,
            results
        );

        Vector2 myPos = ownerCreature.transform.position;

        for (int i = 0; i < count; i++)
        {
            var col = results[i];
            var obj = col.GetComponent<WorldObject>();
            if (obj == null) continue;
            if (obj.Transform == ownerCreature.transform) continue;

            //•ûŒü
            Vector2 pos = obj.Transform.position;
            float dist = Vector2.Distance(myPos, pos);
            if (dist <= 0.001f) continue;
            Vector2 dir = (pos - myPos).normalized;

            //—Dæ“x
            float importance = 0f;
            if (obj is Mana) importance = 1.0f;
            else if (obj is Creature) importance = 0.2f;
            float priority = importance / dist;

            //Motivation‚É“n‚·
            motivations.Add((priority, dir));
        }
    }
}
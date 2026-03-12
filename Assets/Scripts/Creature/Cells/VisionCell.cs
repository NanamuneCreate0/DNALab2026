using System.Collections.Generic;
using UnityEngine;

public class VisionCell : CreatureCell
{
    public override float CellSize => 1.0f;

    private List<(float priority, Vector3 dir)> cachedMotivations=new List<(float priority, Vector3 dir)>();
    private Creature ownerCreature;
    private float scanTimer;

    private static Collider2D[] results = new Collider2D[32];
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;

        // ƒXƒLƒƒƒ“•ªŽU
        scanTimer = Random.Range(0.2f, 0.3f);
    }

    public override void Tick()
    {
        scanTimer += SimulationTime.DeltaTime;

        if (scanTimer >= 0.2f)
        {

            scanTimer -= 0.2f;

            Scan();
        }

        var motivations = ownerCreature.Memory.NextMoveMotivations;
        if (cachedMotivations != null){ foreach (var m in cachedMotivations) { motivations.Add(m); } }
    }
    public override void OnAging()
    {
    }

    private void Scan()
    {
        cachedMotivations.Clear();
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
            else if (obj is Creature) importance = -0.3f;
            float priority = importance / dist;

            //Motivation‚É“n‚·
            cachedMotivations.Add((priority, dir));
        }
    }
}
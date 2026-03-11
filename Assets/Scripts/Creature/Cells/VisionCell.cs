using UnityEngine;

public class VisionCell : ICreatureCell
{
    const float CellSizeValue = 1.0f;
    const float ViewRadius = 5f;

    private Creature ownCreature;
    private float scanTimer;

    private static Collider2D[] results = new Collider2D[32];

    public float CellSize => CellSizeValue;

    public void Initialize(Creature creature)
    {
        ownCreature = creature;

        // ƒXƒLƒƒƒ“•ªŽU
        scanTimer = Random.Range(0.2f, 0.3f);
    }

    public void Tick()
    {
        scanTimer += SimulationTime.DeltaTime;

        if (scanTimer < 0.2f) return;
        scanTimer = 0f;

        Scan();
    }

    private void Scan()
    {
        int count = Physics2D.OverlapCircleNonAlloc(
            ownCreature.transform.position,
            ViewRadius,
            results
        );

        WorldObject nearest = null;
        float nearestDist = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            var col = results[i];

            var obj = col.GetComponent<WorldObject>();
            if (obj == null) continue;

            if (obj.Transform == ownCreature.transform) continue;

            float dist = Vector2.Distance(
                ownCreature.transform.position,
                obj.Transform.position
            );

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = obj;
            }
        }

        ownCreature.Memory.VisibleTarget = nearest;
    }
}
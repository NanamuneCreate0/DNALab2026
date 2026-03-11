using UnityEngine;

public class HPProductionCell : CreatureCell
{
    public override float CellSize => 1.0f;

    private float hpValue;
    private Creature ownerCreature;
    // 5秒ごとに生産するためのタイマー
    private float timer = 0f;
    private const float PRODUCE_INTERVAL = 5f;
    public void Init(float hpValue)
    {
        this.hpValue = hpValue;
    }

    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
    }

    public override void Tick()
    {
        // タイマー更新（Tickはフレームごと）
        timer += SimulationTime.DeltaTime;

        if (timer >= PRODUCE_INTERVAL)
        {
            Produce();
            timer -= PRODUCE_INTERVAL; // タイマーリセット
        }
    }

    private void Produce()
    {
        var hp = ScriptableObject.CreateInstance<ViewRangeCell>();
        hp.Init(hpValue);
        ownerCreature.AddCell(hp);
    }
}
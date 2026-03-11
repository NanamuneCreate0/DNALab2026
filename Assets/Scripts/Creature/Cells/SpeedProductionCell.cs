using UnityEngine;

public class SpeedProductionCell : CreatureCell
{
    public override float CellSize => 1.0f;

    private float speedValue;
    private Creature ownerCreature;

    // 5秒ごとに生産するためのタイマー
    private float timer = 0f;
    private const float PRODUCE_INTERVAL = 5f;
    public void Init(float speedValue)
    {
        this.speedValue = speedValue;
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
        var speed = ScriptableObject.CreateInstance<SpeedCell>();
        speed.Init(speedValue);
        ownerCreature.AddCell(speed);
    }
}
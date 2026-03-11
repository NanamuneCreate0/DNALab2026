using UnityEngine;

public class ViewRangeProductionCell : CreatureCell
{
    public override float CellSize => 1.0f;

    private float viewRangeValue;
    private Creature ownerCreature;

    // 5秒ごとに生産するためのタイマー
    private float timer = 0f;
    private const float PRODUCE_INTERVAL = 5f;
    public void Init(float viewRangeValue)
    {
        this.viewRangeValue = viewRangeValue;
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
        var view = ScriptableObject.CreateInstance<ViewRangeCell>();
        view.Init(viewRangeValue);
        ownerCreature.AddCell(view);
    }
}
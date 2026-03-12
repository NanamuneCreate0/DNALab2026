using UnityEngine;

public class SpeedProductionCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;

    [SerializeField]
    private float speedValue; // 不変の設計値

    [SerializeField]
    private float efficiency = 1f; // 老衰による機能効率
    public float Efficiency => efficiency;

    private Creature ownerCreature;

    private const float CELLSIZE_PER_FUNCTION = 4f;

    private float productionProgress = 0f;
    private const float PRODUCE_INTERVAL = 2f;

    public void Init(float speedValue)
    {
        this.speedValue = speedValue;
        efficiency = 1f;
    }

    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        UpdateCellSize();
    }

    public override void Tick()
    {
        // 生産速度は効率に比例
        productionProgress += SimulationTime.DeltaTime * efficiency;

        if (productionProgress >= PRODUCE_INTERVAL)
        {
            Produce();
            productionProgress -= PRODUCE_INTERVAL; // タイマーリセット
        }

        // Tickごとに体積更新
        UpdateCellSize();
    }

    private void Produce()
    {
        var speed = ScriptableObject.CreateInstance<SpeedCell>();
        speed.Init(speedValue);
        ownerCreature.AddCell(speed);
    }

    public override void OnAging()
    {
        // 指数減少で効率低下
        efficiency *= 1 - AgingRate;
        if (efficiency < 0.0001f)
            efficiency = 0f;

        UpdateCellSize();
    }

    private void UpdateCellSize()
    {
        cellSize = CELLSIZE_PER_FUNCTION * speedValue * efficiency;
    }
}
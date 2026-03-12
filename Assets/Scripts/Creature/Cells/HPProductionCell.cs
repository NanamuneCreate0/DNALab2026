using UnityEngine;

public class HPProductionCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;
    [SerializeField]
    private float hpValue;
    [SerializeField]
    private float efficiency = 1f; // 老衰による機能効率
    public float Efficiency => efficiency;

    private Creature ownerCreature;
    private const float CELLSIZE_PER_FUNCTION = 0.4f;

    private float productionProgress = 0f;
    private const float PRODUCE_INTERVAL = 2f;
    public void Init(float hpValue)
    {
        this.hpValue = hpValue;
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
    }

    private void Produce()
    {
        var hp = ScriptableObject.CreateInstance<ViewRangeCell>();
        hp.Init(hpValue);
        ownerCreature.AddCell(hp);
    }

    public override void OnAging()
    {
        // 老衰で効率を2%減少
        efficiency -= 0.02f;
        if (efficiency < 0f)
            efficiency = 0f;

        UpdateCellSize();
    }
    private void UpdateCellSize()
    {
        cellSize = CELLSIZE_PER_FUNCTION * hpValue * efficiency;
    }
}
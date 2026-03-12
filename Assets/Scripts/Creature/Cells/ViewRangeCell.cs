using UnityEngine;

public class ViewRangeCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;
    [SerializeField]
    private float viewRangeValue;
    private const float CELLSIZE_PER_FUNCTION = 1f;
    private Creature ownerCreature;
    public void Init(float viewRangeValue)
    {
        this.viewRangeValue = viewRangeValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        cellSize = CELLSIZE_PER_FUNCTION * viewRangeValue;
        ownerCreature.ViewRange += viewRangeValue;
    }
    public override void Tick() { return; }
    public override void OnAging()
    {
        float loss = viewRangeValue * AgingRate;// 指数減少：残り値のAgingRateずつ減少
        viewRangeValue -= loss;
        ownerCreature.ViewRange -= loss;
        if (viewRangeValue < 0.0001f)
            viewRangeValue = 0f;

        cellSize = CELLSIZE_PER_FUNCTION * viewRangeValue;
    }
}

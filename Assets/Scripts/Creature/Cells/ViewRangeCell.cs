using UnityEngine;

public class ViewRangeCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;
    [SerializeField]
    private float viewRangeValue;
    private float initialViewRangeValue;
    private const float CELLSIZE_PER_FUNCTION = 1f;
    private Creature ownerCreature;
    public void Init(float viewRangeValue)
    {
        this.viewRangeValue = viewRangeValue;
        initialViewRangeValue = viewRangeValue;
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
        if (viewRangeValue <= 0f) return;

        float loss = initialViewRangeValue * 0.02f;

        if (loss > viewRangeValue)
            loss = viewRangeValue;

        viewRangeValue -= loss;
        ownerCreature.ViewRange -= loss;

        if (viewRangeValue < 0.0001f)
            viewRangeValue = 0f;

        cellSize = CELLSIZE_PER_FUNCTION * viewRangeValue;
    }
}

using UnityEngine;

public class SpeedCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;
    [SerializeField]
    private float speedValue;
    private const float CELLSIZE_PER_FUNCTION = 0.1f;
    private Creature ownerCreature;
    public void Init(float speedValue)
    {
        this.speedValue = speedValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        cellSize = CELLSIZE_PER_FUNCTION * speedValue;
        ownerCreature.Speed += speedValue;
    }
    public override void Tick() { return; }

    public override void OnAging()
    {
        float loss = speedValue * AgingRate;// 指数減少：残り値のAgingRateずつ減少
        speedValue -= loss;
        ownerCreature.Speed -= loss;
        if (speedValue < 0.0001f)
            speedValue = 0f;

        cellSize = CELLSIZE_PER_FUNCTION * speedValue;
    }
}

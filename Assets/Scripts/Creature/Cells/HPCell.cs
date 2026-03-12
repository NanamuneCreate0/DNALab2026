using UnityEngine;

public class HPCell : CreatureCell
{
    [SerializeField]
    float cellSize;
    public override float CellSize => cellSize;
    [SerializeField]
    private float hpValue;
    private float initialHpValue;
    private const float CELLSIZE_PER_FUNCTION = 0.1f;
    private Creature ownerCreature;
    public void Init(float hpValue)
    {
        this.hpValue = hpValue;
        initialHpValue = hpValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        cellSize = CELLSIZE_PER_FUNCTION * hpValue;
        ownerCreature.HP += hpValue;
    }
    public override void Tick(){ return; }
    public override void OnAging()
    {
        if (hpValue <= 0f) return;

        float loss = initialHpValue * 0.02f;
        if (loss > hpValue) { loss = hpValue; }
        hpValue -= loss;
        ownerCreature.HP -= loss;
        if (hpValue < 0.0001f){hpValue = 0f;}

        cellSize = CELLSIZE_PER_FUNCTION * hpValue;
    }
}

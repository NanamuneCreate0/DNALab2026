using UnityEngine;

public class ViewRangeCell : CreatureCell
{
    [SerializeField]
    private float viewRangeValue;
    public override float CellSize => 1.0f;
    private Creature ownerCreature;
    public void Init(float viewRangeValue)
    {
        this.viewRangeValue = viewRangeValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        ownerCreature.ViewRange += viewRangeValue;
    }
    public override void Tick() { return; }
}

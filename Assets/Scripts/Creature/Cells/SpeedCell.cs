public class SpeedCell : CreatureCell
{
    public override float CellSize => 1.0f;
    private float speedValue;
    private Creature ownerCreature;
    public void Init(float speedValue)
    {
        this.speedValue = speedValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        ownerCreature.Speed += speedValue;
    }
    public override void Tick() { return; }
}

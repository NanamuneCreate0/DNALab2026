public class HPCell : CreatureCell
{
    public override float CellSize => 1.0f;
    private float hpValue;
    private Creature ownerCreature;
    public void Init(float hpValue)
    {
        this.hpValue = hpValue;
    }
    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;
        ownerCreature.HP += hpValue;
    }
    public override void Tick(){ return; }
}

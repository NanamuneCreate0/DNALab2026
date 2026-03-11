public class HPCell : ICreatureCell
{
    const float CellSizeValue = 1.0f;
    const float HPValue = 10f;

    private Creature ownCreature;
    public float CellSize => 1.0f; 
    public void Initialize(Creature creature)
    {
        ownCreature = creature;
        ownCreature.HP += HPValue;
    }
    public void Tick(){ return; }
}

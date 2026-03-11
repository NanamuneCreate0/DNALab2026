public interface ICreatureCell
{
    float CellSize { get; }
    void Initialize(Creature creature);
    void Tick();
}
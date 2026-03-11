using UnityEngine;

public abstract class CreatureCell : ScriptableObject
{
    public abstract float CellSize { get; }
    public abstract void Initialize(Creature creature);
    public abstract void Tick();
}
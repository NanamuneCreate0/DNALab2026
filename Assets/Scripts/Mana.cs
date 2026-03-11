using UnityEngine;

public class Mana : WorldObject
{
    [SerializeField] private float energy = 20f;
    public float Energy { get => energy; }
    public override void Tick() { return; }
    public void ExcuteDie(){Die();}
}
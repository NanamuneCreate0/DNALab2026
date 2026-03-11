using UnityEngine;

public abstract class WorldObject : MonoBehaviour, ITickable
{
    public Transform Transform => transform;
    public bool IsDead { get; private set; }

    protected virtual void OnEnable()
    {
        ITickableRegistry.Register(this);
    }

    protected virtual void OnDisable()
    {
        ITickableRegistry.Unregister(this);
    }
    protected void Die()
    {
        IsDead = true;
    }

    public abstract void Tick();
}
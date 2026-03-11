using System.Collections.Generic;

public static class ITickableRegistry
{
    public static List<ITickable> ITickables = new();

    public static void Register(ITickable obj)
    {
        ITickables.Add(obj);
    }

    public static void Unregister(ITickable obj)
    {
        ITickables.Remove(obj);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class SimulationTimeRunner : MonoBehaviour
{
    [SerializeField] private bool showRealTime = true;//ÀÛŠÔ‚Å‚â‚é‚©UpdateŠÔ‚Å‚â‚é‚©Ø‚è‘Ö‚¦‚ê‚é

    [SerializeField] private int tickSlowness = 1;//UpdateŠÔ
    [SerializeField] private int tickQuickness = 1;//UpdateŠÔ

    [SerializeField] private float simulationSpeed = 1;//ÀÛŠÔ

    private int updateCounter;

    private float timer;

    void Update()
    {
        if (!showRealTime)
        {
            updateCounter++;
            if (updateCounter < tickSlowness) { return; }
            updateCounter = 0;

            for (int i = 0; i < tickQuickness; i++)
            {
                RunTick();
            }
            return;
        }
        timer += Time.deltaTime * simulationSpeed;

        while (timer >= SimulationTime.DeltaTime)
        {
            RunTick();
            timer -= SimulationTime.DeltaTime;
        }

    }
    void RunTick()
    {
        //ITickable‘S‚Ä‚ÌTick‚ğ“®‚©‚·
        for (int j = 0; j < ITickableRegistry.ITickables.Count; j++)
        {
            ITickableRegistry.ITickables[j].Tick();
        }
        //WorldObject€–S”»’è
        for (int j = ITickableRegistry.ITickables.Count - 1; j >= 0; j--)
        {
            if (ITickableRegistry.ITickables[j] is WorldObject worldObject && worldObject.IsDead)
            {
                Destroy(worldObject.gameObject);
            }
        }
    }
}
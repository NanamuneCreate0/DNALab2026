using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Creature : WorldObject
{
    [SerializeField] private float hp;
    [SerializeField] private float energy;

    public float HP { get => hp; set => hp = value; }
    public float Energy { get => energy; private set => energy = value; }
    public float TotalCellSize => totalCellSize;

    private float totalCellSize = 0f;
    private List<ICreatureCell> cells = new List<ICreatureCell>();
    public CreatureMemory Memory { get; private set; } = new CreatureMemory();

    public Vector2 Velocity;
    public float Acceleration = 6f;

    public override void Tick()
    {
        //死亡判定
        if (HP <= 0f || Energy <= 0f)
        {
            Die();
            return;
        }

        //動く
        PhysicsTick();

        //全構成セルのTick（Updateみたいなもの）
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].Tick();
        }
    }

    //追加構成セルのInitialize
    public void AddCell(ICreatureCell cell)
    {
        cell.Initialize(this);
        cells.Add(cell);

        totalCellSize += cell.CellSize;
    }
    public void ChangeEnergy(float amount)
    {
        energy += amount;
    }
    public void AddAcceleration(Vector2 accel)
    {
        Velocity += accel * SimulationTime.DeltaTime;
    }
    public void PhysicsTick()
    {
        float drag = 0.98f;
        Velocity *= Mathf.Pow(drag, SimulationTime.DeltaTime * 60f);

        transform.position += (Vector3)(Velocity * SimulationTime.DeltaTime);
    }
}
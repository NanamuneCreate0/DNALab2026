using System.Collections.Generic;
using UnityEngine;

public class Creature : WorldObject
{
    [SerializeField]
    private List<CreatureCell> cells = new List<CreatureCell>();

    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float viewRange;
    [SerializeField] private float energy;

    public float Energy { get => energy; private set => energy = value; }
    public float HP { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float ViewRange { get => viewRange; set => viewRange = value; }
    public float TotalCellSize => totalCellSize;

    private float totalCellSize = 0f; 
    public CreatureMemory Memory { get; private set; } = new CreatureMemory();

    public Vector2 Velocity;

    public override void Tick()
    {
        //死亡判定
        if (HP <= 0f || Energy <= 0f)
        {
            foreach (var cell in cells){Destroy(cell); }//CreatureCellのScriptableObjectを破棄
            cells.Clear();
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
    public void AddCell(CreatureCell cell)
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
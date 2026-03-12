using System.Collections.Generic;
using UnityEngine;

public class Creature : WorldObject
{
    [SerializeField]
    private List<CreatureCell> cells = new List<CreatureCell>();

    [SerializeField] private float totalCellSize = 0f; 
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float viewRange;
    [SerializeField] private float energy;

    public float Energy { get => energy; private set => energy = value; }
    public float HP { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
    public float ViewRange { get => viewRange; set => viewRange = value; }
    public float TotalCellSize => totalCellSize;

    public CreatureMemory Memory { get; private set; } = new CreatureMemory();

    public Vector2 Velocity;


    const float KLEIBER_EXPONENT = 0.75f;//クライバー則。代謝はCellSizeの0.75乗に比例する
    private float metabolismTimer = 0f;
    const float METABOLISM_INTERVAL = 1f;
    const float METABOLISM_RATE = 0.02f;

    // 老衰
    const float AGING_EXPONENT = 0.225f;//寿命はCellSizeの0.225乗に比例する
    const float AGING_INTERVAL_RATE = 1f;
    float agingTimer = 0f;

    //グラフ描画用
    [SerializeField, HideInInspector]
    private List<float> speedHistory = new List<float>();
    [SerializeField, HideInInspector]
    private List<float> timeHistory = new List<float>();
    private float elapsedTime = 0f;
    public List<float> SpeedHistory => speedHistory;
    public List<float> TimeHistory => timeHistory;

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

        //代謝（Energy消費をCellSizeに比例して行う）
        MetabolismTick();

        //老衰
        AgingTick();

        //グラフ描画用
        elapsedTime += SimulationTime.DeltaTime;
        speedHistory.Add(Speed);
        timeHistory.Add(elapsedTime);
    }
    void MetabolismTick()
    {
        metabolismTimer += SimulationTime.DeltaTime;

        if (metabolismTimer < METABOLISM_INTERVAL) return;

        metabolismTimer -= METABOLISM_INTERVAL;

        float metabolicCost =
            METABOLISM_RATE *
            Mathf.Pow(totalCellSize, KLEIBER_EXPONENT);

        energy -= metabolicCost;
    }
    void AgingTick()
    {
        agingTimer += SimulationTime.DeltaTime;

        float interval =
            AGING_INTERVAL_RATE *
            Mathf.Pow(totalCellSize, AGING_EXPONENT);

        if (agingTimer < interval) return;

        agingTimer -= interval;

        for (int i = 0; i < cells.Count; i++)
        {
            cells[i].OnAging();
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
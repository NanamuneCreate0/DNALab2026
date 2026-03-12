using UnityEngine;

[CreateAssetMenu(menuName = "CreatureCells/HomingCell")]
public class HomingCell : CreatureCell
{
    const float HOMING_PRIORITY = 0.1f;

    private Creature ownerCreature;
    private Transform nestTransform;

    public override float CellSize => 1f;

    public override void Initialize(Creature creature)
    {
        ownerCreature = creature;

        // ManaSpawnerをシーンから探してTransformを取得
        var spawner = GameObject.FindObjectOfType<ManaSpawner>();
        if (spawner != null)
            nestTransform = spawner.transform;
        else
            Debug.LogWarning("HomingCell: ManaSpawner not found in scene");
    }

    public override void Tick()
    {
        if (nestTransform == null || ownerCreature == null)
            return;

        Vector3 dir = (nestTransform.position - ownerCreature.Transform.position).normalized;
        //float distance = Vector3.Distance(ownerCreature.Transform.position, nestTransform.position);

        //優先度
        //普通はinportanceは対象ごとに固定だが、帰巣本能はinportance=係数×distance=0.5くらいを目安に
        //平均Distanceは5くらいかなぁ
        //priority=inportance/distanceなので相殺。
        float priority = HOMING_PRIORITY;
        var motivations = ownerCreature.Memory.NextMoveMotivations;//ownerCreature.Memory.NextMoveMotivations;
        motivations.Add((priority, dir));
    }

    public override void OnAging()
    {
        return;
    }
}
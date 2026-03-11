using UnityEngine;

public class InitialCreatureSpawner : MonoBehaviour
{
    const float InitialEnergyRate = 10f;
    [SerializeField] private Creature prefab;
    [SerializeField] private int NumberOfInitialBirth = 10;
    [SerializeField] private float RadiusOfInitialBirth = 15f;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < NumberOfInitialBirth; i++)
        {
            // 小数ランダム距離
            float distance = Random.Range(0f, RadiusOfInitialBirth);

            // 小数ランダム角度
            float angle = Random.Range(0f, 360f);

            float rad = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(rad) * distance;
            float y = Mathf.Sin(rad) * distance;

            Vector3 spawnPosition = new Vector3(x, y, 0f);
            //こうするとInstantiate()はGameObjectの所持コンポーネントCreatureを返す
            Creature creature = Instantiate(prefab, spawnPosition, Quaternion.identity);
            creature.AddCell(new HPCell());
            creature.AddCell(new VisionCell());
            creature.AddCell(new MoveCell());
            creature.AddCell(new ConsumeCell());
            creature.ChangeEnergy(creature.TotalCellSize*InitialEnergyRate);
        }
    }
}
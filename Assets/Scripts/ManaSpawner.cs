using UnityEngine;

public class ManaSpawner : MonoBehaviour,ITickable
{
    [SerializeField] private GameObject manaPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float radiusOfSpawn;
    public Transform Transform { get => transform; }
    private float timer;

    private void Start()
    {
        ITickableRegistry.Register(this);
    }

    public void Tick()
    {
        timer += SimulationTime.DeltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMana();
        }
    }

    private void SpawnMana()
    {
        float distance = Random.Range(0f, radiusOfSpawn);
        float angle = Random.Range(0f, 360f);

        float rad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad) * distance;
        float y = Mathf.Sin(rad) * distance;

        Vector3 spawnPosition = new Vector3(x, y, 0f);

        Instantiate(manaPrefab, spawnPosition, Quaternion.identity);
    }
}
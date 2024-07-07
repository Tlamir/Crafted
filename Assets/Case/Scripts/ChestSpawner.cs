using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField]
    private ChestInventoriesScObj ChestInventories;
    public GameObject chestPrefab;
    public int numberOfChestsToSpawn = 10;
    public Vector3 spawnAreaSize = new Vector3(10f, 0.5f, 10f); // Size of the area where chests can spawn
    public float minSpawnHeight = 2.75f; // Minimum y-coordinate for spawning chests
    public float maxSpawnHeight = 2.8f; // Maximum y-coordinate for spawning chests

    void Start()
    {
        SpawnChests();
    }

    void SpawnChests()
    {
        for (int i = 0; i < numberOfChestsToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(chestPrefab, randomPosition, Quaternion.identity);
            chestPrefab.GetComponent<ChestInteractor>().ChestInventorySelf = ChestInventories.ChestInventories[i];
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
            0f, // Start with y = 0, adjust based on raycast hit
            Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
        );

        randomPosition += transform.position; // Offset by the spawner's position

        // Perform raycast to find valid y-coordinate on the terrain
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            randomPosition.y = Mathf.Clamp(hit.point.y, minSpawnHeight, maxSpawnHeight);
        }
        else
        {
            randomPosition.y = Random.Range(minSpawnHeight, maxSpawnHeight);
        }

        return randomPosition;
    }
}

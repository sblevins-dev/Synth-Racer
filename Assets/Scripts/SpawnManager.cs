using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    [SerializeField] GameObject powerup;
    public GameObject[] spawnPoints;
    [SerializeField] GameObject environment;
    GameManager gameManager;
    GameObject player;
    int difficulty;
    public float respawnRate;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        difficulty = gameManager.difficulty;

        if (!gameManager.isGameOver && gameManager.isGameRunning)
        {
            player = GameObject.Find("Player");
            Invoke(nameof(SpawnItem), 2f);
            Invoke(nameof(SpawnPowerup), 10f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver)
        {
            CancelInvoke(nameof(SpawnItem));
            CancelInvoke(nameof(SpawnPowerup));
        }
    }

    void SpawnItem()
    {
        GameObject item = Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        item.transform.Rotate(0, -180, 0); 
        item.transform.parent = environment.transform;

        Invoke(nameof(SpawnItem), GetRespawnRate());
    }

    float GetRespawnRate()
    {
        difficulty = gameManager.difficulty;

        switch (difficulty)
        {
            case 0:
                respawnRate = Random.Range(5f, 10f);
                break;
            case 1:
                respawnRate = Random.Range(3f, 7f);
                break;
            case 2:
                respawnRate = Random.Range(1f, 5f);
                break;
            default:
                respawnRate = Random.Range(5f, 10f);
                break;
        }

        return respawnRate;
    }

    float GetPowerupRespawnRate()
    {
        return Random.Range(10f, 60f);
    }

    void SpawnPowerup()
    {
        GameObject item = Instantiate(powerup, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
        item.transform.parent = environment.transform;

        Invoke(nameof(SpawnPowerup), GetPowerupRespawnRate());
    }
}

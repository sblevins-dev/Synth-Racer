using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    int damage = 3;
    float offset = 8f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (gameManager.isGameOver || player == null)
        {
            Destroy(gameObject);
        }

        if (player != null && transform.position.z < player.transform.position.z - offset)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!gameManager.isGameOver)
        {
            MoveEnemy();
        }
        
    }

    void MoveEnemy()
    {
        transform.Translate(gameManager.playerSpeed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}

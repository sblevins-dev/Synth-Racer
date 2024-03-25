using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PickupsController : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    float offset = 8f;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speed = gameManager.speed;

        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);   
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || transform.position.z < player.transform.position.z - offset || gameManager.isGameOver)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.isGameOver)
        {
            MoveObject();
        }
        
    }

    

    void MoveObject()
    {
        transform.Translate(new Vector3(0, 0, 1 * speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.UpdateScore();
            Destroy(gameObject);
        }
    }
}

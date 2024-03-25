using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    
    float offset = 8f;
    float speed;
    float boostSpeed = 500f;
    float initialSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speed = gameManager.speed;
        initialSpeed = gameManager.playerSpeed;

       // Invoke(nameof(SpawnPowerup), 1f);
       transform.position = new Vector3(transform.position.x, 1f, transform.position.z);  
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || transform.position.z < player.transform.position.z - offset)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    void MoveObject()
    {
        transform.Translate(new Vector3(0, 0, -1 * speed * Time.deltaTime));
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerSpeed += boostSpeed;
            gameManager.ReduceSpeedHandler(initialSpeed);
            Destroy(gameObject);
        }
    }
}

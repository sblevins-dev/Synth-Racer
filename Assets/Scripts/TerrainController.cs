using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    GameManager gameManager;
    private float terrainPos;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speed = gameManager.playerSpeed;
        terrainPos = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        speed = gameManager.playerSpeed;

        {
            
        }
        if (transform.localPosition.z < -940)
        {
            transform.localPosition = new Vector3(0, 0, terrainPos);
        }

        MoveTerrain();
    }

    void MoveTerrain()
    {
        transform.Translate(0, 0, -1 * speed * Time.deltaTime);
    }
}

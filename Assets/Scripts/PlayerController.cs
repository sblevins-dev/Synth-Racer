using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    float forward;
    float horizontal;
    private Vector2 initialPos;
    float minX = -2f;
    float maxX = 2f;
    int index;
    Vector3 currPos;
    Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        index = 1;
        currPos = transform.position;

        positions = new Vector3[]
        {
            new Vector3(-2f, currPos.y, currPos.z),
            currPos,
            new Vector3(2f, currPos.y, currPos.z)
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.hitPoints <= 0)
            Destroy(gameObject);

        if (gameManager.isGameRunning && !gameManager.isGameOver)
        {
forward = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        currPos.x = Mathf.Clamp(currPos.x, minX, maxX);

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Check if touch position is on the left or right side of the screen
                bool isLeftSide = touch.position.x < Screen.width / 2f;
                //initialPos = touch.position;

                if (isLeftSide)
                {
                    if (index <= 0) return;
                    index--;
                    
                }
                else
                {
                    if (index >= 2) return;
                    index++;
                    
                }
                currPos = positions[index];

                transform.position = currPos;
            }
        }
        }

        
    }

    //private void FixedUpdate()
    //{
        
    //    MovePlayer(horizontal);
    //}

    

    //void MovePlayer(float horizontal)
    //{
    //    transform.position = new Vector3(horizontal * 2, transform.position.y, transform.position.z);  
    //}
}

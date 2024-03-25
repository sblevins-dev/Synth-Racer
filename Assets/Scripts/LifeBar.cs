using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Image[] wrenches;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        UpdateLifeBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLifeBar()
    {

        for (int i = 0; i < wrenches.Length; i++)
        {
            if (i < gameManager.hitPoints)
            {
                wrenches[i].gameObject.SetActive(true);
            }
            else
            {
                wrenches[i].gameObject.SetActive(false);
            }
        }
    }
}

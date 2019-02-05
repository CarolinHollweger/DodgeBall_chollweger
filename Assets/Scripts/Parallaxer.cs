
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{

    public GameObject obstacle;

    private float timer;
    private int offsetCounter = 5;
    private int offsetDistance = 1; // Place obstacles in x units


    // Use this for initialization
    void Start()
    {
        Debug.Log("'Parallaxer.cs' initialized.");
        CreateWorld(offsetCounter);
    }

    // Create world
    void CreateWorld(int offset)
    {
        for (int i = 0; i < 7; i++)
        {
            Instantiate(obstacle, new Vector2((i * 4) + Random.Range(1, 4) - 1, offset), Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > GlobalVariables.speed)
        {
            offsetCounter = offsetCounter + offsetDistance;
            CreateWorld(offsetCounter);
            timer = timer - GlobalVariables.speed;
        }
    }

}


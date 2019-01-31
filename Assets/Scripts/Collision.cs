using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update () {

	}
}

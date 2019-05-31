using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameObject gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //tag game state as GameState
        {
            GetComponent<AudioSource>().Play(); // play aduio as soon as player hits checkpoint 
            gameState.GetComponent<GameState>().Checkpoint();
        }
    }
}

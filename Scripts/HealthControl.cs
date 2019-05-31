using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
    public SimpleHealthBar healthBar;
    public float max = 6000.0f;
    public float current;
    bool gameover = false;
    private GameObject gameState;


    // Update is called once per frame
    void FixedUpdate()
    {   
        if (!gameover) 
        {
            // every update HP - 1
            current = current - 1;
            // Pick up Pre-fab HP + 16 in TimeBoost.cs

            //update bar
            healthBar.UpdateBar(current, max);

            if (current == 0) {
                gameover = true;
            }
        }
        else // gameover = true
        {
            //print text and stop game
            gameState.GetComponent<GameState>().dead();
        }
        
    }
    public void gameStart() // when the game starts
    {
        current = max;
        healthBar.UpdateBar(current, max);
        gameState = GameObject.FindGameObjectWithTag("GameState");
        gameover = false; 
    }
    public void hpboost() 
    {
        current = current + 300.0f; // 5% time boost
        if (current > max) current = max;
    }
    public void megahpboost() {
        current = current + 1200.0f; // 20% time boost
        if (current > max) current = max;
    }
    public void collideElephant() {
        current = current - 1000.0f; //16.7% time reduction
        if (current < 0) current = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    private float startTime = 0;
    private float totalTime = 0; //final time
    private int win = 0;
    private bool isRacing;
    public Text timerText;
    private int checkpointPassedThrough;
    private int numCheckpoints;
    //private GameObject goal;

    public GameObject rainPrefab;
    public GameObject healthBar;
    public GameObject biker;

    public bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        checkpointPassedThrough = 0;
        //  numCheckpoints = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        numCheckpoints = 3;
        //goal = GameObject.FindGameObjectWithTag("Goal");
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRacing) // stop timer and record total time
        {
            timerText.text = "Pass goal to start game";
        }
        else // update time
        {
            timerText.text = (Time.time - startTime).ToString() + " s"; // in second
        }
        if(GameOver) // user loses in the middle of the game
        {
            Initialization();
        }
        
    }

    public void dead() 
    {
        GameOver = true;
    }
    public void Checkpoint()
    {
        checkpointPassedThrough++;
    }
    public void Initialization() // initialization of the game
    {
        //initalization 
        //rainPrefab.SetActive(false); // rain stops at the beginning of the game
        //healthBar.GetComponent<HealthControl>().gameStart(); // set current to max
       // healthBar.SetActive(false); // health bar doesn't start decreasing at the beginning of the game
        biker.GetComponent<Transform>().position = new Vector3(678.2f, 1.3f, 404.9f); //initalize position of biker
        biker.GetComponent<Rigidbody>().velocity = Vector3.zero; //biker is zero velocity
        //transform.rotation = Quaternion.Euler(0, 90, 0);
        win = 0;
        isRacing = false;
        GameOver = false;
    }

    public void Goal() // called when user passes Goal, GameOver has to be false
    {
        if(!isRacing) //passes goal to start game
        {
           // healthBar.SetActive(true); // starts decreasing HP as user passes first goal
            startTime = Time.time;
            //start game
            GameOver = false;
            isRacing = true;
        }
       else // user wins the game
        {
            GameOver = true;
            totalTime = Time.time - startTime;
            timerText.text = totalTime.ToString() + "s";
            //winning stuff
            timerText.text = "YOU WON!";
        }
    }
        /*if (checkpointPassedThrough == 0)
        {
            
            
        }
        if (checkpointPassedThrough == numCheckpoints)
        {
            isRacing = false;
            totalTime = Time.time - startTime;
        }*/
    
    
}

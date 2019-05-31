using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject elephant;
    private elephant_move movement;
    public bool move = false;
    void Start()
    {
        movement  = elephant.GetComponent<elephant_move>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(move)
        {
            elephant.GetComponent<Rigidbody>().isKinematic = false;
            movement.Elephant_walk();
        }
        else
        {
            elephant.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            move = true;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopElephant : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MoveElephant;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Elephant"))
        {
            MoveElephant.GetComponent<trigger>().move = false;
        }
    }


}

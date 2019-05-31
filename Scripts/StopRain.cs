using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRain : MonoBehaviour
{

    public GameObject rainPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) //tag game state as GameState
        {
            rainPrefab.SetActive(false); //deactivate the prefab (stop raining)
        }
    }
}

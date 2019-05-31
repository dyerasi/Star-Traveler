using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.RainMaker;
using UnityEngine.Rendering;

public class TriggerRain : MonoBehaviour
{

    public GameObject rainPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rainPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) //tag game state as GameState
        {
            rainPrefab.SetActive(true); //activate the prefab (start raining)
        }
    }
}

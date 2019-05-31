using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaTimeBoost : MonoBehaviour
{
    public GameObject healthControl;
    private float speed = 500.0f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        //spin object
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            healthControl.GetComponent<HealthControl>().megahpboost();
            gameObject.SetActive(false);
        }
    }
}

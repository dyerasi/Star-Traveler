using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elephant_move : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody elephant;
    private Vector3 elephant_dir = new Vector3(205f, 0f, 205f);
    public int speed = 3;
    void Start()
    {
        elephant = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Elephant_walk()
    {
        elephant.AddForce(elephant_dir*speed);
        Debug.Log("Move");


    }


}

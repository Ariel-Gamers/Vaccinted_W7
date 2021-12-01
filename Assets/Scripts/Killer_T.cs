using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer_T : Cell
{
    // Start is called before the first frame update
    [SerializeField]float speed = 5.0f;
    [SerializeField] Vector2 movement;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 2)
        {
            time = 0;
        }
    }
}

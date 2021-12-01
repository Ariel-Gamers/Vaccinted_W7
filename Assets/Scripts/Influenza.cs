using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Influenza : Cell
{

    List<Type> resistances;
    // Start is called before the first frame update
    void Start()
    {
        resistances = new List<Type>();
        resistances.Add(Type.Neutrophil);
        myType = Type.Influenza;
        timeSinceHit = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        GameObject go = collision.gameObject;
        if (go.CompareTag("GoodCell"))
        {
            StartCoroutine(Engage(go));
        }

    }

    private IEnumerator Engage(GameObject go)
    {

        Cell c = go.GetComponent<Cell>(); // l type
        Debug.Log("Entered");

        while (c != null)
        {
            if (timeSinceHit > 2)
            {
                Debug.Log("damaged");
                c.fight(damage);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
}

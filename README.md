# Vaccinted_W7


Simple example of gameplay. 

This uses https://arongranberg.com/astar/ 

A pathfinding project with easy implementation(not related to other questions)

This game is a fight between immunity cells and infection cells. Your goal is to manage the immune system resources, you get energy every few seconds.

You can use this energy to purchase two types of cells(in the future, more will be added). 


Every good and bad cell inherit from `Cell.cs`, which is the base class. `Cell.cs` contains information about all cells, including a basic type, health, damage, and so on.


It is also responsible for the self destruction method:
```csharp
void LateUpdate()
    {
        time += Time.deltaTime;
        timeSinceHit += Time.deltaTime;
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
```
All cells can hit a certain amount of times. They are destroyed when their health reaches zero - in order to not have this code copied across all cell types, 
all cells inherit from `Cell.cs`.
The cells engage with each other if they are tagged differently:
```csharp
    
        private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.CompareTag("BadCell"))
        {
            StartCoroutine(Engage(go));
        }

    }
 ```
Engage reduces the health of the enemy cell, until one of them is destroyed. 


We used an edge collider to draw around the body in order to limit the movement of the cells. 

We then used a trigger collider to generate random points within the body, managed by the `GameSystem.cs` script:
```csharp

        Vector3 v = new Vector3(Random.Range(min_X, max_X), Random.Range(min_Y, max_Y), -1);
        while (true)
        {
            foreach (PolygonCollider2D pgc in body_parts)
            {
                if (pgc.OverlapPoint(v))
                {
                    active_threats.Add(Instantiate(go, v, Quaternion.identity));
                    Debug.Log("Overlap true: " + v.ToString());
                    return;
                }
                else
                {
                    v.x = Random.Range(min_X, max_X);
                    v.y = Random.Range(min_Y, max_Y);
                }
            }
        }
    }
```
This generated a random point, checks whether it overlaps with the body and instantiates it. 

Active infection cells are kept in a list, which allows the immune cells to target them:
```csharp
    public GameObject getNearestThreat(Transform cell_position)
    {
        float magnitude = float.MaxValue;
        GameObject ans = null;
        foreach(GameObject threat in active_threats)
        {
            float distance = Vector2.Distance(threat.transform.position, cell_position.position);
            if(distance < magnitude)
            {
                Debug.Log("Found non-null threat");
                ans = threat;
                magnitude = distance;
            }
        }
        return ans;
    }
```
    
This function returns the nearest infection cell. Inside the immune cell script, `Neutrophil.cs`, it updates this target:
```csharp
        if(target == null)
        {
            target = gs.getNearestThreat(transform);
            if(target != null)
            {
                GetComponent<AIDestinationSetter>().target = target.transform;
            }
        }

```

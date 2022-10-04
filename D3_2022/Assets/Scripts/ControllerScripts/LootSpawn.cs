using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 1;
    public int seconds;
    public int time;
    

    void Spawn()
    {
        Vector3 randomPos = transform.position + (Vector3)(Random.insideUnitCircle * Radius);

        Instantiate(ItemPrefab, randomPos, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

    void Awake()
    {
        StartCoroutine(TimerTake());
    }


    void Start()
    {
        Spawn();
    }

    IEnumerator TimerTake()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            seconds += 1;
            if (seconds % time == 0) 
            Spawn();
        }
    }
}

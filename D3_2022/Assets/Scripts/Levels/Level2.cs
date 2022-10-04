using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float Radius = 2;
    public int seconds;
    public int time;

    private void Awake()
    {
        StartCoroutine(TimerTake());
    }

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



    IEnumerator TimerTake()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(1);
            seconds += 1;
            if (((seconds > 60) && (seconds % time == 0) && (seconds < 180)) || (seconds == 40))
                Spawn();
        }
    }
}
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    private Coroutine byeRoutune;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 3);
        StartCoroutine(SpawnRoutine());
        //byeRoutune = StartCoroutine(Bye());
    }

    private void Update()
    {
        //if (Time.time > 3) 
        //{
        //    StopCoroutine(byeRoutune);   //stop ได้ครั้งเดียวเเละไม่ null
        //    //StopAllCoroutines();
        //}
    }

    void RandomSpawn() 
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine() 
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }


    IEnumerator Hello(float delay) 
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
        yield return null;
        yield return null;
        Debug.Log("Hello" + Time.frameCount);
    }

    IEnumerator Bye()
    {
        while (true)
        {
            Debug.Log("Bye" + Time.frameCount + " " + Time.time);
            //yield return null;
            yield return new WaitForSeconds(1f);
            yield return Hello(4);
            //StartCoroutine(Hello());
            yield return new WaitForSeconds(1f);
            //if (Time.time > 5) 
            //{
            //    yield break;
            //}
        }
    }

}

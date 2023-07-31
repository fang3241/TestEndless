using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject parent;
    public GameObject obstaclePrefab;
    public Sprite levelSprite;
   
    public float spawnTime;

    public bool canSpawn;
    private void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnTimer(spawnTime));
    }
    
    public void SpawnObstacle()
    {
        if (canSpawn)
        {
            GameObject t = Instantiate(obstaclePrefab, LaneCord(Random.Range(0, 4)), transform.rotation, parent.transform);
            t.GetComponent<SpriteRenderer>().sprite = levelSprite;
        }
        StartCoroutine(SpawnTimer(spawnTime));
    }
    
    private Vector3 LaneCord(int lane)
    {
        return new Vector2(transform.position.x, Lane.objectPosition[lane]);
    }
    
    public void SpawnAtLane(GameObject obj, int lane)
    {
        Instantiate(obj, LaneCord(lane), transform.rotation);
    }
    public IEnumerator SpawnTimer(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnObstacle();

    }

    //public void SpawnAnswer(GameObject obj, List<Sprite> spt)
    //{
    //    Debug.Log("OUT");
    //    char[] temp = { 'A', 'B', 'C', 'D' };

    //    for(int i = 0; i < 4; i++)
    //    {
    //        GameObject ans = Instantiate(obj, LaneCord(i), transform.rotation);
    //        ans.GetComponent<AnswerObj>().option = temp[i];
    //        ans.GetComponent<SpriteRenderer>().sprite = spt[i];
    //    }

    //}


}

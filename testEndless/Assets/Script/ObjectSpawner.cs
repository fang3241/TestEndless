using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public void SpawnObject(GameObject obj)
    {
        int lane = Random.Range(0, 4);
        GameObject t = Instantiate(obj, LaneCord(lane), transform.rotation);
    }

    private Vector3 LaneCord(int lane)
    {
        return new Vector2(transform.position.x, Lane.objectPosition[lane]);
    }
    
    public void SpawnAtLane(GameObject obj, int lane)
    {
        Instantiate(obj, LaneCord(lane), transform.rotation);
    }

    public void SpawnAnswer(GameObject obj, List<Sprite> spt)
    {
        Debug.Log("OUT");
        char[] temp = { 'A', 'B', 'C', 'D' };
        //int randOpt = Random.Range(0, 4);

        ////spawnrate
        ////jawaban benar : 
        //Debug.Log(temp[0]);
        //temp[0].option = 'A';
        //temp[1].option = 'B';
        //temp[2].option = 'C';
        //temp[3].option = 'D';

        for(int i = 0; i < 4; i++)
        {
            GameObject ans = Instantiate(obj, LaneCord(i), transform.rotation);
            ans.GetComponent<AnswerObj>().option = temp[i];
            ans.GetComponent<SpriteRenderer>().sprite = spt[i];
        }


        //GameObject ans = Instantiate(obj, LaneCord(Random.Range(0,4)), transform.rotation);
        //ans.GetComponent<AnswerObj>().option = temp[randOpt];
        //ans.GetComponent<SpriteRenderer>().sprite = spt[randOpt];

        //random opsi
        //spawn object


        //select opsi

        //random lane

        //GameObject t = Instantiate(obj, LaneCord(lane), transform.rotation);
        //t.GetComponent<AnswerObj>().option = answerObj.option;
    }
}

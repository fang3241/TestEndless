using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHelper : MonoBehaviour
{
    /*
     * Move Background (image on canvas)
     * 
     * 
     */

    public float speed = 0;
    float pos = 0;
    private RawImage image;

    private LevelController LevelController;

    private void Awake()
    {
        LevelController = GameObject.FindObjectOfType<LevelController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        pos += (speed * LevelController.speedScaling);

        if (pos > 1.0f)
            pos -= 1.0f;

        image.uvRect = new Rect(pos, 0, 1, 1);
    }
}

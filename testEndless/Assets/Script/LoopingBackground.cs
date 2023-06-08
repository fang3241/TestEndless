using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer bgRenderer;

    private LevelController LevelController;

    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = GetComponent<Renderer>();
        LevelController = GameObject.FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * LevelController.speedScaling * Time.deltaTime, 0);
    }
}

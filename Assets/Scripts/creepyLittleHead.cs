using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepyLittleHead : MonoBehaviour
{
    public int headNumber;
    public rotatingVision body;
    public SpriteRenderer spriteRenderer;
    public Sprite[] headPosition;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
       spriteRenderer.sprite = headPosition[headNumber];
    }
}

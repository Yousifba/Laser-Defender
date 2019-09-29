using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float verticalScrollSpeed = 0.5f;
    [SerializeField] float horizontalScrollSpeed = 0.001f;

    Material myMaterial;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = gameObject.GetComponent<Renderer>().material;
        offset = new Vector2(horizontalScrollSpeed, verticalScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}

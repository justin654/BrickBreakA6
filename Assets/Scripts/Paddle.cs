using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float screenWidthInUnits = 16f;
    public float paddleMinX = 1f;
    public float paddleMaxX = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        MoveWithMouse();
    }

    void MoveWithMouse()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;

        pos.x = Mathf.Clamp(mousePosInUnits, paddleMinX, paddleMaxX);
        transform.position = pos;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesController : MonoBehaviour
{
    public float stdDev = 0.5f; 


    private Camera gameCam;
    private float screenHalfWidth;
    private float screenHalfHeight;
    
    
    void Start()
    {
        gameCam = Camera.main;

        var screenBottomLeft = gameCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        var screenTopRight = gameCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));

        screenHalfWidth = (screenTopRight.x - screenBottomLeft.x) / 2f;
        screenHalfHeight = (screenTopRight.y - screenBottomLeft.y) / 2f;
    }

    void FixedUpdate()
    {
        CheckWidth();
        CheckHeight();
    }


    // Check each boundary
    private void CheckWidth()
    {
        if (transform.position.x > screenHalfWidth + stdDev)
        {
            ScreenWrapX(-1);
        }
        else if (transform.position.x < -(screenHalfWidth + stdDev))
        {
            ScreenWrapX(1);
        }
    }

    private void CheckHeight()
    {
        if (transform.position.y > screenHalfHeight + stdDev)
        {
            ScreenWrapY(-1);
        }
        else if (transform.position.y < -(screenHalfHeight + stdDev))
        {
            ScreenWrapY(1);
        }
    }

    private void ScreenWrapX(int direction)
    {
        transform.position = new Vector2(screenHalfWidth * direction, transform.position.y);
    }

    private void ScreenWrapY(int direction)
    {
        transform.position = new Vector2(transform.position.x, screenHalfHeight * direction);
    }
}

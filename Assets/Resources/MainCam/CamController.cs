using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CamMvDirection
{
    Right,Left,Up,Down
}

public class CamController : MonoBehaviour
{
    [SerializeField]
    float panelWidth;
    [SerializeField]
    float panelHeight;
    public void MoveCam(CamMvDirection dir)
    {
        switch (dir)
        {
            case CamMvDirection.Right:
                transform.Translate(panelWidth,0,0);
                break;
            case CamMvDirection.Left:
                transform.Translate(-panelWidth, 0, 0);
                break;
            case CamMvDirection.Up:
                transform.Translate(0, panelHeight, 0);
                break;
            case CamMvDirection.Down:
                transform.Translate(0, -panelHeight, 0);
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.0f, 11.0f, -10.0f);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}

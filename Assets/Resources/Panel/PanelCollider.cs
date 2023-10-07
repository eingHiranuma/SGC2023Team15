using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCollider : MonoBehaviour
{
    [SerializeField]
    CamMvDirection camDir;


    CamController camCont;
    GunPlayer gPlayer;

    private void Start()
    {
        camCont = Camera.main.GetComponent<CamController>();
        gPlayer = GameObject.FindWithTag("GunPlayer").GetComponent<GunPlayer>();
        Debug.Log(camCont);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == "GunPlayer" && GameStat.stat == GameStat.Status.OnArea)
        {
            Debug.Log("Player Hit" + camDir);

            if (gPlayer.canMoveArea)
            {
                camCont.MoveCam(camDir);
            }

            Vector3 moveDiff = Vector3.zero;
            float moveDistance = 5.0f;
            switch (camDir)
            {
                case CamMvDirection.Right:
                    moveDiff = Vector3.right * moveDistance;
                    break;
                case CamMvDirection.Left:
                    moveDiff = Vector3.left * moveDistance;
                    break;
                case CamMvDirection.Up:
                    moveDiff = Vector3.up * moveDistance;
                    break;
                case CamMvDirection.Down:
                    moveDiff = Vector3.down * moveDistance;
                    break;

            }
            gPlayer.SetMoveDiff(moveDiff);
        }

    }
    



}

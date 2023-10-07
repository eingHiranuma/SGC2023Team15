using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    // アニメータ
    [SerializeField]
    private Animator animator = null;

    // スタートボタン
    [SerializeField]
    private Button btnStart = null;

    private bool isBusy = false;


    private System.Action exitCallBack = null;
    public System.Action ExitCallBack
    {
        set
        {
            exitCallBack = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.Play("Btn_selip", 0, 0);

        btnStart.onClick.RemoveAllListeners();
        btnStart.onClick.AddListener(() =>
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;
            StartCoroutine(start());
        });
    }

    private IEnumerator start()
    {
        // タップ音
        SoundManager.Instance.Play("tap");

        animator.Play("Btn_t", 0, 0);
        yield return new WaitForSeconds(0.5f);
        animator.Play("Title_out", 0, 0);
        yield return new WaitForSeconds(1f);

        exitCallBack?.Invoke();

        GameObject.Destroy(this.gameObject);
    }
}

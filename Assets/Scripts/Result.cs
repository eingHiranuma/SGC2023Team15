using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
        m_Button.onClick.AddListener(() =>
        {
            GameManager.Instance.ReturnTitle();

            Destroy(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : SingletonMonoBehaviour<ShopManager>
{
    /// <summary>
    /// ショップ
    /// </summary>
    private GameObject shop = null;


    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
	{
        shop = Instantiate(Resources.Load("Canvas") as GameObject);
        shop.name = "Shop";
        shop.transform.position = Vector3.zero;
        var scaler = shop.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(800, 600);
        shop.SetActive(false);
    }


    /// <summary>
    /// 表示
    /// </summary>
	public void Disp()
    {
        shop.SetActive(true);
    }


    /// <summary>
    /// 非表示
    /// </summary>
	public void Hide()
    {
        shop.SetActive(false);
    }


    /// <summary>
    /// プレイヤー破棄
    /// </summary>
	public void Destroy()
    {
        GameObject.Destroy(shop.gameObject);
    }
}


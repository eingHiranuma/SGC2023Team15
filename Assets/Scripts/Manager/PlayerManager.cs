
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    private GunPlayer player = null;
	public GunPlayer Player => player;


    /// <summary>
    /// root
    /// </summary>
    private Transform root;
    public Transform PlayerRoot => root;

    /// <summary>
    /// Start
    /// </summary>
    protected override void Start()
	{
	}


    /// <summary>
    /// Init
    /// </summary>
    public void Init()
    {
        // Root作成
        var rootObject = new GameObject("Player");
        root = rootObject.transform;
    }

    /// <summary>
    /// プレイヤーロード
    /// </summary>
	public void LoadPlayer(Vector3 position)
	{
		var go = Instantiate(Resources.Load("Player/Player") as GameObject);
		go.name = "Player";
		go.transform.position = position;
		player = go.GetComponent<GunPlayer>();
	}


    /// <summary>
    /// プレイヤー破棄
    /// </summary>
	public void Destroy()
    {
		Camera.main.transform.position = new Vector3(0, 1, -10);
        GameObject.Destroy(player.gameObject);
        if (null != root)
        {
            GameObject.Destroy(root.gameObject);
            root = null;
        }
    }
}


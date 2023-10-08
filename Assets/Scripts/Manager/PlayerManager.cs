
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
	/// Start
	/// </summary>
	protected override void Start()
	{
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
		GameObject.Destroy(player.gameObject);
    }
}


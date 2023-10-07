
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
	private enum phase
	{
		title_init,
		title,
		game_init,
		game,
		result,
	}

	private phase nowPhase = phase.title_init;

	/// <summary>
	/// Start
	/// </summary>
	protected override void Start()
	{
		// サウンド初期化
		SoundMasterData.Setup();
		SoundManager.Instance.Initialize();

		PlayerManager.Instance.LoadPlayer(Vector3.zero);

        EnemyManager.Instance.SetNest(new Vector3(10, 0, 0));
        EnemyManager.Instance.SetNest(new Vector3(10, 10, 0));
    }

    /// <summary>
    /// Update
    /// </summary>
    protected override void Update()
	{
		switch (nowPhase)
		{
			// タイトル初期化
			case phase.title_init:
				break;

			// タイトル
			case phase.title:
				break;

			// ゲーム初期化
			case phase.game_init:
				break;

			// ゲームメイン
			case phase.game:
				break;

            // リザルト
            case phase.result:
                break;
        }
    }
}


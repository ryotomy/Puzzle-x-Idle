using UnityEngine;

/// <summary>
/// 【背景のコントロール用クラス】
///     背景は3枚、カメラから見切れたら回り込む
/// </summary>
public class BackGroundController : MonoBehaviour
{

	// 背景の枚数
	int spriteCount = 3;
	// 背景が回り込み
	float leftOffset = -2f;


	Transform bgTfm;
	SpriteRenderer mySpriteRndr;
	float width;

	void Start()
	{
		bgTfm = transform;
		mySpriteRndr = GetComponent<SpriteRenderer>();
		width = mySpriteRndr.bounds.size.x-0.02f;
	}


	void Update()
	{
		// 座標変換
		Vector3 myViewport = Camera.main.WorldToViewportPoint(bgTfm.position);

		// 背景の回り込み(カメラがX軸プラス方向に移動時)
		if (myViewport.x < leftOffset)
		{
			bgTfm.position += Vector3.right * (width * spriteCount);
		}

	}
}
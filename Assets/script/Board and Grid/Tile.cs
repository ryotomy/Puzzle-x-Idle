// Copyright (c) 2017 Razeware LLC

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Holoville.HOTween;



public class Tile : MonoBehaviour
{

	private static Color selectedColor = new Color(.9f, .9f, .9f, .9f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private static Color select = new Color(0.5f,0.8f,1.0f,0.8f);
	private static Color enter = new Color(0.8f, 0.8f, 1.0f, 0.9f);
    //private static Color enter2 = new Color(0.8f, 0.8f, 1.0f, 0.9f);
    private static Color temp;


	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	private bool matchFound = false;



	public GameObject up;
	public GameObject down;
	public GameObject left;
	public GameObject right;


	[SerializeField]
	private bool isSelected = false;

	void Awake()
	{
		render = GetComponent<SpriteRenderer>();
	}

    //選択したタイルと、隣接したタイルのハイライト

    private void Select()
    {
        if (StartTime.instance.pause)
            return;
        if (BoardManager.instance.tileMOVE) {
            Debug.Log("aaaa"); return;
         }
        int movecheck;
		movecheck = GUIManager.instance.MoveCounter;
		if (movecheck == 0)
			return;
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
		temp = Color.white;
		up = GetAdjacent3(Vector2.up);
		down = GetAdjacent3(Vector2.down);
		left = GetAdjacent3(Vector2.left);
		right = GetAdjacent3(Vector2.right);
        if (up != null)
            up.gameObject.GetComponent<SpriteRenderer>().color = select; 
        if (down != null)
			down.gameObject.GetComponent<SpriteRenderer>().color = select;
		if (left != null)
			left.gameObject.GetComponent<SpriteRenderer>().color = select;
		if (right != null)
			right.gameObject.GetComponent<SpriteRenderer>().color = select;

	}

    //タイル選択解除
	private void Deselect()
	{
		isSelected = false;
		render.color = Color.white;
		temp = Color.white;
		if (up != null)
			up.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		if (down != null)
			down.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		if (left != null)
			left.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		if (right != null)
			right.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		previousSelected = null;
      

    }

    //マウスオーバータイル表示
	private void OnMouseEnter()
	{
        if (Input.GetMouseButton(0))
        {
            temp = render.color;
            
        }
        if (!isSelected && !Input.GetMouseButton(0))
        {
            temp = render.color;
            render.color = enter;
        }
	}
    //マウスオーバータイル解除
	private void OnMouseExit()
	{
        if (temp == enter)
        {
            return;
        }

        if (!isSelected && !Input.GetMouseButton(0)&&temp==Color.white)
		{
			render.color = Color.white; 
		}
        else if(!isSelected && temp==select)
        {
            render.color = select;
        }
        else if(isSelected)
        {
            render.color = selectedColor;
        }
         else
        {
            render.color =temp;
        }
	}
	

	void OnMouseDown()
	{//タイルクリック時

		// シフト中は何もしない
		if (render.sprite == null || BoardManager.instance.IsShifting)
		{
			return;
		}

		if (isSelected)
		{ // すでに同じタイルが選択されている
			Deselect();
		}
		else
		{
			if (previousSelected == null)
			{ // 最初のタイルを選択
				int movecheck;
				movecheck = GUIManager.instance.MoveCounter;
				if (movecheck == 0)
					return;
				Select();

				_firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				
			}
			else
			{　// ２つ目のタイルを選択
				if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
				{ // 隣接タイルが選ばれた
                    BoardManager.instance.Combo = 0;
                    SwapSprite(previousSelected.render); // 交換
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
                    Deselect();
                    GUIManager.instance.MoveCounter--;
				}
				else
				{ // 隣接タイルじゃない
					previousSelected.GetComponent<Tile>().Deselect();
                    Deselect();
                    int movecheck;
					movecheck = GUIManager.instance.MoveCounter;
					if (movecheck == 0)
						return;
				}
			}
		}
	}
    //タイル交換
    public void SwapSprite(SpriteRenderer render2)
    { // 1
        if (render.sprite == render2.sprite)
        { // 同じ色なら交換する必要はない
            return;
        }

        Sprite tempSprite = render2.sprite; // 3
        render2.sprite = render.sprite; // 4
        render.sprite = tempSprite; // 5

    }

    //以下2つのプログラムで選択したタイルの隣接するタイルを探す
    private GameObject GetAdjacent(Vector2 castDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < adjacentDirections.Length; i++)
        {
            adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
        }

        return adjacentTiles;

    }


    private List<GameObject> FindMatch(Vector2 castDir)
    { // マッチしたタイルを見つける
        List<GameObject> matchingTiles = new List<GameObject>(); //色がつながったタイルをここに格納
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); //上下左右のいずれかに光線を飛ばす
        while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
        { //飛ばした光線の先に同じタイルがあったらタイルを追加
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles; // 5
    }

    private void ClearMatch(Vector2[] paths) // マッチしたタイルを消す
    {
        List<GameObject> matchingTiles = new List<GameObject>(); // FindMatchで格納予定の色がつながったタイル
        for (int i = 0; i < paths.Length; i++) // 上下or左右に光線を飛ばす
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2) // 縦か横に３つ以上つながったとき
        {
            for (int i = 0; i < matchingTiles.Count; i++) // FindMatchで格納したタイルを全部消す
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
                var destroyingParticle = GameObject.Instantiate(_particleEffectWhenMatch as GameObject, new Vector3(matchingTiles[i].GetComponent<SpriteRenderer>().transform.position.x + 0.05f, matchingTiles[i].GetComponent<SpriteRenderer>().transform.position.y, -2), transform.rotation) as GameObject;
                Destroy(destroyingParticle, 0.15f);
            }

            //消したスプライトによってスコアを区別
            if (render.sprite.name == "characters_0001")//文字列で区別しているがListから区別できる
                GUIManager.instance.ATK+= matchingTiles.Count-1;
            if (render.sprite.name == "characters_0002")
                GUIManager.instance.ATKSPD += matchingTiles.Count - 1;
            if (render.sprite.name == "characters_0003")
                GUIManager.instance.SPD += matchingTiles.Count - 1;
            if (render.sprite.name == "characters_0004")
                GUIManager.instance.CRI += matchingTiles.Count - 1;
            if (render.sprite.name == "characters_0005")
                GUIManager.instance.ATKTIMES= matchingTiles.Count - 1;
            BoardManager.instance.Combo++;

            matchFound = true; // 6
        }
    }

    int tempcombo;

    public void ClearAllMatches()//上下と左右それぞれでマッチ判定する
    {
        StopCoroutine(BoardManager.instance.FindNullTiles());
        if (render.sprite == null)
            return;

        ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
        if (matchFound)
        {
            BoardManager.instance.matchfound = true;
            tempcombo=BoardManager.instance.Combo;
            render.sprite = null;
            matchFound = false;
            StartCoroutine(Tiledown());
            var destroyingParticle2 = GameObject.Instantiate(_particleEffectWhenMatch as GameObject, new Vector3(this.transform.position.x + 0.05f, this.transform.position.y, -2), transform.rotation) as GameObject;
            Destroy(destroyingParticle2, 0.15f);
        }
    }

    
    public IEnumerator Tiledown()//空きタイルに対して降下開始
    {
        BoardManager.instance.tileMOVE = true;
        yield return new WaitForSeconds(0.1f);
       
        if (tempcombo==BoardManager.instance.Combo){//コンボ調整用
            StopCoroutine(BoardManager.instance.FindNullTiles());
            StartCoroutine(BoardManager.instance.FindNullTiles());
            //Debug.Log("開始");
        }
    }

    //以下コンボ時のマッチ判定
    public int VHmatch = 0;
    private void ClearMatch2(Vector2[] paths) // マッチしたタイルを消す
    {
        List<GameObject> matchingTiles = new List<GameObject>(); // FindMatchで格納予定の色がつながったタイル
        for (int i = 0; i < paths.Length; i++) // 上下or左右に光線を飛ばす
        {
            matchingTiles.AddRange(FindMatch(paths[i]));
        }
        if (matchingTiles.Count >= 2) // 縦か横に３つ以上つながったとき
        {

            VHmatch++;
        }
    }
    public int ClearAllMatches2()//上下と左右それぞれでマッチ判定する
    {
        VHmatch = 0;
        StopCoroutine(BoardManager.instance.FindNullTiles());
        if (render.sprite == null)
            return 0;

        ClearMatch2(new Vector2[2] { Vector2.left, Vector2.right });
        ClearMatch2(new Vector2[2] { Vector2.up, Vector2.down });

        return VHmatch;
    }
    
 

    //以下スワイプ用変数

    public GameObject _indicator;//The indicator to know the selected tile
    public GameObject[,] _arrayOfShapes;//The main array that contain all games tiles
    private GameObject _currentIndicator=default;//The current indicator to replace and destroy each time the player change the selection
    private GameObject _FirstObject;//The first object selected
    private GameObject _SecondObject;//The second object selected
    public GameObject[] _listOfGems;//The list of tiles we cant to see in the game you can remplace them in unity's inspector and choose all what you want
    public GameObject _emptyGameobject;//After destroying object they are replaced with this one so we will replace them after with new ones
    public GameObject _particleEffect;//The object we want to use in the effect of shining stars 
    public GameObject _particleEffectWhenMatch;//The gameobject of the effect when the objects are matching
    public bool _canTransitDiagonally = false;//Indicate if we can switch diagonally
    public int _scoreIncrement;//The amount of point to increment each time we find matching tiles

    private ArrayList _currentParticleEffets = new ArrayList();//the array that will contain all the matching particle that we will destroy after
    public AudioClip MatchSound;//the sound effect when matched tiles are found
    public int _gridWidth;//the grid number of cell horizontally
    public int _gridHeight;//the grid number of cell vertically
                           //inside class
    private Vector2 _firstPressPos;
    private Vector2 _secondPressPos;
    private Vector2 _currentSwipe;
    public GameObject target;

    //スワイプ用

    GameObject gameobject;
	bool shouldTransit = false;

    //スワイプ予想先タイル表示

    private void OnMouseDrag()
    {
        int movecheck;
        movecheck = GUIManager.instance.MoveCounter;
        if (movecheck == 0)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            //最初にクリックしたときの座標
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (isSelected)
        {
            if (Input.GetMouseButton(0))
            {
                //マウスドラッグ中の座標
                _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //２点間の距離
                _currentSwipe = new Vector2(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

               
                _currentSwipe.Normalize();


                //距離差に応じてスワイプ予測先表示
                //上
                if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (up != null)
                        up.gameObject.GetComponent<SpriteRenderer>().color = enter;
                    if (down != null)
                        down.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (left != null)
                        left.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (right != null)
                        right.gameObject.GetComponent<SpriteRenderer>().color = select;
                    goto skip;
                }
                //下
                if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
                {
                    if (up != null)
                        up.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (down != null)
                        down.gameObject.GetComponent<SpriteRenderer>().color = enter;
                    if (left != null)
                        left.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (right != null)
                        right.gameObject.GetComponent<SpriteRenderer>().color = select;
                    goto skip;

                }
                //左
                if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    if (up != null)
                        up.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (down != null)
                        down.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (left != null)
                        left.gameObject.GetComponent<SpriteRenderer>().color = enter;
                    if (right != null)
                        right.gameObject.GetComponent<SpriteRenderer>().color = select;
                    goto skip;

                }
                //右
                if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
                {
                    if (up != null)
                        up.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (down != null)
                        down.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (left != null)
                        left.gameObject.GetComponent<SpriteRenderer>().color = select;
                    if (right != null)
                        right.gameObject.GetComponent<SpriteRenderer>().color = enter;
                    goto skip;
                }
                //どこにも該当しないとき
                if (up != null)
                    up.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (down != null)
                    down.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (left != null)
                    left.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (right != null)
                    right.gameObject.GetComponent<SpriteRenderer>().color = select;
                skip:;

            }
        }
    }


    void Update()
	{
		if (isSelected)
		{
			shouldTransit = false;
			var direction = Swipe();
			if (direction != Direction.NONE)
			{//スワイプしているとき

				
				if (HOTween.GetTweenInfos() == null)
				{

					Destroy(_currentIndicator);

					if (direction != Direction.STATIONARY)
					{//一定方向にドラッグしたら、タイルを交換する
						switch (direction)
						{

							case Direction.UP:
								gameobject = GetAdjacent2(Vector2.up);
								if (gameobject != null)
								{
									previousSelected = gameobject.gameObject.GetComponent<Tile>(); break;
								}
								break;
							case Direction.DOWN:
								gameobject = GetAdjacent2(Vector2.down);
								if (gameobject != null)
								{
									previousSelected = gameobject.gameObject.GetComponent<Tile>(); break;
								}
								break;
							case Direction.LEFT:
								gameobject = GetAdjacent2(Vector2.left);
								if (gameobject != null)
								{
									previousSelected = gameobject.gameObject.GetComponent<Tile>(); break;
								}
								break;
							case Direction.RIGHT:
								gameobject = GetAdjacent2(Vector2.right);
								if (gameobject != null)
								{
									previousSelected = gameobject.gameObject.GetComponent<Tile>(); break;
								}
								break;

						}


					}
					if (shouldTransit)
					{
                        if (gameobject == null)
                        {
                            Destroy(_currentIndicator);
                            Deselect();
                            return;
                        }
                            
                        BoardManager.instance.Combo = 0;
                        SwapSprite(previousSelected.render); // 2
						previousSelected.ClearAllMatches();
						ClearAllMatches();
						GUIManager.instance.MoveCounter--;
						Destroy(_currentIndicator);
						Deselect();
						return;

					}

				}

			}
		}

        //スワイプ予測用

		if (previousSelected != null)
		{
			up = GetAdjacent3(Vector2.up);
			down = GetAdjacent3(Vector2.down);
			left = GetAdjacent3(Vector2.left);
			right = GetAdjacent3(Vector2.right);
		}

		//update中



	}


    private Direction Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            _firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            _secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            _currentSwipe = new Vector2(_secondPressPos.x - _firstPressPos.x, _secondPressPos.y - _firstPressPos.y);

            //normalize the 2d vector
            _currentSwipe.Normalize();

            //swipe upwards
            if (_currentSwipe.y > 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                return Direction.UP;
            }
            //swipe down
            if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
                return Direction.DOWN;
            }
            //swipe left
            if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
                return Direction.LEFT;
            }
            //swipe right
            if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
                return Direction.RIGHT;
            }
            else
            {
                if (up != null)
                    up.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (down != null)
                    down.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (left != null)
                    left.gameObject.GetComponent<SpriteRenderer>().color = select;
                if (right != null)
                    right.gameObject.GetComponent<SpriteRenderer>().color = select;
            }
            return Direction.STATIONARY;
        }
        return Direction.NONE;
    }
    enum Direction
    {
        NONE,
        STATIONARY,
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }


   
	GameObject Temp;
	private GameObject GetAdjacent2(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
		shouldTransit = true;
		if (hit.collider != null)
		{
			Temp = hit.collider.gameObject;
			shouldTransit = true;
			return Temp;
		}
		return null;
	}
	private GameObject GetAdjacent3(Vector2 castDir)
	{
		RaycastHit2D hit = Physics2D.Raycast(previousSelected.transform.position, castDir);

		if (hit.collider != null)
		{
			Temp = hit.collider.gameObject;
			return Temp;
		}
		else
			return null;
	}
   

}
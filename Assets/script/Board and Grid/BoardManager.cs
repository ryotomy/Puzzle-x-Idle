// Copyright (c) 2017 Razeware LLC
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance;
	public List<Sprite> characters = new List<Sprite>();
	public GameObject tile;
	public GameObject target;
	public int xSize, ySize;

	private GameObject[,] tiles;

	public bool IsShifting { get; set; }

	void Start () {
		

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard(offset.x, offset.y);
    }

	private void CreateBoard (float xOffset, float yOffset) {
		tiles = new GameObject[xSize, ySize];

        float startX = transform.position.x;
		float startY = transform.position.y;
		Sprite[] previousLeft = new Sprite[ySize];
		Sprite previousBelow = null;
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				target = GameObject.Find("puzzlecontent");
				GameObject newTile= Instantiate(tile); ;
				newTile.transform.SetParent(target.transform, false);
	
				newTile.transform.localPosition = new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0);
				tiles[x, y] = newTile;
				newTile.transform.parent = transform; // 1

				List<Sprite> possibleCharacters = new List<Sprite>(); // 1
				possibleCharacters.AddRange(characters); // 2
				possibleCharacters.Remove(previousLeft[y]); // 3
				possibleCharacters.Remove(previousBelow);
				Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite; // 3
				previousLeft[y] = newSprite;
				previousBelow = newSprite;
			}
        }
    }
    int tempcombo;
   
   
    public bool tileMOVE=false;
    List<GameObject> list = new List<GameObject>();
    public IEnumerator FindNullTiles()
    {
        
        if (reset)
            goto skip;

        yield return new WaitForSeconds(0.3f);
    skip:
        reset = false;

 
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    
                    /**
                    for (int y2 = y; y2 < ySize-1; y2++) { 
                        if(!list.Contains(tiles[x, y2]))
                            list.Add(tiles[x, y2]);
                        }
                    **/
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break;
                }
            }
        }
        StartCoroutine(downtest());
        matchtrue = true;
        StartCoroutine(match());
    }
    bool matchtrue = false;
    public bool matchfound = false;
    private IEnumerator match()
    {
        
        if (matchtrue)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                    {


                        goto skip;
                    }
                }
            }


            yield return new WaitForSeconds(0.50f);
            matchfound = false;
            for (int j = 2; j > 0; j--) { //縦横両方につながっているタイルから優先的に消す
                for (int x = 0; x < xSize; x++)
                {
                    for (int y = 0; y < ySize; y++)
                    {
                        tiles[x, y].GetComponent<Tile>().ClearAllMatches2();
                        if (tiles[x, y].GetComponent<Tile>().ClearAllMatches2() == j)
                            list.Add(tiles[x, y]);


                    }
                }

                //縦横・縦か横の順番で消えるはず
                for (int i = 0; i < list.Count; i++)
                {

                    list[i].GetComponent<Tile>().ClearAllMatches();



                }
                list.Clear();
            }
            /**
            for (int i = 0; i < list.Count; i++)
            {

                    list[i].GetComponent<Tile>().ClearAllMatches();

                
             
            }
            **/
        }
        if (!matchfound)
            tileMOVE = false;
        skip:
        matchtrue = false;
        
}
   
    bool start =true;
    bool reset=false;
    private IEnumerator ShiftTilesDown(int x, int yStart)
    {
        //Debug.Log("降下中");
        float shiftDelay = .03f;
       start = true;


        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
    
        int nullCount = 0;
        SpriteRenderer render2 = null;

        for (int y = yStart; y < ySize; y++)
        {  // 1
            SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            { // 2
               
                if (reset) {  goto skip; }
                
                nullCount++;
               
               
                //一個上のタイルにスプライトが存在しているか確認
                if (y + 1<ySize)
                {
                    render2 = tiles[x, y + 1].GetComponent<SpriteRenderer>();
                }
                //もし存在しているなら

                if (render2 != null)
                {
                    if (render2.sprite != null)
                    {
                        //一個上からさらに伸ばして探したとき空タイルがないか確認する
                        for (int y2 = y + 1; y2 < ySize; y2++)
                        {
                            render2 = tiles[x, y2].GetComponent<SpriteRenderer>();
                            //空タイルがあったらそれを無視してタイルを下ろしその後最初からやり直す
                            if (render2.sprite == null)
                            {

                                reset = true;
                                goto nullskip;
                            }
                        }
                    }
                }
                
            skip:;
            }
           
        nullskip:
            renders.Add(render);
        }


        

        for (int i = 0; i < nullCount; i++)
        {
            yield return new WaitForSeconds(shiftDelay);// 4
            if (render2 != null)
            {
                for (int k = 0; k < renders.Count - 1; k++)
                { // 5

                    renders[k].sprite = renders[k + 1].sprite;

                    renders[k + 1].sprite = GetNewSprite(x, ySize - 1);

                }
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                Debug.Log(nullCount);
                for (int k = 0; k < renders.Count ; k++)
                { // 5

                    renders[k].sprite = GetNewSprite(x, ySize - 1);


                }
                yield return new WaitForSeconds(0.05f);
            }
        }
       
        IsShifting = false;
        start = false;
        
    }

    private Sprite GetNewSprite(int x, int y)
    {

        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(characters);

        if (x > 0)
        {
            possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < xSize - 1)
        {
            possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCharacters.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
    }

    //コンボ表記・コンボによる効果付加用

    [SerializeField]
    int combo=0;

    [SerializeField]
    Text comboText=default;

    public int Combo
    {
        get
        {
           return combo;
        }
        set
        {
            combo = value;
            tempcombo = value;
            comboText.text = combo.ToString();
            if (combo >= 1)
            {
                comboGUI.instance.spawncombo();
            }
            if (combo == 2)
            {
                ComboBonus.instance.BonusTimePlus(5,2,125);
            }
            if (combo == 3)
            {
                ComboBonus.instance.BonusTimePlus(6,3,130);
            }
            if (combo == 4)
            {
                ComboBonus.instance.BonusTimePlus(7,4,140);
            }
            if (combo == 5)
            {
                ComboBonus.instance.BonusTimePlus(8,5,150);
            }
            if (combo == 6)
            {
                ComboBonus.instance.BonusTimePlus(5,6,150+25*(combo-5));
            }
            if (combo >= 7)
            {
                ComboBonus.instance.BonusTimePlus(5,combo, 150 +25* (combo-5));
            }
            // StartCoroutine("ComboTime");
        }
    }

    private void Awake()
    {
        Combo = 0;
        instance = GetComponent<BoardManager>();
    }

    private IEnumerator ComboTime()
    {
        int temp = combo;

        yield return new WaitForSeconds(1.5f);

        if (temp == combo)
            Combo = 0;
    }
    private IEnumerator downtest()    {

    
        if (start == false)
        {
            if (reset)
            {
                yield return StartCoroutine(FindNullTiles());//すべてのコルーチンが終わったら起動したい
                //すべてのコルーチンの終わりを受け取り次第起動
                //Debug.Log("降下２セット目");
                
            }
        }
    }

}

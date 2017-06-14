using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

    //現在 我需要一個標示地圖上的方框，來表明現在在哪，然後我要一個上下左右的箭頭操控方向
    //跟一個選擇prefabs的機器來選擇要create哪個tile
    //旋轉部分就先製作完，再用按鈕調整旋轉，然後將旋轉記錄在Tile裡面
    Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();


    public Tile[] TileType;

    //[SerializeField]
    //GameObject tileViewPrefab;
    [SerializeField]
    GameObject tileSelectionIndicatorPrefab;


    public Point pos;
    

    public float Col = 5;
    public float Row = 5;
    
    [HideInInspector]
    public Vector2 Grid_Offset;
   
    public Vector2 Size;
    public bool displayGrid = true;
    //slider bar for angle
    [Range(0.0F, 360.0F)]
    public float Angle = 0f;

    void OnEnable()
    {
        currentSelectTile = TileType[0];
    }
    public Tile currentSelectTile;
    int index=0;

    void OnDrawGizmos()
    {
        if (displayGrid)
        {
            SetSpriteGrid(Size.x, Size.y);
            //float posOffset =  Size.x / Row - 1;
            //float posOffsetY = Size.y / Col - 1;
            Vector3 offset = new Vector3(-Size.x / 2f + Grid_Offset.x / 2, -Size.y / 2f + Grid_Offset.y / 2f,0);
            for (int x = 0; x < Col; x++)
            {
                for (int y = 0; y < Row; y++)
                {
                    
                    Vector3 to = new Vector3(x * Grid_Offset.x , y  * Grid_Offset.y, 0); 
                    Gizmos.DrawWireCube(transform.position+to+ offset, Grid_Offset) ;
                }
            }
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_marker.position, Grid_Offset);
        }



    }

    GameObject tex1;
    public Texture2D pretex1;
    public Texture2D SetPreviewTextrue()
    {
        //tex1 = Resources.Load("tiles", typeof(GameObject)) as GameObject;
        pretex1 = AssetPreview.GetAssetPreview(currentSelectTile.gameObject);
        return pretex1;
    }

    public void SelectTile(int count)
    {
        int tilelength = TileType.Length;
        index += count;
        index = Mathf.Clamp(index,0, tilelength - 1);
        currentSelectTile = TileType[index];
      
        
    }

    #region 舊的區域
    //Transform marker
    //{
    //    get
    //    {

    //        if (_marker != null)
    //        {
    //            DestroyImmediate(_marker.gameObject);
    //        }
    //            //GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
    //        Tile tile = Instantiate(currentSelectTile) as Tile;
    //        _marker = tile.transform;

    //        return _marker;
    //    }
    //}
    //Tile temp;
#endregion

    public Transform _marker;

    public void UpdateMarker()
    {

        
        if (_marker != null)
        {
            DestroyImmediate(_marker.gameObject);
        }

        Tile tile = Instantiate(currentSelectTile) as Tile;
        _marker = tile.transform;
        

        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;

        _marker.parent = transform;
        _marker.localPosition = t != null ? t.center : new Vector3(pos.x, pos.y, 0);
       

    } 

    Tile CreateOrGet(Point p)
    {
        if (tiles.ContainsKey(p))
            return tiles[p];

        Tile t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);
        return t;
    }

    Tile Create()
    {
        //暫時
         //currentSelectTile = tileViewPrefab.GetComponent<Tile>();
         Tile instance = Instantiate(currentSelectTile) as Tile;
         instance.transform.parent = transform;

         return instance;
     
    }
    //以當前pos來調整每個tile的旋轉值，因此

    public void FixRotation()
    {
        
        Tile t = CreateOrGet(pos);
        t.Rotate(Angle);
    }


    public void SetTile()
    {
        Tile t = CreateOrGet(pos);

    }

    public void FixPos(float x,float y)
    {
        pos += new Point(x, y);
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void ClearSingle()
    {

        if (!tiles.ContainsKey(pos))
            return;
        Tile t = tiles[pos];
        tiles.Remove(pos);
        DestroyImmediate(t.gameObject);

    }

    void SetSpriteGrid(float width,float height)
    {
        float _col = width / Col; //6.4 / 5
        float _row = height / Row;
        //Grid_Offset = new Vector2(_gridOffsetX, _gridOffsetY);
        Grid_Offset = new Vector2(_col, _row);

    }
}

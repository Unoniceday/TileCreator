using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public const float stepHeight = 0.25f;

    public Point pos; //保存自己的位置和高度訊息
    public int height;

    public float Trotation = 0;

    public int type;

    public Vector3 center { get { return new Vector3(pos.x, pos.y, height * stepHeight); } }



    //無論何時改變高度，我們都希望可以值觀的看到改變後的高度
    void Match()
    {
        transform.localPosition = new Vector3(pos.x, pos.y,0 );
        //變回原形
       // transform.localScale = new Vector3(1, 1, 1);
    }

    //隨機生成或收縮地快來生成地圖，下面實現更改數據同時刷新介面的方法
    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    public void Rotate(float angle)
    {
        Trotation = angle;
        transform.localRotation = Quaternion.AngleAxis(Trotation, Vector3.left);

    }



    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.y), (int)v.y);
    }
}

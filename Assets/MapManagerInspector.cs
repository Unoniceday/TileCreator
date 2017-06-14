using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerInspector : Editor
{

	public MapManager current
    {
        get
        {
            return (MapManager)target;
        }
    }


    private void OnSceneGUI()
    {
       
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.keyDown:
                
                   
                        Debug.Log("hi");
                    break;
            case EventType.KeyUp:
                Debug.Log("keyup");
                break;
        }
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.BeginHorizontal();
       
        if (GUILayout.Button("<<"))
        {
            current.SelectTile(-1);
        }

        //tex1 = Resources.Load("tiles", typeof(GameObject)) as GameObject;
        //pretex1 = AssetPreview.GetAssetPreview(tex1);
        //    EditorGUI.DrawPreviewTexture(new Rect(50, 50, 50, 50), pretex1);
        //EditorGUI.DrawPreviewTexture(GUILayoutUtility.GetRect(50, 150), current.SetPreviewTextrue());
        GUI.DrawTexture(GUILayoutUtility.GetRect(50, 150), current.SetPreviewTextrue());

        if (GUILayout.Button(">>"))
        {
            current.SelectTile(1);
        }
        GUILayout.EndHorizontal();

        #region 方向鍵
        //操控指標的上下左右
        if (GUILayout.Button("up"))
        {
            current.FixPos(0, 1 * current.Grid_Offset.y);
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("left"))
        {
            current.FixPos(-1*current.Grid_Offset.x, 0);
        }
        if (GUILayout.Button("right"))
        {
            current.FixPos(1 * current.Grid_Offset.x, 0);

        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("down"))
        {
            current.FixPos(0, -1 * current.Grid_Offset.y);
        }
        #endregion 

        EditorGUILayout.BeginVertical();
        GUILayout.BeginArea(GUILayoutUtility.GetRect(50,20));
        GUILayout.EndArea();


        if (GUILayout.Button("CreateTile"))
        {
            current.SetTile();
        }

        if (GUILayout.Button("SetRotation"))
        {
            current.FixRotation();
        }

        EditorGUILayout.EndVertical();


        if (GUILayout.Button("ClearSingle"))
        {
            current.ClearSingle();
        }
        if (GUILayout.Button("ClearAll"))
        {
            current.Clear();
        }
        if (GUI.changed)
        {
            
            current.UpdateMarker();
        }

    }
}

using UnityEngine;
using UnityEditor;

//https://stackoverflow.com/questions/49353971/how-to-create-multidimensional-array-in-unity-inspector
[CustomEditor(typeof(GridPartData))]
public class GridPartDataEditor : Editor
{
    GridPartData gridPartData;

    private void OnEnable()
    {
        gridPartData = target as GridPartData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUIStyle tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        GUIStyle columnStyle = new GUIStyle();
        columnStyle.fixedWidth = 25;

        EditorGUILayout.BeginHorizontal(tableStyle);
        for (int x = 0; x < GridPartDataConstants.RowsAmount; x++)
        {
            EditorGUILayout.BeginVertical(columnStyle);
            for (int y = 0; y < GridPartDataConstants.CollsAmount; y++)
            {
                gridPartData.GridPart[x + y * GridPartDataConstants.RowsAmount] = EditorGUILayout.Toggle(gridPartData.GridPart[x + y * GridPartDataConstants.RowsAmount]);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}

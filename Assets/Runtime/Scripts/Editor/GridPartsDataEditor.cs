using UnityEngine;
using UnityEditor;

//https://stackoverflow.com/questions/49353971/how-to-create-multidimensional-array-in-unity-inspector
[CustomEditor(typeof(GridPartsData))]
public class GridPartsDataEditor : Editor
{
    GridPartsData gridPartsData;

    private void OnEnable()
    {
        gridPartsData = target as GridPartsData;
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
        for (int x = 0; x < GridPartsDataConstants.RowsAmount; x++)
        {
            EditorGUILayout.BeginVertical(columnStyle);
            for (int y = 0; y < GridPartsDataConstants.CollsAmount; y++)
            {
                gridPartsData.GridParts[x + y * GridPartsDataConstants.RowsAmount] = EditorGUILayout.Toggle(gridPartsData.GridParts[x + y * GridPartsDataConstants.RowsAmount]);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}

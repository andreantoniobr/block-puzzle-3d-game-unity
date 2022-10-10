using UnityEngine;
using UnityEditor;

//https://stackoverflow.com/questions/49353971/how-to-create-multidimensional-array-in-unity-inspector
[CustomEditor(typeof(BlockData))]
public class BlockDataEditor : Editor
{
    BlockData blockData;

    private void OnEnable()
    {
        blockData = target as BlockData;
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
        for (int x = 0; x < BlockDataConstants.RowsAmount; x++)
        {
            EditorGUILayout.BeginVertical(columnStyle);
            for (int y = 0; y < BlockDataConstants.CollsAmount; y++)
            {
                blockData.BlockCells[x + y * BlockDataConstants.RowsAmount] = EditorGUILayout.Toggle(blockData.BlockCells[x + y * BlockDataConstants.RowsAmount]);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }
}

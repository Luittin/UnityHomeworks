using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class CreateLevel : EditorWindow
{
    [MenuItem("Window/UI Toolkit/CreateLevel")]
    public static void ShowExample()
    {
        CreateLevel wnd = GetWindow<CreateLevel>();
        wnd.titleContent = new GUIContent("CreateLevel");
    }

    public void CreateGUI()
    {
        // 1
        VisualElement root = rootVisualElement;

        // 2
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/CreateLevel.uxml");
        VisualElement uxmlRoot = visualTree.CloneTree();
        root.Add(uxmlRoot);

        // 3
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/CreateLevel.uss");

        var preMadeStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/CreateLevel.uss");

        root.styleSheets.Add(styleSheet);

        root.styleSheets.Add(preMadeStyleSheet);

        rootVisualElement.Q<VisualElement>("Container").style.height = new StyleLength(position.height);
    }
}
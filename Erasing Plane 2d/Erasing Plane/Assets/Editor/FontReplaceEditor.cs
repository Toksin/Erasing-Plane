using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontReplaceEditor : Editor
{
    [MenuItem("Tools/Replace Fonts")]
    public static void ReplaceFonts()
    {
        Font newFont = AssetDatabase.LoadAssetAtPath<Font>("Assets/Text/Dulinan SDF.asset");
        TMP_FontAsset newTMPFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Text/Dulinan SDF.asset");

        // Заменяем шрифты в текстовых компонентах
        var texts = FindObjectsOfType<Text>(true);
        foreach (var text in texts)
        {
            text.font = newFont;
            text.SetAllDirty();
        }

        // Заменяем шрифты в TextMeshPro компонентах
        var tmpTexts = FindObjectsOfType<TextMeshProUGUI>(true);
        foreach (var tmpText in tmpTexts)
        {
            tmpText.font = newTMPFont;
            tmpText.SetText(tmpText.text);
        }

        Debug.Log("Fonts replaced successfully!");
    }
}

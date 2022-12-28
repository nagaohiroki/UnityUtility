using UnityEditor;
public class SearchGUID : EditorWindow
{
	string mGUID;
	[MenuItem("Assets/UnityUtility/SearchGUID")]
	static void Open()
	{
		EditorWindow.GetWindow<SearchGUID>(typeof(SearchGUID).Name);
	}
	void OnGUI()
	{
		mGUID = EditorGUILayout.TextField(mGUID);
		EditorGUILayout.LabelField(AssetDatabase.GUIDToAssetPath(mGUID));
	}
}

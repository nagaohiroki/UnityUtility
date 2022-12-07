using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class ReferenceViewer : EditorWindow
{
	[SerializeField]
	Object mTargetObject;
	IEnumerable<Object> mReferenceList;
	Vector2 mScroll;
	[MenuItem("Assets/UnityUtiliy/ReferenceViewer")]
	static void FindReference()
	{
		EditorWindow.GetWindow<ReferenceViewer>(typeof(ReferenceViewer).Name).UpdateReference(Selection.activeObject);
	}
	void UpdateReference(Object inObject)
	{
		Undo.RecordObject(this, typeof(ReferenceViewer).Name);
		mTargetObject = inObject;
		var selectPath = AssetDatabase.GetAssetPath(mTargetObject);
		mReferenceList = AssetDatabase.GetAllAssetPaths()
			.Where(path => AssetDatabase.GetDependencies(path, false).Contains(selectPath))
			.Select(path => AssetDatabase.LoadAssetAtPath<Object>(path)).ToArray();
	}
	void OnGUI()
	{
		mScroll = EditorGUILayout.BeginScrollView(mScroll);
		EditorGUILayout.BeginHorizontal();
		mTargetObject = EditorGUILayout.ObjectField(mTargetObject, typeof(Object), false);
		EditorGUILayout.EndHorizontal();
		if (mReferenceList != null)
		{
			foreach(var item in mReferenceList)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.ObjectField(item, typeof(Object), false);
				EditorGUI.EndDisabledGroup();
				EditorGUILayout.EndHorizontal();
			}
		}
		EditorGUILayout.EndScrollView();
	}
}

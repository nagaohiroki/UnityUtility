using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace UnityUtility
{
	public class ReferenceViewer : EditorWindow
	{
		[SerializeField]
		Object mTargetObject;
		IEnumerable<Object> mReferenceList;
		Vector2 mScroll;
		[MenuItem("Assets/UnityUtility/ReferenceViewer")]
		static void FindReference()
		{
			EditorWindow.GetWindow<ReferenceViewer>(typeof(ReferenceViewer).Name).UpdateReference(Selection.activeObject);
		}
		void UpdateReference(Object inObject)
		{
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
			if(GUILayout.Button("Update"))
			{
				UpdateReference(mTargetObject);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUI.BeginDisabledGroup(true);
			if(mReferenceList != null)
			{
				foreach(var item in mReferenceList)
				{
					EditorGUILayout.BeginHorizontal();
					EditorGUILayout.ObjectField(item, typeof(Object), false);
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUI.EndDisabledGroup();
			EditorGUILayout.EndScrollView();
		}
	}
}

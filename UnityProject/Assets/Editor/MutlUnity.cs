using UnityEditor;
using UnityEngine;
using System.IO;
public class MultiUnitySettingWindow : EditorWindow
{
	string mPath = "Logs/MultiUnity/UnityProject_link";
	[MenuItem("Assets/UnityUtility/MultiUnity")]
	static void Open()
	{
		EditorWindow.GetWindow<MultiUnitySettingWindow>(typeof(MultiUnitySettingWindow).Name);
	}
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		mPath = EditorGUILayout.TextField(mPath);
		var linkDir = Path.GetFullPath(mPath);
		var prjDir = Path.GetDirectoryName(Application.dataPath);
		EditorGUILayout.LabelField($"prj {prjDir}");
		EditorGUILayout.LabelField($"link {linkDir}");
		if(GUILayout.Button("generate"))
		{
			var dirs = new[] { "Assets", "ProjectSettings", "Packages" };
			foreach(var dir in dirs)
			{
				var prjPath = Path.Join(prjDir, dir);
				var linkPath = Path.Join(linkDir, dir);
				Directory.CreateDirectory(linkDir);
				var procInfo = new System.Diagnostics.ProcessStartInfo();
#if UNITY_EDITOR_WIN
				procInfo.FileName = "cmd.exe";
				procInfo.Arguments = $"/c mklink /j \"{linkPath}\" \"{prjPath}\"";
#else
				procInfo.FileName = "ln";
				procInfo.Arguments = $"-s \"{prjPath}\" \"{linkPath}\"";
#endif
				var proc = System.Diagnostics.Process.Start(procInfo);
				proc.WaitForExit();
				proc.Close();
			}
		}
		EditorGUILayout.EndVertical();
	}
}

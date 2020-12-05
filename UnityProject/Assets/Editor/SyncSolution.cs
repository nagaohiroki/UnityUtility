using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
public class SyncSolution
{
	[OnOpenAssetAttribute(0)]
	static bool SyncScript(int inInstanceID, int inLine)
	{
		if(Path.GetExtension(AssetDatabase.GetAssetPath(inInstanceID)) == ".cs")
		{
			Sync();
		}
		return false;
	}
	[MenuItem("Assets/Generate Sln #F12")]
	static void Sync()
	{
		var syncVS = Type.GetType("UnityEditor.SyncVS,UnityEditor");
		var synchronizerObject = syncVS.GetField("Synchronizer", BindingFlags.NonPublic | BindingFlags.Static).GetValue(syncVS);
		syncVS.GetMethod("SyncSolution", BindingFlags.Public | BindingFlags.Static).Invoke(null, null);
		synchronizerObject.GetType().GetMethod("Sync", BindingFlags.Public | BindingFlags.Instance).Invoke(synchronizerObject, null);
		Debug.Log("Sync");
	}
}
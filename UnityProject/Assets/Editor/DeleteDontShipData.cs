using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
public class DeleteDontShipData
{
	[PostProcessBuild(2000)]
	public static void OnPostprocessBuild(BuildTarget target, string path)
	{
		if(EditorUserBuildSettings.development)
		{
			return;
		}
		var keyWords = new[]
		{
			"_BackUpThisFolder_ButDontShipItWithYourGame",
			"_BurstDebugInformation_DoNotShip"
		};
		var dirs = Directory.GetDirectories(Path.GetDirectoryName(path));
		foreach(var dir in dirs)
		{
			foreach(var key in keyWords)
			{
				if(dir.Contains(key))
				{
					Directory.Delete(dir, true);
					break;
				}
			}
		}
	}
}

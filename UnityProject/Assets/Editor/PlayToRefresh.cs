using UnityEditor;
namespace UnityUtility
{
	public class PlayToRefresh
	{
		[InitializeOnLoadMethod]
		public static void Initialize()
		{
			if(EditorApplication.isPlaying) return;

			EditorApplication.update += () =>
			{
				if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
				{
					AssetDatabase.Refresh();
				}
			};
		}
	}
}

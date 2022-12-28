using UnityEditor;
public class Dirty
{
	[MenuItem("Assets/UnityUtility/SetDirty")]
	static void SetDirty()
	{
		foreach (var obj in Selection.objects)
		{
			EditorUtility.SetDirty(obj);
		}
	}
}

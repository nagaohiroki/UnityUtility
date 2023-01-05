using UnityEngine;
using System.Collections.Generic;
namespace UnityUtility
{
	public class RandomObject
	{
		static RandomObject mGlobal;
		public static RandomObject GetGlobal
		{
			get
			{
				if(mGlobal == null)
				{
					mGlobal = new RandomObject();
				}
				return mGlobal;
			}
		}
		Random.State mState;
		public RandomObject(int inSeed)
		{
			SetState(inSeed);
		}
		public RandomObject()
		{
			SetState(GenerateSeed());
		}
		public void SetState(int inSeed)
		{
			var prev = Random.state;
			Random.InitState(inSeed);
			mState = Random.state;
			Random.state = prev;
		}
		public int Range(int inMin, int inMax)
		{
			var prev = Random.state;
			Random.state = mState;
			var result = Random.Range(inMin, inMax);
			mState = Random.state;
			Random.state = prev;
			return result;
		}
		public float Range(float inMin, float inMax)
		{
			var prev = Random.state;
			Random.state = mState;
			var result = Random.Range(inMin, inMax);
			mState = Random.state;
			Random.state = prev;
			return result;
		}
		public void Shuffle<T>(List<T> iList)
		{
			var count = iList.Count;
			var last = count - 1;
			for(var i = 0; i < last; ++i)
			{
				var r = Range(i, count);
				var tmp = iList[i];
				iList[i] = iList[r];
				iList[r] = tmp;
			}
		}
		public static int CantorPair(Vector2Int inPos)
		{
			return (((inPos.x + inPos.y) * (inPos.x + inPos.y + 1)) / 2) + inPos.y;
		}
		public static int GenerateSeed()
		{
			return (int)System.DateTime.Now.Ticks;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Utils
{
	public static class ExtensionMethods
	{
		public static Vector3 Set(this Vector3 vector, float? x = null, float? y = null, float? z = null) => new(x ?? vector.x, y ?? vector.y, z ?? vector.z);

		public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null) => new(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));

		public static void FlipTo(this Transform transform, float movementDirection)
		{
			if (movementDirection != 0f)
			{
				Vector3 scale = transform.localScale;

				if (movementDirection > 0f)
					scale.x = Mathf.Abs(scale.x);
				else
					scale.x = -Mathf.Abs(scale.x);

				transform.localScale = scale;
			}
		}

		public static void FlipTo(this Transform transform, Transform target)
		{
			Vector3 scale = transform.localScale;

			if (target.position.x > transform.position.x)
				scale.x = Mathf.Abs(scale.x);
			else
				scale.x = -Mathf.Abs(scale.x);

			transform.localScale = scale;
		}

		public static Vector2 GetMousePos(UnityEngine.Camera camera) => camera ? Camera.main!.ScreenToWorldPoint(Input.mousePosition) : Vector2.zero;

		public static string SecondsToMinutesFormat(this string timeString)
		{
			float time = float.Parse(timeString);
			float minutes = Mathf.FloorToInt(time / 60);
			float seconds = Mathf.FloorToInt(time % 60);

			return $"{minutes:00}:{seconds:00}";
		}

		public static string SecondsToMinutesAndMillisecondsFormat(this string timeString)
		{
			float time = float.Parse(timeString);
			float minutes = Mathf.FloorToInt(time / 60);
			float seconds = Mathf.FloorToInt(time % 60);
			float milliSeconds = time % 1 * 1000;

			return $"{minutes:00}:{seconds:00}:{milliSeconds:000}";
		}

		public static bool Contains(this LayerMask layerMask, int layer) => ((1 << layer) & layerMask) != 0;

		public static void Shuffle<T>(this IList<T> list)
		{
			for (int i = list.Count - 1; i > 1; i--)
			{
				int j = Random.Range(0, i + 1);
				(list[i], list[j]) = (list[j], list[i]);
			}
		}
		public static T RandomItem<T>(this T[] array)
		{
			if (array.Length == 0) throw new IndexOutOfRangeException("Cannot select a random item from an empty array");
			return array[Random.Range(0, array.Length)];
		}
		
		public static T RandomItem<T>(this IList<T> list)
		{
			if (list.Count == 0) throw new IndexOutOfRangeException("Cannot select a random item from an empty list");
			return list[Random.Range(0, list.Count)];
		}

		public static T RemoveRandom<T>(this IList<T> list)
		{
			if (list.Count == 0) throw new IndexOutOfRangeException("Cannot remove a random item from an empty list");
			int index = Random.Range(0, list.Count);
			T item = list[index];
			list.RemoveAt(index);
			return item;
		}
		
		public static T RandomItem<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

			List<T> list = enumerable.ToList();
			if (list.Count == 0) throw new InvalidOperationException("Cannot select a random item from an empty collection");

			int index = new System.Random().Next(list.Count);
			return list[index];
		}

		public static T RemoveRandom<T>(this ICollection<T> collection)
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));

			List<T> list = collection.ToList();
			if (list.Count == 0) throw new InvalidOperationException("Cannot remove a random item from an empty collection");

			int index = new System.Random().Next(list.Count);
			T item = list[index];
			collection.Remove(item);
			return item;
		}
		
		public static T RandomExcluding<T>(this List<T> list, T exceptionElement)
		{
			IEnumerable<T> exception = new[] { exceptionElement };
			List<T> eligibleItems = list.Except(exception).ToList();
			return eligibleItems.RandomItem();
		}
		
		public static T RandomExcluding<T>(this IEnumerable<T> enumerable, T exceptionElement)
		{
			if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

			IEnumerable<T> exception = new[] { exceptionElement };
			IEnumerable<T> eligibleItems = enumerable.Except(exception).ToList();
        
			if (!eligibleItems.Any())
			{
				throw new InvalidOperationException("No eligible items after excluding the exception element");
			}

			return eligibleItems.RandomItem();
		}
		
		public static T RandomExcluding<T>(this List<T> list, List<T> exceptions)
		{
			List<T> eligibleItems = list.Except(exceptions).ToList();
			return eligibleItems.RandomItem();
		}

		public static T GetRandomEnum<T>()
		{
			Array enumArray = Enum.GetValues(typeof(T));
			T randomEnum = (T)enumArray.GetValue(Random.Range(0, enumArray.Length));
			return randomEnum;
		}

		public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
		{
			T component = gameObject.GetComponent<T>();
			return component != null ? component : gameObject.AddComponent<T>();
		}
		
		public static float ToLogarithmicVolume(this float sliderValue) 
		{
			return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
		}
		
		public static float ToLogarithmicFraction(this float fraction) 
		{
			return Mathf.Log10(1 + 9 * fraction) / Mathf.Log10(10);
		}
	}
}
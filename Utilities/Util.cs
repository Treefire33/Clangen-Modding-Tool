using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClanGenModTool.Util;

public static class Utils
{
	public static Random Random = new Random();
}

public static class NullableExtensions
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TryGetValue<T>(this T? self, out T result)
		where T : struct
	{
			result = self.GetValueOrDefault();
			return self.HasValue;
		}
}

public static class EnumerableExtension
{
	public static T PickRandom<T>(this IEnumerable<T> source)
	{
			return source.PickRandom(1).Single();
		}

	public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
	{
			return source.Shuffle().Take(count);
		}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
			return source.OrderBy(x => Guid.NewGuid());
	}

	public static T KeyByValue<T, W>(this Dictionary<T, W> dict, W val)
	{
		T key = default;
		foreach(KeyValuePair<T, W> pair in dict)
		{
			if(EqualityComparer<W>.Default.Equals(pair.Value, val))
			{
				key = pair.Key;
				break;
			}
		}
		return key;
	}
}
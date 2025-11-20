using System.Collections;
using System.Collections.Generic;
using LinqBridge;

namespace System.Linq;

internal sealed class Lookup<TKey, TElement> : ILookup<TKey, TElement>, IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
{
	private readonly Dictionary<Key<TKey>, IGrouping<TKey, TElement>> _map;

	private readonly List<Key<TKey>> _orderedKeys;

	public int Count => _map.Count;

	public IEnumerable<TElement> this[TKey key]
	{
		get
		{
			if (!_map.TryGetValue(new Key<TKey>(key), out var value))
			{
				return Enumerable.Empty<TElement>();
			}
			return value;
		}
	}

	internal Lookup(IEqualityComparer<TKey> comparer)
	{
		_map = new Dictionary<Key<TKey>, IGrouping<TKey, TElement>>(new KeyComparer<TKey>(comparer));
		_orderedKeys = new List<Key<TKey>>();
	}

	internal void Add(IGrouping<TKey, TElement> item)
	{
		Key<TKey> key = new Key<TKey>(item.Key);
		_map.Add(key, item);
		_orderedKeys.Add(key);
	}

	internal IEnumerable<TElement> Find(TKey key)
	{
		if (!_map.TryGetValue(new Key<TKey>(key), out var value))
		{
			return null;
		}
		return value;
	}

	public bool Contains(TKey key)
	{
		return _map.ContainsKey(new Key<TKey>(key));
	}

	public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
	{
		if (resultSelector == null)
		{
			throw new ArgumentNullException("resultSelector");
		}
		foreach (KeyValuePair<Key<TKey>, IGrouping<TKey, TElement>> pair in _map)
		{
			KeyValuePair<Key<TKey>, IGrouping<TKey, TElement>> keyValuePair = pair;
			TKey value = keyValuePair.Key.Value;
			KeyValuePair<Key<TKey>, IGrouping<TKey, TElement>> keyValuePair2 = pair;
			yield return resultSelector(value, keyValuePair2.Value);
		}
	}

	public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
	{
		foreach (Key<TKey> key in _orderedKeys)
		{
			yield return _map[key];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

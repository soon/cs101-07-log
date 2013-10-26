using System;
using System.Collections.Generic;

namespace autocomplete
{
	// Внимание!
	// Есть одна распространенная ловушка при сравнении строк: строки можно сравнивать по-разному:
	// с учетом регистра, без учета, зависеть от кодировки и т.п.
	// В файле словаря все слова отсортированы мтодом StringComparison.OrdinalIgnoreCase.
	// Во всех функциях сравнения строк в C# можно передать способ сравнения.
	class Autocompleter
	{
		private readonly string[] items;

		public Autocompleter(string[] loadedItems)
		{
			items = loadedItems;
		}

		// Найти произвольный элемент словаря, начинающийся с prefix.
		// Ускорьте эту фунцию так, чтобы она работала за O(log(n))
		public string FindByPrefix(string prefix)
		{
			foreach (var item in items)
			{
				if (item.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)) return item;
			}
			return null;
		}

		// Найти первые (в порядке следования в файле) 10 (или меньше, если их меньше 10) элементов словаря, 
		// начинающийся с prefix.
		// Эта функция должна работать за O(log(n) + count)
		public string[] FindByPrefix(string prefix, int count)
		{
			return new string[0];
		}

		// Найти количество слов словаря, начинающихся с prefix
		// Эта функция должна работать за O(log(n))
		public int FindCount(string prefix)
		{
			return -1;
		}
	}
}

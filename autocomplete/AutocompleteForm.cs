using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace autocomplete
{
	public partial class AutocompleteForm : Form
	{
		private Autocompleter autocompleter;
		private long count;
		private long sumMs;

		public AutocompleteForm()
		{
			InitializeComponent();
		}

		private void inputBox_TextChanged(object sender, EventArgs e)
        {

#if DEBUG
            autocompleteList.Items.Clear();

			// Stopwatch — это полезный инструмент для того, чтобы точно засекать время.
			// При измерении скорости работы алгоритма нужно использовать именно его.
			Stopwatch sw = Stopwatch.StartNew();
			string prefix = inputBox.Text;


            string[] foundItemsTest = autocompleter.FindByPrefixBF(prefix, 10);
			string[] foundItems = autocompleter.FindByPrefix(prefix, 10);

            int foundItemsCountBF = autocompleter.FindCountBF(prefix);
            int foundItemsCount = autocompleter.FindCount(prefix);

			if (foundItems.Length == 0)
			{
                string oneItem = autocompleter.FindByPrefix(prefix);
                if(oneItem != null)
                    foundItems = new[] { oneItem };

                // testing only
                //var oldOneOtem = autocompleter.FindByPrefixBF(prefix);
                //if(oldOneOtem != null)
                //{
                //    oldOneOtem = "Old: " + oldOneOtem;
                //}

                //string oneItem = autocompleter.FindByPrefix(prefix);
                //if(oneItem != null)
                //{
                //    oneItem = "New: " + oneItem;
                //}

                //foundItems = (new List<string> { oldOneOtem, oneItem })
                //                    .Where(x => x != null)
                //                    .ToArray<string>();
                
			}
			sw.Stop();
			sumMs += sw.ElapsedMilliseconds;
			count++;
			statusLabel.Text = string.Format(
                "Found BF: {0}\n" +
                "Found BS: {1}\n" +
                "The same search result: {2}\n" +
                "Last time: {3} ms\n" +
                "Average time: {4} ms\n",
                foundItemsCount,
                foundItemsCountBF,
                Enumerable.SequenceEqual(foundItemsTest, foundItems),
                sw.ElapsedMilliseconds,
                sumMs / count);

            foreach(var foundItem in foundItemsTest)
            {
                autocompleteList.Items.Add("Test: " + foundItem);
            }

            foreach(string foundItem in foundItems)
            {
                autocompleteList.Items.Add("BS: " + foundItem);
            }
#else
            autocompleteList.Items.Clear();

            // Stopwatch — это полезный инструмент для того, чтобы точно засекать время.
            // При измерении скорости работы алгоритма нужно использовать именно его.
            Stopwatch sw = Stopwatch.StartNew();
            string prefix = inputBox.Text;
            string[] foundItems = autocompleter.FindByPrefix(prefix, 10);
            int foundItemsCount = autocompleter.FindCount(prefix);
            if (foundItems.Length == 0)
            {
                string oneItem = autocompleter.FindByPrefix(prefix);
                    if (oneItem != null)
                        foundItems = new[] {oneItem};
            }
            sw.Stop();
            sumMs += sw.ElapsedMilliseconds;
            count++;
            statusLabel.Text = string.Format("Found: {0}; Last time: {1} ms; Average time: {2} ms", foundItemsCount, sw.ElapsedMilliseconds, sumMs / count);
            foreach (string foundItem in foundItems)
                    autocompleteList.Items.Add(foundItem);
#endif
        }

		private void AutocompleteForm_Load(object sender, EventArgs e)
		{
			string filename = "..\\..\\..\\words.txt";
			if (!File.Exists(filename))
			{
				MessageBox.Show(
					string.Format(
						"Не найден файл со словарем {0}\r\nВозможно вы забыли распаковать файл words.zip",
						Path.GetFullPath(filename)),
					"Ошибка");
				Environment.Exit(1);
			}
			autocompleter = new Autocompleter(File.ReadAllLines(filename));
		}
	}
}
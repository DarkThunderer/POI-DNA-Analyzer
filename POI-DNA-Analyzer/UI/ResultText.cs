using System.IO;
using System.Text;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
    internal class ResultText
    {
        TextBlock _textBlock;

        public ResultText(TextBlock textBlock)
        {
            _textBlock = textBlock;
        }

        public void Show(StreamReader streamReader)
        {
            if (streamReader == null)
                return;

            _textBlock.Text = streamReader.ReadToEnd();
        }

        public void Clear()
        {
            _textBlock.Text = "";
        }

        public void ShowOccurrencesCount(string text)
        {
            _textBlock.Text = $"Occurrences count: {text}";
		}

        public void ShowOccurrencesIndexes(LinkedList<int> indexes)
        {
            _textBlock.Text += "\n";

			StringBuilder sb = new StringBuilder();

			foreach (int index in indexes)
			{
				sb.Append(index.ToString());
				sb.Append(", ");
			}

			if (sb.Length > 0)
				sb.Length -= 2;

			_textBlock.Text += sb.ToString() + "\n";
		}

		public void ShowOccurrencesIndexes(string indexes)
		{
			_textBlock.Text += "\n";

			_textBlock.Text += indexes;
		}
	}
}

using System.IO;
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

        public void ShowOccurrencesCount(string text)
        {
            _textBlock.Text = $"Occurrences count: {text}";
		}
    }
}

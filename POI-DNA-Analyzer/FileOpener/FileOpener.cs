using System.IO;

namespace POI_DNA_Analyzer
{
	internal class FileOpener
    {
        public StreamReader OpenFile(string filePath)
        {
            if (filePath == "")
                return null;

			StreamReader fileStream = new StreamReader(filePath);

            return fileStream;
        }
    }
}

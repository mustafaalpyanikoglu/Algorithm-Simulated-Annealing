using static TSP.Program;

namespace TSP
{
    public static class FilePath
    {
        private static string _filePathWithoutFileName = "C:\\Users\\malpy\\Desktop\\222802005_MustafaAlpYanıkoğlu\\uygulama dosyaları\\";
        public static string GetFilePath(TspFilePaths filePath)
        {
            return $"{_filePathWithoutFileName}{filePath}";
        }
    }
    public enum TspFilePaths
    {
        tsp_5_1 = 1,
        tsp_124_1 = 2,
        tsp_1000_1 = 3,
        tsp_5915_1 = 4,
        tsp_11849_1 = 5,
        tsp_85900_1 = 6
    }
}
using TSP;

namespace ConsoleApp6
{
    public static class ReadFile
    {
        public static List<Point> ReadXandYCordinates(string filePath)
        {
            List<Point> points = new List<Point>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                int numberOfCities = Convert.ToInt32(sr.ReadLine());
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(' ');
                    points.Add(new Point(Convert.ToDouble(values[0]), Convert.ToDouble(values[1])));
                }
            }
            return points;
        }
    }
}

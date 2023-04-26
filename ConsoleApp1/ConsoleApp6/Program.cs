using ConsoleApp6;
using System.Diagnostics;
using System.IO;

namespace TSP
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            LoginScreen();
        }

        private static void LoginScreen()
        {
            Console.WriteLine("\n\tSelect The Number Of Cities");
            Console.WriteLine("1- 5 Cities\n2- 124 Cities\n3- 1000 Cities\n4- 5915 Cities\n5- 11849 Cities\n6- 85900 Cities\n");
            Console.Write("\nLütfen bir sayı girin: ");
            TspFilePaths tspFilePaths = new TspFilePaths();
            string numberString = Console.ReadLine();
            int number;
            if (int.TryParse(numberString, out number))
            {
                if (Enum.IsDefined(typeof(TspFilePaths), number))
                {
                    tspFilePaths = (TspFilePaths)Enum.Parse(typeof(TspFilePaths), number.ToString());
                }
                else
                {
                    Console.WriteLine("Hata: Geçersiz sayı.");
                }
            }
            else
            {
                Console.WriteLine("Hata: Geçersiz giriş.");
            }

            ReadFileAndFindShortestPath(FilePath.GetFilePath(tspFilePaths));
        }

        public static void ReadFileAndFindShortestPath(string filePath)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Point> points = ReadFile.ReadXandYCordinates(filePath);
            List<Point> soln = SimulatedAnnealing.SimulatedAnnealingStart(points);

            List<double> path = SimulatedAnnealing.Path(soln);
            TimeSpan elapsed = stopwatch.Elapsed;
            double seconds = elapsed.TotalSeconds;

            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                writer.Write("222802005, Mustafa Alp Yanıkoğlu\n");
                Console.Write($"1. Sonuç: {points.IndexOf(soln[0])} ");
                writer.Write($"1. Sonuç: {points.IndexOf(soln[0])} ");
                for (int i = 1; i < soln.Count - 1; i++)
                {
                    Console.Write($"{points.IndexOf(soln[i])} ");
                    writer.Write($"{points.IndexOf(soln[i])} ");
                }
                Console.Write($"{points.IndexOf(soln[soln.Count - 1])}\n");
                writer.Write($"{points.IndexOf(soln[soln.Count - 1])}\n");
                writer.Write($"\nTotal distance = {path.Sum()}\n");
                writer.Write($"Programın çalışma süresi: {seconds} saniye");
            }
            Console.WriteLine("------------Koordinatlari------------");
            PrintList(soln);
            Console.WriteLine($"Programın çalışma süresi: {seconds} saniye");
        }

        public static void PrintList(List<Point> points)
        {
            for (int i = 0; i < points.Count-1; i++)
            {
                Console.Write($"({points[i].X}-{points[i].Y}) ->" );
            }
            Console.Write($"({points[points.Count-1].X}-{points[points.Count-1].Y})");
            List<double> path = SimulatedAnnealing.Path(points);
            Console.WriteLine($"\nTotal distance = {path.Sum()}");
        }
    }
}

/*
/////////Pseude Code/////////
FUNCTIO FindShortestPath(take points as parameters)
    select starting point and add to road list
    while (_remainingPoints.Count > 0) //Loop until you have visited all remaining points:
        for each point ∈ _remainingPoints)
            calculate the distance between the most point and the selected point
            if distance < minDistance //if the distance is less
                change shortest distance distance and define new point
                minDistance = distance;
                nextPoint = point;
        add new point to route
        route remove from _remaining Points
        currentPoint = nextPoint;
    end-loop
    return best solution found


FUNCTIO SimulatedAnnealingStart(take points as parameters)
    sorting the list of points with the nearest neighbor algorithm
    #define _maxIter 2500 // Algoritmanın maksimum iterasyon sayısı
    #define _iteration 100 // Algoritmanın mevcut iterasyon sayısı
    #define _startCurrTemperature 1000.0 // Başlangıç sıcaklığı
    #define _minCurrTemperature 0.00001 // Minimum sıcaklık değeri, algoritma sıfırın altındaki sıcaklıklarda sona erer
    #define _alpha 0.99 // Sıcaklık düşme hızı
    #define _error // Mevcut yolun hata değeri
    make a _randomNumber initial guess
    while (_iteration < _maxIter and _error > 0.0) //until the number of iterations (_iteration) is maximum and the error value (_error) is positive
      a new list (_adjRoute) with a random route is created 
      its error is calculated via the variable _adjErr
      _adjErr = Error(_adjRoute);
      if proposed solution (_adjErr) is better than curr solution (_error) then
        points = _adjRoute; // accept the proposed solution
        _error = _adjErr; // accept the new error value of the solution
      else proposed solution is worse then
        accept the worse solution anyway with small probability
      else
        if _randomNumber.NextDouble() < AcceptanceProbability(_error,_adjErr,_currTemperature)
            points = _adjRoute; // accept the proposed solution
            _error = _adjErr; // accept the new error value of the solution
      if _currTemperature < _minCurrTemperature // if the current temperature is less than the minimum temperature
        _currTemperature = _minCurrTemperature;
      else 
        _currTemperature *= _alpha; // reduce current temperature with alpha coefficient
      iteration += 1;
      end-if
      reduce temperature slightly
    end-loop
    return best solution found

 
 */
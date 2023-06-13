using System.Net.Http.Headers;
using TSP;

namespace ConsoleApp6
{
    public class SimulatedAnnealing
    {
        public static List<Point> Adjacent(List<Point> route, Random random)
        {
            int _routeCount = route.Count;
            List<Point> result = new List<Point>(route);
            int i = random.Next(_routeCount); 
            int j = random.Next(_routeCount);
            
            Point _tempPoint = result[i];
            result[i] = result[j];
            result[j] = _tempPoint;
            return result;
        }
        public static double Error(List<Point> points)
        {
            List<double> _dists = Path(points);
            int minDist = points.Count - 1;
            return _dists.Sum() - minDist;
        }
        
        public static List<double> Path(List<Point> points)
        {
            List<double> _dists = new List<double>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                _dists.Add(Dist(points[i], points[i + 1]));
            }
            return _dists;
        }

        static double Dist(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
        public static List<Point> FindShortestPat2h(List<Point> points)
        {
            List<Point> _path = new List<Point>();
            List<Point> _remainingPoints = new List<Point>(points);

            // İlk noktayı seç ve yolu ekle
            Point currentPoint = _remainingPoints.First();
            _path.Add(currentPoint);
            _remainingPoints.Remove(currentPoint);

            // Kalan tüm noktaları ziyaret et
            while (_remainingPoints.Count > 0)
            {
                double minDistance = double.MaxValue;
                Point nextPoint = null;

                // En yakın noktayı bul
                foreach (Point point in _remainingPoints)
                {
                    double distance = Dist(currentPoint, point);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nextPoint = point;
                    }
                }
                // Yolu ekle ve devam et
                _path.Add(nextPoint);
                _remainingPoints.Remove(nextPoint);
                currentPoint = nextPoint;
            }
            Console.WriteLine(Path(_path).Sum());
            return _path;
        }
        public static List<Point> FindShortestPath(List<Point> points)
        {
            List<Point> _path = new List<Point>();
            List<List<Point>> _paths = new List<List<Point>>();
            // Her eleman için döngü
            foreach (Point startPoint in points)
            {
                List<Point> _remainingPoints = new List<Point>(points);

                // İlk noktayı seç ve yolu ekle
                Point currentPoint = startPoint;
                _remainingPoints.Remove(currentPoint);
                _path.Add(currentPoint);

                // Kalan tüm noktaları ziyaret et
                while (_remainingPoints.Count > 0)
                {
                    double minDistance = double.MaxValue;
                    Point nextPoint = null;

                    // En yakın noktayı bul
                    foreach (Point point in _remainingPoints)
                    {
                        double distance = Dist(currentPoint, point);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nextPoint = point;
                        }
                    }
                    // Yolu ekle ve devam et
                    _path.Add(nextPoint);
                    _remainingPoints.Remove(nextPoint);
                    currentPoint = nextPoint;
                }
                _paths.Add(_path);
                _path = new List<Point>();
            }
            _path = _paths.First();
            for (int i = 0; i < _paths.Count-1; i++)
            {
                
                if (Path(_paths[i]).Sum() < Path(_paths[i + 1]).Sum())
                {
                    _path = _paths[i];
                }
            }
            Console.WriteLine(Path(_path).Sum());
            return _path;
        }
        private static double AcceptanceProbability(double error, double adjError, double currTemperature)
        {
            return Math.Exp((error - adjError) / currTemperature);
        }

        public static List<Point> SimulatedAnnealingStart(List<Point> points)
        {
            points = SimulatedAnnealing.FindShortestPath(points);
            Random _randomNumber = new Random(points.Count);
            int _maxIter = 250000;
            double _minCurrTemperature = 0.00001;
            double _startTemperature = 1000.0; // başlangıç sıcaklığı
            double _alpha = 0.99; // sıcaklığın düşme hızı
            int _iteration = 0;
            double _currTemperature = _startTemperature;

            double _error = Error(points);

            while (_iteration < _maxIter && _error > 0.0)
            {
                List<Point> _adjRoute = Adjacent(points, _randomNumber);
                double _adjErr = Error(_adjRoute);
                if (_adjErr < _error) // rota daha iyiyse kabul et
                {
                    points = _adjRoute;
                    _error = _adjErr;
                }
                else 
                {
                    if (_randomNumber.NextDouble() < AcceptanceProbability(_error,_adjErr,_currTemperature)) //yine de kabul et
                    {
                        points = _adjRoute;
                        _error = _adjErr;
                    }
                    // başka kabul etme
                }

                if (_currTemperature < _minCurrTemperature)
                {
                    _currTemperature = _minCurrTemperature;
                }
                else
                {
                    _currTemperature *= _alpha;
                }
                _iteration++;
            }
            return points;
        }
    }
}



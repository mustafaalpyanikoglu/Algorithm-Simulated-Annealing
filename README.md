# Algorithm-Simulated-Annealing
 - Developing the necessary algorithm for solving the TCP (Travel Salesman Problem) problem

## Explanation about the problem
 - The algorithm to be developed should be run for each file of 5 different sizes (5, 124, 1000, 5915, 11849).

 - Sample File / Sample Output

<img src="https://github.com/mustafaalpyanikoglu/Algorithm-Simulated-Annealing/assets/79158705/42b53892-a077-46f9-a18e-f2198fefc94d" width="400" height="250" alt="">
<img src="https://github.com/mustafaalpyanikoglu/Algorithm-Simulated-Annealing/assets/79158705/91e2790f-763e-4719-a4f8-1d156d88bcc6" width="400" height="250" alt="">


 - The information of the x and y coordinates in the 5-dimensional sample file is as follows:
 - x,y = ([0.0], [0,0.5], [0,1], [1,1], [1.0])
 - The value in the first line represents the file size (number of nodes/city to visit). In the next lines, the first column indicates the x coordinates of these cities, and the second column indicates the y coordinates.
## TCP Algorithm:
Input: 5, 124, 1000, 5915, 11849 size files

Function: Algorithm to find the optimal solution for the TCP problem

Output: Optimal cost value
           Nodes (cities) to go to in order for optimal solution

For sample output;
Optimal cost value: 4
Path providing optimal cost: 0 –> 1 –> 2 –> 3 –> 4
Starting point ‘0. was accepted as 'node' and went to nodes 1, 2, 3, 4, respectively. In this way, all nodes are passed through.

<img src="https://github.com/mustafaalpyanikoglu/Algorithm-Simulated-Annealing/assets/79158705/cb1aec20-cc71-4c19-8d18-e0419d7d0b36" width="400" height="250" alt="">
<img src="https://github.com/mustafaalpyanikoglu/Algorithm-Simulated-Annealing/assets/79158705/c59ad25a-0488-40ab-8b51-d077a85f6b05" width="400" height="250" alt="">


## Pseudo Code Of The Algorithm
```
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
    ```
    

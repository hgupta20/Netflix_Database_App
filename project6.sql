SELECT Stations.StationID, Stations.Name, AVG(DailyTotal) AS AvgDailyRidership, Count(StopID) AS TotalStops
	FROM Stations
	LEFT JOIN Riderships ON Stations.StationID = Riderships.StationID
    FULL OUTER JOIN Stops ON Stations.StationID = Stops.StationID
	WHERE Stations.Name LIKE '%lake%'
	GROUP BY Stations.StationID, Stations.Name
	ORDER BY Stations.Name ASC;

SELECT StopID FROM Stops WHERE StationID = '40380';

SELECT * FROM Stations FULL JOIN Riderships ON Stations.StationID = Riderships.StationID WHERE Stations.StationID = '40380' GROUP BY Stations.StationID, Stations.Name ORDER BY Stations.Name ASC;
SELECT Stations.StationID, Stations.Name, AVG(DailyTotal) AS AvgDailyRidership,COUNT (Distinct StopID) AS TotalStops, COUNT(StopID) AS Stops, sum(case when Stops.ADA = 1 then 1 else 0 end) AS HAccessible
	FROM Stations
	LEFT JOIN Riderships ON Stations.StationID = Riderships.StationID
	FULL JOIN Stops ON Riderships.StationID = Stops.StationID
	WHERE Stations.Name LIKE '%lake%'
	GROUP BY Stations.StationID, Stations.Name
	ORDER BY Stations.Name ASC;

SELECT Stations.StationID, Stations.Name, AVG(DailyTotal) AS AvgDailyRidership, StopID, ADA
	FROM Stations
	LEFT JOIN Riderships ON Stations.StationID = Riderships.StationID
	FULL JOIN Stops ON Riderships.StationID = Stops.StationID
	WHERE Stations.Name LIKE '%lake%'
	GROUP BY Stations.StationID, Stations.Name, StopID, ADA
	ORDER BY Stations.Name ASC

SELECT Stops.StopID, Stops.Name, Stops.Direction, Stops.ADA, Stops.Latitude, Stops.Longitude, Lines.Color FROM Stops 
RIGHT JOIN StopDetails ON Stops.StopID = StopDetails.StopID
RIGHT JOIN Lines ON StopDetails.LineID = Lines.LineID
WHERE StationID = '40710' 
GROUP BY Stops.StationID, Stops.StopID, Stops.Name, Stops.Direction, Stops.ADA, Stops.Latitude, Stops.Longitude, Lines.Color
Order BY Lines.Color ASC;


SELECT Stops.StopID, Stops.Name, Stops.Direction, Stops.ADA, Stops.Latitude, Stops.Longitude, Lines.Color FROM Stops 
        RIGHT JOIN StopDetails ON Stops.StopID = StopDetails.StopID
        RIGHT JOIN Lines ON StopDetails.LineID = Lines.LineID
        WHERE StationID = '40710' 
        GROUP BY Stops.StationID, Stops.StopID, Stops.Name, Stops.Direction, Stops.ADA, Stops.Latitude, Stops.Longitude, Lines.Color
        Order BY Lines.Color ASC;

SELECT YEAR(TheDate) AS Year, SUM(DailyTotal) AS NumRiders FROM Riderships GROUP BY YEAR(TheDate) ORDER BY YEAR ASC;
SELECT YEAR(TheDate) AS Year, SUM(DailyTotal) AS NumRiders FROM Riderships WHERE StationID = '40350' GROUP BY YEAR(TheDate) ORDER BY YEAR ASC;
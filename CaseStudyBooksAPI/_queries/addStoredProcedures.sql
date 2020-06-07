CREATE OR ALTER PROCEDURE pTop3ClosestBranches(@lat float, @lng float)
AS
UPDATE Branches SET Distance = SQRT(POWER(@lat - Branches.Latitude, 2) + POWER(@lng - Branches.Longitude, 2)) * 62.1371192 * 1.60934;
SELECT TOP(3) Id, Street, City, Region, Latitude, Longitude, Distance
FROM Branches
ORDER BY Distance ASC;

exec pTop3ClosestBranches 43.653226, -79.3832;
GO
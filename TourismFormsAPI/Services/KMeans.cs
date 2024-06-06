using System;
using System.Collections.Generic;
using System.Linq;

class KMeans
{
    private List<double> data;
    private double[] centroids;
    private Dictionary<int, List<int>> clusters;

    public KMeans(List<double> data)
    {
        this.data = data;
        Initialize();
    }

    private void Initialize()
    {
        // Initialize centroids randomly
        Random random = new Random();
        centroids = new double[5];
        for (int i = 0; i < centroids.Length; i++)
        {
            centroids[i] = data[random.Next(data.Count())];
        }
    }

    public void Cluster()
    {
        clusters = new Dictionary<int, List<int>>();
        for (int i = 0; i < centroids.Length; i++)
        {
            clusters[i] = new List<int>();
        }

        foreach (int point in data)
        {
            int nearestCentroid = FindNearestCentroid(point);
            clusters[nearestCentroid].Add(point);
        }

        UpdateCentroids();
    }

    private int FindNearestCentroid(double point)
    {
        int nearestCentroid = 0;
        double minDistance = Math.Abs(point - centroids[0]);
        for (int i = 1; i < centroids.Length; i++)
        {
            double distance = Math.Abs(point - centroids[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCentroid = i;
            }
        }
        return nearestCentroid;
    }

    private void UpdateCentroids()
    {
        for (int i = 0; i < centroids.Length; i++)
        {
            if (clusters[i].Count > 0)
            {
                centroids[i] = (int)clusters[i].Average();
            }
        }
    }

    public void PrintClusters()
    {
        for (int i = 0; i < centroids.Length; i++)
        {
            Console.WriteLine($"Cluster {i + 1}: {string.Join(", ", clusters[i])}");
        }
    }

    public int FindClusterIndex(double number)
    {
        int nearestCentroid = FindNearestCentroid(number);
        return nearestCentroid;
    }
}
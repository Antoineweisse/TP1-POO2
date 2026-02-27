public interface IDistance
{
    double CalculateDistance(Seed a, Seed b);
}


public class EuclideanDistance : IDistance
{
    public double CalculateDistance(Seed a, Seed b)
    {
        
        return Math.Sqrt(
            Math.Pow(a.Area - b.Area, 2) +
            Math.Pow(a.Perimeter - b.Perimeter, 2) +
            Math.Pow(a.Compactness - b.Compactness, 2) +
            Math.Pow(a.LengthOfKernel - b.LengthOfKernel, 2) +
            Math.Pow(a.WidthOfKernel - b.WidthOfKernel, 2) +
            Math.Pow(a.AsymmetryCoefficient - b.AsymmetryCoefficient, 2) +
            Math.Pow(a.LengthOfGroove - b.LengthOfGroove, 2)
        );
    }
}


public class ManhattanDistance : IDistance
{
    public double CalculateDistance(Seed a, Seed b)
    {
        return 
            Math.Abs(a.Area - b.Area) + 
            Math.Abs(a.Perimeter - b.Perimeter) + 
            Math.Abs(a.Compactness - b.Compactness) + 
            Math.Abs(a.LengthOfKernel - b.LengthOfKernel) +
            Math.Abs(a.WidthOfKernel - b.WidthOfKernel) + 
            Math.Abs(a.AsymmetryCoefficient - b.AsymmetryCoefficient) +
            Math.Abs(a.LengthOfGroove - b.LengthOfGroove);
    }
}
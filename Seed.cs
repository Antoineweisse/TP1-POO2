public class Seed
{
    public string Variety{ get; set; } = string.Empty;
    public double Area{ get; set; }
    public double Perimeter{ get; set; }
    public double Compactness{ get; set; }
    public double LengthOfKernel{ get; set; }
    public double WidthOfKernel{ get; set; }
    public double AsymmetryCoefficient{ get; set; }
    public double LengthOfGroove{ get; set; }

    public Seed(string variety, double area, double perimeter, double compactness, double lengthOfKernel, double widthOfKernel, double asymmetryCoefficient, double lengthOfGroove)
    {
        this.Variety = variety;
        this.Area = area;
        this.Perimeter = perimeter;
        this.Compactness = compactness;
        this.LengthOfKernel = lengthOfKernel;
        this.WidthOfKernel = widthOfKernel;
        this.AsymmetryCoefficient = asymmetryCoefficient;
        this.LengthOfGroove = lengthOfGroove;
    }
}
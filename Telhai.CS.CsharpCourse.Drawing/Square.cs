using Telhai.CS.CsharpCourse.Drawing;

public class Square : Drawing
{
    public double Length { get; set; }

    public Square()
    {
        Length = 6;
    }

    public override double Area()
    {
        return Length * Length;
    }

    public override string ToString()
    {
        return $"Square (ID = {Id}, Length = {Length}, Area = {Area()})";
    }
}
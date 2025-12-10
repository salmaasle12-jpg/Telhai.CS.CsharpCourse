using Telhai.CS.CsharpCourse.Drawing;

public class Rectangle : Drawing
{
    public double Height { get; set; }
    public double Width { get; set; }

    public Rectangle()
    {
        Height = 5.3;
        Width = 3.4;
    }

    public override double Area()
    {
        return Height * Width;
    }

    public override string ToString()
    {
        return $"Rectangle (ID = {Id}, Height = {Height}, Width = {Width}, Area = {Area()})";
    }
}

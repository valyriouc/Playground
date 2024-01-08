using System.Data;

namespace LiskovTesting;

internal interface IGeometryFigure
{
    public double CalcSurface();
}

internal class Quadrat : IGeometryFigure
{
    public int Breite { get; set; }

    public double CalcSurface()
    {
        return Breite * Breite;
    }
}

internal class Rechteck : IGeometryFigure
{
    public int Breite { get; set; }

    public int Hoehe { get; set; }

    public double CalcSurface()
    {
        return Breite * Hoehe;
    }
}

internal static class Program
{
    public static void Main()
    {
        Quadrat quad = new Quadrat();
        quad.Breite = 5;

        double res1 = Calculate(quad);
        Console.WriteLine(res1);

        Rechteck recht = new Rechteck();
        recht.Hoehe = 2;
        recht.Breite = 4;

        double res2 = Calculate(recht);
        Console.WriteLine(res2);
    }

    public static double Calculate(IGeometryFigure figure)
    {
        return figure.CalcSurface();
    }
}
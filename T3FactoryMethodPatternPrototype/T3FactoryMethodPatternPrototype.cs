using System;
using System.Collections.Generic;

namespace T3FactoryMethodPatternPrototype
{
    public abstract class Figure : ICloneable //абстрактный класс фигуры
    {
        public abstract string NameFigure { get; }
        public abstract int CellsСount { get; }

        public string FigureType
        {
            get { return CellsСount > 4 ? "Супер-фигура" : "Обычная фигура"; }
        }
        public abstract object Clone();

        public void PrintProperties()
        {
            Console.WriteLine($"{NameFigure} (Число клеток: {CellsСount}) - {FigureType}");
        }
    }

    //конкретные фигуры
    public class Line: Figure //линия
    {
        public override string NameFigure => "Линия";
        public override int CellsСount => 4;

        public override object Clone()
        {
            return new Line();
        }
    }

    public class Square: Figure //квадрат
    {
        public override string NameFigure => "Квадрат";
        public override int CellsСount => 4;

        public override object Clone()
        {
            return new Square();
        }
    }
    //супер-фигуры

    public class Cross : Figure //крест
    {
        public override string NameFigure => "Крест";
        public override int CellsСount => 5;

        public override object Clone()
        {
            return new Cross();
        }
    }
    public class SuperLine: Figure //супер-линия
    {
        public override string NameFigure => "Супер-линия";
        public override int CellsСount => 6;

        public override object Clone()
        {
            return new SuperLine();
        }
    }

    public class FigureFactory //класс фабрики фигур
    {
        private readonly List<Figure> prototypes;
        private readonly Random rnd;

        public FigureFactory()
        {
            rnd = new Random();
            prototypes = new List<Figure>
        {
            new Line(),
            new Square(),
            new Cross(),
            new SuperLine()
        };
        }

        public Figure GenerationRNDFigure()
        {
            int index = rnd.Next(prototypes.Count);
            return (Figure) prototypes[index].Clone();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            FigureFactory factory = new FigureFactory();

            for (int i = 0; i < 3; i++)
            {
                Figure figure = factory.GenerationRNDFigure();
                Figure clnfigure = (Figure)figure.Clone();

                Console.WriteLine("Оригинал фигуры:");
                figure.PrintProperties();

                Console.WriteLine("Копия фигуры:");
                clnfigure.PrintProperties();

                Console.WriteLine(new string('=', 30));
            }
        }
    }
}

using System;
using System.Linq;
using CSharpBasics.Entities;

namespace CSharpBasics
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Action<object> cw = (obj) => Console.WriteLine(obj.ToString());
            cw("-----");

            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");

            // Calculate distances
            cw("#1 " + new Route("ABC", matrix));
            cw("#2 " + new Route("AD", matrix));
            cw("#3 " + new Route("ADC", matrix));
            cw("#4 " + new Route("AEBCD", matrix));
            cw("#5 " + new Route("AED", matrix));
            var finder = new RouteFinder(matrix);
            // Output 6 -> 2
            cw("---");
            var routes = finder.GetRoutesWithLessOrMaxStops("C", "C", 3);
            cw("#6 " + routes.Select(x => x.ToString()).Aggregate((current, next) => current + "\n#6 " + next));
            // Output 7 -> 3
            cw("---");
            routes = finder.GetRoutesWithMaxStops("A", "C", 4);
            cw("#7 " + routes.Select(x => x.ToString()).Aggregate((current, next) => current + "\n#7 " + next));
            cw("---");
            // Output 8,9 -> 9
            var route = finder.GetShortestRoute("A", "C");
            cw("#8 " + route);
            route = finder.GetShortestRoute("B", "B");
            cw("#9 " + route);

            // Output 10
            routes = finder.GetRoutes("C", "C", 30);
            cw("#10 Number of routes C to C with distance 30 " + routes.Count());
        }
    }
}



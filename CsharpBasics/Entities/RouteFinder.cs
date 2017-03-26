using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpBasics.Entities
{
    public interface IRouteFinder
    {
        IEnumerable<Route> GetRoutesWithMaxStops(string origin, string target, int maxStops);
        IEnumerable<Route> GetRoutesWithLessOrMaxStops(string origin, string target, int stops);
        IEnumerable<Route> GetRoutes(string origin, string target, int maxDistance);
        Route GetShortestRoute(string origin, string target);
    }

    public static class StringExtensions
    {
        public static string Head(this string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.ToCharArray().First().ToString();
        }

        public static string Tail(this string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return value.ToCharArray().Last().ToString();
        }
    }

    public class RouteFinder : IRouteFinder
    {
        private IMatrix matrix;

        public RouteFinder(IMatrix matrix)
        {
            this.matrix = matrix;
        }

        public IEnumerable<Route> GetRoutesWithMaxStops(string origin, string target, int maxStops)
        {
            checkParams(origin , target);
            return doIterate(origin, target, maxStops:maxStops).Select(x=> new Route(x, matrix)).ToList();
        }

        public IEnumerable<Route> GetRoutesWithLessOrMaxStops(string origin, string target, int stops)
        {
            checkParams(origin , target);
            return doIterate(origin, target, maxStops: stops, lessOrEqualStops:true).Select(x=> new Route(x, matrix)).ToList();
        }

        public IEnumerable<Route> GetRoutes(string origin, string target, int maxDistance)
        {
            checkParams(origin , target);
            return doIterate(origin, target, maxDistance:maxDistance).Select(x=> new Route(x, matrix)).ToList();
        }

        public Route GetShortestRoute(string origin, string target)
        {
            checkParams(origin , target);
            var shortest = doIterate(origin, target, shortestRoute:true).OrderBy(x => x.Length).FirstOrDefault();
            return new Route(shortest, matrix);
        }

        private static void checkParams(string origin, string target)
        {
            if(origin.Count() > 1 || target.Count() > 1)
                throw new ArgumentException("Origin and target are only one letter");
        }

        private IEnumerable<string> doIterate(string source, string target,
            int maxStops = -1,
            int maxDistance = -1,
            bool shortestRoute = false,
            bool lessOrEqualStops = false)
        {
            var idx = -1;
            var minWeight = int.MaxValue;
            var results = new List<string>();
            var pending = new List<string>();
            var last_node = source.Tail();
            foreach (var weight in matrix.GetRow(last_node))
            {
                idx++;
                if (weight == -1) continue;
                pending.Add($"{source[0]}{matrix.NameOf(idx)}");
            }

            while (true)
            {
                var nextIteration = new List<string>();
                foreach (var trunk in pending)
                {
                    idx = -1;
                    last_node = trunk.Tail();
                    foreach (var weight in matrix.GetRow(last_node))
                    {
                        idx++;
                        if (weight == -1) continue;
                        string newRoute = $"{trunk}{matrix.NameOf(idx)}";

                        // Stops
                        if (maxStops != -1)
                        {
                            if (lessOrEqualStops && newRoute.Length <= maxStops + 1)
                            {
                                results.Add(newRoute);
                            }
                            if (!lessOrEqualStops && newRoute.Length == maxStops + 1)
                            {
                                results.Add(newRoute);
                                continue;
                            }
                            if (newRoute.Length > maxStops + 1)
                                continue;
                        }
                        // Distance
                        if (maxDistance != -1)
                        {
                            var w = new Route(newRoute, matrix).Weight;
                            if (w < maxDistance)
                                results.Add(newRoute);
                            else
                                continue;
                        }
                        // ShortestRoute
                        if (shortestRoute)
                        {
                            var tmp = new Route(newRoute, matrix).Weight;
                            // Discard this route if it is already larger than the minWeight
                            if (tmp > minWeight) continue;
                            if(newRoute.Head() == source && newRoute.Tail() == target)
                                minWeight = tmp;
                            results.Add(newRoute);
                        }
                        nextIteration.Add(newRoute);
                    }
                }
                pending = nextIteration;

                // All branches were already processed
                if (!pending.Any())
                    break;
            }

            // Filter the results by origin and target
            if (results.Any())
                return results.Where(x => x.Head() == source &&
                                          x.Tail() == target)
                              .Select(x=>x).Distinct().ToList();
            else
                return results;
        }
    }
}
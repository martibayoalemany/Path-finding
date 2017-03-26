using System;
using System.Collections.Generic;
using System.Linq;
using TrainsCSharpMA;
using Xunit;

namespace CSharpBasics.test
{
    public class RouteFinderTest : IDisposable, IRouteFinder
    {
        private IRouteFinder finder;

        public RouteFinderTest()
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            finder = new RouteFinder(matrix);
        }
        public void Dispose()
        {
            finder = null;
        }

        [Theory]
        [InlineData("A","C", 4)]
        public IEnumerable<Route> GetRoutesWithMaxStops(string origin, string target, int maxStops)
        {
            var routes = finder.GetRoutesWithMaxStops(origin, target, maxStops);
            Assert.Equal(3, routes.Count());
            return null;
        }

        [Theory]
        [InlineData("C","C",3)]
        public IEnumerable<Route> GetRoutesWithLessOrMaxStops(string origin, string target, int stops)
        {
            var routes = finder.GetRoutesWithLessOrMaxStops(origin, target, stops);
            Assert.Equal(2, routes.Count());
            return null;
        }

        [Theory]
        [InlineData("C","C",30)]
        public IEnumerable<Route> GetRoutes(string origin, string target, int maxDistance)
        {
            var routes = finder.GetRoutes("C", "C", 30);
            Assert.Equal(7, routes.Count());
            return null;
        }

        [Theory]
        [InlineData("A","C")]
        [InlineData("B","B")]
        public Route GetShortestRoute(string origin, string target)
        {
            var route = finder.GetShortestRoute(origin, target);
            Assert.Equal(9, route.Weight);
            return null;
        }
    }
}
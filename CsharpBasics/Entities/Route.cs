using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CSharpBasics.Entities
{
    public class Route
    {
        public string Name { get; private set; }
        public bool IsConnected { get; private set; }
        public int Weight { get; private set; }

        public Route(string route, IMatrix matrix)
        {
            Name = route;
            var idx = 0;
            IsConnected = true;
            while(IsConnected && idx < route.Length - 1 && route.Length > 1)
            {
                var w = matrix.Weight(route[idx].ToString(), route[idx + 1].ToString());
                Weight = Weight + w;
                if (Weight != -1)
                    IsConnected = true;
                idx++;
            }
        }

        public override string ToString()
        {
            return (IsConnected)? $"Length of {this.Name, 6} is {this.Weight, 5}":
                $"{this.Name,6} NO SUCH A ROUTE";
        }
    }
}
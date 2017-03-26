using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpBasics
{
    public struct Edge
    {
        public char Source { get; set; }
        public char Target { get; set; }
        public int Weight { get; set; }
    }

    public interface IMatrix
    {
        int IndexOf(char nodeName);
        string NameOf(int nodeIdx);
        int Weight(int source_idx, int target_idx);
        int Weight(string source_name, string target_name);
        int Count { get; }
        IEnumerable<int> GetRow(string nodeName);
        IEnumerable<int> GetRow(int idx);
    }

    public class StandardMatrix : IMatrix
    {
        private IList<char> edge_names;
        private int[,] distance_matrix;

        public StandardMatrix(string graphStr)
        {
            var regx = new Regex(@"(?<source>\w)(?<target>\D+)(?<weight>\d+)", RegexOptions.Compiled);
            var edges = graphStr.Split(',').Select(x =>
            {
                var match = regx.Match(x);
                if(!match.Success)
                    throw new ArgumentException("graphStr does not have the correct format");
                if(match.Groups["target"].Length > 1 )
                    throw new ArgumentException("graphStr does not have the correct format");
                return new Edge
                {
                    Source = match.Groups["source"].Value[0],
                    Target = match.Groups["target"].Value[0],
                    Weight = int.Parse(match.Groups["weight"].Value)
                };
            }).ToList();
            initialize(edges);
        }

        public StandardMatrix(IList<Edge> edges)
        {
            initialize(edges);
        }

        private void initialize(IList<Edge> edges )
        {

            edge_names = edges.SelectMany(x => new char[] {x.Target, x.Source}).OrderBy(x => x).Distinct().ToList();

            // Initialize the distance matrix to -1
            Count = edge_names.Count;
            distance_matrix = new int[Count, Count];
            for (var i = 0; i < Count; i++)
                for (var j = 0; j < Count; j++)
                    distance_matrix[i, j] = -1;

            // Initialize the weights matrix using the edges provided
            foreach (var edge in edges)
            {
                var i = edge_names.IndexOf(edge.Source);
                var j = edge_names.IndexOf(edge.Target);
                distance_matrix[i, j] = edge.Weight;
            }

        }

        public int IndexOf(char nodeName)
        {
            return this.edge_names.IndexOf(nodeName);
        }

        public string NameOf(int nodeIdx)
        {
            return this.edge_names[nodeIdx].ToString();
        }


        public int Weight(string source_name, string target_name)
        {
            if(source_name.Length > 1 || target_name.Length> 1)
                throw new ArgumentException("Only nodes with one letter are actually supported");

            var source_idx = this.IndexOf(source_name[0]);
            var target_idx = this.IndexOf(target_name[0]);
            return this.Weight(source_idx, target_idx);
        }

        public int Weight(int source_idx, int target_idx)
        {
            return this.distance_matrix[source_idx, target_idx];
        }

        public int Count { get; private set; }

        public IEnumerable<int> GetRow(string nodeName)
        {
            return GetRow(IndexOf(nodeName[0]));
        }

        public IEnumerable<int> GetRow(int idx)
        {
            for (var i = 0; i < Count; i++)
                yield return this.distance_matrix[idx, i];
        }
    }

}
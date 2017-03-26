using System;
using System.Collections.Generic;
using System.Linq;
using CSharpBasics.Entities;

namespace CsharpBasics.test
{
    public class StandardMatrixTest : IDisposable, IMatrix
    {
        public IMatrix Matrix { get; private set; }

        public StandardMatrixTest()
        {
            Matrix = Substitute.For<IMatrix>();
        }

        public void Dispose()
        {
            Matrix = null;
        }

        [Fact]
        [Trait("Category", "Constructor")]
        public void ConstructorString()
        {
            Assert.Throws<ArgumentException>(() => new StandardMatrix("ABB5, BBC4, CBD8"));
            Assert.Throws<ArgumentException>(() => new StandardMatrix("5BA, 4BA, 8BD"));
        }

        [Fact]
        [Trait("Category", "Constructor")]
        public void ConstructorEdges()
        {
            var edges = new List<Edge>()
            {
                new Edge {Source = 'A', Target = 'B', Weight = 5},
                new Edge {Source = 'B', Target = 'C', Weight = 4},
            };
            var mat = new StandardMatrix(edges);
            Assert.Equal(3, mat.Count);
        }

        [Theory]
        [InlineData('A')]
        public int IndexOf(char nodeName)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.Equal(0, matrix.IndexOf(nodeName));
            return -1;
        }

        [Theory]
        [InlineData(0)]
        public string NameOf(int nodeIdx)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.Equal("A", matrix.NameOf(nodeIdx));
            return "A";
        }

        [Theory]
        [InlineData(0,1)]
        [InlineData(0,3)]
        public int Weight(int source_idx, int target_idx)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            matrix.Weight(source_idx, target_idx);
            return -1;
        }

        [Theory]
        [InlineData('A','B')]
        [InlineData('A','D')]
        public int Weight(string source_name, string target_name)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.Equal(5, matrix.Weight(source_name, target_name));
            return -1;
        }

        [Fact]
        public int Weight()
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.Throws<ArgumentException>(() => matrix.Weight("AA", "BBB"));
            return -1;
        }

        public IEnumerable<int> GetRow(string nodeName)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.NotEmpty(matrix.GetRow('A'));
            return Enumerable.Empty<int>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        public IEnumerable<int> GetRow(int idx)
        {
            IMatrix matrix = new StandardMatrix("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            Assert.NotEmpty(matrix.GetRow(idx));
            return Enumerable.Empty<int>();
        }

        public int Count { get; }


    }

}
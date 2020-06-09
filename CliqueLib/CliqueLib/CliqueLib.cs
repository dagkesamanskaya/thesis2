using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CliqueLib
{
    public class CliqueLib
    {

        public static List<List<T>> CliquesToStrategies<T>(List<List<int>> cliques, T[] seq) where T : IInput
        {
            List<List<T>> res = new List<List<T>>();
            foreach (var cliq in cliques)
            {
                List<T> resLine = new List<T>();
                foreach(var v in cliq)
                {
                    resLine.Add(seq[v]);
                }
                res.Add(resLine);
            }
            return res;
        }
        public static List<List<T>> GraphToStrategies<T>(List<List<int>> comp, T[] seq) where T : IInput
        {
            List<List<T>> res = new List<List<T>>();
            for (int i = 0; i < comp.Count; i++)
            {
                List<T> resLine = new List<T>();
                for (int j = 0; j < comp[i].Count; j++)
                {
                    resLine.Add(seq[comp[i][j]]);
                }
                res.Add(resLine);
            }
            return res;
        }

        public static List<List<T>> CreateComponents<T>(T[] seq) where T : IInput
        {
            List<List<T>> ComponentsList = new List<List<T>>();

            Graph g = new Graph(seq.Length); //isWeaker
            //проверять, если один сильнее другого, то 
            for (int i = 0; i < seq.Length; i++)
            {

                for (int j = i + 1; j < seq.Length; j++)
                {
                    bool checkInc = (seq[i].CheckInconsistency(seq[j]));
                    if (checkInc)
                    {
                        g.addEdgeUndir(i, j);
                    }

                }

                //bool? isWeaker = seq[i].IsWeaker(seq[j]);
            }


            return GraphToStrategies(g.connectedComponents(), seq);

        }

        public static List<List<T>> CreateStrongComponents<T>(T[] seq) where T : IInput
        {
            List<List<T>> ComponentsList = new List<List<T>>();

            Graph g = new Graph(seq.Length); //isWeaker
            //проверять, если один сильнее другого, то 
            for (int i = 0; i < seq.Length; i++)
            {

                for (int j = i + 1; j < seq.Length; j++)
                {
                    bool checkInc = (seq[i].CheckInconsistency(seq[j]));
                    if (checkInc)
                    {
                        g.addEdgeDir(i, j);
                    }

                }

            }

            return GraphToStrategies(g.stronglyConnectedComponents(), seq);

        }

        public static List<List<T>> GreedySetCover<T>(T[] seq) where T : IInput
        {


            return null;
        }

        public static List<List<T>> PowerSetCover<T>(T[] seq) where T : IInput
        {
            return null;
        }


        public static List<T> MaxCliqueToStrategies<T>(int[] clique, T[] seq) where T : IInput
        {
            List<T> maxClique = new List<T>();
            for (int i = 0; i < clique.Length; i++)
            {
                if (clique[i] == 1)
                {
                    maxClique.Add(seq[i]);
                }
            }
            return maxClique;
        }
        public static List<T> MaxClique<T>(T[] seq) where T : IInput
        {

            Graph g = new Graph(seq.Length);

            for (int i = 0; i < seq.Length; i++)
            {

                for (int j = i + 1; j < seq.Length; j++)
                {
                    bool checkInc = (seq[i].CheckInconsistency(seq[j]));
                    if (checkInc)
                    {
                        g.addEdgeUndir(i, j);
                    }

                }

            }



            g.search();

            int[] clique = g.solution;

            return MaxCliqueToStrategies(clique, seq);
        }

        public static List<List<int>> BronKerbosch(HashSet<int>[] AdjListArray, List<List<int>> cliques, HashSet<int> remainingNodes, HashSet<int> potentialClique, HashSet<int> skipNodes)
        {
            //Console.WriteLine("1 potential clique [{0}]", String.Join(",", potentialClique));
            //Console.WriteLine("1 remaining nodes [{0}]", String.Join(",", remainingNodes));
            //Console.WriteLine("1 skip nodes [{0}]", String.Join(",", skipNodes));

            if (remainingNodes.Count == 0 & skipNodes.Count == 0)
            {
                List<int> copyClique = new List<int>(potentialClique);
                copyClique.Sort();
                cliques.Add(copyClique);
                return null;
            }
            while(remainingNodes.Any())
            {
                int v = remainingNodes.First();
                //Console.WriteLine(v);
                HashSet<int> newPotentialClique = potentialClique;
                newPotentialClique.Add(v);
                //Console.WriteLine("2 potential clique [{0}]", String.Join(",", newPotentialClique));
                HashSet<int> newRemainingNodes = new HashSet<int>(remainingNodes.Where(X => AdjListArray[v].Contains(X)));
                //Console.WriteLine("2 remaining nodes [{0}]", String.Join(",", newRemainingNodes));
                HashSet<int> newSkipNodes = new HashSet<int>(skipNodes.Where(X => AdjListArray[v].Contains(X)));
                //Console.WriteLine("2 skip nodes [{0}]", String.Join(",", newSkipNodes));
                BronKerbosch(AdjListArray, cliques, newRemainingNodes, newPotentialClique, newSkipNodes); 
                remainingNodes.Remove(v); //нужно менять еще и remaining nodes
                skipNodes.Add(v);
                potentialClique.Clear();
            }
            cliques = cliques.OrderByDescending(arr => arr.Count).ToList();
            return cliques;
        }                      
        public static List<List<T>> AllCliques<T>(T[]seq) where T : IInput
        {
            Graph g = new Graph(seq.Length);

            for (int i = 0; i < seq.Length; i++)
            {

                for (int j = i + 1; j < seq.Length; j++)
                {
                    bool checkInc = (seq[i].CheckInconsistency(seq[j]));
                    if (checkInc)
                    {
                        g.addEdgeUndir(i, j);
                    }

                }

            }

            HashSet<int> skipNodes = new HashSet<int>();
            HashSet<int> potentialClique = new HashSet<int>();
            HashSet<int> remainingNodes = new HashSet<int>();
            List<List<int>> cliques = new List<List<int>>();
            for(int i = 0; i<seq.Length; i++)
            {
                remainingNodes.Add(i);
            }
            List<List<int>> BronKerboschCliques = BronKerbosch(g.adjListArray, cliques, remainingNodes, potentialClique, skipNodes);
            return CliquesToStrategies(BronKerboschCliques, seq);
        }

        

    }

}
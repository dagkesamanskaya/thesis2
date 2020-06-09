// C# program to print connected components in  
// an undirected graph  
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace CliqueLib
{
    public class Graph
    {
        int V;
        public HashSet<int>[] adjListArray;
        int maxSize;          //  size of max clique
        public int[] solution;       //  as it says
        int nodes;
        int timeLimit;
        int cpuTime;
        int style;
        public Graph(int V)
        {
            this.V = V;
            this.maxSize = 0;
            this.solution = new int[V];
            this.nodes = 0;
            this.cpuTime = this.timeLimit = -1;
            this.style = 1;
            this.adjListArray = new HashSet<int>[V];
            for(int i = 0; i < adjListArray.Length; i++)
            {
                adjListArray[i] = new HashSet<int>();
            }
        }

        public void addEdgeUndir(int src, int dest)
        {
            if (!adjListArray[src].Contains(dest))
                adjListArray[src].Add(dest);
            if (!adjListArray[dest].Contains(src))
                adjListArray[dest].Add(src);
        }

        public void addEdgeDir(int src, int dest)
        {
            // Add an edge from src to dest. 
            adjListArray.ElementAt(src).Add(dest);
            //degree[src] += 1;
        }


        public void search()
        {
            cpuTime = DateTime.Now.Millisecond;
            nodes = 0;
            List<int> C = new List<int>();
            List<int> P = new List<int>(V);
            for (int i = 0; (i < this.V); i++)
            {
                P.Add(i);
            }

            this.expand(C, P);
        }

        void expand(List<int> C, List<int> P)
        {
            if (timeLimit > 0 && DateTime.Now.Millisecond - cpuTime >= timeLimit)
                return;
            for (int i = P.Count - 1; i >= 0; i--)
            {
                if (C.Count + P.Count <= maxSize) return;

                int v = P.ElementAt(i);

                C.Add(v);

                List<int> newP = new List<int>(i);

                foreach (int w in P)
                {
                    if ((adjListArray[v].Contains(w)))
                    {
                        newP.Add(w);
                    }

                }

                if (newP.Count == 0 && C.Count() > maxSize) saveSolution(C);
                if (newP.Count > 0) expand(C, newP);
                C.Remove(i);
                P.Remove(i);
            }
        }

        public void saveSolution(List<int> C)
        {
            for (int i = 0; i < solution.Length; i++)
            {
                solution[i] = 0;
            }
            foreach (int i in C)
            {
                this.solution[i] = 1;
            }
            this.maxSize = C.Count;
        }


        public List<int> DFS(int v, bool[] visited, List<int> newComponent)
        {
            // Mark the current node as visited and print it 
            visited[v] = true;
            newComponent.Add(v);

            // Recur for all the vertices 
            // adjacent to this vertex 
            foreach (int x in adjListArray[v])
            {
                if (!visited[x]) DFS(x, visited, newComponent);
            }

            return newComponent;
        }

        Graph GetTranspose()
        {
            Graph g = new Graph(V);
            for (int v = 0; v < V; v++)
            {
                foreach (int x in adjListArray[v])
                {
                    g.addEdgeDir(v, x);
                }
            }
            return g;
        }

        void fillOrder(int v, Boolean[] visited, Stack<int> stack)
        {
            visited[v] = true;
            foreach (int x in adjListArray[v])
            {
                if (!visited[x])
                {
                    fillOrder(x, visited, stack);
                }
            }

            stack.Push(v);
        }


        public List<List<int>> connectedComponents()
        {
            // Mark all the vertices as not visited 
            bool[] visited = new bool[V];
            List<List<int>> res = new List<List<int>>();
            for (int v = 0; v < V; ++v)
            {
                if (!visited[v])
                {
                    List<int> newComponent = new List<int>();
                    // print all reachable vertices 
                    // from v 
                    res.Add(DFS(v, visited, newComponent));
                }
            }

            return res;
        }

        public List<List<int>> stronglyConnectedComponents()
        {
            Stack<int> stack = new Stack<int>();
            List<List<int>> res = new List<List<int>>();

            // Mark all the vertices as not visited  (for the first DFS)
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
            {
                visited[i] = false;
            }

            for (int i = 0; i < V; i++)
                if (visited[i] == false)
                {
                    fillOrder(i, visited, stack);
                }

            Graph gr = GetTranspose();

            for (int i = 0; i < V; i++)
                visited[i] = false;

            // Now process all vertices in order defined by Stack 
            while (stack.Count != 0)
            {
                // Pop a vertex from stack 
                int v = (int)stack.Pop();

                // Print Strongly connected component of the popped vertex 
                if (visited[v] == false)
                {
                    List<int> newComponent = new List<int>();
                    res.Add(gr.DFS(v, visited, newComponent));
                }
            }

            return res;
        }
    }
}
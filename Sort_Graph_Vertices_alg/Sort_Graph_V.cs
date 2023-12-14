using System;
using System.Collections.Generic;

namespace Task_Ukleiko
{
    public class Sort_Graph_V
    {
        static void Main()
        {
            Console.Title = ("TASK");
            Console.Write("Task \"Sort Graph Vertices\", Ukleiko Ekaterina, 2 group, 3 course\n\n");

            Console.Write("Graph 1. HAS cycle\n");

            int[][] edges1 = new int[][]
        {
            new int[] {0, 1},
            new int[] {1, 2},
            new int[] {2, 3},
            new int[] {3, 0},
            new int[] {1, 4},
            new int[] {4, 5},
            new int[] {5, 6},
            new int[] {6, 1},
            new int[] {0, 7},
            new int[] {7, 8},
            new int[] {8, 9},
            new int[] {9, 0}
        };

            int numVertices1 = 10;

            TopologicalSort.GenerateGraph(edges1, numVertices1);
            Console.WriteLine();
            TopologicalSort.TopoSort(edges1, numVertices1);

            Console.Write("\nGraph 2. DOESN\'T HAVE cycle\n");

            int[][] edges2 = new int[][]
       {
            new int[] {0, 1},
            new int[] {1, 2},
            new int[] {2, 3},
       };

            int numVertices2 = 4;

            TopologicalSort.GenerateGraph(edges2, numVertices2);
            TopologicalSort.TopoSort(edges2, numVertices2);
        }
    }

    public static class TopologicalSort
    {
        public static void GenerateGraph(int[][] edges, int numVertices)
        {
            bool[,] adjMatrix = new bool[numVertices, numVertices];

            foreach (var edge in edges)
            {
                int from = edge[0];
                int to = edge[1];
                adjMatrix[from, to] = true;
            }

            for (int i = 0; i < numVertices; i++)
            {
                for (int j = 0; j < numVertices; j++)
                {
                    Console.Write(adjMatrix[i, j] ? "1" : "0");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            bool hasCycle = HasCycle(adjMatrix, numVertices);
            if (hasCycle)
            {
                Console.WriteLine("\nThe graph has a cycle.");
            }
            else
            {
                Console.WriteLine("\nThe graph does not have a cycle.\n");
            }
        }

        private static bool HasCycle(bool[,] adjMatrix, int numVertices)
        {
            bool[] visited = new bool[numVertices];
            bool[] inStack = new bool[numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                if (HasCycleDFS(adjMatrix, numVertices, i, visited, inStack))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasCycleDFS(bool[,] adjMatrix, int numVertices, int curr, bool[] visited, bool[] isInStack)
        {
            if (isInStack[curr])
            {
                return true;
            }

            if (visited[curr])
            {
                return false;
            }

            visited[curr] = true;
            isInStack[curr] = true;

            for (int i = 0; i < numVertices; i++)
            {
                if (adjMatrix[curr, i] && HasCycleDFS(adjMatrix, numVertices, i, visited, isInStack))
                {
                    return true;
                }
            }

            isInStack[curr] = false;

            return false;
        }

        public static void TopoSort(int[][] edges, int numVertices)
        {
            bool[,] adjacencyMatrix = new bool[numVertices, numVertices];

            foreach (var edge in edges)
            {
                int from = edge[0];
                int to = edge[1];
                adjacencyMatrix[from, to] = true;
            }

            Stack<int> stack = new Stack<int>();
            bool[] visited = new bool[numVertices];

            for (int i = 0; i < numVertices; i++)
            {
                if (!visited[i])
                {
                    TopoSortDFS(adjacencyMatrix, numVertices, i, visited, stack);
                }
            }

            Console.WriteLine("Topological sorted order: ");
            while (stack.Count > 1)
            {
                Console.Write(stack.Pop() + " -> ");
            }
            if (stack.Count == 1)
            {
                Console.Write(stack.Pop() + "\n");
            }
        }

        private static void TopoSortDFS(bool[,] adjMatrix, int numVertices, int curr, bool[] visited, Stack<int> stack)
        {
            visited[curr] = true;

            for (int i = 0; i < numVertices; i++)
            {
                if (adjMatrix[curr, i] && !visited[i])
                {
                    TopoSortDFS(adjMatrix, numVertices, i, visited, stack);
                }
            }

            stack.Push(curr);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Client;
using clientlib;

namespace ClientEX
{
    class Simulator
    {
        Random rnd = new Random();
        public Graph simulateGraph(int graphSize, double graphDensity, int maxWeight = Constants.MAXWEIGHT)
        {
            if (1 < graphDensity) graphDensity = 1;
            int[,] graph = new int[graphSize, graphSize];

            for (int i = 0; i < graphSize; i++)
            {
                graph[i, i] = 0;
                for (int j = i + 1; j < graphSize; j++)
                {
                    if (rnd.Next(100) < 100 * graphDensity)
                    {
                        int weight = rnd.Next(1, maxWeight);
                        graph[i, j] = weight;
                        graph[j, i] = weight;

                    }
                    else
                    {
                        graph[i, j] = int.MaxValue;
                        graph[j, i] = int.MaxValue;
                    }
                }
            }
            int start = rnd.Next(graphSize - 1);
            int end;
            while (start == (end = rnd.Next(graphSize - 1))) ;

            Graph simGraph = new Graph(graphSize, graph, rnd.Next(graphSize - 1), rnd.Next(graphSize - 1));

            return simGraph;
        }

        public Threats simulateThreats(Graph graph)
        {
            Threats threats = new Threats(graph);
            int temp = rnd.Next(graph.nrOfNodes - 1);
            while (temp == graph.start || temp == graph.end)
                temp = rnd.Next(graph.nrOfNodes - 1);
            threats.positionsOfThreats[rnd.Next(graph.nrOfNodes - 1)] = true;

            return threats;
        }
        public Threats addThreats(Graph graph, Threats threats, int nrOfThreats)
        {
            int tempRand = 0;
            for (int i = 0; i < nrOfThreats; i++)
            {
                while (threats.positionsOfThreats[tempRand = rnd.Next(graph.nrOfNodes)]) ;
                threats.positionsOfThreats[tempRand] = true;
            }


            return threats;
        }

        public Threats changeThreats(Graph graph, Threats threats)
        {

            for (int i = 0; i < graph.nrOfNodes; i++)
            {
                if (threats.positionsOfThreats[i])
                {
                    for (int j = 0; j < graph.nrOfNodes; j++)
                    {
                        if (rnd.Next(Constants.MAXWEIGHT * 3) > (graph.graph[i, j] + 2 * Constants.MAXWEIGHT) && graph.start != j && graph.end != j)
                        {
                            threats.positionsOfThreats[j] = true;
                            return threats;
                        }
                    }
                }
            }
            return threats;
        }
    }
    class Test
    {
        private Simulator sim;
        private ClientClass clnt;
        private Stopwatch stopwatch;

        public Test()
        {
            sim = new Simulator();
            clnt = new ClientClass();
            stopwatch = new Stopwatch();
        }

        // Bez zagrożeń

        // Czas pracy
        private int TimeValue(int graphSize)
        {
            Graph graph = sim.simulateGraph(graphSize, 1);

            stopwatch.Restart();
            clnt.sendGraph(graph);
            var path = clnt.receivePath();
            stopwatch.Stop();
            return (int)stopwatch.ElapsedMilliseconds;
        }

        // Średni czas trwania pracy
        private int middleTimeValue(int graphSize, int repeat)
        {
            int allResults = 0;
            // Pierwsze uruchomienie nie jest brane pod uwagę , ponieważ trwa dłużej od pozostałych
            TimeValue(graphSize);
            for (int i = 0; i < repeat; i++)
            {
                Console.WriteLine("\nITERATION NUMBER : " + i);
                Console.WriteLine(allResults += TimeValue(graphSize));
            }

            return (allResults / repeat);
        }

        public int[] timeOfWork(int interVal, int nrOfSamples, int repeat)
        {
            int[] results = new int[nrOfSamples];

            for (int i = 0; i < nrOfSamples; i++)
            {
                Console.WriteLine("\n\nGRAPH SIZE : " + (i + 1) * interVal);
                results[i] = middleTimeValue((i + 1) * interVal, repeat);
            }

            return results;
        }

        // Z zagrożeniami
        private int TimeValue(Graph graph, Threats threats)
        {
            stopwatch.Restart();
            clnt.sendGraph(graph, threats);
            var path = clnt.receivePath();
            stopwatch.Stop();
            return (int)stopwatch.ElapsedMilliseconds;
        }

        public int[] timeOfWorkWithThreats(int graphSize, int interval, int nrOfSamples)
        {
            Graph graph = sim.simulateGraph(graphSize, 1);
            Threats threats = new Threats(graph);
            Threats threatsClear = new Threats(graph);
            int[] resultsThreats = new int[nrOfSamples + 1];
            int[] results = new int[nrOfSamples + 1];
            // Pierwsze użycie trwa dłużej
            TimeValue(graph, threatsClear);

            for (int i = 0; i < nrOfSamples + 1; i++)
            {
                results[i] = TimeValue(graph, threatsClear);
                resultsThreats[i] = TimeValue(graph, threats);
                threats = sim.addThreats(graph, threats, interval);
            }

            Console.WriteLine("Wyniki bez zagrożenia : ");
            foreach (int res in results)
                Console.Write(" " + res);
            Console.WriteLine("\nWyniki z zagrożeniami : ");
            foreach (int res in resultsThreats)
                Console.Write(" " + res);

            return resultsThreats;
        }


    }
}

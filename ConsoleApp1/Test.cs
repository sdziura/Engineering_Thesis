using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Client
{
    class Test
    {
        private Simulator sim ;
        private ClientClass clnt ;
        private Stopwatch stopwatch ;

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
            
            return (allResults/repeat);
        }
        
        public int[] timeOfWork(int interVal, int nrOfSamples, int repeat)
        {
            int[] results = new int[nrOfSamples];

            for(int i = 0; i < nrOfSamples; i++)
            {
                Console.WriteLine("\n\nGRAPH SIZE : " + (i + 1) * interVal);
                results[i] = middleTimeValue((i+1) * interVal, repeat);
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
            int[] resultsThreats = new int[nrOfSamples+1];
            int[] results = new int[nrOfSamples+1];
            // Pierwsze użycie trwa dłużej
            TimeValue(graph, threatsClear);

            for (int i = 0; i<nrOfSamples+1; i++)
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ExceptionBenchmark
{
    class Program
    {
        public const int Iterations = 1000;

        static void Main(string[] args)
        {
            var throwing = ThrowingBenchmark();
            var operationResult = OperationResultBenchmark();
            Console.WriteLine($"throwing: {throwing.ElapsedTicks} ticks | Ticks per Operation: {(float)throwing.ElapsedTicks / Iterations}");
            Console.WriteLine($"operationResult: {operationResult.ElapsedTicks} ticks | Ticks per Operation: {(float)operationResult.ElapsedTicks / Iterations}");
            Console.WriteLine($"throwing/operationResult: {(float)throwing.ElapsedTicks / operationResult.ElapsedTicks}");
            Console.ReadLine();
        }

        private static Stopwatch ThrowingBenchmark()
        {
            var timer = new Stopwatch();
            var service = new ThrowingService();
            timer.Start();
            for (int i = 0; i < Iterations; i++)
            {
                try
                {
                    service.Operation();
                }
                catch (Exception)
                {
                }
            }
            timer.Stop();
            Console.Clear();
            return timer;
        }

        private static Stopwatch OperationResultBenchmark()
        {
            var timer = new Stopwatch();
            var service = new OperationResultService();
            timer.Start();
            for (int i = 0; i < Iterations; i++)
            {
                var result = service.Operation();
            }
            timer.Stop();
            Console.Clear();
            return timer;
        }
    }

    public class ThrowingService
    {
        public void Operation()
        {
            throw new Exception();
        }
    }

    public class OperationResultService
    {
        public OperationResult Operation()
        {
            return new OperationResult();
        }
    }

    public class OperationResult
    {
        public bool Success { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;

namespace Ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 2.10 使用SpinWait类
            var t1 = new Thread(UserModeWait);
            var t2 = new Thread(HybridSpinWait);

            Console.WriteLine("Running user mode waiting");
            t1.Start();
            Thread.Sleep(20);
            isCompleted = true;

            Thread.Sleep(TimeSpan.FromSeconds(1));
            isCompleted = false;
            Console.WriteLine("Running hybrid Spinwait construct waiting");
            t2.Start();

            Thread.Sleep(5);
            isCompleted = true;
            #endregion

            #region 2.9 使用ReaderWriterLockSlim类
            //new Thread(Read) { IsBackground = true }.Start();
            //new Thread(Read) { IsBackground = true }.Start();
            //new Thread(Read) { IsBackground = true }.Start();

            //new Thread(() => Write("Thread 1")) { IsBackground = true }.Start();
            //new Thread(() => Write("Thread 2")) { IsBackground = true }.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(30));
            #endregion

            #region 2.8 使用Barrier类
            //var t1 = new Thread(() => PlayMusic("The guitarist", "play an amazing solo", 5));
            //var t2 = new Thread(() => PlayMusic("the singer", "sing the song", 2));

            //t1.Start();
            //t2.Start();
            #endregion

            #region 2.7 使用CountDownEvent类
            //Console.WriteLine("Start two operations");
            //var t1 = new Thread(() => PerformOperation("operation 1 is completed", 4));
            //var t2 = new Thread(() => PerformOperation("operation 2 is completed", 8));

            //t1.Start();
            //t2.Start();
            //countdown.Wait();
            //Console.WriteLine("Both operations have been completed");
            //countdown.Dispose();


            #endregion

            #region 2.6 使用ManualResetEventSlim类
            //var t1 = new Thread(() => TravelThroughGates("Thread 1", 5));
            //var t2 = new Thread(() => TravelThroughGates("Thread 2", 6));
            //var t3 = new Thread(() => TravelThroughGates("Thread 3", 12));

            //t1.Start();
            //t2.Start();
            //t3.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //Console.WriteLine("The gates are now open!");
            //mainEvent.Set();

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //mainEvent.Reset();

            //Console.WriteLine("The gates have been closed!");

            //Thread.Sleep(TimeSpan.FromSeconds(10));

            //Console.WriteLine("The gates are now open for the second time");
            //mainEvent.Set();

            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //Console.WriteLine("The gates have been closed!");
            //mainEvent.Reset();
            #endregion

            #region 2.5 使用AutoResetEvent类
            //var t = new Thread(() => Process(10));
            //t.Start();

            //Console.WriteLine("Waiting for another thread to complete work");
            //workerEvent.WaitOne();
            //Console.WriteLine("First operation is completed");
            //Console.WriteLine("Performing an operation on a main thread");

            //Thread.Sleep(TimeSpan.FromSeconds(5));
            //mainEvent.Set();
            //Console.WriteLine("Now running the second operation on a second thread");
            //workerEvent.WaitOne();

            //Console.WriteLine("Second operation is completed!");
            #endregion

            #region 2.4 使用SemaphoreSlim类
            //for (int i = 0; i <= 6; i++)
            //{
            //    string threadName = "Thread " + i;
            //    int secondsToWait = 2 + 2 * i;
            //    var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
            //    t.Start();

            //}
            #endregion

            #region 2.3 使用Mutex类
            //const string MutexName = "CSharpThreadingingCookBook";

            //using (var m = new Mutex(false, MutexName))
            //{
            //    if (!m.WaitOne(TimeSpan.FromSeconds(5), false))
            //    {
            //        Console.WriteLine("Second instance is running!");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Running!");
            //        Console.ReadLine();
            //        m.ReleaseMutex();
            //    }
            //}
            #endregion

            #region 2.2 执行基本的原子操作
            //Console.WriteLine("Incorrect counter");

            //var c = new Counter();

            //var t1 = new Thread(() => TestCounter(c));
            //var t2 = new Thread(() => TestCounter(c));
            //var t3 = new Thread(() => TestCounter(c));
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t1.Join();
            //t2.Join();
            //t3.Join();
            //Console.WriteLine("Total count:{0}", c.Count);
            //Console.WriteLine("--------------------------");
            //Console.WriteLine("Correct counter");

            //var c1 = new CounterNoLock();

            //t1 = new Thread(() => TestCounter(c1));
            //t2 = new Thread(() => TestCounter(c1));
            //t3 = new Thread(() => TestCounter(c1));
            //t1.Start();
            //t2.Start();
            //t3.Start();
            //t1.Join();
            //t2.Join();
            //t3.Join();

            //Console.WriteLine("Total count:{0}", c1.Count);
            #endregion


        }

        #region 2.10
        static volatile bool isCompleted = false;
        static void UserModeWait()        
        {
            while (isCompleted)
            {
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine("Waiting is complete");
        }

        static void HybridSpinWait()
        {
            var w = new SpinWait();
            while (isCompleted)
            {
                w.SpinOnce();
                Console.WriteLine(w.NextSpinWillYield);

            }

            Console.WriteLine("Waiting is complete");
        }
        #endregion

        #region 2.9
        //static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        //static Dictionary<int, int> items = new Dictionary<int, int>();

        //static void Read()
        //{
        //    Console.WriteLine("Reading contents of a dictionary");
        //    while (true)
        //    {
        //        try
        //        {
        //            rw.EnterReadLock();
        //            foreach (var key in items.Keys)
        //            {
        //                Thread.Sleep(TimeSpan.FromSeconds(0.1));
        //            }
        //        }
        //        finally
        //        {
        //            rw.ExitReadLock();
        //        }
        //    }
        //}

        //static void Write(string threadName)
        //{
        //    while (true)
        //    {
        //        try
        //        {
        //            int newKey = new Random().Next(250);
        //            rw.EnterUpgradeableReadLock();
        //            if (!items.ContainsKey(newKey))
        //            {
        //                try
        //                {
        //                    rw.EnterWriteLock();
        //                    items[newKey] = 1;
        //                    Console.WriteLine("New key {0} is added to a dictionary by a {1}", newKey, threadName);
        //                }
        //                finally
        //                {
        //                    rw.ExitWriteLock();
        //                }
        //            }

        //            Thread.Sleep(TimeSpan.FromSeconds(0.1));
        //        }
        //        finally
        //        {
        //            rw.ExitUpgradeableReadLock();
        //        }
        //    }
        //}
        #endregion

        #region 2.8
        //static Barrier barrier = new Barrier(2, b => Console.WriteLine("End of phase {0}", b.CurrentPhaseNumber + 1));

        //static void PlayMusic(string name, string message, int seconds)
        //{
        //    for (int i = 1; i < 3; i++)
        //    {
        //        Console.WriteLine("----------------------");
        //        Thread.Sleep(TimeSpan.FromSeconds(seconds));

        //        Console.WriteLine("{0} starts to {1}", name, message);

        //        Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //        Console.WriteLine("{0} finishes to {1}", name, message);
        //        barrier.SignalAndWait();
        //    }
        //}
        #endregion

        #region 2.7
        //static CountdownEvent countdown = new CountdownEvent(2);
        //static void PerformOperation(string message, int seconds)
        //{
        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    Console.WriteLine(message);
        //    countdown.Signal();
        //}
        #endregion

        #region 2.6
        //static ManualResetEventSlim mainEvent = new ManualResetEventSlim(false);

        //static void TravelThroughGates(string threadName, int seconds)
        //{
        //    Console.WriteLine("{0} falls to sleep",threadName);
        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    Console.WriteLine("{0} waits for the gates to open!",threadName);
        //    mainEvent.Wait();
        //    Console.WriteLine("{0} enters the gates!",threadName);
        //}
        #endregion

        #region 2.5
        //private static AutoResetEvent workerEvent = new AutoResetEvent(false);
        //private static AutoResetEvent mainEvent = new AutoResetEvent(false);

        //static void Process(int seconds)        
        //{
        //    Console.WriteLine("Starting a long running work...");
        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    Console.WriteLine("Work is done!");
        //    workerEvent.Set();
        //    Console.WriteLine("waiting for a main thread to complete its work");
        //    mainEvent.WaitOne();
        //    Console.WriteLine("Starting second operation...");
        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    Console.WriteLine("Work is done!");
        //    workerEvent.Set();
        //}
        #endregion

        #region 2.4
        //static SemaphoreSlim semaphore = new SemaphoreSlim(4);
        //static void AccessDatabase(string name, int seconds)
        //{
        //    Console.WriteLine("{0} waits to access a database",name); ;
        //    semaphore.Wait();
        //    Console.WriteLine("{0} was granted an access to a database",name);

        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    Console.WriteLine("{0} is completed",name);
        //    semaphore.Release();

        //}
        #endregion

        #region 2.3

        #endregion

        #region 2.2
        //static void TestCounter(CounterBase c)
        //{
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        c.Increment();
        //        c.Decrement();
        //    }
        //}

        //abstract class CounterBase
        //{
        //    public abstract void Increment();
        //    public abstract void Decrement();
        //}

        //class Counter : CounterBase
        //{
        //    private int _count;
        //    public int Count { get { return _count; } }

        //    public override void Increment()
        //    {
        //        _count++;
        //    }

        //    public override void Decrement()
        //    {
        //        _count--;
        //    }
        //}
        //class CounterNoLock : CounterBase
        //{
        //    private int _count;
        //    public int Count { get { return _count; } }

        //    public override void Increment()
        //    {
        //        Interlocked.Increment(ref _count);
        //    }

        //    public override void Decrement()
        //    {
        //        Interlocked.Decrement(ref _count);
        //    }
        //}
        #endregion
    }
}

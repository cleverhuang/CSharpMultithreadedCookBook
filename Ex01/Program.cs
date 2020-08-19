using System;
using System.Threading;

namespace Ex01
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 1.2 线程开始

            //Thread t = new Thread(PrintNumbers);
            //t.Start();
            //PrintNumbers();L
            #endregion

            #region 1.3 线程暂停

            //Thread t = new Thread(PrintNumbersWithDelay);
            //t.Start();
            //PrintNumbers();
            #endregion

            #region 1.4 线程等待


            //Console.WriteLine("Starting..."); ;
            //Thread t = new Thread(PrintNumbersWithDelay);
            //t.Start();
            //t.Join();
            //PrintNumbers();
            //Console.WriteLine("Thread completed");
            #endregion

            #region 1.5 终止线程
            //Console.WriteLine("Starting program...");
            //Thread t = new Thread(PrintNumbersWithDelay);
            //t.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //t.Abort();
            //Console.WriteLine("A thread has been aborted");
            //Thread t1 = new Thread(PrintNumbers);
            //t1.Start();
            //PrintNumbers();


            #endregion

            #region 1.6 检测线程状态

            //Console.WriteLine("Starting program...");
            //Thread t = new Thread(PrintNumbersWithStatus);
            //Thread t2 = new Thread(DoNothing);
            //Console.WriteLine(t.ThreadState.ToString());
            //t2.Start();
            //t.Start();

            //for (int i = 0; i < 30; i++)
            //{
            //    Console.WriteLine(t.ThreadState.ToString());
            //}

            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //t.Abort();

            //Console.WriteLine("A thrad has beem aborted");
            //Console.WriteLine(t.ThreadState.ToString());
            //Console.WriteLine(t2.ThreadState.ToString());

            #endregion

            #region 1.7 线程优先级
            //Console.WriteLine("Current thread priority:{0}",Thread.CurrentThread.Priority);

            //Console.WriteLine("Running on all cores available");
            //RunThreads();

            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //Console.WriteLine("Running on a single core");

            //Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);

            //RunThreads();
            #endregion

            #region 1.8 前台线程与后台线程
            //var sampleForeground = new ThreadSample(10);
            //var sampleBackground = new ThreadSample(20);

            //var threadOne = new Thread(sampleForeground.CountNumbers);
            //threadOne.Name = "ForegroundThread";

            //var threadTwo = new Thread(sampleBackground.CountNumbers);
            //threadTwo.Name = "BackgroundThread";
            //threadTwo.IsBackground = true;

            //threadOne.Start();
            //threadTwo.Start();

            #endregion

            #region 1.9 向线程传递参数
            //var sample = new ThreadSample(10);

            //var threadOne = new Thread(sample.CountNumbers);
            //threadOne.Name = "ThreadOne";
            //threadOne.Start();
            //threadOne.Join();
            //Console.WriteLine("-----------------------");

            //var threadTwo = new Thread(Count);
            //threadTwo.Name = "ThreadTwo";
            //threadTwo.Start(8);
            //threadTwo.Join();
            //Console.WriteLine("-----------------------");

            //var threadThree = new Thread(() => CountNumbers(12));
            //threadThree.Name = "ThreadThree";
            //threadThree.Start();
            //threadThree.Join();
            //Console.WriteLine("-----------------------");

            //int i = 10;
            //var threadFour = new Thread(() => PrintNumber(i));

            //i = 20;
            //var threadFive = new Thread(() => PrintNumber(i));
            //threadFour.Start();
            //threadFive.Start();
            #endregion

            #region 1.10 使用C#中的lock关键字
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

            //Console.WriteLine("Total count:{0}",c.Count);
            //Console.WriteLine("----------------------");
            //Console.WriteLine("Correct counter");


            //var c1 = new CounterWithLock();

            //t1 = new Thread(() => TestCounter(c1));
            //t2 = new Thread(() => TestCounter(c1));
            //t3 = new Thread(() => TestCounter(c1));

            //t1.Start();
            //t2.Start();
            //t3.Start();

            //t1.Join();
            //t2.Join();
            //t3.Join();

            //Console.WriteLine("Total count:{0}", c.Count);
            #endregion

            #region 1.11 使用Monitor类锁定资源
            //object lock1 = new object();
            //object lock2 = new object();

            //new Thread(() => LockTooMuch(lock1, lock2)).Start();

            //lock (lock2)
            //{
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Monitor.TryEnter allows not to get stuck,returning false after a specified timeout is elapsed");
            //    if (Monitor.TryEnter(lock1,TimeSpan.FromSeconds(5)))
            //    {
            //        Console.WriteLine("Acquired a protected resource successfully");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Time out acquiring a resource"); 
            //    }
            //}
            //new Thread(() => LockTooMuch(lock1, lock2)).Start();

            //Console.WriteLine("----------------------");
            //lock (lock2)
            //{
            //    Console.WriteLine("This will be a deadlock");

            //    Thread.Sleep(1000);

            //    lock (lock1)
            //    {
            //        Console.WriteLine("Acquired a protected resource successfully");
            //    }
            //}
            #endregion

            #region 1.12 使用异常
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("We won't get here!");
                throw;
            }
            #endregion
        }

        #region 1.12
        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a faultt thread...");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            throw new Exception("Boom!");
        }

        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a faulty thread...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception handled:{0}",ex.Message);                
            }
        }
        #endregion

        #region 1.11
        //static void LockTooMuch(object lock1, object lock2)
        //{
        //    lock (lock1)
        //    {
        //        Thread.Sleep(1000);
        //        lock (lock2)
        //        {

        //        }
        //    }
        //}
        #endregion

        #region 1.10
        //static void TestCounter(CounterBase c)
        //{
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        c.Increment();
        //        c.Decrement();
        //    }
        //}

        //class Counter : CounterBase
        //{
        //    public int Count { get; private set; }
        //    public override void Increment()
        //    {
        //        Count++;
        //    }
        //    public override void Decrement()
        //    {
        //        Count--;
        //    }
        //}

        //class CounterWithLock : CounterBase
        //{
        //    private readonly object _syncRoot = new object();
        //    public int Count { get; private set; }

        //    public override void Increment()
        //    {
        //        lock (_syncRoot)
        //        {
        //            Count++;
        //        }
        //    }

        //    public override void Decrement()
        //    {
        //        lock (_syncRoot)
        //        {
        //            Count--;
        //        }
        //    }
        //}
        //abstract class CounterBase
        //{
        //    public abstract void Increment();
        //    public abstract void Decrement();

        //}
        #endregion

        #region 1.9
        //static void Count(object iterations)
        //{
        //    CountNumbers((int)iterations);
        //}

        //static void CountNumbers(int interations)
        //{
        //    for (int i = 0; i <= interations; i++)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(0.5));
        //        Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
        //    }
        //}

        //static void PrintNumber(int number)
        //{
        //    Console.WriteLine(number);
        //}

        //class ThreadSample
        //{
        //    private readonly int _iterations;
        //    public ThreadSample(int iterations)
        //    {
        //        _iterations = iterations;
        //    }

        //    public void CountNumbers()
        //    {
        //        for (int i = 0; i <= _iterations; i++)
        //        {
        //            Thread.Sleep(TimeSpan.FromSeconds(0.5));

        //            Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
        //        }
        //    }
        //}
        #endregion

        #region 1.8
        //class ThreadSample
        //{
        //    private readonly int _interations;
        //    public ThreadSample(int interations)
        //    {
        //        _interations = interations;
        //    }

        //    public void CountNumbers()
        //    {
        //        for (int i = 0; i < _interations; i++)
        //        {
        //            Thread.Sleep(TimeSpan.FromSeconds(0.5));
        //            Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
        //        }
        //    }
        //}
        #endregion

        #region 1.7及之前的数据        
        //static void RunThreads()
        //{
        //    var sample = new ThreadSample();
        //    var threadOne = new Thread(sample.CountNumbers);
        //    threadOne.Name = "ThreadOne";
        //    var threadTwo = new Thread(sample.CountNumbers);
        //    threadTwo.Name = "ThreadTwo";

        //    threadOne.Priority = ThreadPriority.Highest;
        //    threadTwo.Priority = ThreadPriority.Lowest;

        //    threadOne.Start();
        //    threadTwo.Start();

        //    Thread.Sleep(TimeSpan.FromSeconds(2));

        //    sample.Stop();
        //}


        //        class ThreadSample
        //        {
        //            private bool _isStopped = false;
        //            public void Stop()
        //            {
        //                _isStopped = true;
        //            }

        //            public void CountNumbers()
        //            {
        //                long counter = 0;
        //                while (!_isStopped)
        //                {
        //                    counter++;
        //                }

        //                Console.WriteLine("{0} with {1,11} priority has a count = {2,13}", 
        //                    Thread.CurrentThread.Name, 
        //Thread.CurrentThread.Priority, 
        //counter.ToString("NO"));
        //            }
        //        }

        //static void DoNothing()
        //{
        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //}

        //static void PrintNumbersWithStatus()
        //{
        //    Console.WriteLine("Starting...");
        //    Console.WriteLine(Thread.CurrentThread.ThreadState.ToString());
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(2));
        //        Console.WriteLine(i);
        //    }
        //}

        //static void PrintNumbers()
        //{
        //    Console.WriteLine("Starting...");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}

        //static void PrintNumbersWithDelay()
        //{
        //    Console.WriteLine("Starting...");
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(2));
        //        Console.WriteLine(i);
        //    }
        //}
        #endregion
    }
}

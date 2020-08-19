using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 3.2 在线程池中调用委托
            //int threadId = 0;
            //RunOnThreadPool poolDelegate = Test;

            //var t = new Thread(() => Test(out threadId));
            //t.Start();
            //t.Join();


            //Console.WriteLine("Thread id:{0}",threadId);

            //IAsyncResult r = poolDelegate.BeginInvoke(out threadId, CallBack, "a delegate asynchronous call");
            //r.AsyncWaitHandle.WaitOne();

            //string result = poolDelegate.EndInvoke(out threadId, r);

            //Console.WriteLine("Thread pool worker thread id:{0}",threadId);

            //Console.WriteLine(result);

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            #endregion

            #region 3.3 向线程池中放入异步操作
            //const int x = 1;
            //const int y = 2;
            //const string lambdaState = "lambda state 2";

            //ThreadPool.QueueUserWorkItem(AsyncOperation, "async state");
            //Thread.Sleep(TimeSpan.FromSeconds(1));

            //ThreadPool.QueueUserWorkItem(state =>
            //{
            //    Console.WriteLine("Operation state:{0}", state);
            //    Console.WriteLine("Worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //}, "lambda state");

            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    Console.WriteLine("Operation state:{0},{1}", x + y, lambdaState);
            //    Console.WriteLine("Worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //}, "lambda state");

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            #endregion

            #region 3.4 线程池与并行度
            //const int numberOfOperations = 500;
            //var sw = new Stopwatch();

            //sw.Start();
            //UseThreads(numberOfOperations);

            //sw.Stop();
            //Console.WriteLine("Execution time using threads:{0}", sw.ElapsedMilliseconds);


            //sw.Reset();
            //sw.Start();

            //UseThreadPool(numberOfOperations);
            //sw.Stop();

            //Console.WriteLine("Execution time using threads:{0}", sw.ElapsedMilliseconds);
            #endregion

            #region 3.5 实现一个取消选项
            //using (var cts = new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;
            //    ThreadPool.QueueUserWorkItem(_ => AsyncOperation1(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //    cts.Cancel();
            //}
            //using (var cts = new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;
            //    ThreadPool.QueueUserWorkItem(_ => AsyncOperation2(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //    cts.Cancel();
            //}

            //using (var cts = new CancellationTokenSource())
            //{
            //    CancellationToken token = cts.Token;
            //    ThreadPool.QueueUserWorkItem(_ => AsyncOperation3(token));
            //    Thread.Sleep(TimeSpan.FromSeconds(2));
            //    cts.Cancel();
            //}

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            #endregion

            #region 3.6 在线程池中使用等待事件处理器及超时
            //RunOperations(TimeSpan.FromSeconds(5));

            //RunOperations(TimeSpan.FromSeconds(10));
            #endregion

            #region 3.7 使用计时器

            //Console.WriteLine("Please 'Enter' to stop the timer...");
            //DateTime start = DateTime.Now;

            //timer = new Timer(_ => TimerOperation(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            //Thread.Sleep(TimeSpan.FromSeconds(6));

            //timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5));

            //Console.ReadLine();

            //timer.Dispose();

            #endregion

            #region 3.8 使用BackgroundWorker组件
            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;

            bw.DoWork += Worker_DoWork;
            bw.ProgressChanged += Worker_ProgressChanged;

            bw.RunWorkerCompleted += Worker_Completed;

            bw.RunWorkerAsync();

            Console.WriteLine("Press c to cancel work");
            do
            {
                if (Console.ReadKey(true).KeyChar == 'C')
                {
                    bw.CancelAsync();
                }
            } while (bw.IsBusy);
            #endregion
        }

        #region 3.8
        static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("DoWork thread pool thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            var bw = (BackgroundWorker)sender;
            for (int i = 0; i <= 100; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                if (i % 10 == 0)
                {
                    bw.ReportProgress(i);
                }

                Thread.Sleep(TimeSpan.FromSeconds(0.1));
            }
            e.Result = 42;
        }

        static void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("{0}% completed.Progress thread pool thread id:{1}", e.ProgressPercentage, Thread.CurrentThread.ManagedThreadId);
        }

        static void Worker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Completed thread pool thread id:{0}", Thread.CurrentThread.ManagedThreadId);

            if (e.Error != null)
            {
                Console.WriteLine("Exception {0} has occured.", e.Error.Message);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Operation has been canceled.");
            }
            else
            {
                Console.WriteLine("The answer is :{0}", e.Result);
            }
        }
        #endregion

        #region 3.7
        //static Timer timer;
        //static void TimerOperation(DateTime start)
        //{
        //    TimeSpan elapsed = DateTime.Now - start;
        //    Console.WriteLine("{0} seconds from {1}.Timer thread pool thread id:{2}", elapsed.Seconds, start, Thread.CurrentThread.ManagedThreadId);
        //}
        #endregion

        #region 3.6
        //static void RunOperations(TimeSpan workerOperationTimeout)
        //{
        //    using (var evt = new ManualResetEvent(false))
        //    using (var cts = new CancellationTokenSource())
        //    {
        //        Console.WriteLine("Registering timeout operations...");
        //        var worker = ThreadPool.RegisterWaitForSingleObject(evt, (state, isTimeOut) => WorkerOperationWait(cts, isTimeOut), null, workerOperationTimeout, true);

        //        Console.WriteLine("Starting long running operation...");

        //        ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));

        //        Thread.Sleep(workerOperationTimeout.Add(TimeSpan.FromSeconds(2)));

        //        worker.Unregister(evt);
        //    }
        //}

        //static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        //{
        //    for (int i = 0; i < 6; i++)
        //    {
        //        if ((token.IsCancellationRequested))
        //        {
        //            return;
        //        }
        //        Thread.Sleep(TimeSpan.FromSeconds(1));
        //    }
        //    evt.Set();
        //}

        //static void WorkerOperationWait(CancellationTokenSource cts, bool isTimeOut)
        //{
        //    if (isTimeOut)
        //    {
        //        cts.Cancel();
        //        Console.WriteLine("Worker operation timed out and was canceled.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Worker operation successed.");
        //    }
        //}
        #endregion

        #region 3.5
        //static void AsyncOperation1(CancellationToken token)
        //{
        //    Console.WriteLine("Starting the first task");
        //    for (int i = 0; i < 5; i++)
        //    {
        //        if (token.IsCancellationRequested)
        //        {
        //            Console.WriteLine("The first task has been canceled.");
        //            return;
        //        }
        //        Thread.Sleep(TimeSpan.FromSeconds(1));
        //    }
        //    Console.WriteLine("The first task has completed successfully");
        //}

        //static void AsyncOperation2(CancellationToken token)
        //{
        //    try
        //    {
        //        Console.WriteLine("Starting the second task");
        //        for (int i = 0; i < 5; i++)
        //        {
        //            token.ThrowIfCancellationRequested();
        //            Thread.Sleep(TimeSpan.FromSeconds(1));
        //        }
        //        Console.WriteLine("The second task has completed successfully");
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        Console.WriteLine("The second task has been canceled.");
        //    }
        //}

        //private static void AsyncOperation3(CancellationToken token)
        //{
        //    bool cancellationFlag = false;
        //    token.Register(() => cancellationFlag = true);

        //    Console.WriteLine("Starting the third task");
        //    for (int i = 0; i < 5; i++)
        //    {
        //        if (cancellationFlag)
        //        {
        //            Console.WriteLine("The third task has been canceled.");
        //            return;
        //        }
        //        Thread.Sleep(TimeSpan.FromSeconds(1));
        //    }

        //    Console.WriteLine("The third task has completed succesfully");
        //}
        #endregion

        #region 3.4
        //static void UseThreads(int numberOfOPerations)
        //{
        //    using (var countdown = new CountdownEvent(numberOfOPerations))
        //    {
        //        Console.WriteLine("Scheduling work by creating threads");

        //        for (int i = 0; i < numberOfOPerations; i++)
        //        {
        //            var thread = new Thread(() =>
        //            {
        //                Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);
        //                Thread.Sleep(TimeSpan.FromSeconds(0.1));

        //                countdown.Signal();
        //            });

        //            thread.Start();
        //        }

        //        countdown.Wait();
        //        Console.WriteLine();
        //    }
        //}

        //static void UseThreadPool(int numberOfOperations)
        //{
        //    using (var countdown = new CountdownEvent(numberOfOperations))
        //    {
        //        Console.WriteLine("Starting work on a threadpool");
        //        for (int i = 0; i < numberOfOperations; i++)
        //        {
        //            ThreadPool.QueueUserWorkItem(_ =>
        //            {
        //                Console.Write("{0},", Thread.CurrentThread.ManagedThreadId);
        //                Thread.Sleep(TimeSpan.FromSeconds(0.1));
        //                countdown.Signal();
        //            });
        //        }

        //        countdown.Wait();
        //        Console.WriteLine();
        //    }
        //}
        #endregion

        #region 3.3
        //private static void AsyncOperation(object state)
        //{
        //    Console.WriteLine("Operation state:{0}",state??"(null)");
        //    Console.WriteLine("Worker thread id:{0}",Thread.CurrentThread.ManagedThreadId);

        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //}
        #endregion

        #region 3.2

        //private delegate string RunOnThreadPool(out int threadId);
        //private static void CallBack(IAsyncResult ar)
        //{
        //    Console.WriteLine("Starting a callback...");
        //    Console.WriteLine("State passed to callback:{0}",ar.AsyncState);
        //    Console.WriteLine("Is thread pool thread:{0}",Thread.CurrentThread.IsThreadPoolThread);
        //    Console.WriteLine("Thread pool worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
        //}

        //private static string Test(out int threadId)
        //{
        //    Console.WriteLine("Starting...");
        //    Console.WriteLine("Is thread pool thread：{0}",Thread.CurrentThread.IsThreadPoolThread);
        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //    threadId = Thread.CurrentThread.ManagedThreadId;
        //    return string.Format("Thread pool worker thread id was :{0}", threadId);
        //}
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ex04
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 4.2 创建任务
            //var t1 = new Task(() => TaskMethod("Task 1"));
            //var t2 = new Task(() => TaskMethod("Task 2"));

            //t2.Start();
            //t1.Start();

            //Task.Run(() => TaskMethod("Task 3"));

            //Task.Factory.StartNew(() => TaskMethod("Task 4"));
            //Task.Factory.StartNew(() => TaskMethod("Task 5"), TaskCreationOptions.LongRunning);

            //Thread.Sleep(TimeSpan.FromSeconds(1));
            #endregion

            #region 4.3 使用任务执行基本的操作
            //TaskMethod("Main Thread Task");

            //Task<int> task = CreateTask("Task 1");
            //task.Start();

            //int result = task.Result;
            //Console.WriteLine("Result is: {0}",result);


            //task = CreateTask("Task 2");
            //task.RunSynchronously();
            //result = task.Result;
            //Console.WriteLine("Result is: {0}", result);

            //task = CreateTask("Task 3");
            //task.Start();

            //while (!task.IsCompleted)
            //{
            //    Console.WriteLine(task.Status);
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //}

            //Console.WriteLine(task.Status);
            //result = task.Result;
            //Console.WriteLine("Result is: {0}", result);
            #endregion

            #region 4.4 组合任务

            //var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            //var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));

            //firstTask.ContinueWith(t => Console.WriteLine("The first answer is {0}.Thread id {1}, is thread pool thread:{2}", t.Result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.OnlyOnRanToCompletion);

            //firstTask.Start();
            //secondTask.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(4));

            //Task continuation = secondTask.ContinueWith(t => Console.WriteLine("The second answer {0}.Thread id {1},is thread pool thread:{2}", t.Result, Thread.CurrentThread.ManagedThreadId,
            //    Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            //continuation.GetAwaiter().OnCompleted(() => Console.WriteLine("Continuation Task Completed!Thread id {0},is thread pool thread:{1}", Thread.CurrentThread.ManagedThreadId,
            //    Thread.CurrentThread.IsThreadPoolThread));

            //Thread.Sleep(TimeSpan.FromSeconds(2));

            //Console.WriteLine();

            //firstTask = new Task<int>(() => { var innerTask = Task.Factory.StartNew(() => TaskMethod("Second Task", 5), TaskCreationOptions.AttachedToParent);
            //    innerTask.ContinueWith(t => TaskMethod("Third Task", 2), TaskContinuationOptions.AttachedToParent);
            //    return TaskMethod("First Task", 2);
            //});

            //firstTask.Start();

            //while (!firstTask.IsCompleted)
            //{
            //    Console.WriteLine(firstTask.Status);
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //}
            //Console.WriteLine(firstTask.Status);

            //Thread.Sleep(TimeSpan.FromSeconds(10));
            #endregion

            #region 4.5 将APM模式转换为任务
            //int threadId;
            //AsynchronousTask d = Test;
            //IncompatibleAsynchronousTask e = Test;

            //Console.WriteLine("Option 1");
            //Task<string> task = Task<string>.Factory.FromAsync(d.BeginInvoke("AsyncTaskThread", CallBack, "a delegate asynchronous call"), d.EndInvoke);

            //task.ContinueWith(t => Console.WriteLine("Callback is finished,now running a continuation! Result:{0}", t.Result));

            //while (!task.IsCompleted)
            //{
            //    Console.WriteLine(task.Status);
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //}

            //Console.WriteLine(task.Status);
            //Thread.Sleep(TimeSpan.FromSeconds(1));


            //Console.WriteLine("------------------------");
            //Console.WriteLine();
            //Console.WriteLine("Option 2");

            //task = Task<string>.Factory.FromAsync(d.BeginInvoke, d.EndInvoke, "AsyncTaskThread", "a delegate asynchronous call");
            //task.ContinueWith(t => Console.WriteLine("Task is completed,nor running a continuation! Result: {0}", t.Result));

            //while (!task.IsCompleted)
            //{
            //    Console.WriteLine(task.Status);
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //}
            //Console.WriteLine(task.Status);
            //Thread.Sleep(TimeSpan.FromSeconds(1));


            //Console.WriteLine("------------------------");
            //Console.WriteLine();
            //Console.WriteLine("Option 3");


            //IAsyncResult ar = e.BeginInvoke(out threadId, CallBack, "a delegate asynchronous call");
            //ar = e.BeginInvoke(out threadId, CallBack, "a delegate asynchronous call");
            //task = Task<string>.Factory.FromAsync(ar, _ => e.EndInvoke(out threadId, ar));

            //task.ContinueWith(t => Console.WriteLine("Task is completed, now running a continuation! Result:{0},ThreadId:{1}", t.Result, threadId));

            //while (!task.IsCompleted)
            //{
            //    Console.WriteLine(task.Status);
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //}
            //Console.WriteLine(task.Status);

            //Thread.Sleep(TimeSpan.FromSeconds(1));
            #endregion

            #region 4.6 将EAP模式转换为任务

            #endregion
        }

        #region 4.6

        #endregion

        #region 4.5

        //private delegate string AsynchronousTask(string threadName);
        //private delegate string IncompatibleAsynchronousTask(out int threadId);

        //private static void CallBack(IAsyncResult ar)
        //{
        //    Console.WriteLine("Starting a callback...");
        //    Console.WriteLine("State passed to a callback:{0}", ar.AsyncState);

        //    Console.WriteLine("Is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);
        //    Console.WriteLine("Thread pool worker thread id:{0}", Thread.CurrentThread.ManagedThreadId);
        //}

        //private static string Test(string threadName)
        //{
        //    Console.WriteLine("Starting...");
        //    Console.WriteLine("Is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);
        //    Thread.Sleep(TimeSpan.FromSeconds(2));

        //    Thread.CurrentThread.Name = threadName;
        //    return string.Format("Thread name:{0}", Thread.CurrentThread.Name);
        //}
        //private static string Test(out int threadId)
        //{
        //    Console.WriteLine("Starting...");
        //    Console.WriteLine("Is thread pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);
        //    Thread.Sleep(TimeSpan.FromSeconds(2));

        //    threadId = Thread.CurrentThread.ManagedThreadId;
        //    return string.Format("Thread pool worker thread id was:{0}", threadId);
        //}
        #endregion

        #region 4.4
        //static int TaskMethod(string name, int seconds)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    return 42 * seconds;
        //}
        #endregion

        #region 4.3
        //static Task<int> CreateTask(string name)
        //{
        //    return new Task<int>(() => TaskMethod(name));
        //}

        //static int TaskMethod(string name)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

        //    Thread.Sleep(TimeSpan.FromSeconds(2));
        //    return 42;
        //}
        #endregion

        #region 4.2
        //static void TaskMethod(string name)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        //}
        #endregion
    }
}
;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            //var tcs = new TaskCompletionSource<int>();

            //var worker = new BackgroundWorker();
            //worker.DoWork += (sender, eventArgs) =>
            //  {
            //      eventArgs.Result = TaskMethod("Background worker", 5);
            //  };

            //worker.RunWorkerCompleted += (sender, eventArgs) =>
            //  {
            //      if (eventArgs.Error != null)
            //      {
            //          tcs.SetException(eventArgs.Error);
            //      }
            //      else if (eventArgs.Cancelled)
            //      {
            //          tcs.SetCanceled();
            //      }
            //      else
            //      {
            //          tcs.SetResult((int)eventArgs.Result);
            //      }
            //  };

            //worker.RunWorkerAsync();

            //int result = tcs.Task.Result;

            //Console.WriteLine("Result is:{0}", result);

            #endregion

            #region 4.7 实现取消选项
            //var cts = new CancellationTokenSource();
            //var longTask = new Task<int>(() => TaskMethod("Task 1", 10, cts.Token), cts.Token);

            //Console.WriteLine(longTask.Status);
            //cts.Cancel();
            //Console.WriteLine(longTask.Status);
            //Console.WriteLine("First task has been cancelled before execution");

            //cts = new CancellationTokenSource();
            //longTask = new Task<int>(() => TaskMethod("Task 2", 10, cts.Token), cts.Token);
            //longTask.Start();

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //    Console.WriteLine(longTask.Status);
            //}

            //cts.Cancel();
            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(0.5));
            //    Console.WriteLine(longTask.Status);
            //}

            //Console.WriteLine("A task has been completed with result {0}.", longTask.Result);

            #endregion

            #region 4.8 处理任务中的异常
            //Task<int> task;
            //try
            //{
            //    task = Task.Run(() => TaskMethod("Task 1", 2));
            //    int result = task.Result;
            //    Console.WriteLine("Result: {0}",result);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception caught: {0}",ex);
            //}

            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine();

            //try
            //{
            //    task = Task.Run(() => TaskMethod("Task 2", 2));
            //    int result = task.GetAwaiter().GetResult();
            //    Console.WriteLine("Result: {0}", result);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception caught: {0}", ex);
            //}

            //Console.WriteLine("------------------------------------------");
            //Console.WriteLine();

            //var t1 = new Task<int>(() => TaskMethod("Task 3", 3));
            //var t2 = new Task<int>(() => TaskMethod("Task 4", 2));

            //var complexTask = Task.WhenAll(t1, t2);

            //var exceptionHandler = complexTask.ContinueWith(t => Console.WriteLine("Exception caught: {0}", t.Exception), TaskContinuationOptions.OnlyOnFaulted);

            //t1.Start();
            //t2.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(5));
            #endregion

            #region 4.9 并行运行任务
            //var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            //var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));
            //var whenAllTask = Task.WhenAll(firstTask, secondTask);

            //whenAllTask.ContinueWith(t => Console.WriteLine("The first answer is {0},the second is {1}", t.Result[0], t.Result[1]));

            //firstTask.Start();
            //secondTask.Start();

            //Thread.Sleep(TimeSpan.FromSeconds(4));


            //var tasks = new List<Task<int>>();
            //for (int i = 0; i < 4; i++)
            //{
            //    int counter = i;
            //    var task = new Task<int>(() => TaskMethod(string.Format("Task {0}", counter), counter));

            //    tasks.Add(task);
            //    task.Start();

            //}
            #endregion

            #region 4.10 使用TaskScheduler配置任务的执行

            #endregion
        }

        #region 4.10

        #endregion

        #region 4.9
        //static int TaskMethod(string name, int seconds)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);

        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    return 42 * seconds;
        //}
        #endregion

        #region 4.8
        //static int TaskMethod(string name, int seconds)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);

        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));

        //    throw new Exception("Boom!");
        //    return 42 * seconds;
        //}
        #endregion

        #region 4.7
        //private static int TaskMethod(string name, int seconds, CancellationToken token)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is Thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

        //    for (int i = 0; i < seconds; i++)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(1));
        //        if (token.IsCancellationRequested)
        //        {
        //            return -1;
        //        }
        //    }
        //    return 42 * seconds;
        //}
        #endregion

        #region 4.6
        //static int TaskMethod(string name, int seconds)
        //{
        //    Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);

        //    Thread.Sleep(TimeSpan.FromSeconds(seconds));
        //    return 42 * seconds;
        //}
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
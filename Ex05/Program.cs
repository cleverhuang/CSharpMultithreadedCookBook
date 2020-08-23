using ImpromptuInterface;
using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Ex05
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region 5.2 使用await操作符获取异步任务结果
            //Task t = AsynchronyWithTPL();
            //t.Wait();

            //t = AsynchronyWithAwait();
            //t.Wait();
            #endregion

            #region 5.3 在lambda表达式中使用await操作符
            //Task t = AsynchronousProcessing();
            //t.Wait();
            #endregion

            #region 5.4 对连续的异步任务使用await操作符
            //Task t = AsynchronyWithTPL();
            //t.Wait();

            //t = AsynchronyWithAwait();
            //t.Wait();
            #endregion

            #region 5.5 对并行执行的异步任务使用await操作符
            //Task t = AsynchronousProcessing();
            //t.Wait();
            #endregion

            #region 5.6 处理异步操作中的异常
            //Task t = AsynchronousProcessing();
            //t.Wait();
            #endregion

            #region 5.7 避免使用捕获的同步上下文
            //var app = new Application();
            //var win = new Window();
            //var panel = new StackPanel();
            //var button = new Button();
            //label = new Label();
            //label.FontSize = 32;
            //label.Height = 200;
            //button.Height = 100;
            //button.FontSize = 32;
            //button.Content = new TextBlock { Text = "Start asynchronous operatons" };
            //button.Click += Click;
            //panel.Children.Add(label);
            //panel.Children.Add(button);
            //win.Content = panel;
            //app.Run(win);
            //Console.ReadLine();
            #endregion

            #region 5.8 使用async void方法
            //Task t = AsyncTask();
            //t.Wait();

            //AsyncVoid();
            //Thread.Sleep(TimeSpan.FromSeconds(3));

            //t = AsyncTaskWithErrors();

            //while (!t.IsFaulted)
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(1));
            //}
            //Console.WriteLine(t.Exception);



            //try
            //{
            //    AsyncVoidWithErrors();
            //    Thread.Sleep(TimeSpan.FromSeconds(3));
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //int[] numbers = new int[] { 1, 2, 3, 4, 5 };

            //Array.ForEach(numbers, async number =>
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //    if (number == 3)
            //    {
            //        throw new Exception("Boom!");
            //    }
            //    Console.WriteLine(number);
            //});
            //Console.ReadLine();
            #endregion

            #region 5.9 设计一个自定义的awaitable类型
            //Task t = AsynchronousProcessing();
            //t.Wait();
            #endregion

            #region 5.10 对动态类型使用await
            Task t = AsynchronousProcessing();
            t.Wait();
            #endregion
        }

        #region 5.10
        async static Task AsynchronousProcessing()
        {
            string result = await GetDynamicAwaitableObjetc(true);
            Console.WriteLine(result);

            result = await GetDynamicAwaitableObjetc(false);
            Console.WriteLine(result);
        }
        static dynamic GetDynamicAwaitableObjetc(bool completeSynchronously)
        {
            dynamic result = new ExpandoObject();
            dynamic awaiter = new ExpandoObject();

            awaiter.Message = "Completed synchronously";
            awaiter.IsCompleted = completeSynchronously;
            awaiter.GetResult = (Func<string>)(() => awaiter.Message);

            awaiter.OnCompleted = (Action<Action>)(callback => ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                awaiter.Message = GetInfo();
                if (callback != null)
                {
                    callback();
                }
            }));

            IAwaiter<string> proxy = Impromptu.ActLike(awaiter);
            result.GetAwaiter = (Func<dynamic>)(() => proxy);

            return result;
        }

        static string GetInfo()
        {
            return string.Format("Task is running on a thread id {0}.Is thread pool thread:{1}", Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread);
        }


        #endregion

        #region 5.9
        //async static Task AsynchronousProcessing()
        //{
        //    var sync = new CustomAwaitable(true);
        //    string result = await sync;
        //    Console.WriteLine(result);

        //    var async = new CustomAwaitable(false);
        //    result = await async;
        //    Console.WriteLine(result);
        //}
        #endregion

        #region 5.8
        //async static Task AsyncTaskWithErrors()
        //{
        //    string result = await GetInfoAsync("AsyncTaskException", 2);
        //    Console.WriteLine(result);
        //}

        //async static void AsyncVoidWithErrors()
        //{
        //    string result = await GetInfoAsync("AsyncVoidException", 2);
        //    Console.WriteLine(result);
        //}

        //async static Task AsyncTask()
        //{
        //    string result = await GetInfoAsync("AsyncTask", 2);
        //    Console.WriteLine(result);
        //}
        //private static async void AsyncVoid()
        //{
        //    string result = await GetInfoAsync("AsyncVoid", 2);
        //    Console.WriteLine(result);
        //}
        //async static Task<string> GetInfoAsync(string name, int seconds)
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(seconds));
        //    if (name.Contains("Exception"))
        //    {
        //        throw new Exception(string.Format("Boom from {0}!", name));
        //    }

        //    return string.Format("Task {0} is running on a thread id {1}. Is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);
        //}
        #endregion

        #region 5.7
        //private static Label label;
        //async static void Click(object sender, EventArgs e)
        //{
        //    label.Content = new TextBlock { Text = "Calculating..." };
        //    TimeSpan resultWithContext = await Test();
        //    TimeSpan resultNoContext = await TestNoContext();

        //    //TimeSpan resultNoContext = await TestNoContext().ConfigureAwait(false);

        //    var sb = new StringBuilder();
        //    sb.AppendLine(string.Format("With the context:{0}", resultWithContext));
        //    sb.AppendLine(string.Format("Without the context:{0}", resultNoContext));
        //    sb.AppendLine(string.Format("Ratio:{0:0.00}", resultWithContext.TotalMilliseconds / resultNoContext.TotalMilliseconds));

        //    label.Content = new TextBlock { Text = sb.ToString() };
        //}

        //async static Task<TimeSpan> Test()
        //{
        //    const int interationsNumber = 10000;
        //    var sw = new Stopwatch();

        //    sw.Start();
        //    for (int i = 0; i < interationsNumber; i++)
        //    {
        //        var t = Task.Run(() => { });
        //        await t;
        //    }

        //    sw.Stop();
        //    return sw.Elapsed;
        //}

        //async static Task<TimeSpan> TestNoContext()
        //{
        //    const int iterationsNumber = 100000;
        //    var sw = new Stopwatch();
        //    sw.Start();

        //    for (int i = 0; i < iterationsNumber; i++)
        //    {
        //        var t = Task.Run(() => { });
        //        await t.ConfigureAwait(continueOnCapturedContext: false);
        //    }

        //    sw.Stop();
        //    return sw.Elapsed;
        //}
        #endregion

        #region 5.6
        //async static Task AsynchronousProcessing()
        //{
        //    Console.WriteLine("1. Single exception");
        //    try
        //    {
        //        string result = await GetInfoAsync("Task 1", 2);
        //        Console.WriteLine(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception details:{0}", ex);
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("2. Multiple exceptions");

        //    Task<string> t1 = GetInfoAsync("Task 1", 3);
        //    Task<string> t2 = GetInfoAsync("Task 2", 2);
        //    try
        //    {
        //        string[] results = await Task.WhenAll(t1, t2);
        //        Console.WriteLine(results.Length);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Exception details:{0}", ex);
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("2. Multiple exceptions with AggregateException");

        //    t1 = GetInfoAsync("Task 1", 3);
        //    t2 = GetInfoAsync("Task 2", 2);
        //    Task<string[]> t3 = Task.WhenAll(t1, t2);

        //    try
        //    {
        //        string[] results = await t3;
        //        Console.WriteLine(results.Length);
        //    }
        //    catch
        //    {
        //        var ae = t3.Exception.Flatten();
        //        var exceptions = ae.InnerExceptions;
        //        Console.WriteLine("Exceptions caught:{0}", exceptions.Count);
        //        foreach (var e in exceptions)
        //        {
        //            Console.WriteLine("Exception details:{0}", e);
        //            Console.WriteLine();
        //        }
        //    }
        //}

        //async static Task<string> GetInfoAsync(string name, int seconds)
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(seconds));
        //    throw new Exception(string.Format("Boom from {0}!", name));
        //}
        #endregion

        #region 5.5
        //async static Task AsynchronousProcessing()
        //{
        //    Task<string> t1 = GetInfoAsync("Task 1", 3);
        //    Task<string> t2 = GetInfoAsync("Task 2", 5);

        //    string[] results = await Task.WhenAll(t1, t2);

        //    foreach (string result in results)
        //    {
        //        Console.WriteLine(result);
        //    }
        //}

        //async static Task<string> GetInfoAsync(string name, int seconds)
        //{
        //    //await Task.Delay(TimeSpan.FromSeconds(seconds));

        //    await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(seconds)));

        //    return string.Format("Task {0} is running on a thread id {1}. Is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);

        //}
        #endregion

        #region 5.4
        //static Task AsynchronyWithTPL()
        //{
        //    var containerTask = new Task(() =>
        //    {
        //        Task<string> t = GetInfoAsync("TPL 1");
        //        t.ContinueWith(task =>
        //        {
        //            Console.WriteLine(t.Result);
        //            Task<string> t2 = GetInfoAsync("TPL 2");

        //            t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Result), TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent);

        //            t2.ContinueWith(innerTask => Console.WriteLine(innerTask.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
        //        },
        //        TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.AttachedToParent);
        //        t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);

        //    });
        //    containerTask.Start();
        //    return containerTask;
        //}

        //async static Task AsynchronyWithAwait()        
        //{
        //    try
        //    {
        //        string result = await GetInfoAsync("Async 1");
        //        Console.WriteLine(result);

        //        result = await GetInfoAsync("Async 2");
        //        Console.WriteLine(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        //async static Task<string> GetInfoAsync(string name)
        //{
        //    Console.WriteLine("Task {0} started!",name);
        //    await Task.Delay(TimeSpan.FromSeconds(2));

        //    if (name=="TPL 2")
        //    {
        //        throw new Exception("Boom!");
        //    }

        //    return string.Format("Task {0} is running on a thread id {1}. Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);
        //}
        #endregion

        #region 5.3
        //async static Task AsynchronousProcessing()
        //{
        //    Func<string, Task<string>> asyncLambda = async name =>
        //    {
        //        await Task.Delay(TimeSpan.FromSeconds(2));
        //        return string.Format("Task {0} is running on a thread id {1}. Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        //    };
        //    string result = await asyncLambda("async lambda");
        //    Console.WriteLine(result);

        //}

        #endregion

        #region 5.2
        //static Task AsynchronyWithTPL()
        //{
        //    Task<string> t = GetInfoAsync("Task 1");
        //    Task t2 = t.ContinueWith(task => Console.WriteLine(t.Result), TaskContinuationOptions.NotOnFaulted);

        //    Task t3 = t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);

        //    return Task.WhenAny(t2, t3);
        //}

        //async static Task AsynchronyWithAwait()
        //{
        //    try
        //    {
        //        string result = await GetInfoAsync("Task 2");
        //        Console.WriteLine(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //}

        //async static Task<string> GetInfoAsync(string name)
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(2));

        //    return string.Format("Task {0} is running on thread id {1}, Is thread pool thread:{2}", name, Thread.CurrentThread.ManagedThreadId,
        //        Thread.CurrentThread.IsThreadPoolThread);
        //}
        #endregion
    }
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }

    //internal class CustomAwaitable
    //{
    //    private readonly     bool completeSynchronously;

    //    public CustomAwaitable(bool completeSynchronously)
    //    {
    //        this.completeSynchronously = completeSynchronously;
    //    }

    //    public CustomAwaiter GetAwaiter()
    //    {
    //        return new CustomAwaiter(completeSynchronously);
    //    }
    //}

    //public class CustomAwaiter:INotifyCompletion
    //{
    //    private string result = "Completed synchronously";
    //    private readonly bool completeSynchronously;

    //    public bool IsCompleted { get { return completeSynchronously; } }

    //    public CustomAwaiter(bool completeSynchronously)
    //    {
    //        this.completeSynchronously = completeSynchronously;
    //    }
    //    public string GetResult()
    //    {
    //        return result;
    //    }

    //    public void OnCompleted(Action continuation)
    //    {
    //        ThreadPool.QueueUserWorkItem(state =>
    //        {
    //            Thread.Sleep(TimeSpan.FromSeconds(1));
    //            result = GetInfo();
    //            if (continuation != null)
    //            {
    //                continuation();
    //            }
    //        });
    //    }

    //    private string GetInfo()
    //    {
    //        return string.Format("Task is running on a thread id {0}.Is thread  pool thread:{1}", Thread.CurrentThread.ManagedThreadId,
    //            Thread.CurrentThread.IsThreadPoolThread);
    //    }
    //}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex06
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 6.2 使用ConcurrentDictionary
            //var concurrentDictionary = new ConcurrentDictionary<int, string>();
            //var dictionary = new Dictionary<int, string>();

            //var sw = new Stopwatch();
            //sw.Start();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    lock (dictionary)
            //    {
            //        dictionary[i] = Item;
            //    }
            //}
            //sw.Stop();
            //Console.WriteLine($"Writing to dictionary with a lock:{sw.Elapsed,20}");

            //sw.Restart();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    concurrentDictionary[i] = Item;
            //}
            //sw.Stop();
            //Console.WriteLine($"Writing to concurrent dictionary:{sw.Elapsed,20}");

            //sw.Restart();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    lock (dictionary)
            //    {
            //        CurrentItem = dictionary[i];
            //    }
            //}
            //sw.Stop();
            //Console.WriteLine($"Reading from dictionary with a lock:{ sw.Elapsed,20}");
            //sw.Restart();
            //for (int i = 0; i < 10000000; i++)
            //{
            //    CurrentItem = concurrentDictionary[i];
            //}
            //sw.Stop();
            //Console.WriteLine($"Reading from a concurrent dictionary:{sw.Elapsed,20}");

            #endregion

            #region 6.3 使用ConcurrentQueue实现异步处理
            //Task t = RunProgram();
            //t.Wait();
            #endregion

            #region 6.4 改变ConcurrentStack异步处理顺序
            //Task t = RunProgram();
            //t.Wait();
            #endregion

            #region 6.5 使用ConcurrentBag创建一个可扩展的爬虫

            #endregion

            #region 6.6 使用BlockingCollection进行异步处理
            Console.WriteLine("Using a Queue inside of BlockingCollection");
            Console.WriteLine();
            Task t = RunProgram();
            t.Wait();

            Console.WriteLine();
            Console.WriteLine("Using a Stack inside of BlockingCollection");
            Console.WriteLine();
            t = RunProgram(new ConcurrentStack<CustomTask>());
            t.Wait();
            #endregion
        }
        
        #region 6.6
        static async Task RunProgram(IProducerConsumerCollection<CustomTask> collection=null)
        {
            var taskCollection = new BlockingCollection<CustomTask>();
            if (null!=collection)
            {
                taskCollection = new BlockingCollection<CustomTask>(collection);
            }

            var taskSource = Task.Run(() => TaskProducer(taskCollection));

            Task[] processors = new Task[4];
            for (int i = 1; i <= 4; i++)
            {
                string processorId = "Processor " + i;
                processors[i - 1] = Task.Run(() => TaskProcessor(taskCollection, processorId));
            }
            await taskSource;
            await Task.WhenAll(processors);
        }

        static async Task TaskProducer(BlockingCollection<CustomTask> collection)
        {
            for (int i = 1; i <= 20; i++)
            {
                await Task.Delay(20);
                var workItem = new CustomTask { Id = i };
                collection.Add(workItem);
                Console.WriteLine("Task {0} have been posted", workItem.Id);
            }

            collection.CompleteAdding();
        }

        static async Task TaskProcessor(BlockingCollection<CustomTask> collection, string name)
        {
            await GetRandomDelay();
            foreach (CustomTask item in collection.GetConsumingEnumerable())
            {
                Console.WriteLine("Task {0} have been processed by {1}",item.Id,name);
                await GetRandomDelay();
            }
        }

        static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
            return Task.Delay(delay);
        }
        class CustomTask
        {
            public int Id { get; set; }
        }
        #endregion

        #region 6.5
        //static Dictionary<string, string[]> contentEnulation = new Dictionary<string, string[]>();

        //static async Task RunProgram()
        //{
        //    var bag = new ConcurrentBag<CrawlingTask>();
        //    string[] urls = new string[] { "https://microsoft.com/", "https://baidu.com", "https://youku.com/", "https://www.iqiyi.com/" };

        //    var crawlers = new Task[4];
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        string crawlerName = "Crawler " + i.ToString();
        //        bag.Add(new CrawlingTask { UrlToCrawl = urls[i - 1], ProducerName = "root" });
        //        crawlers[i - 1] = Task.Run(() => Crawl(bag, crawlerName));
        //    }

        //    await Task.WhenAll(crawlers);
        //}

        //static async Task Crawl(ConcurrentBag<CrawlingTask> bag, string crawlerName)
        //{
        //    CrawlingTask task;
        //    while (bag.TryTake(out task))
        //    {
        //        IEnumerable<string> urls = await GetLinksFromContent(task);

        //        if (urls != null)
        //        {
        //            foreach (var url in urls)
        //            {
        //                var t = new CrawlingTask
        //                {
        //                    UrlToCrawl = url,
        //                    ProducerName = crawlerName
        //                };
        //                bag.Add(t);
        //            }
        //        }
        //        Console.WriteLine("Indexing url {0} posted by {1} is completed by {2}!", task.UrlToCrawl, task.ProducerName, crawlerName);
        //    }
        //}

        //static async Task<IEnumerable<string>> GetLinksFromContent(CrawlingTask task)
        //{
        //    await GetRandomDelay();
        //    if (contentEnulation.ContainsKey(task.UrlToCrawl))
        //    {
        //        return contentEnulation[task.UrlToCrawl];
        //    }
        //    return null;
        //}
        //static void CreateLinks()
        //{
        //    contentEnulation["https://microsoft.com/"] =
        //        new[] { "https://microsoft.com/a.html", "https://microsoft.com/b.html" };
        //    contentEnulation["https://microsoft.com/a.html"] =
        //         new[] { "https://microsoft.com/c.html", "https://microsoft.com/d.html" };
        //}

        //static Task GetRandomDelay()
        //{
        //    int delay = new Random(DateTime.Now.Millisecond).Next(150, 200);
        //    return Task.Delay(delay);
        //}
        //class CrawlingTask
        //{
        //    public string UrlToCrawl { get; set; }
        //    public string ProducerName { get; set; }
        //}
        #endregion

        #region 6.4
        //async static Task RunProgram()
        //{
        //    var taskStack = new ConcurrentStack<CustomTask>();
        //    var cts = new CancellationTokenSource();

        //    var taskSource = Task.Run(() => TaskProducer(taskStack));

        //    Task[] processors = new Task[4];
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        string processorId = i.ToString();
        //        processors[i - 1] = Task.Run(() => TaskProcessor(taskStack, "Processor " + processorId, cts.Token));
        //    }

        //    await taskSource;
        //    cts.CancelAfter(TimeSpan.FromSeconds(2));

        //    await Task.WhenAll(processors);
        //}

        //static async Task TaskProducer(ConcurrentStack<CustomTask> stack)
        //{
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        await Task.Delay(50);
        //        var workItem = new CustomTask { Id = i };
        //        stack.Push(workItem);
        //        Console.WriteLine("Task {0} has been posted", workItem.Id);
        //    }
        //}

        //static async Task TaskProcessor(ConcurrentStack<CustomTask> stack, string name, CancellationToken token)
        //{
        //    await GetRandomDelay();
        //    do
        //    {
        //        CustomTask workItem;
        //        bool popSuccessful = stack.TryPop(out workItem);

        //        if (popSuccessful)
        //        {
        //            Console.WriteLine("Task {0} has been processed by {1}", workItem.Id, name);
        //        }
        //        await GetRandomDelay();
        //    } while (!token.IsCancellationRequested);
        //}

        //static Task GetRandomDelay()
        //{
        //    int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
        //    return Task.Delay(delay);
        //}

        //class CustomTask
        //{
        //    public int Id { get; set; }
        //}
        #endregion

        #region 6.3
        //async static Task RunProgram()
        //{
        //    var taskQueue = new ConcurrentQueue<CustomTask>();
        //    var cts = new CancellationTokenSource();

        //    var taskSource = Task.Run(() => TaskProducer(taskQueue));

        //    Task[] processors = new Task[4];
        //    for (int i = 1; i <= 4; i++)
        //    {
        //        string processorId = i.ToString();
        //        processors[i - 1] = Task.Run(() => TaskProcessor(taskQueue, "Processor " + processorId, cts.Token));
        //    }

        //    await taskSource;
        //    cts.CancelAfter(TimeSpan.FromSeconds(2));

        //    await Task.WhenAll(processors);
        //}

        //static async Task TaskProducer(ConcurrentQueue<CustomTask> queue)
        //{
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        await Task.Delay(50);
        //        var workItem = new CustomTask { Id = i };
        //        queue.Enqueue(workItem);
        //        Console.WriteLine("Task {0} has been posted",workItem.Id);
        //    }
        //}

        //static async Task TaskProcessor(ConcurrentQueue<CustomTask> queue, string name, CancellationToken token)
        //{
        //    CustomTask workItem;
        //    bool dequeueSuccessful = false;
        //    await GetRandomDelay();

        //    do
        //    {
        //        dequeueSuccessful = queue.TryDequeue(out workItem);
        //        if (dequeueSuccessful)
        //        {
        //            Console.WriteLine("Task {0} has been processed by {1}", workItem.Id, name);
        //        }
        //        await GetRandomDelay();
        //    } while (!token.IsCancellationRequested);
        //}

        //static Task GetRandomDelay()
        //{
        //    int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
        //    return Task.Delay(delay);
        //}

        //class CustomTask
        //{
        //    public int Id { get; set; }
        //}
        #endregion

        #region 6.2
        //const string Item = "Dictionary item";
        //public static string CurrentItem;
        #endregion
    }
}

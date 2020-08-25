using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Ex10
{
    //第10章 并行编程模式
    class Program
    {
        static void Main(string[] args)
        {
            #region 10.2 实现惰性求值的共享状态
            //var t = ProcessAsynchronously();
            //t.GetAwaiter().GetResult();

            //Console.WriteLine("Press Enter to exit.");
            //Console.ReadLine();
            #endregion

            #region 10.3 使用BlockingCollection实现并行管道

            //var cts = new CancellationTokenSource();
            //Task.Run(() =>
            //{
            //    if (Console.ReadKey().KeyChar == 'c')
            //    {
            //        cts.Cancel();
            //    }
            //});

            //var sourceArrays = new BlockingCollection<int>[CollectionsNumber];
            //for (int i = 0; i < sourceArrays.Length; i++)
            //{
            //    sourceArrays[i] = new BlockingCollection<int>(Count);
            //}

            //var filter1 = new PipelineWorker<int, decimal>(
            //    sourceArrays,
            //    (n) => Convert.ToDecimal(n * 0.97), 
            //    cts.Token,
            //    "filter1");


            //var filter2 = new PipelineWorker<decimal, string>(
            //    filter1.Output, 
            //    (s) => string.Format("--{0}--", s),
            //    cts.Token, 
            //    "filter2");

            //var filter3 = new PipelineWorker<string, string>(
            //    filter2.Output,
            //    (s) => Console.WriteLine("The final result is {0} on thread id {1}", s, Thread.CurrentThread.ManagedThreadId), cts.Token, 
            //    "filter3");

            //try
            //{
            //    Parallel.Invoke(() =>
            //    {
            //        Parallel.For(0, sourceArrays.Length * Count, (j, state) =>
            //            {
            //                if (cts.Token.IsCancellationRequested)
            //                {
            //                    state.Stop();
            //                }
            //                int k = BlockingCollection<int>.TryAddToAny(sourceArrays, j);
            //                if (k >= 0)
            //                {
            //                    Console.WriteLine("added {0} to source data on thread id {1}", j, Thread.CurrentThread.ManagedThreadId);
            //                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
            //                }
            //            });
            //        foreach (var arr in sourceArrays)
            //        {
            //            arr.CompleteAdding();
            //        }
            //    },
            //    () => filter1.Run(),
            //    () => filter2.Run(),
            //    () => filter3.Run());
            //}
            //catch (AggregateException ae)
            //{
            //    foreach (var ex in ae.InnerExceptions)
            //    {
            //        Console.WriteLine(ex.Message + ex.StackTrace);
            //    }

            //}
            //if (cts.Token.IsCancellationRequested)
            //{
            //    Console.WriteLine("Operation has been canceled! Press ENTER to exit.");
            //}
            //else
            //{
            //    Console.WriteLine("Press ENTER to exit.");
            //}
            //Console.ReadLine();
            #endregion

            #region 10.4 使用TPL数据流实现并行管道
            //Task t = ProcessAsynchronously();
            //t.Wait();
            #endregion

            #region 10.5 使用PLINQ实现Map/Reduce模式

            #endregion
        }

        #region 10.5

        #endregion

        #region 10.4
        //async static Task ProcessAsynchronously()
        //{
        //    var cts = new CancellationTokenSource();

        //    Task.Run(() =>
        //    {
        //        if (Console.ReadKey().KeyChar == 'c')
        //        {
        //            cts.Cancel();
        //        }
        //    });

        //    var inputBlock = new BufferBlock<int>(new DataflowBlockOptions { BoundedCapacity = 5, CancellationToken = cts.Token });

        //    var filterBlock = new TransformBlock<int, decimal>(n =>
        //    {
        //        decimal result = Convert.ToDecimal(n * 0.97);
        //        Console.WriteLine("Filter 1 sent {0} to the next stage on thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
        //        Thread.Sleep(TimeSpan.FromMilliseconds(100));
        //        return result;
        //    },
        //    new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4, CancellationToken = cts.Token });

        //    var filter2Block = new TransformBlock<decimal, string>(
        //        n =>
        //        {
        //            string result = string.Format("---{0}---", n);
        //            Console.WriteLine("Filter 2 sent {0} to the next stage on thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
        //            Thread.Sleep(TimeSpan.FromMilliseconds(100));
        //            return result;
        //        },
        //        new ExecutionDataflowBlockOptions
        //        {
        //            MaxDegreeOfParallelism = 4,
        //            CancellationToken = cts.Token
        //        });

        //    var outputBlock = new ActionBlock<string>(
        //        s =>
        //        {
        //            Console.WriteLine("The final result is {0} on the thread id {1}", s, Thread.CurrentThread.ManagedThreadId);
        //        },
        //        new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 4, CancellationToken = cts.Token });

        //    inputBlock.LinkTo(filterBlock, new DataflowLinkOptions { PropagateCompletion = true });

        //    filterBlock.LinkTo(filter2Block, new DataflowLinkOptions { PropagateCompletion = true });

        //    filter2Block.LinkTo(outputBlock, new DataflowLinkOptions { PropagateCompletion = true });

        //    try
        //    {
        //        Parallel.For(0, 20, new ParallelOptions { MaxDegreeOfParallelism = 4, CancellationToken = cts.Token },
        //            i =>
        //            {
        //                Console.WriteLine("added {0} to source data on thread id {1}", i, Thread.CurrentThread.ManagedThreadId);
        //                inputBlock.SendAsync(i).GetAwaiter().GetResult();
        //            });

        //        inputBlock.Complete();
        //        await outputBlock.Completion;

        //        Console.WriteLine("Press ENTER to exist.");
        //    }
        //    catch (OperationCanceledException )
        //    {
        //        Console.WriteLine("Operation has been canceled! Press ENTER to exist.");
        //    }

        //    Console.ReadLine();
        //}
        #endregion

        #region 10.3
        //private const int CollectionsNumber = 4;
        //private const int Count = 10;

        //class PipelineWorker<TInput, TOutput>
        //{
        //    Func<TInput, TOutput> _processor = null;
        //    Action<TInput> _outputProcessor = null;
        //    BlockingCollection<TInput>[] _input;
        //    CancellationToken _token;

        //    public PipelineWorker(
        //        BlockingCollection<TInput>[] input, Func<TInput, TOutput> processor,
        //        CancellationToken token,
        //        string name)
        //    {
        //        _input = input;
        //        Output = new BlockingCollection<TOutput>[_input.Length];
        //        for (int i = 0; i < Output.Length; i++)
        //        {
        //            Output[i] = null == input[i] ? null : new BlockingCollection<TOutput>(Count);
        //        }

        //        _processor = processor;
        //        _token = token;
        //        Name = name;
        //    }


        //    public PipelineWorker(BlockingCollection<TInput>[] input,
        //        Action<TInput> renderer,
        //        CancellationToken token,
        //        string name)
        //    {
        //        _input = input;
        //        _outputProcessor = renderer;
        //        _token = token;
        //        Name = name;
        //        Output = null;
        //    }

        //    public BlockingCollection<TOutput>[] Output { get; private set; }

        //    public string Name { get; private set; }
        //    public void Run()
        //    {
        //        Console.WriteLine("{0} is running", this.Name);

        //        while (!_input.All(bc => bc.IsCompleted) && !_token.IsCancellationRequested)
        //        {
        //            TInput receivedItem;
        //            int i = BlockingCollection<TInput>.TryTakeFromAny(_input, out receivedItem, 50, _token);
        //            if (i >= 0)
        //            {
        //                if (Output != null)
        //                {
        //                    TOutput outputItem = _processor(receivedItem);
        //                    BlockingCollection<TOutput>.AddToAny(Output, outputItem);
        //                    Console.WriteLine("{0} sent {1} to next, on thread id {2}", Name, outputItem, Thread.CurrentThread.ManagedThreadId);
        //                    Thread.Sleep(TimeSpan.FromSeconds(100));
        //                }
        //                else
        //                {
        //                    _outputProcessor(receivedItem);
        //                }
        //            }
        //            else
        //            {
        //                Thread.Sleep(TimeSpan.FromMilliseconds(50));
        //            }
        //        }
        //        if (Output != null)
        //        {
        //            foreach (var bc in Output)
        //            {
        //                bc.CompleteAdding();
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 10.2
        //static async Task ProcessAsynchronously()
        //{
        //    var unsafeState = new UnsafeState();
        //    Task[] tasks = new Task[4];

        //    for (int i = 0; i < 4; i++)
        //    {
        //        tasks[i] = Task.Run(() => Worker(unsafeState));
        //    }

        //    await Task.WhenAll(tasks);
        //    Console.WriteLine(" ------------------------- ");

        //    var firstState = new DoubleCheckedLocking();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        tasks[i] = Task.Run(() => Worker(firstState));
        //    }

        //    await Task.WhenAll(tasks);
        //    Console.WriteLine(" ------------------------- ");

        //    var secondState = new BCLDoubleChecked();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        tasks[i] = Task.Run(()=>Worker(secondState));
        //    }
        //    await Task.WhenAll(tasks);
        //    Console.WriteLine(" ------------------------- ");

        //    var thirdState = new Lazy<ValueToAccess>(Compute);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        tasks[i] = Task.Run(() => Worker(thirdState));
        //    }
        //    await Task.WhenAll(tasks);
        //    Console.WriteLine(" ------------------------- ");

        //    var fourthState = new BCLThreadSafeFactory();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        tasks[i] = Task.Run(() => Worker(fourthState));
        //    }
        //    await Task.WhenAll(tasks);
        //    Console.WriteLine(" ------------------------- ");
        //}

        //static void Worker(IHasValue state)
        //{
        //    Console.WriteLine("Worker runs on thread id {0}", Thread.CurrentThread.ManagedThreadId);
        //    Console.WriteLine("State value:{0}", state.Value.Text);
        //}

        //static void Worker(Lazy<ValueToAccess> state)
        //{
        //    Console.WriteLine("Worker runs on thread id {0}", Thread.CurrentThread.ManagedThreadId);
        //    Console.WriteLine("State value:{0}",state.Value.Text);
        //}

        //static ValueToAccess Compute()
        //{
        //    Console.WriteLine("The value is being constructed on a thread id {0}",Thread.CurrentThread.ManagedThreadId);
        //    Thread.Sleep(TimeSpan.FromSeconds(1));
        //    return new ValueToAccess(string.Format("Constructed on thread id {0}", Thread.CurrentThread.ManagedThreadId));
        //}
        //class ValueToAccess
        //{
        //    private readonly string _text;
        //    public ValueToAccess(string text)
        //    {
        //        _text = text;
        //    }

        //    public string Text { get { return _text; } }
        //}

        //class UnsafeState : IHasValue
        //{
        //    private ValueToAccess _value;
        //    public ValueToAccess Value
        //    {
        //        get
        //        {
        //            if (_value == null)
        //            {
        //                _value = Compute();
        //            }
        //            return _value;
        //        }
        //    }
        //}

        //class DoubleCheckedLocking:IHasValue
        //{
        //    private object _syncRoot = new object();
        //    private volatile ValueToAccess _value;

        //    public ValueToAccess Value
        //    {
        //        get
        //        {
        //            if (_value == null)
        //            {
        //                lock (_syncRoot)
        //                {
        //                    if (_value==null)
        //                    {
        //                        _value = Compute();
        //                    }
        //                }

        //            }
        //            return _value;
        //        }
        //    }
        //}

        //class BCLDoubleChecked : IHasValue
        //{
        //    private object _syncRoot = new object();
        //    private ValueToAccess _value;
        //    private bool _initialized = false;
        //    public ValueToAccess Value
        //    {
        //        get
        //        {
        //            return LazyInitializer.EnsureInitialized(ref _value, ref _initialized, ref _syncRoot, Compute);
        //        }
        //    }
        //}

        //class BCLThreadSafeFactory : IHasValue
        //{
        //    private ValueToAccess _value;
        //    public ValueToAccess Value
        //    {
        //        get
        //        {
        //            return LazyInitializer.EnsureInitialized(ref _value, Compute);
        //        }
        //    }

        //}

        //interface IHasValue
        //{
        //    ValueToAccess Value { get; }
        //}
        #endregion
    }
}

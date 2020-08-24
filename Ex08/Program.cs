using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Ex08
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 8.2 将普通集合转换为异步的可观察集合
            //foreach (int i in EnumerableEventSequence())
            //{
            //    Console.Write(i);
            //}
            //Console.WriteLine();
            //Console.WriteLine("IEnumerable");

            //IObservable<int> o = EnumerableEventSequence().ToObservable();

            //using (IDisposable subscription = o.Subscribe(Console.Write))
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("IObservable");
            //}

            //o = EnumerableEventSequence().ToObservable().SubscribeOn(TaskPoolScheduler.Default);
            //using (IDisposable subscription = o.Subscribe(Console.Write))
            //{
            //    Console.WriteLine();
            //    Console.WriteLine("IObservable async");
            //    Console.ReadLine();
            //}

            #endregion

            #region 8.3 编写自定义的可观察对象
            //var observer = new CustomObserver();

            //var goodObservable = new CustomSequence(new[] { 1, 2, 3, 4, 5, 6 });
            //var badObservable = new CustomSequence(null);

            //using (IDisposable subscription = goodObservable.Subscribe(observer))
            //{

            //}

            //using (IDisposable subscription = goodObservable.SubscribeOn(TaskPoolScheduler.Default).Subscribe(observer))
            //{
            //    Thread.Sleep(100);
            //}

            //using (IDisposable subscription = badObservable.SubscribeOn(TaskPoolScheduler.Default).Subscribe(observer))
            //{
            //    Console.ReadLine();
            //}
            #endregion

            #region 8.4 使用Subject
            //    Console.WriteLine("Subject");
            //    var subject = new Subject<string>();


            //    subject.OnNext("A");
            //    using (var subscription = OutputToConsole(subject))
            //    {
            //        subject.OnNext("B");
            //        subject.OnNext("C");
            //        subject.OnNext("D");
            //        subject.OnCompleted();
            //        subject.OnNext("Will not be print out");
            //    }

            //    Console.WriteLine("ReplaySubject");
            //    var replaySubject = new ReplaySubject<string>();

            //    replaySubject.OnNext("A");
            //    using (var subscription = OutputToConsole(replaySubject))
            //    {
            //        replaySubject.OnNext("B");
            //        replaySubject.OnNext("C");
            //        replaySubject.OnNext("D");
            //        replaySubject.OnCompleted();
            //    }

            //    Console.WriteLine("Buffered ReplaySubject");
            //    var bufferedSubject = new ReplaySubject<string>(2);

            //    bufferedSubject.OnNext("A");
            //    bufferedSubject.OnNext("B");
            //    bufferedSubject.OnNext("C");

            //    using (var subscription = OutputToConsole(bufferedSubject))
            //    {
            //        bufferedSubject.OnNext("D");
            //        bufferedSubject.OnCompleted();
            //    }

            //    Console.WriteLine("Time window ReplaySubject");
            //    var timeSubject = new ReplaySubject<string>(TimeSpan.FromMilliseconds(200));

            //    timeSubject.OnNext("A");
            //    Thread.Sleep(TimeSpan.FromMilliseconds(100));

            //    timeSubject.OnNext("B");
            //    Thread.Sleep(TimeSpan.FromMilliseconds(100));

            //    timeSubject.OnNext("C");
            //    Thread.Sleep(TimeSpan.FromMilliseconds(100));


            //    using (var subscription = OutputToConsole(timeSubject))
            //    {
            //        Thread.Sleep(TimeSpan.FromMilliseconds(300));
            //        timeSubject.OnNext("D");
            //        timeSubject.OnCompleted();
            //    }

            //    Console.WriteLine("AsyncSubject");
            //    var asyncSubject = new AsyncSubject<string>();
            //    asyncSubject.OnNext("A");
            //    using (var subscription = OutputToConsole(asyncSubject))
            //    {
            //        asyncSubject.OnNext("B");
            //        asyncSubject.OnNext("C");
            //        asyncSubject.OnNext("D");
            //        asyncSubject.OnCompleted();
            //    }

            //    Console.WriteLine("BehaviorSubject");
            //    var behaviorSubject = new BehaviorSubject<string>("Default");

            //    using (var subscription = OutputToConsole(behaviorSubject))
            //    {
            //        behaviorSubject.OnNext("B");
            //        behaviorSubject.OnNext("C");
            //        behaviorSubject.OnNext("D");
            //        behaviorSubject.OnCompleted();
            //    }

            #endregion

            #region 8.5 创建可观察的对象
            //IObservable<int> o = Observable.Return(0);
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Empty<int>();
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Throw<int>(new Exception());
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Repeat(42);
            //using (var sub = OutputToConsole(o.Take(5))) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Range(0, 10);
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Create<int>(ob =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        ob.OnNext(i);
            //    }
            //    return Disposable.Empty;
            //});
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //o = Observable.Generate(
            //    0,
            //    i => i < 5,
            //    i => ++i,
            //    i => i * 2);
            //using (var sub = OutputToConsole(o)) ;
            //Console.WriteLine(" -------------- ");

            //IObservable<long> ol = Observable.Interval(TimeSpan.FromSeconds(1));
            //using (var sub = OutputToConsole(ol))
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(3));
            //}
            //Console.WriteLine(" -------------- ");


            //ol = Observable.Timer(DateTimeOffset.Now.AddSeconds(2));
            //using (var sub = OutputToConsole(ol))
            //{
            //    Thread.Sleep(TimeSpan.FromSeconds(3));
            //}
            //Console.WriteLine(" -------------- ");
            #endregion

            #region 8.6 对可观察的集合使用LINQ查询
            //IObservable<long> sequence = Observable.Interval(TimeSpan.FromMilliseconds(50)).Take(21);

            //var evenNumbers = from n in sequence
            //                 where n % 2 == 0
            //                 select n;

            //var oddNumbers = from n in sequence
            //                where n % 2 != 0
            //                select n;

            //var combine = from n in evenNumbers.Concat(oddNumbers)
            //              select n;

            //var nums = (from n in combine
            //            where n % 5 == 0
            //            select n).Do(n => Console.WriteLine("------Number {0} is processed in Do method", n));

            //using (var sub=OutputToConsole(sequence,0))
            //using (var sub2=OutputToConsole(combine,1))
            //using (var sub3=OutputToConsole(nums,2))
            //{
            //    Console.WriteLine("Press ENTER to finish the demo");
            //    Console.ReadLine();
            //}
            #endregion

            #region 8.7 使用Rx创建异步操作
            IObservable<string> o = LongRunningOperationAsync("Task 1");
            using (var sub=OutputToConsole(o))
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            };
            Console.WriteLine(" ----------------- ");

            Task<string> t = LongRunningOperationTaskAsync("Task 2");
            using (var sub=OutputToConsole(t.ToObservable()))
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            };
            Console.WriteLine(" ----------------- ");

            Func<string,string> asyncMethod = LongRunningOperation;

            Func<string, IObservable<string>> observableFactory = Observable.FromAsyncPattern<string, string>(asyncMethod.BeginInvoke, asyncMethod.EndInvoke);

            o = observableFactory("Task 3");
            using (var sub =OutputToConsole(o))
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            };
            Console.WriteLine(" ----------------- ");

            o = observableFactory("Task 4");
            AwaitOnObservable(o).Wait();
            Console.WriteLine(" ----------------- ");


            using (var timer=new System.Timers.Timer(1000))
            {
                var ot = Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>(
                    h => timer.Elapsed += h,
                    h => timer.Elapsed -= h);

                timer.Start();

                using (var sub=OutputToConsole(ot))
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                }
                Console.WriteLine(" ----------------- ");
                timer.Stop();
            }


            #endregion
        }

        #region 8.7
        static async Task<T> AwaitOnObservable<T>(IObservable<T> observable)
        {
            T obj = await observable;
            Console.WriteLine("{0}", obj);
            return obj;
        }

        static Task<string> LongRunningOperationTaskAsync(string name)
        {
            return Task.Run(() => LongRunningOperation(name));
        }

        static IObservable<string> LongRunningOperationAsync(string name)
        {
            return Observable.Start(() => LongRunningOperation(name));
        }

        static string LongRunningOperation(string name)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return string.Format("Task {0} is completed.Thread Id {1}", name, Thread.CurrentThread.ManagedThreadId);
        }

        static IDisposable OutputToConsole(IObservable<EventPattern<ElapsedEventArgs>> sequence)
        {
            return sequence.Subscribe(
                obj => Console.WriteLine("{0}", obj.EventArgs.SignalTime),
                ex => Console.WriteLine("Error:{0}", ex.Message),
                () => Console.WriteLine("Completed")
                );
        }

        static IDisposable OutputToConsole<T>(IObservable<T> sequence)
        {
            return sequence.Subscribe(
        obj => Console.WriteLine("{0}", obj),
        ex => Console.WriteLine("Error:{0}", ex.Message),
        () => Console.WriteLine("Completed")
        );
        }

        #endregion

        #region 8.6

        //static IDisposable OutputToConsole<T>(IObservable<T> sequence,int innerLevel)
        //{
        //    string delimiter = innerLevel == 0 ? string.Empty : new string('-', innerLevel * 3);

        //    return sequence.Subscribe(
        //        obj => Console.WriteLine("{0}{1}", delimiter, obj),
        //        ex => Console.WriteLine("Error:{0}", ex.Message),
        //        () => Console.WriteLine("{0} Completed", delimiter)
        //        );
        //}
        #endregion

        #region 8.5
        //static IDisposable OutputToConsole<T>(IObservable<T> sequence)
        //{
        //    return sequence.Subscribe(
        //        obj => Console.WriteLine("{0}", obj),
        //        ex => Console.WriteLine("Error:{0}", ex.Message),
        //        () => Console.WriteLine("Completed")
        //        );
        //}
        #endregion


        #region 8.4
        //static IDisposable OutputToConsole<T>(IObservable<T> sequence)
        //{
        //    return sequence.Subscribe(
        //        obj => Console.WriteLine("{0}", obj),
        //        ex => Console.WriteLine("Error:{0}", ex.Message),
        //        () => Console.WriteLine("Completed"));
        //}
        #endregion

        #region 8.3
        //class CustomObserver : IObserver<int>
        //{
        //    public void OnCompleted()
        //    {
        //        Console.WriteLine("Completed");
        //    }

        //    public void OnError(Exception error)
        //    {
        //        Console.WriteLine("Error:{0}", error.Message);
        //    }

        //    public void OnNext(int value)
        //    {
        //        Console.WriteLine("Next value:{0};Thread Id:{1}", value, Thread.CurrentThread.ManagedThreadId);
        //    }
        //}

        //class CustomSequence : IObservable<int>
        //{
        //    private readonly IEnumerable<int> _numbers;
        //    public CustomSequence(IEnumerable<int> numbers)
        //    {
        //        _numbers = numbers;
        //    }

        //    public IDisposable Subscribe(IObserver<int> observer)
        //    {
        //        foreach (var number in _numbers)
        //        {
        //            observer.OnNext(number);
        //        }
        //        observer.OnCompleted();
        //        return Disposable.Empty;
        //    }

        //}
        #endregion

        #region 8.2
        //static IEnumerable<int> EnumerableEventSequence()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(TimeSpan.FromSeconds(0.5));
        //        yield return i;
        //    }
        //}
        #endregion
    }
}


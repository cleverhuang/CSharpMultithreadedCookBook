using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Ex09
{
    //第9章 使用异步I/O
    class Program
    {
        static void Main(string[] args)
        {
            #region 9.2 异步地使用文件
            //var t = ProcessAsynchronousIO();
            //t.GetAwaiter().GetResult();
            #endregion

            #region 9.3 编写一个异步的HTTP服务器和客户端
            //var server = new AsyncHttpServer(portNumber: 1234);
            //var t = Task.Run(() => server.Start());

            //Console.WriteLine("Listening on port 1234. Open http://localhost:1234 in your browser.");
            //Console.WriteLine("Trying to connect:");
            //Console.WriteLine();

            //GetResponseAsync("http://localhost:1234").GetAwaiter().GetResult();

            //Console.WriteLine();

            //Console.WriteLine("Press Enter to stop the server.");
            //Console.ReadLine();

            //server.Stop().GetAwaiter().GetResult();
            #endregion

            #region 9.4 异步操作数据库
            //const string dataBaseName = "CustomDatabase";
            //var t = ProcessAsynchronousIO(dataBaseName);
            //t.GetAwaiter().GetResult();
            //Console.WriteLine("Press enter to exit");
            //Console.ReadLine();
            #endregion

            #region 9.5 异步调用WCF服务
            ServiceHost host = null;

            try
            {
                host = new ServiceHost(typeof(HelloWorldService), new Uri(SERVICE_URL));

                var metadata = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (null == metadata)
                {
                    metadata = new ServiceMetadataBehavior();
                }

                metadata.HttpGetEnabled = true;
                metadata.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

                host.Description.Behaviors.Add(metadata);

                host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                var endpoint = host.AddServiceEndpoint(typeof(IHelloWorldService), new BasicHttpBinding(), SERVICE_URL);

                host.Faulted += (sender, e) => Console.WriteLine("Error!");

                host.Open();

                Console.WriteLine("Greeting service is running and listening on:");
                Console.WriteLine("{0} ({1})", endpoint.Address, endpoint.Binding.Name);

                var client = RunServiceClient();

                client.GetAwaiter().GetResult();

                Console.WriteLine("Press Enter to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in catch block:{0}", ex);
            }
            finally
            {
                if (null!=host)
                {
                    if (host.State== CommunicationState.Faulted)
                    {
                        host.Abort();
                    }
                    else
                    {
                        host.Close();
                    }
                }
            }
            #endregion
        }

        #region 9.5
        const string SERVICE_URL = "http://localhost:1234/HelloWorld";

        static async Task RunServiceClient()
        {
            var endpoint = new EndpointAddress(SERVICE_URL);
            var channel = ChannelFactory<IHelloWorldServiceClient>.CreateChannel(new BasicHttpBinding(), endpoint);

            var greeting = await channel.GreetAsync("Eugene");
            Console.WriteLine(greeting);
        }
        [ServiceContract(Namespace ="Packt",Name ="HelloWorldServiceContract")]
        public interface IHelloWorldService
        {
            [OperationContract]
            string Greet(string name);
        }

        [ServiceContract(Namespace = "Packt", Name = "HelloWorldServiceContract")]
        public interface IHelloWorldServiceClient            
        {
            [OperationContract]
            string Greet(string name);

            [OperationContract]
            Task<string> GreetAsync(string name);
        }

        public class HelloWorldService : IHelloWorldService
        {
            public string Greet(string name)
            {
                return string.Format("Greetings, {0}!", name);
            }
        }
        #endregion

        #region 9.4
        //        static async Task ProcessAsynchronousIO(string dbName)
        //        {
        //            try
        //            {
        //                const string connectionString = @"Data Source=127.0.0.1;Initial Catalog=master;Integrated Security=True";

        //                string outputFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //                string dbFileName = Path.Combine(outputFolder, string.Format(@".\{0}.mdf", dbName));

        //                string dbLogFileName = Path.Combine(outputFolder, string.Format(@".\{0}_log.ldf", dbName));
        //                string dbConnectionString = string.Format(@"Data Source=127.0.0.1;AttachDBFileName={1};Initial Catalog={0};Integrated Security=True;", dbName, dbFileName);

        //                using (var connection=new SqlConnection(connectionString))
        //                {
        //                    await connection.OpenAsync();

        //                    if (File.Exists(dbFileName))
        //                    {
        //                        Console.WriteLine("Detaching the database...");

        //                        var detachCommand = new SqlCommand("sp_detach_db", connection);

        //                        detachCommand.CommandType = System.Data.CommandType.StoredProcedure;

        //                        detachCommand.Parameters.AddWithValue("@dbname", dbName);

        //                        await detachCommand.ExecuteNonQueryAsync();

        //                        Console.WriteLine("The database was detached successfully."); 
        //                        Console.WriteLine("Deleting the database...");

        //                        if (File.Exists(dbLogFileName))
        //                        {
        //                            File.Delete(dbLogFileName);
        //                        }
        //                        File.Delete(dbFileName);

        //                        Console.WriteLine("The databse was deleted successfully.");
        //                    }

        //                    Console.WriteLine("Create the database...");
        //                    string createCommand = string.Format("CREATE DATABASE {0} ON (NAME=N'{0}',FILENAME='{1}')", dbName, dbFileName);

        //                    var cmd = new SqlCommand(createCommand, connection);

        //                    await cmd.ExecuteNonQueryAsync();
        //                    Console.WriteLine("The database was created successfully");
        //                }

        //                using (var connection=new SqlConnection(dbConnectionString))
        //                {
        //                    await connection.OpenAsync();

        //                    var cmd = new SqlCommand("Select newid()", connection);

        //                    var result = await cmd.ExecuteScalarAsync();

        //                    Console.WriteLine("New GUID from Database：{0}", result);

        //                    cmd = new SqlCommand(@"Create table customTable(ID int Identity(1,1) not nullm
        //name nvarchar(50) not null,
        //constraint pk_id primary key Clusterer (id asc) on primary on primary) ", connection);

        //                    await cmd.ExecuteNonQueryAsync();

        //                    Console.WriteLine("Table was created successfully.");

        //                    cmd = new SqlCommand(@"insert into customtable(name) values('John');
        //insert into customtable(name) values('Peter');
        //insert into customtable(name) values('James');
        //insert into customtable(name) values('Eugene');", connection);
        //                    await cmd.ExecuteNonQueryAsync();

        //                    Console.WriteLine("Inserted data successfully  ");
        //                    Console.WriteLine("Reading data from table...");

        //                    cmd = new SqlCommand(@"select * from customtable", connection);
        //                    using (SqlDataReader reader=await cmd.ExecuteReaderAsync())
        //                    {
        //                        while (await reader.ReadAsync())
        //                        {
        //                            var id = reader.GetFieldValue<int>(0);
        //                            var name = reader.GetFieldValue<string>(1);

        //                            Console.WriteLine("Table row:Id {0},Name {1}", id, name);
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                Console.WriteLine("Error:{0}",ex.Message);
        //            }

        //        }
        #endregion

        #region 9.3
        //static async Task GetResponseAsync(string url)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        HttpResponseMessage responseMessage = await client.GetAsync(url);
        //        string responseHeaders = responseMessage.Headers.ToString();
        //        string response = await responseMessage.Content.ReadAsStringAsync();


        //        Console.WriteLine("Response headers:");
        //        Console.WriteLine(responseHeaders);
        //        Console.WriteLine("Response body:");
        //        Console.WriteLine(response);
        //    }
        //}

        //class AsyncHttpServer
        //{
        //    readonly HttpListener listener;
        //    const string RESPONSE_TEMPLATE = "<html><head><title>Test</title></head><body><h2>Test page</h2><h4>Today is :{0}</h4></body></html>";

        //    public AsyncHttpServer(int portNumber)
        //    {
        //        listener = new HttpListener();
        //        listener.Prefixes.Add(string.Format("http://+:{0}/", portNumber));
        //    }

        //    public async Task Start()
        //    {
        //        listener.Start();
        //        while (true)
        //        {
        //            var ctx = await listener.GetContextAsync();
        //            Console.WriteLine("Client connected...");

        //            string response = string.Format(RESPONSE_TEMPLATE, DateTime.Now);

        //            using (var sw = new StreamWriter(ctx.Response.OutputStream))
        //            {
        //                await sw.WriteAsync(response);
        //                await sw.FlushAsync();
        //            }
        //        }
        //    }

        //    public async Task Stop()
        //    {
        //        listener.Abort();
        //    }
        //}
        #endregion

        #region 9.2
        //const int BUFFER_SIZE = 4096;
        //async static Task ProcessAsynchronousIO()
        //{
        //    using (var stream = new FileStream("test1.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE))
        //    {
        //        Console.WriteLine("1. Uses I/O Threads:{0}", stream.IsAsync);

        //        byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());

        //        var writeTask = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);

        //        await writeTask;
        //    }

        //    using (var stream = new FileStream("test2.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.None, BUFFER_SIZE, FileOptions.Asynchronous))
        //    {
        //        Console.WriteLine("2. Uses I/O Threads:{0}", stream.IsAsync);

        //        byte[] buffer = Encoding.UTF8.GetBytes(CreateFileContent());

        //        var writeTask = Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, buffer, 0, buffer.Length, null);

        //        await writeTask;
        //    }

        //    using (var stream = File.Create("test3.txt", BUFFER_SIZE, FileOptions.Asynchronous))
        //    using (var sw = new StreamWriter(stream))
        //    {
        //        Console.WriteLine("3. Uses I/O Threads:{0}", stream.IsAsync);
        //        await sw.WriteAsync(CreateFileContent());
        //    }
        //    using (var sw = new StreamWriter("text4.txt", true))
        //    {
        //        Console.WriteLine("4. Uses I/O Threads {0}", ((FileStream)sw.BaseStream).IsAsync);
        //        await sw.WriteAsync(CreateFileContent());
        //    }


        //    Console.WriteLine("Starting parsing files in parallel");

        //    Task<long>[] readTasks = new Task<long>[4];

        //    for (int i = 0; i < 4; i++)
        //    {
        //        readTasks[i] = SumFileContent(string.Format("test{0}.txt", i + 1));
        //    }

        //    long[] sums = await Task.WhenAll(readTasks);
        //    Console.WriteLine("Sum in all files：{0}", sums.Sum());


        //    Console.WriteLine("Deleting files...");
        //    Task[] deleteTasks = new Task[4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        string fileName = string.Format("test{0}.txt", i + 1);
        //        deleteTasks[i] = SimulateAsynchronousDelete(fileName);
        //    }

        //    await Task.WhenAll(deleteTasks);
        //    Console.WriteLine("Deleting complete.");
        //}

        //static string CreateFileContent()
        //{
        //    var sb = new StringBuilder();
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        sb.AppendFormat("{0}", new Random(i).Next(0, 99999));
        //        sb.AppendLine();
        //    }

        //    return sb.ToString();
        //}

        //async static Task<long> SumFileContent(string fileName)
        //{
        //    using (var stream = new FileStream(fileName, 
        //        FileMode.Open, 
        //        FileAccess.Read, 
        //        FileShare.None, 
        //        BUFFER_SIZE, 
        //        FileOptions.Asynchronous))
        //    using (var sr = new StreamReader(stream))
        //    {
        //        long sum = 0;
        //        while (sr.Peek() > -1)
        //        {
        //            string line = await sr.ReadLineAsync();
        //            sum += long.Parse(line);
        //        }

        //        return sum;
        //    }
        //}

        //static Task SimulateAsynchronousDelete(string fileName)
        //{
        //    return Task.Run(() => File.Delete(fileName));
        //}
        #endregion
    }
}

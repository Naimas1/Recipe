using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RecipeServerApp
{
    public partial class ServerForm : Form
    {
        private static List<Recipe> recipes = new List<Recipe>
        {
            new Recipe
            {
                Name = "Pancakes",
                Ingredients = new List<string> { "flour", "milk", "eggs", "sugar" },
                Instructions = "Mix ingredients and fry in a pan.",
                ImageBase64 = Convert.ToBase64String(File.ReadAllBytes("images/pancakes.jpg"))
            },
            new Recipe
            {
                Name = "Scrambled Eggs",
                Ingredients = new List<string> { "eggs", "milk", "butter", "salt" },
                Instructions = "Whisk eggs and milk, then cook in butter.",
                ImageBase64 = Convert.ToBase64String(File.ReadAllBytes("images/scrambled_eggs.jpg"))
            }
            // Add more recipes here with images
        };

        private static ConcurrentDictionary<string, ClientRequestInfo> clientRequests = new ConcurrentDictionary<string, ClientRequestInfo>();
        private static readonly int requestLimit = 10;
        private static readonly TimeSpan timeSpan = TimeSpan.FromHours(1);
        private static readonly int maxClients = 100;
        private static readonly TimeSpan inactivityLimit = TimeSpan.FromMinutes(10);

        private Thread serverThread;
        private UdpClient udpServer;
        private bool isRunning = false;
        private bool InvokeRequired;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                serverThread = new Thread("Request limit exceeded. Please try again later.", StartServer);
                serverThread.IsBackground = true;
                serverThread.Start();
                isRunning = true;
                Log("Server started.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                isRunning = false;
                udpServer.Close();
                serverThread.Join();
                Log("Server stopped.");
            }
        }

        private void StartServer(string responseString)
        {
            udpServer = new UdpClient(11000);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 11000);

            Thread cleanupThread = new Thread(CleanupInactiveClients);
            cleanupThread.IsBackground = true;
            cleanupThread.Start();

            while (isRunning)
            {
                try
                {
                    var receivedBytes = udpServer.Receive(ref remoteEP);
                    var clientKey = remoteEP.ToString();

                    if (!clientRequests.ContainsKey(clientKey))
                    {
                        if (clientRequests.Count >= maxClients)
                        {
                            udpServer.Send((byte[]?)Encoding.UTF8.GetBytes((string?)"Server is full. Try again later."), Encoding.UTF8.GetBytes((string?)"Server is full. Try again later.").Length, remoteEP);
                            Log($"Connection attempt from {clientKey} failed: server is full.");
                            continue;
                        }

                        clientRequests[clientKey] = new ClientRequestInfo
                        {
                            RequestCount = 0,
                            LastRequestTime = DateTime.Now,
                            LastActiveTime = DateTime.Now
                        };
                        Log($"Client connected: {clientKey}");
                    }

                    if (clientInfo.RequestCount >= requestLimit && DateTime.Now - clientInfo.LastRequestTime < timeSpan)
                    {
                        var responseBytes = Encoding.UTF8.GetBytes(responseString);
                        udpServer.Send(responseBytes, responseBytes.Length, remoteEP);
                        Log($"Request limit exceeded for client {clientKey}.");
                        continue;
                    }

                    if (DateTime.Now - clientInfo.LastRequestTime >= timeSpan)
                    {
                        clientInfo.RequestCount = 0;
                        clientInfo.LastRequestTime = DateTime.Now;
                    }

                    clientInfo.RequestCount++;
                    clientInfo.LastActiveTime = DateTime.Now;

                    var receivedString = Encoding.UTF8.GetString(receivedBytes);
                    var ingredients = JsonSerializer.Deserialize<List<string>>(receivedString);

                    var matchingRecipes = recipes.Where(r => r.Ingredients.Intersect(ingredients).Any()).ToList();
                    var responseString = JsonSerializer.Serialize(matchingRecipes);

                    var responseBytes = Encoding.UTF8.GetBytes((string?)JsonSerializer.Serialize(matchingRecipes));
                    udpServer.Send(responseBytes, responseBytes.Length, remoteEP);

                    Log($"Processed request from {clientKey}: {string.Join(", ", ingredients)}");
                }
                catch (SocketException ex)
                {
                    Log($"Socket exception: {ex.Message}");
                }
            }
        }

        private void CleanupInactiveClients()
        {
            while (isRunning)
            {
                foreach (var clientKey in clientRequests.Keys)
                {
                    if (DateTime.Now - clientRequests[clientKey].LastActiveTime >= inactivityLimit)
                    {
                        clientRequests.TryRemove(clientKey, out _);
                        Log($"Client disconnected due to inactivity: {clientKey}");
                    }
                }

                Thread.Sleep(60000); // Check every 60 seconds
            }
        }

        private void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
                Logger.Log(message);
            }
        }

        private void Invoke(Action<string> action, string message)
        {
            throw new NotImplementedException();
        }
    }

    internal class Logger
    {
        internal static void Log(string message)
        {
            throw new NotImplementedException();
        }
    }

    internal class txtLog
    {
        internal static void AppendText(string v)
        {
            throw new NotImplementedException();
        }
    }

    internal class clientInfo
    {
        internal static int RequestCount;
        internal static DateTime LastRequestTime;
        internal static DateTime LastActiveTime;
    }

    public class Form
    {
    }

    public class ClientRequestInfo
    {
        public int RequestCount { get; set; }
        public DateTime LastRequestTime { get; set; }
        public DateTime LastActiveTime { get; set; }
    }
}

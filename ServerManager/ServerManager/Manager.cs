using Newtonsoft.Json;
using System.Diagnostics;

namespace ServerManager
{
    internal class Manager
    {
        public static List<object[]> Servers = new();
        public string Path = "PandemicServer.exe";
        public bool ActiveConfigs = false;
        public bool ForceShutdown = false;


        public void Start()
        {
            try
            {
                Console.Clear();
                Console.Title = $"SCP-Server Manager - Starting Servers...";

                Config[] configs = GetConfigs();
                Startup(configs);

                while (true)
                {
                    if (ActiveConfigs && configs.Count() != GetConfigs().Count())
                    {
                        Console.Title = $"SCP-Server Manager - Checking Configs...";

                        if (configs.Count() < GetConfigs().Count())
                            configs = AddServer(GetConfigs(), configs);
                        if (configs.Count() > GetConfigs().Count())
                            configs = RemoveServer(GetConfigs(), configs);
                    }

                    Console.Title = $"SCP-Server Manager - {Servers.Count} of {configs.Count()} servers active";

                    foreach (var server in Servers)
                    {
                        var process = (Process)server[0];
                        if (process.HasExited)
                        {
                            var config = (Config)server[1];
                            Servers.Remove(server);
                            var result = CreateServer(config);
                            Servers.Add(result);
                            break;
                        }
                    }
                    Thread.Sleep(3000);
                }
            } catch (Exception ex) 
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Trace: {ex.StackTrace}");
                Console.WriteLine();
                Console.ResetColor();
                Console.WriteLine("Press 'Enter' to close this window...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void Startup(Config[] configs)
        {
            foreach (var config in configs)
            {
                Servers.Add(CreateServer(config));
                Console.Title = $"SCP-Server Manager - {Servers.Count} of {configs.Count()} servers active";
            }
        }

        private object[] CreateServer(Config config)
        {
            if ((bool)config.RandomMap!)
            {
                Random rnd = new Random();
                int index = 0;
                do
                {
                    index = rnd.Next(1, 4);
                } while (config.Map == Enum.GetName(typeof(Program.Maps), index));
                config.Map = Enum.GetName(typeof(Program.Maps), index);
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[{DateTime.Now}] Starting Server: {config.ServerName} Map: {config.Map}");
            Console.ResetColor();
            string args = $"{config.Map} -ConfigFileName={config.ConfigFile} -log -port={config.Port} -queryport={config.QueryPort} -force_steamclient_link";
            return new object[] { Process.Start(Path, args), config };
        }

        private Config[] AddServer(Config[] newConfigs, Config[] prevConfigs)
        {
            List<Config> _configs = prevConfigs.ToList();

            foreach (var config in newConfigs)
            {
                if (prevConfigs.Count() != 0)
                {
                    if (!prevConfigs.Contains(config))
                    {
                        var result = CreateServer(config);
                        Servers.Add(result);
                        _configs.Add(config);
                    }
                }
                else
                {
                    var result = CreateServer(config);
                    Servers.Add(result);
                    _configs.Add(config);
                }
            }

            return _configs.ToArray();
        }

        private Config[] RemoveServer(Config[] newConfigs, Config[] prevConfigs)
        {
            List<Config> _configs = prevConfigs.ToList();

            for (int i = 0; i < Servers.Count(); i++)
            {
                if (!newConfigs.Contains(Servers[i][1]))
                {
                    Config config = (Config)Servers[i][1];
                    Servers.Remove(Servers[i]);
                    _configs.Remove(config);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"[{DateTime.Now}] Server {config.ServerName} was removed.");
                    if (ForceShutdown)
                    {
                        var process = (Process)Servers[i][0];
                        process.Kill();
                        Console.WriteLine($"[{DateTime.Now}] Server {config.ServerName} got terminated.");
                    }
                    Console.ResetColor();
                }
            }
            return _configs.ToArray();
        }

        private Config[] GetConfigs()
        {
            var configs = new List<Config>();
            var files = Directory.GetFiles(Program.ConfigFolder);
            foreach (var file in files)
            {
                try
                {
                    configs.Add(JsonConvert.DeserializeObject<Config>(File.ReadAllText(file))!);

                } catch (Exception) { Error($"Could not load config: {file}"); }
            }
            return configs.ToArray();
        }

        private void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}

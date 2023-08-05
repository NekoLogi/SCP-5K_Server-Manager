using Newtonsoft.Json;

namespace ServerManager
{
    public class Config
    {
        public string? ServerName = null;
        public string? Map = null;
        public int? MaxPlayers = null;
        public int? Port = null;
        public int? QueryPort = null;
        public string? ConfigFile = null;
        public bool? RandomMap = null;


        public static void Create()
        {
            char key = 'y';
            while (key == 'y')
            {
                Console.Title = "SCP-Server Manager - Config Creator";

                Config config = new Config();
                // Config Name
                Console.Clear();
                Console.Write("Name for this config: ");
                var configName = Console.ReadLine()!;
                Console.Clear();

                Summary(configName, config);

                // Server Name
                Console.Write("\nServer name: ");
                config.ServerName = Console.ReadLine();
                Console.Clear();

                Summary(configName, config);

                // Map
                Console.WriteLine("1 | Preset map");
                Console.WriteLine("2 | Custom name map");
                char option = '0';
                while (true)
                {
                    option = Console.ReadKey().KeyChar;
                    if (option == '1')
                    {
                        Console.Clear();
                        int i = 1;
                        foreach (var map in Enum.GetValues(typeof(Program.Maps)))
                        {
                            Console.WriteLine($"{i} | {map}");
                            i++;
                        }

                        char option1 = '1';
                        while (option1 != '0')
                        {
                            option1 = Console.ReadKey().KeyChar!;
                            switch (option1)
                            {
                                case '1':
                                    config.Map = Program.Maps.Area12_PersistentLevel.ToString();
                                    option1 = '0';
                                    break;
                                case '2':
                                    config.Map = Program.Maps.M_WaveSurvival.ToString();
                                    option1 = '0';
                                    break;
                                case '3':
                                    config.Map = Program.Maps.M_Sewer_CanalPVP.ToString();
                                    option1 = '0';
                                    break;
                                case '4':
                                    config.Map = Program.Maps.M_TestingMap.ToString();
                                    option1 = '0';
                                    break;
                                case '5':
                                    config.Map = Program.Maps.M_AITesting.ToString();
                                    option1 = '0';
                                    break;
                            }
                        }
                        break;
                    }
                    else if (option == '2')
                    {
                        Console.Write("\nMAP name: ");
                        config.Map = Console.ReadLine()!.Replace(' ', '_');
                        break;
                    }
                    Console.Clear();
                }
                Console.Clear();

                Summary(configName, config);

                // Max. Players
                Console.Write("\nMax. players: ");
                config.MaxPlayers = int.Parse(Console.ReadLine()!);

                Summary(configName, config);

                // Port
                Console.Write("\nPort: ");
                config.Port = int.Parse(Console.ReadLine()!);

                Summary(configName, config);

                // Query-Port
                Console.Write("\nQuery-port: ");
                config.QueryPort = int.Parse(Console.ReadLine()!);

                Summary(configName, config);

                // Random Map
                Console.WriteLine("\nShould the map be randomly picked?: ");
                Console.WriteLine("1 | Yes");
                Console.WriteLine("2 | No");
                char option2 = '1';
                while (option2 != '0')
                {
                    option2 = Console.ReadKey().KeyChar;
                    switch (option2)
                    {
                        case '1':
                            config.RandomMap = true;
                            option2 = '0';
                            break;
                        case '2':
                            config.RandomMap = false;
                            option2 = '0';
                            break;
                    }
                }

                Summary(configName, config);

                // Pandemic Config-File
                Console.Write("\nConfig-file name ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("(The Config-file of the PandemicServer.exe)");
                Console.ResetColor();
                Console.Write(":");

                config.ConfigFile = Console.ReadLine()!.Replace(' ', '_');

                Console.Clear();

                var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText($"{Program.ConfigFolder}/{configName}.json", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Config saved!");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Do you want to create another config? (y\\n): ");
                key = Console.ReadKey().KeyChar;
                Console.Clear();
            }
        }

        private static void Summary(string name, Config config)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{name}.json");
            Console.WriteLine($"Name: {config.ServerName}");
            Console.WriteLine($"Map: {config.Map}");
            Console.WriteLine($"Max. Players: {config.MaxPlayers}");
            Console.WriteLine($"Port: {config.Port}");
            Console.WriteLine($"Query-Port: {config.QueryPort}");
            Console.WriteLine($"Pandemic Config-File: {config.ConfigFile}");
            Console.WriteLine($"Random Map: {config.RandomMap}");
            Console.ResetColor();
            //
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

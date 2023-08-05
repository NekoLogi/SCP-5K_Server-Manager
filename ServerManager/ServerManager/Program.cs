
namespace ServerManager
{
    internal class Program
    {
        public static readonly string ConfigFolder = "configs";
        private static string Path = "";
        private static bool ActiveConfigs = false;
        private static bool ForceShutdown = false;

        public enum Maps
        {
            Area12_PersistentLevel = 1,
            M_WaveSurvival,
            M_HuntSweden,
            M_Sewer_CanalPVP,
            M_TestingMap,
            M_AITesting
        }

        static void Main(string[] args)
        {
            Console.Title = "SCP-Server Manager";

            if (!Directory.Exists(ConfigFolder))
            {
                Directory.CreateDirectory(ConfigFolder);
                return;
            }

            if (args.Length != 0)
            {
                Console.WriteLine("Arguments:");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (string arg in args)
                {
                    if (arg.ToLower().StartsWith("-path="))
                    {
                        Path = arg.Replace("-path=", "");
                        Console.WriteLine($"Game Path: {Path}");
                    }
                    else if (arg.ToLower().StartsWith("-activeconfigs="))
                    {
                        ActiveConfigs = bool.Parse(arg.Replace("-activeconfigs=", "").ToLower());
                        Console.WriteLine($"Active Configs: {ActiveConfigs}");
                    }
                    else if (arg.ToLower().StartsWith("-forceshutdown="))
                    {
                        ForceShutdown = bool.Parse(arg.Replace("-forceshutdown=", "").ToLower());
                        Console.WriteLine($"Force Shutdown: {ForceShutdown}");
                    }
                }
                Console.ResetColor();
                Console.WriteLine();
            }

            while (true)
            {
                Console.WriteLine("1 | Start manager");
                Console.WriteLine("2 | Create config");
                Console.WriteLine("3 | Startup parameters");
                Options(Console.ReadKey().KeyChar!);
            }
        }

        private static void Options(char key)
        {
            switch (key)
            {
                case '1':
                    var manager = new Manager();

                    if (Path.Length > 1)
                    {
                        manager.Path = Path;
                        manager.ActiveConfigs = ActiveConfigs;
                        manager.ForceShutdown = ForceShutdown;
                    }
                    manager.Start();
                    break;
                case '2':
                    Config.Create();
                    break;
                case '3':
                    Console.Clear();
                    // -path=
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-path=");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("{Full path of PandemicServer.exe} ");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write("To use outside of Pandemic Server folder or if .exe can't be found.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Default: Same path/folder as ServerManager.exe]");

                    Console.WriteLine();

                    // -activeconfigs=
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-activeconfigs=");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("{Boolean: true or false} ");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write("If set to 'true', by removing config-file's, the manager will not restart the server corresponding to the config-file.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Default: False.]");

                    Console.WriteLine();

                    // -forceshutdown=
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-forceshutdown=");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("{Boolean: true or false} ");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.Write("[This parameter is connected to ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-activeconfigs=");
                    Console.ResetColor();
                    Console.Write("] If set to 'true', by removing config-file's the manager will shutdown the server corresponding to the config-file.");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Default: False.]");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Press 'Enter' to go back...");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
    }
}
# SCP:5K Server-Manager

# Installation
There are two ways you can install the manager.

1. You can put all the files from the manager, into the same folder of the PandemicServer.exe. (Default)
2. You can also put all the files of the manager, into a random folder, in your system and run it via Batch-File or CMD-Terminal like below.

- Start ServerManager.exe -path={Path of the PandemicServer.exe [.exe file included in the path]}


# Parameters
There are currently three parameters you can use.

1. -path={Path of the PandemicServer.exe [.exe file included in the path]} - With this you can put your manager everywhere you want.
   [Default: Same path/folder as ServerManager.exe]
   
2. -activeconfigs={Boolean: true or false} - If "true" by adding a config, the manager will create a server corresponding to the Config-File,
   also by removing a config, the manager won't restart the server after Serf-Termination.
   (Manager can Force-Terminate by using -forceshutdown=)
   [Default: False.]

3. -forceshutdown={Boolean: true or false} - If set to 'true', by removing configs the manager will Force-Terminate the server corresponding to the Config-File.
   (This parameter is connected to parameter: "-activeconfigs=")
   [Default: False.]


# How it works
By starting the manager the first time it will create a directory/folder named "configs", there will all configs be stored.
The manager will close immediatly after, then start the manager again for the actual program.

To start a server you will need to create atleast one config, go to "Create config" and follow the procedure.
After you created your config, go to "Start manager", now all servers in the "configs" directory/folder will start up, the title of the program tells you how many servers are running and how much should be running.
The Terminal tells you what servers got created at which time and what config it's using.

-Have fun
~NekoLogi

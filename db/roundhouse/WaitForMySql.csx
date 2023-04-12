#r "nuget: MySqlConnector, 0.60.2"

using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;


string connectionString = Args[Args.Count - 1];
connectionString = Regex.Replace(connectionString, "\\s*;\\s*", ";");
Console.WriteLine("Connection String=[" + connectionString + "]");

int retries = 20;
int interval = 500;
MySqlConnection connection = null;
for (int i = 0; i < retries; i++) {
    try {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        Console.WriteLine("Database connected.");
        connection.Close();

        var arguments = "";
        for (int j = 1; j < Args.Count; j++) {
            if(Args[j-1] == "-cs")
            {
                arguments += connectionString;
            } else { 
                arguments += Args[j] + " ";
            }
        }

        Process process = new Process();
        // Configure the process using the StartInfo properties.
        process.StartInfo.FileName = Args[0];
        process.StartInfo.Arguments = arguments;
        Console.WriteLine($"Running Roundhouse command: {Args[0]} with arguments: {arguments}");
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        process.WaitForExit();// Waits here for the process to exit.

        Environment.Exit(0);
    }
    catch (MySqlException ex) {
        Console.WriteLine($"Cannot establish connection to the DB. {ex.Message}");
    }
    Thread.Sleep(interval);
}
Console.WriteLine("Failed to connect to the DB, QUIT!");
Environment.Exit(1);
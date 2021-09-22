using ChallengerDiscordBot.Instances;
using MySqlConnector;
using SupportBot;
using SupportBot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengerDiscordBot.Utils
{
    class DatabaseMethods
    {

        private static MySqlConnection connection;

        public static void SetupDatabase()
        {
            connection = SecretClass.GetConnection();
            try
            {
                connection.Open();
                Console.WriteLine("MySQL connection opened successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("MySQL connection opened unsuccessfully\nError: " + ex.Message);

            }
        }

        public static void RefreshDatabaseConnection()
        {
            connection.Close();
            connection = SecretClass.GetConnection();
            try
            {
                connection.Open();
                Console.WriteLine("MySQL connection opened successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("MySQL connection opened unsuccessfully\nError: " + ex.Message);
            }
        }

        public static void LoadData()
        {
            var command = new MySqlCommand($"SELECT * FROM Challenger;", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {

                nChallenger ch = new nChallenger(reader.GetUInt64(0), reader.GetString(1), reader.GetDateTime(2), reader.GetBoolean(3), reader.GetDouble(4));
                ProgramVariables.Guild.CHALLANGERS.Add(ch);

            }
            reader.Close();
            command = new MySqlCommand($"SELECT * FROM Task WHERE DateEnd>NOW();", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {

                nTask task = new nTask(reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetDateTime(5), reader.GetDateTime(6), Convert.ToUInt64(reader.GetValue(7)), Convert.ToUInt64(reader.GetValue(8)));
                task.ID = reader.GetInt32(0);
                ProgramVariables.Guild.TASKS.Add(task);

            }
            reader.Close();
            command = new MySqlCommand($"SELECT DataValue FROM ProgramVariable WHERE DataKey='INFO_CHANNEL';", connection);
            reader = command.ExecuteReader();
            reader.Read();
            ProgramVariables.Guild.INFO_CHANNEL = Convert.ToUInt64(reader.GetValue(0));
            reader.Close();
            command = new MySqlCommand($"SELECT DataValue FROM ProgramVariable WHERE DataKey='FAME_CHANNEL';", connection);
            reader = command.ExecuteReader();
            reader.Read();
            ProgramVariables.Guild.FAME_CHANNEL = Convert.ToUInt64(reader.GetValue(0));
            reader.Close();
        }

        public static int AddTask(nTask task)
        {
            var command = new MySqlCommand($"SELECT Task.ID FROM Task WHERE Task.Name='{task.Name}';", connection);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                return 0;
            }
            reader.Close();
            string query = $"INSERT INTO Task(Name, MaxPoints, Creator, DateCreate, DateStart, DateEnd, TextMessageID, TextChannelID) VALUES('{task.Name}', {task.MaxPoints}, '{task.Creator}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{task.DateStart.ToString("yyyy-MM-dd HH:mm:ss")}', '{task.DateEnd.ToString("yyyy-MM-dd HH:mm:ss")}', {task.TextMessageID}, {task.TextChannelID});";
            command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            command = new MySqlCommand($"SELECT Task.ID FROM Task WHERE Task.Name='{task.Name}';", connection);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                int i = reader.GetInt32(0);
                reader.Close();
                return i;
            }
            return 0;
        }

        public static bool AddChallanger(ulong userID, string username)
        {
            var command = new MySqlCommand($"SELECT Challenger.UserID FROM Challenger WHERE Challenger.UserID={userID};", connection);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                return false;
            }
            reader.Close();
            string query = $"INSERT INTO Challenger(UserID, Username, DateCreate) VALUES({userID}, '{username}', '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');";
            command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
            return true;
        }

        public static void UpdateChallangerActivity(ulong userID, bool status)
        {
            var command = new MySqlCommand($"SELECT Challenger.UserID FROM Challenger WHERE Challenger.UserID={userID};", connection);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                return;
            }
            reader.Close();
            string query = $"UPDATE Challenger SET Challenger.IsActive={status} WHERE Challenger.UserID={userID};";
            command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public static void UpdateInfoChannel(string value)
        {
            var command = new MySqlCommand($"UPDATE ProgramVariable SET ProgramVariable.DataValue='{value}' WHERE ProgramVariable.DataKey='INFO_CHANNEL';", connection);
            command.ExecuteNonQuery();
        }

        public static void UpdateFameChannel(string value)
        {
            var command = new MySqlCommand($"UPDATE ProgramVariable SET ProgramVariable.DataValue='{value}' WHERE ProgramVariable.DataKey='FAME_CHANNEL';", connection);
            command.ExecuteNonQuery();
        }

    }
}

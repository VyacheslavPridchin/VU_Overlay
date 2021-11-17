using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Overlay_Client_DSTU
{
    public class SQL //сделать асинхронными методы
    {
        ~SQL() => CloseDB();

        private static MySqlConnection database_connection;

        public static class User
        {
            public static string id;
            public static string login;
            public static string nickname;
            public static string type;
        }

        public static class Lesson
        {
            public static string id;
            public static string name;
            public static string UniqueKey;
            public static string password;
        }

        public static bool ConnectDB()
        {
            
            try
            {

                var builder = new MySqlConnectionStringBuilder
                {
                    Server = "serevr",
                    Database = "database_name",
                    UserID = "user",
                    Password = "password",
                    CharacterSet = "utf8",
                    SslMode = MySqlSslMode.Required,
                };

                database_connection = new MySqlConnection(builder.ConnectionString);
                database_connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }

        public static bool Authorize(string login, string password)
        {
            if (SQLCounter($"SELECT id FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'") == 0 )
            {
                if (SQLCounter($"SELECT id FROM Accounts WHERE email LIKE '{login}' AND password LIKE '{password}'") == 0)
                {
                    return false;
                } else
                {
                    User.login = Read($"SELECT login FROM Accounts WHERE email LIKE '{login}' AND password LIKE '{password}'");
                    User.id = Read($"SELECT id FROM Accounts WHERE email LIKE '{login}' AND password LIKE '{password}'");
                    User.nickname = Read($"SELECT nickname FROM Accounts WHERE email LIKE '{login}' AND password LIKE '{password}'"); ;
                    User.type = Read($"SELECT type FROM Accounts WHERE email LIKE '{login}' AND password LIKE '{password}'");
                    ChangeMac(GetMAC());
                    return true;
                }
            }
            else
            {
                User.login = login;
                User.id = Read($"SELECT id FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'");
                User.nickname = Read($"SELECT nickname FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'");
                User.type = Read($"SELECT type FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'");
                ChangeMac(GetMAC());
                return true;
            }
        }

        public static bool Authorize(string mac)
        {
            if (SQLCounter($"SELECT id FROM Accounts WHERE currentDevice LIKE '{mac}'") == 0)
            {
                return false;
            }
            else
            {
                User.login = Read($"SELECT login FROM Accounts WHERE currentDevice LIKE '{mac}'");
                User.id = Read($"SELECT id FROM Accounts WHERE currentDevice LIKE '{mac}'");
                User.nickname = Read($"SELECT nickname FROM Accounts WHERE currentDevice LIKE '{mac}'");
                User.type = Read($"SELECT type FROM Accounts WHERE currentDevice LIKE '{mac}'");
                ChangeMac(GetMAC());
                return true;
            }
        }

        public static void CloseDB()
        {
            if (database_connection != null)
                database_connection.Close();
        }

        public static List<List<string>> FullRead(string sql)
        {
            List<List<string>> table_grid = new List<List<string>>();
            MySqlCommand command = new MySqlCommand(sql, database_connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                List<string> row = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row.Add(reader[i].ToString());

                table_grid.Add(row);
            }

            reader.Close();
            return table_grid;
        }

        public static int SQLCounter(string sql)
        {
            int counter = 0;
            MySqlCommand command = new MySqlCommand(sql, database_connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
                counter++;
            reader.Close();

            return counter;
        }

        public static void SQLExecute(string sql) => new MySqlCommand(sql, database_connection).ExecuteNonQuery();


        public static string RegistrationCode = "";
        public static string EMail = "";

        public static byte CreateNewAccount(string login, string password, string nickname, string type)
        {
            if (SQLCounter($"SELECT id FROM Accounts WHERE login LIKE '{login}'") == 0)
            {
                if (ValidateEMAIL(nickname))
                {

                    SQLExecute($"INSERT INTO Accounts (id, login, password, nickname, type, remoteIP, email) VALUES (NULL, '{login}', '{password}', '{nickname}', '{type}', 'null', '{SQL.EMail}')");
                    User.login = login;
                    User.nickname = nickname;
                    User.id = Read($"SELECT id FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'");
                    User.type = Read($"SELECT type FROM Accounts WHERE login LIKE '{login}' AND password LIKE '{password}'");

                    //MessageBox.Show(GlobalID);
                    ChangeMac(GetMAC());
                    return 0;
                } else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
        }


        public static string TempNickname = "";
        private static bool ValidateEMAIL(string nickname)
        {
            TempNickname = nickname;

            EnterCode entrcd = new EnterCode();
            entrcd.ShowDialog();

            if(RegistrationCode == "Correct")
            {
                return true;
            } else
            {
                return false;
            }
        }

        

        public static (string, string) CreateNewLesson(string name, string decription, string password, string type)
        {
            Random rnd = new Random();
            var key = rnd.Next(100000, 999999);
            if (SQLCounter($"SELECT id FROM Lessons WHERE UniqueKey = '{key}'") == 0)
            {
                SQLExecute($"INSERT INTO Lessons (id, idOwner, name, description, UniqueKey, password, type, Server_IP) VALUES (NULL, {User.id}, '{name}', '{decription}', '{key}', '{password}', '{type}', '{GetBestServer()}')");

                    string temp_id = Read($"SELECT id FROM Lessons WHERE UniqueKey = '{key}'");

                    //SQLExecute($"UPDATE Lessons SET UniqueKey = NULL WHERE id = {temp_id}");
                    return (temp_id, key.ToString());
            }
            else
            {
                return CreateNewLesson(name, decription, password, type); //рекурсия для UniqueKey
            }
        }

        public static bool ChangeLogin(string login)
        {
            if (SQLCounter($"SELECT id FROM Accounts WHERE login LIKE '{login}'") == 0)
            {
                //MessageBox.Show($"UPDATE accounts SET login = '{login}' WHERE accounts.id = {GlobalID}");

                SQLExecute($"UPDATE Accounts SET login = '{login}' WHERE Accounts.id = {User.id}");
                SQL.User.login = login;
                return true;
            }
            else return false;
        }

        public static void ChangePassword(string password) => SQLExecute($"UPDATE Accounts SET password = '{password}' WHERE Accounts.id = {User.id}");

        public static void ChangeIDLesson(string idLesson) => SQLExecute($"UPDATE Accounts SET idLesson = {idLesson} WHERE Accounts.id = {User.id}");

        public static void DeleteAccount() => SQLExecute($"DELETE FROM Accounts WHERE Accounts.id = {User.id}");

        public static void ChangeNickname(string nickname)
        {
            //MessageBox.Show($"UPDATE accounts SET login = '{login}' WHERE accounts.id = {GlobalID}");
            SQLExecute($"UPDATE Accounts SET nickname = '{nickname}' WHERE Accounts.id = {User.id}");
            SQL.User.nickname = nickname;
        }

        public static string GetMAC()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        public static void ChangeMac(string mac)
        {
            SQLExecute($"UPDATE Accounts SET currentDevice = NULL WHERE Accounts.currentDevice = '{mac}'"); //Выход из аккаунтов, если есть такой мак
            SQLExecute($"UPDATE Accounts SET currentDevice = '{mac}' WHERE Accounts.id = {User.id}"); //Сохранение мака
        }

        public static void DeleteMac(string mac)
        {
            SQLExecute($"UPDATE Accounts SET currentDevice = NULL WHERE Accounts.currentDevice = '{mac}'"); //Выход из аккаунтов, если есть такой мак
        }

        public static string Read(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, database_connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string temp = reader[0].ToString();
            reader.Close();
            return temp;
        }

        //public static List<IPEndPoint> GetIps()
        //{
        //    List<IPEndPoint> IPs = new List<IPEndPoint>();
        //    var temp = FullRead("SELECT remoteIP FROM Accounts WHERE idLesson = " + SQL.Lesson.id);
        //    foreach (List<string> vs in temp)
        //    {
        //        if (vs[0] != "null")
        //            IPs.Add(CreateIPEndPoint(vs[0]));
        //    }
        //    //Console.WriteLine(IPs.Count);
        //    return IPs;
        //}

        public static IPEndPoint CreateIPEndPoint(string endPoint)
        {
            string[] ep = endPoint.Split(':');
            if (ep.Length < 2) throw new FormatException("Invalid endpoint format");
            IPAddress ip;
            if (ep.Length > 2)
            {
                if (!IPAddress.TryParse(string.Join(":", ep, 0, ep.Length - 1), out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            else
            {
                if (!IPAddress.TryParse(ep[0], out ip))
                {
                    throw new FormatException("Invalid ip-adress");
                }
            }
            int port;
            if (!int.TryParse(ep[ep.Length - 1], NumberStyles.None, NumberFormatInfo.CurrentInfo, out port))
            {
                throw new FormatException("Invalid port");
            }
            return new IPEndPoint(ip, port);
        }

        public static bool CheckIP(string IP)
        {
            if (Read("SELECT remoteIP FROM Accounts WHERE id = " + User.id) == IP)
                return true;
            else
                return false;
        }

        public static void ChangeIP(string IP) => SQLExecute($"UPDATE Accounts SET remoteIP = '{IP}' WHERE id = {User.id}");

        //public static List<string> GetServerIPs()
        //{
        //    var data = SQL.FullRead("SELECT ServerIP FROM Link_ClientSystem WHERE RoomID = " + Lesson.id);
        //    var temp = new List<string>();
        //    for (int i = 0; i < data.Count; i++)
        //        temp.Add(data[i][0]);
        //    return temp;
        //}

        public static IPAddress GetBestServer()
        {
            return IPAddress.Parse(SQL.Read("SELECT IP FROM Servers WHERE CountOfLesson = (SELECT MIN(CountOfLesson) FROM Servers)"));
        }


    }
}

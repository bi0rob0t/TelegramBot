using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Telegram.Bot;
using TelegramBot.Models.Commands;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        

        public static async Task<TelegramBotClient> Get()
        {
            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            commandsList.Add(new StartCommand());
            commandsList.Add(new HelpCommand());
            commandsList.Add(new InBeginCommand());
            commandsList.Add(new CarCommand());
            commandsList.Add(new PaymetCommand());



            client = new TelegramBotClient(AppSettings.Key);
            client.OnMessage += MessageUpdate;
            client.StartReceiving(new UpdateType[] {UpdateType.Message });
            //var hook = string.Format(AppSettings.Url, "api/message/update");
            //await client.SetWebhookAsync(hook);

            return client;
        }

        public static async void MessageUpdate(object sender,MessageEventArgs e) {

            var commands = Bot.Commands;
            var message = e.Message;
            var client = await Bot.Get();
            string connectionString = @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=dataabase;Integrated Security=True";

            #region AddItem

            string[] words = e.Message.Text.Split(new char[] { ' ' });
            if (words[0]=="/add") {
                string item = "SELECT * FROM Products WHERE product_name='"+words[1]+"';";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Создаем объект DataAdapter
                    SqlDataAdapter adapter = new SqlDataAdapter(item, connection);
                    // Создаем объект Dataset
                    DataSet ds = new DataSet();
                    // Заполняем Dataset
                    adapter.Fill(ds);
                    var add = new AddItemCommand(message, client, (int)ds.Tables[0].Rows[0][0]);
                }
            }

            #endregion

            #region SelectCategory

            string sql = "SELECT * FROM Categories";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][1].ToString()==e.Message.Text) {
                        var cat = new CategoryItemCommand();
                        cat.ExecuteCategoryItem(message, client,(int)ds.Tables[0].Rows[i][0]);
                    }
                }

            }

            #endregion

            #region EditCommand

            message.Text=message.Text.Replace(" ","");
            if (message.Text!="/start" || message.Text != "/pay") {
                message.Text = "/" + message.Text;
            }
            message.Text += "@railgun-2000";

            #endregion

            #region SelectCommand

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    command.Execute(message, client);
                    break;
                }
            }

            #endregion
        }
    }
}
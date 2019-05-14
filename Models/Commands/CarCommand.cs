using Telegram.Bot;
using Telegram.Bot.Types;
using System.Data;
using System.Data.SqlClient;


namespace TelegramBot.Models.Commands
{
    public class CarCommand:Command
    {
        public override string Name => "Машина";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var rkm = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup { };

            string connectionString = @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=RolikBot;Integrated Security=True";
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

                var reply = new Telegram.Bot.Types.ReplyMarkups.KeyboardButton[ds.Tables[0].Rows.Count];
                for (int i=0;i< ds.Tables[0].Rows.Count; i++) {
                    reply[i] = ds.Tables[0].Rows[i][1].ToString();
                }

                rkm = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
                {
                    Keyboard = new[] { reply}
                };
            }


           



            client.SendTextMessageAsync(chatId, "Выберите категорию", replyToMessageId: messageId, replyMarkup: rkm);
        }
    }
}
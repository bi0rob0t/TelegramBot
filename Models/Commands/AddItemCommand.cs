using System.Data.SqlClient;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class AddItemCommand
    {
        public AddItemCommand(Message message, TelegramBotClient client, int ItemId)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string text = "Товар добавлен в корзину для оплаты напишите /pay";

            string connectionString = @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=RolikBot;Integrated Security=True";
            string sql = "INSERT INTO Orders (user_chat_id, buyable, valid) VALUES('" + message.Chat.Id+"','"+ItemId+"','true'); ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }






            client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }
    }
}
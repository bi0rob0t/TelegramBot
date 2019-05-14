using System.Data.SqlClient;
using System.Data;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class CategoryItemCommand
    {

        public void ExecuteCategoryItem(Message message, TelegramBotClient client,int id)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string text="Список товаров, для покупке /add [название предмета]. \n";

            string connectionString = @"Data Source=localhost\MSSQLSERVER01;Initial Catalog=RolikBot;Integrated Security=True";
            string sql = "SELECT * FROM Products WHERE categories_id=" + id+";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                // Создаем объект Dataset
                DataSet ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds);

                for (int i=0;i<ds.Tables[0].Rows.Count;i++) {
                    text += ""+ds.Tables[0].Rows[i][1].ToString()+" - "+ds.Tables[0].Rows[i][3].ToString()+" рублей\n";
                }
                
            }



            client.SendTextMessageAsync(chatId, text, replyToMessageId: messageId);
        }

    }
}
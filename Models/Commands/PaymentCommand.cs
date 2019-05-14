using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net.Mail;
using System.Net;

namespace TelegramBot.Models.Commands
{
    public class PaymetCommand : Command
    {
        public override string Name => "pay";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            //var payment = new LabeledPrice {
            //    Label = "Товар",
            //    //Amount = ;
            //};

            var rkm = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                    new Telegram.Bot.Types.ReplyMarkups.KeyboardButton[]{
                            "В начало",
                    }
                }
            };


            //TODO: Command logic -_-
           
            MailAddress from = new MailAddress("krolik.abc.test@gmail.com", "Tom");
            MailAddress to = new MailAddress("krolik.abc@gmail.com");

            MailMessage m = new MailMessage(from, to);
            m.Subject = "Ну типо тут заказ";
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("krolik.abc.test@gmail.com", "Den30623322");
            smtp.EnableSsl = true;
            smtp.Send(m);
            

            //client.SendInvoiceAsync(chatId, "Спасибо за оплату", replyToMessageId: messageId, replyMarkup: rkm);
            client.SendTextMessageAsync(chatId, "Спасибо за оплату", replyToMessageId: messageId, replyMarkup: rkm);
        }
    }
}
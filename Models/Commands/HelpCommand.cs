using Telegram.Bot;
using Telegram.Bot.Types;


namespace TelegramBot.Models.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "Помощь";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var rkm = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                    new Telegram.Bot.Types.ReplyMarkups.KeyboardButton[]{
                            "В начало",
                    }
                }
            };



            client.SendTextMessageAsync(chatId, "Telegram bot railgun-2000", replyToMessageId: messageId, replyMarkup: rkm);
        }
    }
}
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "start";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var rkm = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup
            {
                Keyboard = new[] {
                    new Telegram.Bot.Types.ReplyMarkups.KeyboardButton[]{
                            "Машина",
                            "Помощь"
                    }
                }
            };
            //TODO: Command logic -_-



            client.SendTextMessageAsync(chatId, "Допбро пожаловать в магазин Ролика Дениса!\n Пожалуйста выбирите раздел", replyToMessageId: messageId,replyMarkup: rkm );
        }
    }
}
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace FootBall_Bot.Controllers
{
    public class BotController
    {
        private TelegramBotClient BotClient = new TelegramBotClient(Constants.Token);
        private CancellationToken CancellationToken = new CancellationToken();
        private ReceiverOptions ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        public async Task Start()
        {
            BotClient.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, ReceiverOptions, CancellationToken);
            var botMe = await BotClient.GetMeAsync();
            Console.WriteLine($"Bot {botMe.Username} started.");
        }

        private Task HandlerErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ: \n{apiRequestException.ErrorCode}" + $"{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(BotClient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                        new[]
                        {
                            new KeyboardButton[] { "/lookforlive", "Check matches by date" }
                        }
                    )
                { ResizeKeyboard = true };

                await BotClient.SendTextMessageAsync(message.Chat.Id, "Оберіть команду", replyMarkup: replyKeyboardMarkup);
                return;
            }

        }
    }
}

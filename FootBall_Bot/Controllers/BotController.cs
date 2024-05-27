using FootBall_Bot.Models.Leagues;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
                await BotClient.SendTextMessageAsync(message.Chat.Id, "Оберіть команду");
                return;
            }

            else if (message.Text == "/help")
            {
                await BotClient.SendTextMessageAsync(message.Chat.Id,
                    "/checklive - пошук матчів в лайві " +
                    "\n/checkteaminseason - пошук матчів команди в сезоні " +
                    "\n/checkdate - пошук матчів за датою");
            }

            else if (message.Text == "/checklive")
            {
                await BotClient.SendTextMessageAsync(message.Chat.Id, APIController.GetFixtures());
                return;
            }

            else if (message.Text.StartsWith("/checkteaminseason"))
            {
                await CheckTeamInCeason(message);
                return;
            }

            else if (message.Text.StartsWith("/checkdate"))
            {
                await CheckDate(message);
                return;
            }

            else
            {
                await BotClient.SendTextMessageAsync(message.Chat.Id, "Невідома команда. Будь ласка використайте команду /help для ознайомлення зі списком доступних команд.");
                return;
            }
        }

        private async Task CheckDate(Message message)
        {
            try
            {
                string[] answer = message.Text.Split(" ");
                if(answer.Length < 2)
                {
                    await BotClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка вкажіть корректну дату в форматі рррр-мм-дд");
                    return;
                }

                await BotClient.SendTextMessageAsync(message.Chat.Id, APIController.GetFixtures(answer[1]));
                return;

            }
            catch (Exception ex)
            {
                await BotClient.SendTextMessageAsync(message.Chat.Id, "Виникла помилка. Перевірте вказані дані.");
                return;
            }
        }

        private async Task CheckTeamInCeason(Message message)
        {
            try
            {
                string[] answer = message.Text.Split(" ");
                ushort season = 0;
                if (answer.Length < 3)
                {
                    await BotClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка вкажіть повну назву команди (наприклад Manchester City) і сезон, як 4-значне число (наприклад 2023)");
                    return;
                }

                try
                {
                    season = Convert.ToUInt16(answer[2]);
                }
                catch (Exception ex)
                {
                    await BotClient.SendTextMessageAsync(message.Chat.Id, "Вкажіть сезон, як 4-значне число (наприклад 2023)");
                    return;
                }
                await BotClient.SendTextMessageAsync(message.Chat.Id, APIController.GetFixtures(answer[1], season));
                return;
            }
            catch (Exception ex)
            {
                await BotClient.SendTextMessageAsync(message.Chat.Id, "Виникла помилка. Перевірте вказані дані.");
                return;
            }
        }

    }
}

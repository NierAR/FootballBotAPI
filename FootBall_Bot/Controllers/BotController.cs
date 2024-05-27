using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace FootBall_Bot.Controllers
{
    public class BotController
    {
        private readonly TelegramBotClient _botClient = new TelegramBotClient(Constants.Token);
        private readonly CancellationToken _cancellationToken = new CancellationToken();
        private readonly ReceiverOptions _receiverOptions = new ReceiverOptions { AllowedUpdates = { } };
        private readonly APIController _apiController = new APIController();

        public async Task Start()
        {
            _botClient.StartReceiving(HandlerUpdateAsync, HandlerErrorAsync, _receiverOptions, _cancellationToken);
            var botMe = await _botClient.GetMeAsync();
            
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
                await HandlerMessageAsync(_botClient, update.Message);
            }
        }

        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Оберіть команду");
                return;
            }

            else if (message.Text == "/help")
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id,
                    "/checklive - пошук матчів в лайві " +
                    "\n/checkteaminseason - пошук матчів команди в сезоні " +
                    "\n/checkdate - пошук матчів за датою");
            }

            else if (message.Text == "/checklive")
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, _apiController.GetFixtures());
                return;
            }

            else if (message.Text.StartsWith("/checkteaminseason"))
            {
                await CheckTeamInCeason(message);
                return;
            }

            else if (message.Text.StartsWith("/checkdate"))
            {
                await CheckDate(message, false);
                return;
            }

            else if (message.Text.StartsWith("/checktoday"))
            {
                await CheckDate(message, true);
                return;
            }

            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Невідома команда. Будь ласка використайте команду /help для ознайомлення зі списком доступних команд.");
                return;
            }
        }

        private async Task CheckDate(Message message, bool IsToday)
        {
            try
            {
                if(IsToday == true)
                {
                    DateTime DateTime = DateTime.Now;
                    await _botClient.SendTextMessageAsync(message.Chat.Id, _apiController.GetFixtures(DateTime.ToString("yyyy-MM-dd"), IsToday));
                    return;
                }
                string[] answer = message.Text.Split(" ");
                if(answer.Length < 2)
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка вкажіть корректну дату в форматі рррр-мм-дд");
                    return;
                }

                await _botClient.SendTextMessageAsync(message.Chat.Id, _apiController.GetFixtures(answer[1], IsToday));
                return;

            }
            catch (Exception ex)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Виникла помилка. Перевірте вказані дані.");
                return;
            }
        }

        private async Task CheckTeamInCeason(Message message)
        {
            try
            {
                string[] answer = message.Text.Split(" ");
                string teamName = "";
                for(int i = 1; i < answer.Length - 1; i++)
                {
                    
                    teamName += answer[i];
                    if (i != answer.Length - 1) teamName += " ";
                }

                ushort season = 0;
                if (answer.Length < 3)
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка вкажіть повну назву команди (наприклад Manchester City) і сезон, як 4-значне число (наприклад 2023)");
                    return;
                }

                try
                {
                    season = Convert.ToUInt16(answer[answer.Length-1]);
                }
                catch (Exception ex)
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Вкажіть сезон, як 4-значне число (наприклад 2023)");
                    return;
                }
                await _botClient.SendTextMessageAsync(message.Chat.Id, _apiController.GetFixtures(teamName, season));
                return;
            }
            catch (Exception ex)
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Виникла помилка. Перевірте вказані дані.");
                return;
            }
        }

    }
}

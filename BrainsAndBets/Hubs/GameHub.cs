using Microsoft.AspNetCore.SignalR;
using BrainsAndBets.Data;

public class GameHub : Hub
{
    private static List<string> ConnectedUsers = new List<string>();
    private static (string Question, int Answer) currentQuestion;

    private static List<(string Question, int Answer)> questions = QuestionRepository.Questions;

    public async Task JoinGame(string username)
    {
        ConnectedUsers.Add(username);
        await Clients.All.SendAsync("UpdateConnectedUsers", ConnectedUsers);
        await Clients.Caller.SendAsync("ReceiveQuestion", currentQuestion.Question);
    }

    public async Task FetchRandomQuestion()
    {
        var random = new Random();
        int index = random.Next(questions.Count);
        currentQuestion = questions[index];
        await Clients.All.SendAsync("ReceiveQuestion", currentQuestion.Question);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var user = ConnectedUsers.FirstOrDefault(u => u == Context.GetHttpContext().Request.Query["username"]);
        if (user != null)
        {
            ConnectedUsers.Remove(user);
            await Clients.All.SendAsync("UpdateConnectedUsers", ConnectedUsers);
        }

        await base.OnDisconnectedAsync(exception);
    }
}

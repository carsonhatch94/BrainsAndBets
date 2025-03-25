namespace BrainsAndBets.Data
{
    public static class QuestionRepository
    {
        public static List<(string Question, int Answer)> Questions { get; } = new List<(string, int)>
        {
            ("What is the error code for \"I'm a teapot\" in the Hyper Text Coffee Pot Control Protocol?", 418),
            ("What is the number of average strands of hair in the human head?", 125000),
            ("What is the maximum number of players on a NFL team roster?", 53)
        };
    }
}

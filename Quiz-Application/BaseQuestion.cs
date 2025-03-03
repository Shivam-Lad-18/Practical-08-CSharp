namespace Quiz_Application
{
    public abstract class BaseQuestion
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public string CorrectAnswer { get; set; }

        public abstract void DisplayQuestion();
    }

}

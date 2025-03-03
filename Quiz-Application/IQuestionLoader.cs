namespace Quiz_Application
{
    public interface IQuestionLoader
    {
        List<BaseQuestion> LoadQuestions(int count, string difficulty);
    }

}

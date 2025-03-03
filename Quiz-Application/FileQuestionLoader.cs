namespace Quiz_Application
{
    public class FileQuestionLoader : IQuestionLoader
    {
        public List<BaseQuestion> LoadQuestions(int count, string difficulty)
        {
            string path = "QuestionBank.txt";

            if (!File.Exists(path))
                throw new FileNotFoundException("Question bank file not found!");

            var allLines = File.ReadAllLines(path);
            var questions = new List<BaseQuestion>();

            foreach (var line in allLines)
            {
                var parts = line.Split('|');
                if (parts[0].Equals(difficulty, StringComparison.OrdinalIgnoreCase))
                {
                    questions.Add(new MCQQuestion
                    {
                        Text = parts[1],
                        Options = new[] { parts[2], parts[3], parts[4], parts[5] },
                        CorrectAnswer = parts[6]
                    });

                    if (questions.Count == count)
                        break;
                }
            }

            if (questions.Count < count)
                throw new Exception("Not enough questions available for this difficulty.");

            return questions;
        }
    }

}

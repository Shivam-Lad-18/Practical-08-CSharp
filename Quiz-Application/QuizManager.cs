namespace Quiz_Application
{
    public class QuizManager
    {
        private readonly IQuestionLoader questionLoader;
        private List<BaseQuestion> questions;
        private QuizTimer timer;

        public QuizManager(IQuestionLoader loader)
        {
            this.questionLoader = loader;
            questions = new List<BaseQuestion>();
            timer = new QuizTimer();
        }

        public void StartQuiz(string difficulty, int questionCount)
        {
            try
            {
                questions = questionLoader.LoadQuestions(questionCount, difficulty);
                SetTimerBasedOnDifficulty(difficulty, questionCount);

                timer.StartTimer();

                int correct = 0;
                int wrong = 0;

                for (int i = 0; i < questions.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"Question {i + 1}/{questions.Count}:");

                    var question = questions[i];
                    question.DisplayQuestion();

                    Console.Write("Your Answer: ");
                    string answer = ReadAnswerWithTimeout(timer.TimePerQuestion);

                    if (string.IsNullOrEmpty(answer))
                    {
                        Console.WriteLine("⏰ Time's up! No answer selected.");
                        wrong++;
                    }
                    else if (answer.ToUpper() == question.CorrectAnswer.ToUpper())
                    {
                        correct++;
                    }
                    else
                    {
                        wrong++;
                    }
                }

                int timeTaken = timer.GetTimeElapsed();
                ShowResults(correct, wrong, timeTaken, questionCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error: {ex.Message}");
            }
        }

        private void SetTimerBasedOnDifficulty(string difficulty, int questionCount)
        {
            switch (difficulty.ToLower())
            {
                case "easy":
                    timer.TimePerQuestion = 13;
                    break;
                case "medium":
                    timer.TimePerQuestion = 10;
                    break;
                case "hard":
                    timer.TimePerQuestion = 7;
                    break;
                default:
                    throw new Exception("Invalid difficulty level!");
            }

            timer.TotalTime = timer.TimePerQuestion * questionCount;
            Console.WriteLine($"✅ Timer set to: {timer.TimePerQuestion} seconds per question ({timer.TotalTime} seconds total)");
        }

        private string ReadAnswerWithTimeout(int seconds)
        {
            string answer = string.Empty;
            DateTime deadline = DateTime.Now.AddSeconds(seconds);

            while (DateTime.Now < deadline)
            {
                if (Console.KeyAvailable)
                {
                    answer = Console.ReadLine().Trim();
                    return answer; // User answered within time
                }
            }

            return ""; // Time ran out
        }

        public void ShowResults(int correct, int wrong, int timeTaken, int totalQuestions)
        {
            Console.Clear();
            Console.WriteLine("Quiz Complete!");
            Console.WriteLine($"✅ Correct: {correct}");
            Console.WriteLine($"❌ Wrong: {wrong}");
            Console.WriteLine($"⏳ Time Taken: {timeTaken} seconds");

            double score = ((double)correct / totalQuestions) * 100;
            Console.WriteLine($"🎉 Final Score: {score:F2}% - {(score > 70 ? "Great Job!" : "Keep Practicing")}");

            Console.WriteLine("\nWhat next?");
            Console.WriteLine("1. Play Again");
            Console.WriteLine("2. Exit");
        }
    }
}

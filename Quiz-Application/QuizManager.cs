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
                        Console.WriteLine("Time's up! No answer selected.");
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
                Console.WriteLine($"Error: {ex.Message}");
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
        }

        private string ReadAnswerWithTimeout(int seconds)
        {
            string answer = string.Empty;
            DateTime deadline = DateTime.Now.AddSeconds(seconds);

            while (DateTime.Now < deadline)
            {
                if (Console.KeyAvailable)
                {
                    EnterOption:
                    answer = Console.ReadLine().Trim();
                    if(answer.ToUpper() == "A" || answer.ToUpper() == "B" || answer.ToUpper() == "C" || answer.ToUpper() == "D")
                    {
                        return answer; // User answered within time
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter A, B, C or D.");
                        goto EnterOption;
                    }
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

        }
    }
}

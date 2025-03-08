using Quiz_Application;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to QuizApp!");

        while (true)
        {
            getdifficuilty:
            Console.WriteLine("Choose Difficulty Level:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            Console.Write("Choice: ");
            string difficulty = Console.ReadLine();
            if (difficulty.Length == 0)
            {
                Console.WriteLine("Enter a valid difficulty .");
                goto getdifficuilty;
            }
            difficulty = GetDifficulty(difficulty);
            Console.Write("How many questions do you want to attempt? (Max 20): ");
            
            int questionCount = int.Parse(Console.ReadLine());
            if(questionCount > 20 )
            {
                Console.WriteLine("You can attempt maximum 20 questions.");
                questionCount = 20;
            }else if(questionCount < 1)
            {
                Console.WriteLine("You have to attempt at least 1 question.");
                questionCount = 1;
            }
            var loader = new FileQuestionLoader();
            var quizManager = new QuizManager(loader);
            quizManager.StartQuiz(difficulty, questionCount);

            Console.WriteLine("Choose:");
            Console.WriteLine("1. Play Again");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();
            if (choice != "1") break;
        }
    }

    static string GetDifficulty(string input)
    {
        return input switch
        {
            "1" => "Easy",
            "2" => "Medium",
            "3" => "Hard",
            _ => throw new ArgumentException("Invalid choice.")
        };
    }
}

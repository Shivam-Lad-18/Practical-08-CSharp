namespace Quiz_Application
{
    public class MCQQuestion : BaseQuestion
    {
        public override void DisplayQuestion()
        {
            Console.WriteLine(Text);
            Console.WriteLine($"A. {Options[0]}");
            Console.WriteLine($"B. {Options[1]}");
            Console.WriteLine($"C. {Options[2]}");
            Console.WriteLine($"D. {Options[3]}");
        }
    }

}

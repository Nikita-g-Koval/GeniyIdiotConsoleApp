namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        // получить вопросы
        static Question[] GetQuestions(int countQuestions)
        {
            Question[] questions = new Question[countQuestions];
            questions[0] = new Question("Сколько будет два плюс два умноженное на два?", 6);
            questions[1] = new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9);
            questions[2] = new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25);
            questions[3] = new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60);
            questions[4] = new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2);
            return questions;
        }

        // получить диагнозы
        static string[] GetDiagnoses()
        {
            string[] diagnoses = new string[6];
            diagnoses[0] = "кретин";
            diagnoses[1] = "идиот";
            diagnoses[2] = "дурак";
            diagnoses[3] = "нормальный";
            diagnoses[4] = "талант";
            diagnoses[5] = "гений";
            return diagnoses;
        }

        // перемешать массив
        static void Shuffle(Question[] questions)
        {
            Random random = new Random();
            for (int i = questions.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (questions[i], questions[j]) = (questions[j], questions[i]);
            }
        }

        // задать вопросы
        static int AskQuestions(Question[] questions)
        {
            int countRightAnswers = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine("Вопрос №" + (i + 1));
                Console.WriteLine(questions[i].text);

                int userAnswer = Convert.ToInt32(Console.ReadLine());

                int rightAnswer = questions[i].answer;

                if (userAnswer == rightAnswer)
                {
                    countRightAnswers++;
                }
            }
            return countRightAnswers;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Напишите ваше имя.");
            string name = Console.ReadLine();

            int countQuestions = 5;
            Question[] questions = GetQuestions(countQuestions);

            Shuffle(questions);

            int countRightAnswers = AskQuestions(questions);

            Console.WriteLine("Количество правильных ответов: " + countRightAnswers);

            string[] diagnoses = GetDiagnoses();

            Console.WriteLine($"{name}, ваш диагноз: {diagnoses[countRightAnswers]}");
        }
    }
}
﻿namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name = GetUserName();

            int countQuestions = 5;
            Question[] questions = GetQuestions(countQuestions);

            bool repeat = true;

            while (repeat)
            {
                Shuffle(questions);

                int countRightAnswers = RunTest(questions);

                Console.WriteLine("Количество правильных ответов: " + countRightAnswers);

                string diagnose = GetDiagnose(countQuestions, countRightAnswers);

                Console.WriteLine($"{name}, ваш диагноз: {diagnose}");

                repeat = Repeat();
            }
        }
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
        static string GetDiagnose(int countQuestions, int countRightAnswers)
        {
            string[] diagnoses = new string[6];
            diagnoses[0] = "кретин"; // < 10% правильных ответов
            diagnoses[1] = "идиот"; // >= 10% и < 30%
            diagnoses[2] = "дурак"; // >= 30% и < 60%
            diagnoses[3] = "нормальный"; // >= 60% и < 85%
            diagnoses[4] = "талант"; // >= 80% и < 95%
            diagnoses[5] = "гений"; // >= 95%

            int rightAnswersProcent = countRightAnswers * 100 / countQuestions;
            string result = diagnoses[0];

            switch (rightAnswersProcent)
            {
                case >= 10 and < 30:
                    result = diagnoses[1];
                    break;
                case >= 30 and < 60:
                    result = diagnoses[2];
                    break;
                case >= 60 and < 80:
                    result = diagnoses[3];
                    break;
                case >= 80 and < 95:
                    result = diagnoses[4];
                    break;
                case >=95:
                    result = diagnoses[5];
                    break;
            }

            return result;
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

        // получить целочисленный ответ от пользователя с проверкой данных
        static int GetIntUserAnswer()
        {
            Console.WriteLine("Введите ответ(любое целочисленное число)");
            string inputAnswer = Console.ReadLine();

            int answer;

            while (true)
            {
                if (!int.TryParse(inputAnswer, out answer))
                {
                    Console.WriteLine("Вы ввели некорректные данные! Попробуйте снова! Примеры корректных данных(любое целочисленное число): \"3\", \"120\", \"51\".");
                }
                else
                    break;

                inputAnswer = Console.ReadLine();
            }

            return answer;
        }

        // задать вопросы
        static int RunTest(Question[] questions)
        {
            int countRightAnswers = 0;
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine("Вопрос №" + (i + 1));
                Console.WriteLine(questions[i].Text);

                int userAnswer = GetIntUserAnswer();

                int rightAnswer = questions[i].Answer;

                if (userAnswer == rightAnswer)
                {
                    countRightAnswers++;
                }
            }
            return countRightAnswers;
        }

        // повторить тест или нет
        static bool Repeat()
        {
            bool result = true;
            Console.WriteLine("Вы хотите повторить тест? (Введите \"Да\" или \"Нет\")");

            while (true)
            { 
                string userAnswer = Console.ReadLine();
                if (userAnswer == "Нет")
                {
                    result = false;
                    break;
                }
                else if (userAnswer == "Да")
                    break;

                Console.WriteLine($"Вы ввели некорректные данные! (Введите \"Да\" или \"Нет\")!");
            }

            return result;
        }

        // спросить у пользователя имя
        static string GetUserName()
        {
            string name;

            Console.WriteLine("Здравствуйте! Напишите ваше имя.");
            while (true)
            {
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                    break;
                Console.WriteLine("Имя не должно быть пустым!");
            }

            return name;
        }
    }
}
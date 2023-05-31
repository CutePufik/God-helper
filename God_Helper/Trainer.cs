
namespace Tutor;

public class Trainer
{
    public int countRigthAnswers { get; private set; }
    public int countWrongAnswers { get; private set; }
    private const string OutPutDirectory = "../../../files/";
    private string recordFile = "recordFile.txt";
    private string statisticsFile = "statisticsFile.txt";
    public string _theme; 
    public string _question; 
    public string _answer; 


    public List<Formula> GetThemeQuestions(String theme)
    {
        var allQuestions = Formula.getQuestions();
        return allQuestions[theme];
    }

    public Trainer()
    {
    }

    public void MinusOneCorrectAnswer()
    {
        countRigthAnswers--;
    }

    public void PlusOneCorrectAnswer()
    {
        countRigthAnswers++;
    }

    public void PlusOneWrongAnswer()
    {
        countWrongAnswers++;
    }

    
    
    public void recordIncorrectAnswers(int indexQuestion)
    {
        string? parentDirectory = OutPutDirectory + _theme + statisticsFile;
        using (var fs = new FileStream(parentDirectory, FileMode.Append))
        using (var sw = new StreamWriter(fs))
        {
            sw.WriteLine(indexQuestion);
        }
    }
    

    public void showStatistics(String path)
    {
        try
        {
            string? parentDirectory = OutPutDirectory + _theme + "recordFile.txt";
            var text = File.ReadAllLines(parentDirectory);
            if (text.Count(x => x != "") == 0)
            {
                Console.WriteLine("У вас было ноль занятий");
                return;
            }
            Console.WriteLine($"Выберите количество последних занятий,сейчас {text.Count(x => x!="")}");
            var numberGames = int.Parse(Console.ReadLine().Trim().ToLower());
            while (numberGames<=0 || numberGames>text.Length)
            {
                numberGames = int.Parse(Console.ReadLine());
            }
            (int, int) res = parseRecordFiles(text,numberGames);
            Console.WriteLine(
                $"тема:{path},  правильных ответов-{res.Item1}  неправильных ответов-{res.Item2}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public (int, int) parseRecordFiles(string[] text,int numberGames)
    {
        (int, int) answers = (0, 0); 
        
        foreach (string s in text.Reverse().Take(numberGames))
        {
            var str = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            answers.Item1 += int.Parse(str[^2]);
            answers.Item2 += int.Parse(str[^1]);
        }
        
        return answers;
    }

    

    public void removeRigthAnswerFromStatisticks(int indexOfQuestion)
    {
        string? parentDirectory = OutPutDirectory + _theme + statisticsFile;
        int countThisQuestion = File.ReadAllLines(parentDirectory).Where(x => x!="").Select(x => int.Parse(x))
            .Count(x => x == indexOfQuestion);

        if (countThisQuestion >= 2)
        {
            removeIndex(indexOfQuestion);
        }
    }

    private void removeIndex(int indexOfQuestion)
    {
        
        string? filePath =OutPutDirectory + _theme + statisticsFile;
        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            if (int.Parse(lines[i]) == indexOfQuestion)
            {
                lines[i] = lines[i].Replace(lines[i], "-1");
                break;
            }
            
        }
        File.WriteAllLines(filePath, lines);
        
    }

    public void makeCountAnswersZero()
    {
        countRigthAnswers = 0;
        countWrongAnswers = 0;
    }


    public void clearStatistickFileFromLitter()
    {
        var file = File.ReadAllLines(OutPutDirectory + _theme + statisticsFile).Where(x => x!="-1");
        File.WriteAllLines(OutPutDirectory + _theme + statisticsFile,file);
    }
}
namespace Tutor;

public class Formula
{
    public string name { get; }
    public string answer { get; }
    private const string OutPutDirectory = "../../../files/";


    public (int, int) answers;
    

    public Formula(string name, string answer)
    {
        this.name = name;
        this.answer = answer;
    }

    public static Dictionary<String,List<Formula>> getQuestions()
    {
        if (!Directory.Exists(OutPutDirectory))
        {
            Directory.CreateDirectory(OutPutDirectory);
        }
        
        var result = new Dictionary<String, List<Formula>>();
        string? parentDirectory = OutPutDirectory + "formulas.txt";
        var text = File.ReadAllLines(parentDirectory);
        foreach (var s in text)
        {
            var str = s.Split("|");
            var theme = str[0];
            var formula = new Formula(str[1], str[2]);
            if (result.ContainsKey(theme))
                result[theme].Add(formula);
            else
            {
                result.Add(theme,new List<Formula>());
                result[theme].Add(formula);
            }
        }
        return result;
    }

  
    
    
    public override string ToString()
    {
        return name +"  "+ answer;
    }
}
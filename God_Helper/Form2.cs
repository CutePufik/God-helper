using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
using Tutor;

namespace God_Helper;

public partial class Form2 : Form
{
    private const string OutPutDirectory = "../../../files/";
    private string recordFile = "recordFile.txt";
    
    
    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
    private bool isCorrectFormula;
    private bool isCorrectBonusFormula;

    private bool previousWasIncorrect = false;

    private Label countRigthAnswersLabel;
    private Label countWrongAnswersLabel;
    private Button goMenu;

    private int randomIndex;
    private bool isNowBonus = false;

    private Label questionLabel;
    private Label answerLabel;
    
    private Button isReadyButton;
    private Button isCorrectFormulaButton;
    private Button isNotCorrectFormulaButton;
    private Button BonusIsCorrectButton;
    private Button BonusisNotCorrectFormulaButton;
    private static Trainer trainer = new Trainer();

    private int indexFormula = 1;
    
    private int countForBonusQuestion = 1;

    private int countRigthAnswers;
    private int countWrongAnswers;
    
    
    public Form2(string theme) //
    {
        trainer._theme = theme;
        InitializeComponent();
        initButtons();
        mainGame();
    }

    private void initButtons()
    {
        questionLabel = new Label();
        isReadyButton = new Button();
        answerLabel = new Label();
        isCorrectFormulaButton = new Button();
        isNotCorrectFormulaButton = new Button();
        BonusisNotCorrectFormulaButton = new Button();
        BonusIsCorrectButton = new Button();
        countWrongAnswersLabel = new Label();
        countRigthAnswersLabel = new Label();
        goMenu = new Button();
        
        goMenu.Text = "выйти в меню?";
        goMenu.Size = new Size(400, 100);
        goMenu.Location = new Point((screenWidth - 400) / 2, (screenHeight - 100) / 2 + 100);
        goMenu.Click += buttonToMenu;
        Controls.Add(goMenu);
        goMenu.Hide();
        
        
        countRigthAnswersLabel.Size = new Size(400, 100);
        countRigthAnswersLabel.Location = new Point((screenWidth-400) / 2, (screenHeight-100) / 2-100);
        countRigthAnswersLabel.Font = new Font("Times New Roman", 20, FontStyle.Regular);
        Controls.Add(countRigthAnswersLabel);
        countRigthAnswersLabel.Hide();
        
        countWrongAnswersLabel.Size = new Size(400, 100);
        countWrongAnswersLabel.Location = new Point((screenWidth - 400) / 2, (screenHeight - 100) / 2);
        countWrongAnswersLabel.Font = new Font("Times New Roman", 20, FontStyle.Regular);
        Controls.Add(countWrongAnswersLabel);
        countWrongAnswersLabel.Hide();

        //// вопрос
        questionLabel.Size = new Size(400, 100);
        questionLabel.Location = new Point((screenWidth) / 2 - 80, (screenHeight - 100) / 2 - 100);
        questionLabel.Font = new Font("Times New Roman", 16, FontStyle.Regular);
        Controls.Add(questionLabel);

        //// кнопка готовности
        isReadyButton.Text = "готовы?";
        isReadyButton.Size = new Size(400, 100);
        isReadyButton.Location = new Point((screenWidth - 400) / 2, (screenHeight - 100) / 2 + 100);
        isReadyButton.Click += IsReadyButtonClick;
        Controls.Add(isReadyButton);
        isReadyButton.Font = new Font("Times New Roman", 16, FontStyle.Regular);

        //// ответ
        answerLabel.Text = "ответ";
        answerLabel.Size = new Size(300, 400);
        answerLabel.Location = new Point((screenWidth - 200) / 2, (screenHeight - 100) / 2 + 100);
        Controls.Add(answerLabel);
        answerLabel.Font = new Font("Times New Roman", 16, FontStyle.Bold);
        answerLabel.Hide();

        ///// формула правильно написана
        isCorrectFormulaButton.Text = "правильно";
        isCorrectFormulaButton.Size = new Size(400, 100);
        isCorrectFormulaButton.Location = new Point((screenWidth - 400) / 2 - 500, (screenHeight - 100) / 2 );
        isCorrectFormulaButton.Click += IsCorrectClick;
        Controls.Add(isCorrectFormulaButton);
        isCorrectFormulaButton.Hide();
        isCorrectFormulaButton.Font = new Font("Times New Roman", 16, FontStyle.Bold);

        /////  формула неправильно написана
        isNotCorrectFormulaButton.Text = "неправильно";
        isNotCorrectFormulaButton.Size = new Size(400, 100);
        isNotCorrectFormulaButton.Location = new Point((screenWidth - 400) / 2 + 500, (screenHeight - 100) / 2 );
        isNotCorrectFormulaButton.Click += IsNotCorrectClick;
        Controls.Add(isNotCorrectFormulaButton);
        isNotCorrectFormulaButton.Hide();
        isNotCorrectFormulaButton.Font = new Font("Times New Roman", 16, FontStyle.Bold);
        
        
        
        
        ///// BONUS формула правильно написана
        BonusIsCorrectButton.Text = "правильно bonus";
        BonusIsCorrectButton.Size = new Size(400, 100);
        BonusIsCorrectButton.Location = new Point((screenWidth - 400) / 2 - 500, (screenHeight - 100) / 2 + 100);
        BonusIsCorrectButton.Click += IsCorrectBonusButtonClick;
        Controls.Add(BonusIsCorrectButton);
        BonusIsCorrectButton.Hide();
            
        ///// BONUS формула neправильно написана
        BonusisNotCorrectFormulaButton.Text = "неправильно bonus";
        BonusisNotCorrectFormulaButton.Size = new Size(400, 100);
        BonusisNotCorrectFormulaButton.Location = new Point((screenWidth - 400) / 2 - 500, (screenHeight - 100) / 2 + 100);
        BonusisNotCorrectFormulaButton.Click += IsNotCorrectBonusButtonClick;
        Controls.Add(BonusisNotCorrectFormulaButton);
        BonusisNotCorrectFormulaButton.Hide();
            
    }
    

    

    private void IsReadyButtonClick(object sender, EventArgs e)
    {
        isReadyButton.Hide();
        
        showAnswersButtons();
        
        List<Formula> listQuestions = trainer.GetThemeQuestions(trainer._theme);
        var formula = listQuestions[indexFormula-1];
        answerLabel.Text =formula.answer; 
        answerLabel.Show();
    }

    private void askForReady()
    {
        isReadyButton.Show();
    }
    private void showBonusButtons()
    {
        BonusIsCorrectButton.Show();
        BonusisNotCorrectFormulaButton.Show();
    }


    private void mainGame()
    {
        List<Formula> listQuestions = trainer.GetThemeQuestions(trainer._theme);
        var formula = listQuestions[0];
        questionLabel.Text = formula.name;
        answerLabel.Text = formula.answer;
        trainer._question = formula.name;
        trainer._answer = formula.answer;
        
    }

    

    private void showAnswersButtons()
    {
        isNotCorrectFormulaButton.Show();
        isCorrectFormulaButton.Show();
    }

    private void hideAnswersButtons()
    {
        isNotCorrectFormulaButton.Hide();
        isCorrectFormulaButton.Hide();
    }

    private void IsCorrectClick(object sender, EventArgs e)
    {
        answerLabel.Hide();
        hideAnswersButtons();
        isReadyButton.Show();
        countRigthAnswers++;
        //
        
        if(indexFormula == 1)
            trainer.removeRigthAnswerFromStatisticks(0);  // дайте возможность пожить...
        //
        if (isNowBonus)
        {
            trainer.removeRigthAnswerFromStatisticks(randomIndex);
        }
        if (countForBonusQuestion % 3 == 0)
        {
            string? parentDirectory =  OutPutDirectory + trainer._theme + "statisticsFile.txt";
            //hideAnswersButtons();
            //showBonusButtons();
            Random r = new Random();
            var file = File
                .ReadAllLines(parentDirectory)
                .Where(x => int.Parse(x) != -1).ToArray();
            randomIndex = int.Parse(file[r.Next(0, file.Length)]);
            var allQuestion = trainer.GetThemeQuestions(trainer._theme);
            answerLabel.Text = (allQuestion[randomIndex].answer) ;
            questionLabel.Text = (allQuestion[randomIndex].name) + " бонус";
            trainer._question = (allQuestion[randomIndex].name);
            trainer._answer = (allQuestion[randomIndex].answer);
            countForBonusQuestion++;
            indexFormula++;
            isNowBonus = true;
            return;
        }
       
        //showAnswersButtons();
        
        List<Formula> listQuestions = trainer.GetThemeQuestions(trainer._theme);
        var formula = listQuestions[indexFormula];
        questionLabel.Text = formula.name;
        trainer._question = formula.name;
        trainer._answer = formula.answer;
        var indexQuestion = listQuestions.IndexOf(formula); 
        trainer.removeRigthAnswerFromStatisticks(indexQuestion);
        
        
        if (indexFormula == listQuestions.Count-1)
        {
            recordResult();
            showResults();
            countRigthAnswers = 0;
            countWrongAnswers = 0;
            trainer.clearStatistickFileFromLitter();   // ???
            hideAnswersButtons();
            
            
        }
        indexFormula++;
        countForBonusQuestion++;
        //askForQuestion(formula, indexQuestion);
         // if (countForBonusQuestion % 3 == 0)
         // {
         //     //hideAnswersButtons();
         //     //showBonusButtons();
         //     Random r = new Random();
         //     var file = File
         //         .ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + '\\' + trainer._theme + "statisticsFile.txt")
         //         .Where(x => int.Parse(x) != -1).ToArray();
         //     randomIndex = int.Parse(file[r.Next(0, file.Length)]);
         //     var allQuestion = trainer.GetThemeQuestions(trainer._theme);
         //     answerLabel.Text = (allQuestion[randomIndex].name) + "бонус";
         // }
         
        

         
        
       
    }
    public void recordResult()
    {
        string? parentDirectory = OutPutDirectory + trainer._theme + recordFile;
        using (var fs = new FileStream(parentDirectory, FileMode.Append))
        using (var sw = new StreamWriter(fs))
        {
            sw.WriteLine(trainer._theme + " " + countRigthAnswers + " " + countWrongAnswers);
        }
        
    }
    private void showResults()
    {
        hideAnswersButtons();
        questionLabel.Hide();

        countWrongAnswersLabel.Text = $"неправильных ответов {countWrongAnswers.ToString()}";
        countWrongAnswersLabel.Show();
        
        countRigthAnswersLabel.Text = $"правильных ответов {countRigthAnswers.ToString()}";
        countRigthAnswersLabel.Show();

        goMenu.Show();
        
    }
    
    private void buttonToMenu(object sender, EventArgs e)
    {
        Form1 form1 = new Form1();
        form1.ShowDialog();
        Close();
        Hide();
    }

    private void IsNotCorrectClick(object sender, EventArgs e)
    {
        
        isNotCorrectFormulaButton.Hide();
        countWrongAnswers++;
        countRigthAnswers--; // ужасно...
        
        //
        if(indexFormula == 1)
            trainer.removeRigthAnswerFromStatisticks(0);  // дайте возможность пожить...
        //
        
        
        List<Formula> listQuestions = trainer.GetThemeQuestions(trainer._theme);   // плохо,но иначе прога не запускалась
        var formula = listQuestions[indexFormula];

        var indexQuestion = listQuestions.IndexOf(formula);
        trainer.recordIncorrectAnswers(indexQuestion);

    }

    public void askForQuestion(Formula formula, int indexOfQuestion)
    {
        if (isCorrectFormula)
        {
            trainer.PlusOneCorrectAnswer();
            trainer.removeRigthAnswerFromStatisticks(indexOfQuestion);
        }
        else
        {
            trainer.PlusOneWrongAnswer();
            trainer.recordIncorrectAnswers(indexOfQuestion);
        }
    }

    
    private void IsCorrectBonusButtonClick(object sender, EventArgs e)
    {
        countRigthAnswers++;
        //trainer.removeRigthAnswerFromStatisticks(random);
        hideBonusButtons();
    }
    
    private void IsNotCorrectBonusButtonClick(object sender, EventArgs e)
    {
        countWrongAnswers++;
        hideBonusButtons();
    }

    private void hideBonusButtons()
    {
        BonusIsCorrectButton.Hide();
        BonusisNotCorrectFormulaButton.Hide();
    }
    
}
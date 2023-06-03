using System.Windows.Forms;
using Tutor;

namespace God_Helper;

public partial class StatistickForm : Form
{
    private const string OutPutDirectory = "../../../files/";
    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
    int screenHeight = Screen.PrimaryScreen.Bounds.Height;

    private static Trainer trainer = new();
    private String theme;
    
    private Label stata;
    private Label mistakeLabel;
    private Label infoGamesLabel;
    private TextBox countGames;


    public StatistickForm(String theme)
    {
        InitializeComponent();
        this.theme = theme;
        init();
        this.WindowState = FormWindowState.Maximized;
    }

    private void init()
    {
        
        countGames = new TextBox();
        mistakeLabel = new Label();
        stata = new Label();
        infoGamesLabel = new Label();

        
        infoGamesLabel.Text = $"У вас {File.ReadAllLines(OutPutDirectory + theme + "recordFile.txt").Count(x => x != "").ToString()} занятий";
        infoGamesLabel.Size = new Size(500, 300);
        infoGamesLabel.Font = new Font("Times New Roman", 16, FontStyle.Bold);
        infoGamesLabel.Location = new Point((screenWidth - 200) / 2-40, (screenHeight - 100) / 2 - 300);
        

        stata.Font = new Font("Times New Roman", 16, FontStyle.Bold);
        stata.Size = new Size(400, 300);
        stata.Location = new Point((screenWidth - 400) / 2, (screenHeight - 100) / 2 + 100);
        stata.Hide();
        
        
        
        
        mistakeLabel.Text = "попробуй ещё раз!";
        mistakeLabel.Size = new Size(400, 100);
        mistakeLabel.Location = new Point((screenWidth - 400) / 2+50, (screenHeight - 100) / 2+100);
        mistakeLabel.Font = new Font("Times New Roman", 16, FontStyle.Bold);
        mistakeLabel.Hide();


        countGames.Size = new Size(350, 50);
        countGames.Location = new Point((screenWidth - 350) / 2-20, (screenHeight - 50) / 2);
        countGames.KeyPress += countGamesChanged;

        
        Controls.Add(countGames);
        Controls.Add(mistakeLabel);
        Controls.Add(stata);
        Controls.Add(infoGamesLabel);
        
        
    }

    private void countGamesChanged(object sender, KeyPressEventArgs  e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            try
            {
                string? parentDirectory = OutPutDirectory + theme + "recordFile.txt";
                var text = File.ReadAllLines(parentDirectory);
                if (text.Count(x => x != "") == 0)
                {
                    mistakeLabel.Text = "У вас было ноль занятий";
                }

            
                var numberGames = int.Parse(countGames.Text.Trim());
                if(numberGames <= 0 || numberGames > text.Length)
                {
                    mistakeLabel.Show();
                    stata.Hide();
                    return;
                }

                mistakeLabel.Hide();
                (int, int) res = trainer.parseRecordFiles(text, numberGames);
                stata.Text = $"тема:{theme}, правильных ответов-{res.Item1}  неправильных ответов-{res.Item2}";
            
                stata.Show();
            
            
                e.Handled = true;
            }
            catch (Exception exception)
            {
                mistakeLabel.Show();
            }
        }
        
        
        
            
    }
}
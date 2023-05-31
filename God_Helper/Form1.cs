using System.Net.Mime;
using System.Windows.Forms.VisualStyles;
using Tutor;

namespace God_Helper;

public partial class Form1 : Form
{
    int screenWidth = Screen.PrimaryScreen.Bounds.Width - 100;
    int screenHeight = Screen.PrimaryScreen.Bounds.Height;

    private Trainer trainer = new();
    private Button algebraButton;
    private Button triginometriyaButton;
    private Button matanButton;
    private Button statistickButton;
    private Button exitButton;
    private Button showStatistickAlgebra;
    private Button showStatistickTriga;
    private Button showStatistickMatan;


    public string _theme { get; private set; }

    public Form1()
    {
        InitializeComponent();
        initButtons();
    }

    private void initButtons()
    {
        algebraButton = new Button();
        triginometriyaButton = new Button();
        matanButton = new Button();
        statistickButton = new Button();
        exitButton = new Button();
        showStatistickAlgebra = new Button();
        showStatistickTriga = new Button();
        showStatistickMatan = new Button();


        algebraButton.Text = "Алгебра";
        algebraButton.Size = new Size(400, 100);
        algebraButton.Location = new Point((screenWidth - 400) / 2-200, (screenHeight - 100) / 2 - 200);
        algebraButton.Click += BeginButtonClick;


        triginometriyaButton.Text = "Тригонометрия";
        triginometriyaButton.Size = new Size(400, 100);
        triginometriyaButton.Location = new Point((screenWidth - 400) / 2-200, (screenHeight - 100) / 2 - 100);
        triginometriyaButton.Click += BeginButtonClick;


        matanButton.Text = "Математический анализ";
        matanButton.Size = new Size(400, 100);
        matanButton.Location = new Point((screenWidth - 400) / 2-200, (screenHeight - 100) / 2);
        matanButton.Click += BeginButtonClick;


        showStatistickAlgebra.Text = "Статистика Алгебры";
        showStatistickAlgebra.Size = new Size(400, 100);
        showStatistickAlgebra.Location = new Point((screenWidth - 400) / 2+200, (screenHeight - 100) / 2 -200);
        showStatistickAlgebra.Click += showStatistickAlgebraButton;

        showStatistickTriga.Text = "Статистика Тригонометрии";
        showStatistickTriga.Size = new Size(400, 100);
        showStatistickTriga.Location = new Point((screenWidth - 400) / 2+200, (screenHeight - 100) / 2 - 100);
        showStatistickTriga.Click += showStatistickTrigaButton;

        showStatistickMatan.Text = "Статистика Матана";
        showStatistickMatan.Size = new Size(400, 100);
        showStatistickMatan.Location = new Point((screenWidth - 400) / 2+200, (screenHeight - 100) / 2);
        showStatistickMatan.Click += showStatistickManatButton;


        Controls.Add(algebraButton);
        Controls.Add(triginometriyaButton);
        Controls.Add(matanButton);
        Controls.Add(showStatistickAlgebra);
        Controls.Add(showStatistickTriga);
        Controls.Add(showStatistickMatan);
    }

    private void showStatistickAlgebraButton(object sender, EventArgs e)
    {
        _theme = "Алгебра";
        StatistickForm statistickForm = new StatistickForm(_theme); // Создание экземпляра второй формы (
        statistickForm.ShowDialog(); // Отображение второй формы модально
        Close();
        Hide();
    }

    private void showStatistickTrigaButton(object sender, EventArgs e)
    {
        _theme = "Тригонометрия";
        StatistickForm statistickForm = new StatistickForm(_theme); // Создание экземпляра второй формы (
        statistickForm.ShowDialog(); // Отображение второй формы модально
        Close();
        Hide();
    }

    private void showStatistickManatButton(object sender, EventArgs e)
    {
        _theme = "Математический анализ";
        StatistickForm statistickForm = new StatistickForm(_theme); // Создание экземпляра второй формы (
        statistickForm.ShowDialog(); // Отображение второй формы модально
        Close();
        Hide();
    }

    private void BeginButtonClick(object sender, EventArgs e)
    {
        if (sender is Button b)
        {
            _theme = b.Text;
        }

        Form2 form2 = new Form2(_theme); // Создание экземпляра второй формы (
        form2.ShowDialog(); // Отображение второй формы модально
        Close();
        Hide();
    }
}
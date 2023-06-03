namespace God_Helper;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        
        Application.Run(new Form1());
        /*
         * Дополнение "бонус" показывает, что метод повторения работает.
         * Плюс намеренно не учитывается ответы на бонусные вопросы. Кроме того, весь функционал,
         * включая тупую задумку с нажатием кнопки "неправильно",
         * сделан намеренно, проблема не в коде, а с моей головой...
         * В любом случае мне понравилось!!
         */
       
    }
}
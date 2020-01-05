namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            wform form = new wform();
            form.ShowDialog();
            System.Console.WriteLine("Завершение программы");
            System.Console.ReadKey();
        }
    }
}

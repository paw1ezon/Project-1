using Persistence;

namespace CS
{
    public class Ultilities
    {
        public int MenuHandle(string? title, string[] menuItem)
        {
            int i = 0, choice;
            if(title != null)
                Title(title);
            for (; i < menuItem.Count(); i++)
            {
                System.Console.WriteLine("" + (i+1) + ". " + menuItem[i]);
            }
            Line();
            do
            {
                System.Console.Write("Your choice: ");
                int.TryParse(System.Console.ReadLine(), out choice);
            }while (choice <= 0 || choice > menuItem.Count());
            return choice;
        }
        public void Title(string title)
        {
            Line();
            System.Console.WriteLine(" " + title);
            Line();
        }
        public void Line(){
            System.Console.WriteLine("===================================================================");
        }
        public void PressAnyKeyToContinue(){
            Console.Write("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
    

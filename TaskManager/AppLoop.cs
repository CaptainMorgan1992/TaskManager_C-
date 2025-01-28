using TaskManager.IRepository;
using TaskManager.IService;

namespace TaskManager.Service;

// En klass som fungerar som "gameloop" - ett service-lager som implementerar ett ActivityService-Interface
// med diverse metoder. 

public class AppLoop 
{
    private readonly IActivityService _activityService;
    public AppLoop(IActivityService activityService)
    {
        _activityService = activityService;
    }
    public void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\nTask Manager\n");
            Console.WriteLine("What would you like to do next? \n");
            Console.WriteLine("1. Add new activity\n" +
                              "2. See all upcoming activities\n" +
                              "3. See how much time you have left until the next activity\n" +
                              "4. Exit");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    _activityService.AddActivity();
                    break;
                case 2:
                    _activityService.GetAllActivities();
                    break;
                case 3:
                    _activityService.CalculateTimeUntilNextActivity();
                    break;
                case 4:
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}
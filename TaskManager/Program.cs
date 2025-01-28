// See https://aka.ms/new-console-template for more information

using TaskManager.Repository;
using TaskManager.Service;


var activityRepository = new ActivityRepository();
var activityService = new ActivityService(activityRepository);


AppLoop appLoop = new AppLoop(activityService);

appLoop.MainMenu();
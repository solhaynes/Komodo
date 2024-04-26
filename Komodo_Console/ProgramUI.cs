using Repository;

namespace Komodo.Console;

public class ProgramUI
{
  private DeveloperRepository _devRepo = new DeveloperRepository();
  private DevTeamRepository _teamRepo = new DevTeamRepository();

  public void Run()
  {
    SeedContentList();
    Menu();
  }

  private void Menu()
  {
    bool keepRunning = true;

    while (keepRunning)
    {
      // Display options to the user
      System.Console.WriteLine("Select a menu option: \n" +
        "1. Create a new developer\n" + 
        "2. Delete a developer\n" + 
        "3. See list of developers\n" + 
        "4. Update a developer's information\n" + 
        "5. See list of devs who need Pluralsight access\n" + 
        "6. Create a new team of developers\n" + 
        "7. Delete a team of developers\n" + 
        "8. Update a team of developers\n" + 
        "9. See a list of all developer teams\n" + 
        "10. See the developers on a specific team\n" + 
        "11. Exit");

        // Get user's input
        string input = System.Console.ReadLine();

        // Evaluate the user's input
        switch (input)
        {
          case "1":
            // Create new developer
            CreateNewDeveloper();
            break;
          case "2":
            // delete a developer
            DeleteDeveloper();
            break;
          case "3":
            // Print list of developers
            PrintAllDevelopers();
            break;
          case "4":
            // Update a developer
            UpdateExistingDeveloper();
            break;
          case "5":
            // Print list of devs who need to be licensed for Pluralsight
            PrintNeedPluralsightAccess();
            break;
          case "6":
            // Create a developer team
            CreateNewDevTeam();
            break;
          case "7":
            // Delete team
            DeleteTeam();
            break;
          case "8":
            // Update team
            UpdateTeam();
            break;
          case "9":
            // Print a list of teams
            DisplayAllDevTeams();
            break;
          case "10":
            // Print the Developers on a specified team
            DisplayDevelopersOnTeam();
            break;
          case "11":
            // Exit
            System.Console.WriteLine("Goodbye!");
            keepRunning = false;
            break;
          default:
            System.Console.WriteLine("Please enter a valid number.");
            break;
        }
        System.Console.WriteLine("Please Press any key to continue...");
      System.Console.ReadKey();
      System.Console.Clear();
    }
  }

  private void CreateNewDeveloper()
  {
    System.Console.Clear();
    Developer newDeveloper = new Developer();

    // Name
    System.Console.WriteLine("Enter the name of the developer: ");
    newDeveloper.Name = System.Console.ReadLine();

    // ID
    System.Console.WriteLine("Enter the new developer's ID: ");
    newDeveloper.ID = int.Parse(System.Console.ReadLine());

    // HasAccessToPluralsight
    System.Console.WriteLine("Does the developer have access to Pluralsight? (Enter Y or N)");
    string answer = System.Console.ReadLine();

    bool valid = false;

    while (!valid)
    {
      if (answer == "y")
      {
        newDeveloper.HasAccessToPluralsight = true;
        valid = true;
      }
      else if(answer == "n")
      {
        newDeveloper.HasAccessToPluralsight = false;
        valid = true;
      }
      else
      {
        System.Console.WriteLine("Invalid response.  Try again.");
      }
    }
    _devRepo.AddDeveloperToList(newDeveloper);
  }

  //Delete a Developer
  private void DeleteDeveloper()
  {
    PrintAllDevelopers();

    // Get the ID they want to delete
    System.Console.WriteLine("Enter the ID of the developer you would like to delete from the system:");
    int input = int.Parse(System.Console.ReadLine());

    // Call the delete method
    bool wasDeleted = _devRepo.RemoveDeveloperFromList(input);

    // If the deletion was successful, let them know. Otherwise, tell them it couldn't be deleted
    if (wasDeleted)
    {
      System.Console.WriteLine("The developer was successfully removed from the system.");
    }
    else
    {
      System.Console.WriteLine("The developer couldn't be deleted.");
    }
  }

  private void UpdateExistingDeveloper()
  {
    // Display all content
    PrintAllDevelopers();

    // Ask for title of content they wish to update
    System.Console.WriteLine("\nEnter the ID number of the developer you would like to update:");

    // Get title from user
    int oldID = int.Parse(System.Console.ReadLine());
    
    Developer newDev = new Developer();

    // Name
    System.Console.WriteLine("Enter the developer's name:");
    newDev.Name = System.Console.ReadLine();

    // ID number
    System.Console.WriteLine("Enter the developer's ID number:");
    newDev.ID = int.Parse(System.Console.ReadLine());
    
    // Access to Pluralsight
    System.Console.WriteLine("Does this developer have access to Pluralsight? (y/n)");
    string response = System.Console.ReadLine();

    if(response == "y")
    {
      newDev.HasAccessToPluralsight = true;
    }
    else if (response == "n")
    {
      newDev.HasAccessToPluralsight = false;
    }
    else 
    {
      System.Console.WriteLine("Invalid answer. Try again later.");
    }

      // verify that it worked
      bool wasUpdated = _devRepo.UpdateDeveloperList(oldID, newDev);

      if (wasUpdated)
      {
        System.Console.WriteLine("Developer details were successfully updated.");
      }
      else 
      {
        System.Console.WriteLine("Couldn't update content.");
      }
  }

  private void PrintAllDevelopers()
  {
    System.Console.Clear();
    List<Developer> developerList = _devRepo.GetDeveloperList();

    System.Console.WriteLine("All developers currently in directory" + 
       "\n--------------------" );
      // Loop through each developer on the list and print their info
    foreach (Developer dev in developerList)
    {
      System.Console.WriteLine("ID: " + dev.ID + "\nName: " + dev.Name + "\nHas access to Pluralsight: " + dev.HasAccessToPluralsight);
      System.Console.WriteLine("--------------------");
    }
  }

  private void PrintNeedPluralsightAccess()
  {
    System.Console.Clear();
    List<Developer> developerList = _devRepo.GetDeveloperList();

    System.Console.WriteLine("All developers who do not currently have access to Pluralsight:" + 
      "\n---------------------------" );

    foreach (Developer dev in developerList)
    {
      if(!dev.HasAccessToPluralsight)
      {
        System.Console.WriteLine("ID: " + dev.ID + "\nName: " + dev.Name + "\nHas access to Pluralsight: " + dev.HasAccessToPluralsight);
        System.Console.WriteLine("--------------------");
      }
      
    }
  }

  // Display developers on a particular team
  private void DisplayDevelopersOnTeam()
  {
    System.Console.Clear();
    DisplayAllDevTeams();

    System.Console.WriteLine("Enter a team ID to see its members:");
    int response = int.Parse(System.Console.ReadLine());
    DevTeam team = _teamRepo.GetDevTeamByID(response);

    List<Developer> devList = team.DeveloperList;
    
    foreach(Developer dev in devList)
    {
      System.Console.WriteLine("ID: " + dev.ID + "\nName: " + dev.Name + "\nHas access to Pluralsight: " + dev.HasAccessToPluralsight);
      System.Console.WriteLine("--------------------");
    }
  }

  // Display all teams currently in library
  private void DisplayAllDevTeams()
  {
    System.Console.Clear();
    List<DevTeam> teamList = _teamRepo.GetListOfDevTeams();

    System.Console.WriteLine("All developer teams currently in directory" + 
       "\n--------------------" );
      // Loop through each developer on the list and print their info
    foreach (DevTeam team in teamList)
    {
      System.Console.WriteLine("Team ID: " + team.TeamID + "\nTeam Name: " + team.TeamName);
      System.Console.WriteLine("--------------------");
    }
  }

  private void CreateNewDevTeam()
  {
    System.Console.Clear();
    DevTeam newDevTeam = new DevTeam();

    // Name
    System.Console.WriteLine("Enter the team nane: ");
    newDevTeam.TeamName = System.Console.ReadLine();

    // ID
    System.Console.WriteLine("Enter the team ID: ");
    newDevTeam.TeamID = int.Parse(System.Console.ReadLine());
    System.Console.WriteLine();

    // Add Developers
    List<Developer> devList = new List<Developer>();
    bool done = false;
    Developer teamMember = new Developer();

    while(!done)
    {
      PrintAllDevelopers();
      System.Console.WriteLine("Enter a developer's ID to add them to the team");
      int response = int.Parse(System.Console.ReadLine());
      teamMember = _devRepo.GetDeveloperByID(response);

      // Add the team member to the list
      devList.Add(teamMember);

      // Ask the user if they'd like to add another member
      System.Console.WriteLine("Would you like to add another team member? (y/n)");
      string answer = System.Console.ReadLine();

      if(answer == "n")
      {
        done = true;
      }
    }
    
    newDevTeam.DeveloperList = devList;

    _teamRepo.AddDevTeamToList(newDevTeam);
  }

  // Delete a team from the list
  private void DeleteTeam()
  {
    System.Console.Clear();
    DisplayAllDevTeams();
    System.Console.WriteLine("Enter the ID of the team you would like to delete:");
    int response = int.Parse(System.Console.ReadLine());

    bool removed = _teamRepo.RemoveTeamFromList(response);

    if (removed)
    {
      System.Console.WriteLine("Team successfully removed from the directory.");
    }
    else
    {
      System.Console.WriteLine("Something went wrong.  Team could not be removed from directory.");
    }
  }

  // Update team
  private void UpdateTeam()
  {
    System.Console.Clear();
    DisplayAllDevTeams();
    System.Console.WriteLine("Enter the ID of the team you would like to update:");
    int response = int.Parse(System.Console.ReadLine());

    DevTeam team = _teamRepo.GetDevTeamByID(response);

    System.Console.WriteLine("How would you like to update " + team.TeamName + "?\n" +
      "1. Add a team member\n" + 
      "2. Remove a team member\n" + 
      "3. Change the team ID number");
    string response2 = System.Console.ReadLine();

    switch (response2)
    {
      case "1":
        AddDeveloperToTeam(team);
        break;
      case "2":
        RemoveDeveloperFromTeam(team);
        break;
      case "3":
        ChangeTeamID(team);
        break;
    }
  }

  private void AddDeveloperToTeam(DevTeam team)
  {
    List<Developer> devList = team.DeveloperList;
    bool done = false;
    Developer teamMember = new Developer();

    PrintAllDevelopers();
      System.Console.WriteLine("Enter a developer's ID to add them to the team");
      int response = int.Parse(System.Console.ReadLine());
      teamMember = _devRepo.GetDeveloperByID(response);

    while(!done)
    {
      // Add the team member to the list
      devList.Add(teamMember);

      // Ask the user if they'd like to add another member
      System.Console.WriteLine("Would you like to add another team member? (y/n)");
      string answer = System.Console.ReadLine();

      if(answer == "n")
      {
        done = true;
      }
    }
    
    team.DeveloperList = devList;

    bool updated = _teamRepo.UpdateExistingDevTeam(response, team);
    if (updated)
    {
      System.Console.WriteLine("Team member successfully added");
    }
    else
    {
      System.Console.WriteLine("Couldn't add the team member. Try again later.");
    }
  }

  private void RemoveDeveloperFromTeam(DevTeam team)
  {
    List<Developer> devList = team.DeveloperList;
    bool done = false;
    Developer teamMember = new Developer();

    while(!done)
    {
    foreach(Developer dev in devList)
    {
      System.Console.WriteLine("ID: " + dev.ID + "\nName: " + dev.Name + "\nHas access to Pluralsight: " + dev.HasAccessToPluralsight);
      System.Console.WriteLine("--------------------");
    }

      System.Console.WriteLine("Enter a developer's ID to remove them from the team");
      int response = int.Parse(System.Console.ReadLine());
      teamMember = _devRepo.GetDeveloperByID(response);

      // Add the team member to the list
      devList.Remove(teamMember);

      // Ask the user if they'd like to add another member
      System.Console.WriteLine("Would you like to remove another team member? (y/n)");
      string answer = System.Console.ReadLine();

      if(answer == "n")
      {
        done = true;
      }
    }
    
    team.DeveloperList = devList;

    bool updated = _teamRepo.UpdateExistingDevTeam(team.TeamID, team);
    if (updated)
    {
      System.Console.WriteLine("Team member successfully removed");
    }
    else
    {
      System.Console.WriteLine("Couldn't remove the team member. Try again later.");
    }
  }

  private void ChangeTeamID(DevTeam team)
  {
    int oldTeamID = team.TeamID;
    System.Console.WriteLine("What would you like to change the ID number to?");
    int newTeamID = int.Parse(System.Console.ReadLine());

    team.TeamID = newTeamID;

    bool updated = _teamRepo.UpdateExistingDevTeam(oldTeamID, team);
    if (updated)
    {
      System.Console.WriteLine("Team ID successfully removed");
    }
    else
    {
      System.Console.WriteLine("Couldn't change the team ID number. Try again later.");
    }

  }
 private void SeedContentList()
  {
    Developer dev1 = new Developer("Solomon Haynes", 100135, true);
    Developer dev2 = new Developer("Joe Smith", 100375, false);
    Developer dev3 = new Developer("Marty McFly", 103472, true);
    Developer dev4 = new Developer("Peter Pan", 123492, false);
    Developer dev5 = new Developer("Michael Jackson", 339193, false);

    _devRepo.AddDeveloperToList(dev1);
    _devRepo.AddDeveloperToList(dev2);
    _devRepo.AddDeveloperToList(dev3);
    _devRepo.AddDeveloperToList(dev4);
    _devRepo.AddDeveloperToList(dev5);

    List<Developer> list1 = new List<Developer>{dev1, dev2, dev3};
    List<Developer> list2 = new List<Developer>{dev3, dev4, dev5};
    

    DevTeam team1 = new DevTeam(list1, "Team Alpha", 10172);
    DevTeam team2 = new DevTeam(list2, "Team Beta", 04372);


    _teamRepo.AddDevTeamToList(team1);
    _teamRepo.AddDevTeamToList(team2);

  }
}
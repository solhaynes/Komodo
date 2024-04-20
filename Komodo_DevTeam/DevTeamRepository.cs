namespace DevTeam.Repository;

public class DevTeamRepository 
{
  private List<DevTeam> _listOfDevTeams = new List<DevTeam>();

  // Create
  public void AddDevTeamToList(DevTeam team)
  {
    _listOfDevTeams.Add(team);
  }

  // Read
  public List<DevTeam> GetListOfDevTeams()
  {
    return new List<DevTeam>(_listOfDevTeams);
  }

  // Update
  public bool UpdateExistingDevTeam(int originalId, DevTeam newTeamContent)
  {
    // Find the team by ID
    DevTeam oldDevTeam = GetDevTeamByID(originalId);

    // Update the team content
    if (oldDevTeam != null)
    {
      oldDevTeam.DeveloperList = newTeamContent.DeveloperList;
      oldDevTeam.TeamName = newTeamContent.TeamName;
      oldDevTeam.TeamID = newTeamContent.TeamID;

      return true;
    }
    else
    {
      return false;
    }
  }

  // Delete
  public bool RemoveTeamFromList(int id)
  {
    DevTeam team = GetDevTeamByID(id);

    if (team == null)
    {
      return false;
    }

    int initialCount = _listOfDevTeams.Count;
    _listOfDevTeams.Remove(team);

    if (initialCount > _listOfDevTeams.Count)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  // Helper
  public DevTeam GetDevTeamByID(int id)
  {
    foreach(DevTeam team in _listOfDevTeams)
    {
      if(team.ID == id)
      {
        return team;
      }
    }
    return null;
  }
}
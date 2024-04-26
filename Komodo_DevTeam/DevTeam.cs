namespace Repository;

public class DevTeam
{ 
  public List<Developer> DeveloperList { get; set; }
  public string TeamName { get; set; }
  public int TeamID { get; set; }

  public DevTeam(){}

  // POCO
  public DevTeam(List<Developer> devList, string teamName, int teamID)
  {
    DeveloperList = devList;
    TeamName = teamName;
    TeamID = teamID;
  }
}



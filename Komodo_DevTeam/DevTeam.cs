namespace Developer.Repository;
namespace DevTeam.Repository;

public class DevTeam
{ 
  public List<Developer> DeveloperList { get; set; }
  public string TeamName { get; set; }
  public int TeamID { get; set; }

  // POCO
  public DevTeam(List<Developer> devList, string teamName, int teamID)
  {
    DeveloperList = devList;
    TeamName = teamName;
    TeamID = teamID;
  }
}



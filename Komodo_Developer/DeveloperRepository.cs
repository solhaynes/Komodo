namespace Developer.Repository;

public class DeveloperRepository
{
  private List<Developer> _developerList = new List<Developer>();

  // Create
  public void AddDeveloperToList(Developer dev)
  {
    _developerList.Add(dev);
  }

  // Read
  public List<Developer> GetDeveloperList()
  {
    return new List<Developer>(_developerList);
  }

  // Update
  public bool UpdateDeveloperList(int originalID, Developer newDev)
  {
    //Find the developer
    Developer oldDevContent = GetDeveloperByID(originalID);

    if (oldDevContent != null)
    {
      oldDevContent.Name = newDev.Name;
      oldDevContent.ID = newDev.ID;
      oldDevContent.HasAccessToPluralsight = newDev.HasAccessToPluralsight;

      return true;
    }
    else
    {
      return false;
    }
  }

  // Delete
  public bool RemoveDeveloperFromList(int id)
  {
    Developer dev = GetDeveloperByID(id);

    if (dev == null)
    {
      return false;
    }

    int initialCount = _developerList.Count;
    _developerList.Remove(dev);

    if (initialCount > _developerList.Count)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  //Helper method for update
  public Developer GetDeveloperByID(int id)
  {
    foreach(Developer developer in _developerList)
    {
      if(developer.ID == id)
      {
        return developer;
      }
    }
    return null;
  }
}
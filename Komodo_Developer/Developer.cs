﻿namespace Repository;

public class Developer
{
  public string Name { get; set; }
  public int ID { get; set; }
  public bool HasAccessToPluralsight { get; set; } 


  public Developer(){}
  // Plain old POCO
  public Developer(string name, int id, bool hasAccess)
  {
    Name = name;
    ID = id;
    HasAccessToPluralsight = hasAccess; 
  }
}
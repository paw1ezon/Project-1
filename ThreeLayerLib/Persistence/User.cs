namespace Persistence;

public class User
{
    public int ID{get;set;}
    public string UserName{get;set;} = "default";
    public string Password{get;set;} = "default";
    public int Role{get;set;}
}
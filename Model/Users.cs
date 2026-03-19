public enum Authorization
{
    Admin, //0
    Dev //1
}
public abstract class User
{
    public int Id{get;set;}
    public string Name{get;set;}
    public Authorization Acces{get;set;}
}
public class Developer : User
{
    public Developer(string name) : base()
    {
        Name = name;
        Acces = Authorization.Dev;
    }
}
public class Admin : User
{
    // public int Key{get;}
    public Admin() : base()
    {
        Acces = Authorization.Admin;
        // Key = 0;
    }
}
// public class Developer : IDeveloper
// {
//     private class Admin : IAdminPrivileges
//     {
//         public int Key { get; set; }
//         public Authorization Rights { get; set; }
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public Admin(int key)
//         {
//             Key = key;
//             Name = "Admin";
//         }
//     }
//     public Authorization Rights { get; set; }
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public Developer(string name)
//     {
//         Name = name;
//     }
// }

// public class Admin : IAdminPrivileges
// {
//     public Admin(int key)
//     {
//         Rights = Authorization.Admin;
//         Name = "Admin";
//         Key = key;
//     }

//     public int Key { get; set; }
//     public Authorization Rights { get; set; }

//     public int Id { get; set; }
//     public string Name { get; set; }
// }

// public class Developer : IDeveloper
// {
//     public Developer(string name)
//     {
//         Rights = Authorization.Dev;
//         Name = name;
//     }

//     public Authorization Rights { get; set; }

//     public int Id { get; set; }
//     public string Name { get; set; }
// }
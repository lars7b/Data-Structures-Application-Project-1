public interface IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IDeveloper : IUser
{
    public Authorization Rights { get; set; }
}

public interface IAdminPrivileges : IDeveloper
{
    public int Key { get; set; }
}
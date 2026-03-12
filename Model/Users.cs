public enum Authorization
{
    Admin, //0
    Dev //1
}
public class Admin : IAdminPrivileges
{
    private Authorization _rights;

    public int Key { get; set; }
    public Authorization Rights { get => _rights; set => value = Authorization.Admin; }
    public int Id { get; set; }
    public string Name { get; set; }
    public Admin(string name, int key)
    {
        Name = name;
        Key = key;
    }
}
public class Developer : IDeveloper
{
    private Authorization _rights;
    public Authorization Rights { get => _rights; set => value = Authorization.Dev; }
    public int Id { get; set; }
    public string Name { get; set; }
    public Developer(string name)
    {
        Name = name;
    }
}
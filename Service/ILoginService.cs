using Project_1.Model;

namespace Project_1.Service;
public interface ILoginService
{
    public User? CurrentUser{get;set;}
    public bool Login(string name);
    public bool Logout();
    public Access CheckRoles();
    public bool CompareNames(string name);
}
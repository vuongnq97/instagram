using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.IService
{
    public class UserService : IUserService
    {
        private MyDatabaseContext _context;
        public UserService(MyDatabaseContext context)
        {
            _context = context;
        }
        public ResponseModel DeleteUser(int employeeId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                User _temp = GetEmployeeDetailsById(employeeId);
                if (_temp != null)
                {
                    _context.Remove<User>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Messsage = "User Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "User Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public User GetEmployeeDetailsById(int empId)
        {
            User emp;
            try
            {
                emp = _context.Find<User>(empId);
            }
            catch (Exception)
            {
                throw;
            }
            return emp;
        }

        public List<User> GetEmployeesList()
        {
            List<User> empList;
            try
            {
                empList = _context.Set<User>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return empList;

        }

        public ResponseModel SaveEmployee(User employeeModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                User _temp = GetEmployeeDetailsById(employeeModel.Id);
                if (_temp != null)
                {
                    _temp.FirstName = employeeModel.FirstName;
                    _temp.LastName = employeeModel.LastName;
                    _temp.Age = employeeModel.Age;
                    model.Messsage = "User Update Successfully";
                }
                else
                {
                    _context.Add<User>(employeeModel);
                    model.Messsage = "User Inserted Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }
    }
}

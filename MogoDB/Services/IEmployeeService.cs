using MogoDB.Models;
using MongoDB.Driver;

namespace MogoDB.Services
{
    public interface IEmployeeService
    {
        List<Employee> Get();
        Employee Get(string id);
        Employee Create(Employee student);
        void Update(string id, Employee student);
        void Remove(string id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IMongoCollection<Employee> _students;

        public EmployeeService(IEmployeeDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Employee>(settings.EmployeesCollectionName);
        }

        public Employee Create(Employee student)
        {
            _students.InsertOne(student);
            return student;
        }

        public List<Employee> Get()
        {
            return _students.Find(student => true).ToList();
        }

        public Employee Get(string id)
        {
            return _students.Find(student => student.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _students.DeleteOne(student => student.Id == id);
        }

        public void Update(string id, Employee student)
        {
            _students.ReplaceOne(student => student.Id == id, student);
        }
    }
}

namespace WebStore.ViewModels
{
    //Обычно ViewModel не отличается от Models
    public class EmployeeViewModel
    {
        public int Id { get; init; }

        public string LastName { get; init; }

        public string Name { get; init; }

        public string MiddleName { get; init; }

        public int Age { get; init; }
    }
}

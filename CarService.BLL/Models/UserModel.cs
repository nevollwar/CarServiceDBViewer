namespace CarService.BLL.Models
{
    /// <summary>
    /// Модель пользователя системы.
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

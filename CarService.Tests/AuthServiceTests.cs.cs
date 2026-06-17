using System;
using NUnit.Framework;
using CarService.BLL.Services;

namespace CarService.Tests
{
    /// <summary>
    /// Класс модульных тестов для проверки бизнес-логики безопасности (AuthService).
    /// Выполнен с использованием фреймворка NUnit 3 в соответствии с ГОСТ.
    /// </summary>
    [TestFixture]
    public class AuthServiceTests
    {
        // Тесты сложности паролей

        [Test]
        public void IsPasswordStrong_CorrectPassword_ReturnsTrue()
        {
            // Arrange (Подготовка)
            string strongPassword = "MySecurePassword123";

            // Act (Действие)
            bool result = AuthService.IsPasswordStrong(strongPassword);

            // Assert (Проверка)
            Assert.IsTrue(result); // Надежный пароль должен вернуть true
        }

        [Test]
        public void IsPasswordStrong_ShortPassword_ReturnsFalse()
        {
            // Arrange
            string shortPassword = "P1as"; // Менее 8 символов

            // Act
            bool result = AuthService.IsPasswordStrong(shortPassword);

            // Assert
            Assert.IsFalse(result); // Слишком короткий пароль должен быть отклонен
        }

        [Test]
        public void IsPasswordStrong_NoDigits_ReturnsFalse()
        {
            // Arrange
            string noDigitsPassword = "OnlyLettersPassword"; // Нет цифр

            // Act
            bool result = AuthService.IsPasswordStrong(noDigitsPassword);

            // Assert
            Assert.IsFalse(result); // Без цифр пароль считается слабым
        }

        [Test]
        public void IsPasswordStrong_NoUpperLetters_ReturnsFalse()
        {
            // Arrange
            string noUpperPassword = "onlylowerletters123"; // Нет заглавных букв

            // Act
            bool result = AuthService.IsPasswordStrong(noUpperPassword);

            // Assert
            Assert.IsFalse(result); // Без заглавных букв пароль считается слабым
        }

        // Тесты хеширования

        [Test]
        public void HashPassword_Deterministic_ReturnsSameHash()
        {
            // Arrange
            string password = "TestPassword_2026";

            // Act
            string hash1 = AuthService.HashPassword(password);
            string hash2 = AuthService.HashPassword(password);

            // Assert
            Assert.AreEqual(hash1, hash2); // Хеширование одного пароля должно давать одинаковый результат
            Assert.AreEqual(64, hash1.Length); // Длина SHA-256 хеша всегда должна быть ровно 64 символа
        }

        [Test]
        public void HashPassword_Unique_ReturnsDifferentHashes()
        {
            // Arrange
            string password1 = "PasswordNumberOne";
            string password2 = "PasswordNumberTwo";

            // Act
            string hash1 = AuthService.HashPassword(password1);
            string hash2 = AuthService.HashPassword(password2);

            // Assert
            Assert.AreNotEqual(hash1, hash2); // Разные пароли должны давать разные хеши
        }
    }
}
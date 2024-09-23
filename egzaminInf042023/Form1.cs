using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace egzaminInf042023
{
    // Główna klasa formularza aplikacji Windows Forms
    public partial class Form1 : Form
    {
        // Zmienna do przechowywania wygenerowanego hasła
        private string generatedPassword = "";

        // Konstruktor klasy Form1, wywołuje metodę InitializeComponent, która tworzy elementy GUI
        public Form1()
        {
            InitializeComponent();
        }

        // Metoda obsługi zdarzenia po kliknięciu przycisku "Generuj hasło"
        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            // Pobieranie długości hasła z pola tekstowego
            int passwordLength = int.Parse(txtPasswordLength.Text);

            // Sprawdzanie, które checkboxy są zaznaczone
            bool useUppercase = chkUppercase.Checked;
            bool useDigits = chkDigits.Checked;
            bool useSpecialChars = chkSpecialChars.Checked;

            // Wywołanie funkcji generującej hasło z odpowiednimi opcjami
            generatedPassword = GeneratePassword(passwordLength, useUppercase, useDigits, useSpecialChars);

            // Wyświetlenie wygenerowanego hasła w oknie dialogowym
            MessageBox.Show($"Wygenerowane hasło: {generatedPassword}");
        }

        // Funkcja generująca hasło na podstawie zadanych parametrów
        private string GeneratePassword(int length, bool useUppercase, bool useDigits, bool useSpecialChars)
        {
            // Zestaw małych liter, który jest domyślnie używany
            string lowercase = "abcdefghijklmnopqrstuvwxyz";

            // Zestaw wielkich liter, cyfr i znaków specjalnych
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            string specialChars = "!@#$%^&*()_+-=";

            // Początkowy zestaw dozwolonych znaków obejmuje tylko małe litery
            string allowedChars = lowercase;

            // Lista przechowująca wygenerowane znaki hasła
            List<char> password = new List<char>();

            // Obiekt Random do losowania znaków
            Random random = new Random();

            // Jeśli zaznaczono checkbox wielkich liter, dodaj wielkie litery do dozwolonych znaków
            // i dodaj losową wielką literę do hasła
            if (useUppercase)
            {
                allowedChars += uppercase;
                password.Add(uppercase[random.Next(uppercase.Length)]);
            }

            // Jeśli zaznaczono checkbox cyfr, dodaj cyfry do dozwolonych znaków
            // i dodaj losową cyfrę do hasła
            if (useDigits)
            {
                allowedChars += digits;
                password.Add(digits[random.Next(digits.Length)]);
            }

            // Jeśli zaznaczono checkbox znaków specjalnych, dodaj je do dozwolonych znaków
            // i dodaj losowy znak specjalny do hasła
            if (useSpecialChars)
            {
                allowedChars += specialChars;
                password.Add(specialChars[random.Next(specialChars.Length)]);
            }

            // Wypełnianie pozostałych miejsc w haśle losowymi znakami z allowedChars
            while (password.Count < length)
            {
                password.Add(allowedChars[random.Next(allowedChars.Length)]);
            }

            // Przetasowanie hasła, aby znaki były rozmieszczone losowo
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }

        // Metoda obsługi zdarzenia po kliknięciu przycisku "Zatwierdź"
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Pobieranie danych pracownika z pól tekstowych i combo box
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            string position = comboBoxPosition.SelectedItem.ToString();

            // Wyświetlanie danych pracownika oraz wygenerowanego hasła w oknie dialogowym
            MessageBox.Show($"Dane pracownika:{employeeName} {employeeSurname} {position} Hasło:  {generatedPassword}");
        }
    }
}

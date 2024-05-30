using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TLC_Dolzhkovyi
{
    // Створюємо структуру пасажира
    public struct Passenger
    {
        public string Surname;
        public string Name;
        public string Patronymic;
        public string Destination;
        public int CarriageNumber;
        public int SeatNumber;
        public int NumberOfItems;
        public double TotalWeight;
    }
    public partial class TLC : Form
    {
        public TLC()
        {
            InitializeComponent();
            // Приховуємо рядок зліва
            SetReadOnlyColumns(dgvAddPassenger);
            SetReadOnlyColumns(dgvSearchPassenger);
            SetRowHeadersVisibility();
            DisableAddingRows();
        }
        private bool isRowHeadersVisible = false;
        private void SetReadOnlyColumns(DataGridView dgv)
        {
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.ReadOnly = true;
            }
        }
        List<Passenger> originalPassengers = new List<Passenger>();
        List<Passenger> passengers = new List<Passenger>();
        private void SetRowHeadersVisibility()
        {
            dgvAddPassenger.RowHeadersVisible = isRowHeadersVisible;
            dgvSearchPassenger.RowHeadersVisible = isRowHeadersVisible;
        }

        private void DisableAddingRows()
        {
            dgvAddPassenger.AllowUserToAddRows = false;
            dgvSearchPassenger.AllowUserToAddRows = false;
        }
        //Додавання відомостей
        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            Passenger passenger = CreatePassenger();
            AddPassengerToGrid(passenger);
            ClearTextBoxes();

        }
        //Валідація
        private bool ValidateInputs()
        {
            if (!ValidateSurname() || !ValidateName() || !ValidatePatronymic() || !ValidateDestinationStation() || !ValidateCarriageNumber() || !ValidateSeatNumber() || !ValidateNumberOfItems() || !ValidateTotalWeight())
            {
                return false;
            }

            return true;
        }
        private bool ValidateSurname()
        {
            surnametxt.Text = surnametxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(surnametxt.Text))
            {
                MessageBox.Show("Введіть прізвище", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!Regex.IsMatch(surnametxt.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$"))
            {
                MessageBox.Show("Некоректне прізвище", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool ValidateName()
        {
            nametxt.Text = nametxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(nametxt.Text))
            {
                MessageBox.Show("Введіть ім'я", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!Regex.IsMatch(nametxt.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$"))
            {
                MessageBox.Show("Некоректне ім'я", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool ValidatePatronymic()
        {
            patronymictxt.Text = patronymictxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(patronymictxt.Text))
            {
                MessageBox.Show("Введіть ім'я по-батькові", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!Regex.IsMatch(patronymictxt.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$"))
            {
                MessageBox.Show("Некоректне ім'я по-батькові", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool ValidateDestinationStation()
        {
            destinationStationtxt.Text = destinationStationtxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(destinationStationtxt.Text))
            {
                MessageBox.Show("Введіть станцію призначення", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!Regex.IsMatch(destinationStationtxt.Text, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$"))
            {
                MessageBox.Show("Некоректна станція призначення", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateCarriageNumber()
        {
            CarriageNumbertxt.Text = CarriageNumbertxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(CarriageNumbertxt.Text))
            {
                MessageBox.Show("Введіть номер вагона", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!int.TryParse(CarriageNumbertxt.Text, out int carriageNumber))
            {
                MessageBox.Show("Некоректний номер вагона, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateSeatNumber()
        {
            SeatNumbertxt.Text = SeatNumbertxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(SeatNumbertxt.Text))
            {
                MessageBox.Show("Введіть номер місця", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!int.TryParse(SeatNumbertxt.Text, out int seatNumber))
            {
                MessageBox.Show("Некоректний номер місця, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateNumberOfItems()
        {
            numberOfItemstxt.Text = numberOfItemstxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(numberOfItemstxt.Text))
            {
                MessageBox.Show("Введіть кількість речей", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!int.TryParse(numberOfItemstxt.Text, out int numberOfItems))
            {
                MessageBox.Show("Некоректна кількість речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ValidateTotalWeight()
        {
            TotalWeighttxt.Text = TotalWeighttxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(TotalWeighttxt.Text))
            {
                MessageBox.Show("Введіть загальну вагу речей", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!double.TryParse(TotalWeighttxt.Text, out double totalWeight))
            {
                MessageBox.Show("Некоректна загальна вага речей, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private Passenger CreatePassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Surname = surnametxt.Text;
            passenger.Name = nametxt.Text;
            passenger.Patronymic = patronymictxt.Text;
            passenger.Destination = destinationStationtxt.Text;
            passenger.CarriageNumber = int.Parse(CarriageNumbertxt.Text);
            passenger.SeatNumber = int.Parse(SeatNumbertxt.Text);
            passenger.NumberOfItems = int.Parse(numberOfItemstxt.Text);
            passenger.TotalWeight = double.Parse(TotalWeighttxt.Text);

            return passenger;
        }
        private void AddPassengerToGrid(Passenger passenger)
        {
            foreach (DataGridViewRow row in dgvAddPassenger.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                if (row.Cells[0].Value.ToString() == passenger.Surname &&
                    row.Cells[1].Value.ToString() == passenger.Name &&
                     row.Cells[2].Value.ToString() == passenger.Patronymic &&
                      row.Cells[3].Value.ToString() == passenger.Destination &&
                    int.Parse(row.Cells[4].Value.ToString()) == passenger.CarriageNumber &&
                    int.Parse(row.Cells[5].Value.ToString()) == passenger.SeatNumber &&
                    int.Parse(row.Cells[6].Value.ToString()) == passenger.NumberOfItems &&
                    double.Parse(row.Cells[7].Value.ToString()) == passenger.TotalWeight)
                {
                    MessageBox.Show("Такий запис вже існує", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            int n = dgvAddPassenger.Rows.Add();
            dgvAddPassenger.Rows[n].Cells[0].Value = passenger.Surname;
            dgvAddPassenger.Rows[n].Cells[1].Value = passenger.Name;
            dgvAddPassenger.Rows[n].Cells[2].Value = passenger.Patronymic;
            dgvAddPassenger.Rows[n].Cells[3].Value = passenger.Destination;
            dgvAddPassenger.Rows[n].Cells[4].Value = passenger.CarriageNumber;
            dgvAddPassenger.Rows[n].Cells[5].Value = passenger.SeatNumber;
            dgvAddPassenger.Rows[n].Cells[6].Value = passenger.NumberOfItems;
            dgvAddPassenger.Rows[n].Cells[7].Value = passenger.TotalWeight;
            // Виконуємо сортування вставками
            InsertionSortDataGridView(dgvAddPassenger, 0);
        }
        // Метод для сортування вставками DataGridView
        private void InsertionSortDataGridView(DataGridView dgv, int columnIndex)
        {
            for (int i = 1; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow key = dgv.Rows[i];
                int j = i - 1;

                // Переміщуємо рядки dgv[0..i-1], які є більшими за ключ, на одну позицію вперед
                while (j >= 0 && string.Compare(dgv.Rows[j].Cells[columnIndex].Value.ToString(), key.Cells[columnIndex].Value.ToString()) > 0)
                {
                    dgv.Rows.RemoveAt(j + 1);
                    dgv.Rows.Insert(j, key);
                    j = j - 1;
                }
            }
        }
        //метод очищення текстових полів
        private void ClearTextBoxes()
        {
            surnametxt.Clear();
            nametxt.Clear();
            patronymictxt.Clear();
            destinationStationtxt.Clear();
            CarriageNumbertxt.Clear();
            SeatNumbertxt.Clear();
            numberOfItemstxt.Clear();
            TotalWeighttxt.Clear();
        }
        //кнопка видалення рядку
        private void delButton_Click(object sender, EventArgs e)
        {
            if (isRowHeadersVisible)
            {
                DeleteSelectedRow();
            }
            else
            {
                ShowRowSelectionMessage();
            }
        }

        private void DeleteSelectedRow()
        {
            if (dgvAddPassenger.SelectedRows.Count > 0)
            {
                dgvAddPassenger.Rows.RemoveAt(dgvAddPassenger.SelectedRows[0].Index);
                MessageBox.Show("Рядок видалено", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ToggleRowHeadersVisibility();
            }
        }
        private void ShowRowSelectionMessage()
        {
            MessageBox.Show("Оберіть рядок для видалення", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ToggleRowHeadersVisibility();
        }
        private void ToggleRowHeadersVisibility()
        {
            isRowHeadersVisible = !isRowHeadersVisible;
            dgvAddPassenger.RowHeadersVisible = isRowHeadersVisible;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            ToggleEditMode();

            if (isRowHeadersVisible)
            {
                EditSelectedRow();
            }
            else
            {
                ShowRowSelectionMessageForEdit();
            }

        }
        private bool editMode = false; // Початково режим редагування вимкнено
        private void ToggleEditMode()
        {
            // Включення/вимкнення режиму редагування
            editMode = !editMode;

            if (editMode)
            {
                // Включення обробника подій для dataGridView1_SelectionChanged
                dgvAddPassenger.SelectionChanged += dataGridView1_SelectionChanged;
            }
            else
            {
                // Вимкнення обробника подій для dataGridView1_SelectionChanged
                dgvAddPassenger.SelectionChanged -= dataGridView1_SelectionChanged;
            }
        }
        private Dictionary<string, string> originalData = new Dictionary<string, string>();
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!editMode) // Якщо не в режимі редагування, просто поверніться
                return;

            {
                if (dgvAddPassenger.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvAddPassenger.SelectedRows[0];
                    // Зберігаємо оригінальні дані
                    originalData["Прізвище"] = selectedRow.Cells[0].Value.ToString();
                    originalData["Ім'я"] = selectedRow.Cells[1].Value.ToString();
                    originalData["По-батькові"] = selectedRow.Cells[2].Value.ToString();
                    originalData["Станція призначення"] = selectedRow.Cells[3].Value.ToString();
                    originalData["№ вагона"] = selectedRow.Cells[4].Value.ToString();
                    originalData["№ місця"] = selectedRow.Cells[5].Value.ToString();
                    originalData["Кількість речей"] = selectedRow.Cells[6].Value.ToString();
                    originalData["Загальна вага речей"] = selectedRow.Cells[7].Value.ToString();

                    surnametxt.Text = selectedRow.Cells[0].Value.ToString();
                    nametxt.Text = selectedRow.Cells[1].Value.ToString();
                    patronymictxt.Text = selectedRow.Cells[2].Value.ToString();
                    destinationStationtxt.Text = selectedRow.Cells[3].Value.ToString();
                    CarriageNumbertxt.Text = selectedRow.Cells[4].Value.ToString();
                    SeatNumbertxt.Text = selectedRow.Cells[5].Value.ToString();
                    numberOfItemstxt.Text = selectedRow.Cells[6].Value.ToString();
                    TotalWeighttxt.Text = selectedRow.Cells[7].Value.ToString();
                }
            }
        }
        //метод відміни дії в випадку не валідності
        private void CancelEdit()
        {
            if (dgvAddPassenger.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAddPassenger.SelectedRows[0];
                // Відновлюємо оригінальні дані
                selectedRow.Cells[0].Value = originalData["Прізвище"];
                selectedRow.Cells[1].Value = originalData["Ім'я"];
                selectedRow.Cells[2].Value = originalData["По-батькові"];
                selectedRow.Cells[3].Value = originalData["Станція призначення"];
                selectedRow.Cells[4].Value = originalData["№ вагона"];
                selectedRow.Cells[5].Value = originalData["№ місця"];
                selectedRow.Cells[6].Value = originalData["Кількість речей"];
                selectedRow.Cells[7].Value = originalData["Загальна вага речей"];
            }
        }
        private void ShowRowSelectionMessageForEdit()
        {
            MessageBox.Show("Оберіть рядок для редагування", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ToggleRowHeadersVisibility();
        }
        private void EditSelectedRow()
        {
            if (dgvAddPassenger.SelectedRows.Count > 0)
            {
                int n = dgvAddPassenger.SelectedRows[0].Index;
                UpdateRowIfNotEmpty(n);
                if (!IsDuplicateEntry())
                {
                    MessageBox.Show("Редагування завершено", "Редагування", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ClearTextBoxes();
                ToggleRowHeadersVisibility();
                dgvAddPassenger.ClearSelection(); // Скидання виділення
            }
        }
        private void UpdateCellIfNotEmpty(int rowIndex, int cellIndex, TextBox textBox, string pattern, string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (!Regex.IsMatch(textBox.Text, pattern))
                {
                    MessageBox.Show(errorMessage, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvAddPassenger.Rows[rowIndex].Cells[cellIndex].Value = textBox.Text;
            }
        }
        //Перевірка на дублікат
        private bool IsDuplicateEntry()
        {
            for (int i = 0; i < dgvAddPassenger.Rows.Count; i++)
            {
                if (surnametxt.Text == dgvAddPassenger.Rows[i].Cells[0].Value.ToString() &&
                    nametxt.Text == dgvAddPassenger.Rows[i].Cells[1].Value.ToString() &&
                    patronymictxt.Text == dgvAddPassenger.Rows[i].Cells[2].Value.ToString() &&
                    destinationStationtxt.Text == dgvAddPassenger.Rows[i].Cells[3].Value.ToString() &&
                    CarriageNumbertxt.Text == dgvAddPassenger.Rows[i].Cells[4].Value.ToString() &&
                    SeatNumbertxt.Text == dgvAddPassenger.Rows[i].Cells[5].Value.ToString() &&
                    numberOfItemstxt.Text == dgvAddPassenger.Rows[i].Cells[6].Value.ToString() &&
                    TotalWeighttxt.Text == dgvAddPassenger.Rows[i].Cells[7].Value.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        private void UpdateRowIfNotEmpty(int rowIndex)
        {
            if (IsDuplicateEntry())
            {
                MessageBox.Show("Дублікат запису \nРедагування відмінено", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CancelEdit(); // Відміна редагування
                return;
            }
            // Перевірка та оновлення прізвища та ініціалів, якщо поле не порожнє
            UpdateCellIfNotEmpty(rowIndex, 0, surnametxt, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$", "Некоректне прізвище");
            UpdateCellIfNotEmpty(rowIndex, 1, nametxt, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$", "Некоректне ім'я");
            UpdateCellIfNotEmpty(rowIndex, 2, patronymictxt, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$", "Некоректне ім'я по-батькові");
            // Перевірка та оновлення станції призначення, якщо поле не порожнє
            UpdateCellIfNotEmpty(rowIndex, 3, destinationStationtxt, @"^[a-zA-Zа-яА-ЯїЇіІєЄ\s\.`'’]*$", "Некоректна станція призначення");
            // Перевірка та оновлення номера вагона, якщо поле не порожнє
            UpdateCellIfInt(rowIndex, 4, CarriageNumbertxt, "Некоректний номер вагона, введіть число");
            // Перевірка та оновлення номера місця, якщо поле не порожнє
            UpdateCellIfInt(rowIndex, 5, SeatNumbertxt, "Некоректний номер місця, введіть число");
            // Перевірка та оновлення кількості речей, якщо поле не порожнє
            UpdateCellIfInt(rowIndex, 6, numberOfItemstxt, "Некоректна кількість речей, введіть число");
            // Перевірка та оновлення загальної ваги речей, якщо поле не порожнє
            UpdateCellIfDouble(rowIndex, 7, TotalWeighttxt, "Некоректна загальна вага речей, введіть число");

        }

        private void UpdateCellIfInt(int rowIndex, int cellIndex, TextBox textBox, string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (!int.TryParse(textBox.Text, out int number))
                {
                    MessageBox.Show(errorMessage, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvAddPassenger.Rows[rowIndex].Cells[cellIndex].Value = number;
            }
        }
        //оновлюємо стовпець в випадку помилку
        private void UpdateCellIfDouble(int rowIndex, int cellIndex, TextBox textBox, string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (!double.TryParse(textBox.Text, out double number))
                {
                    MessageBox.Show(errorMessage, "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dgvAddPassenger.Rows[rowIndex].Cells[cellIndex].Value = number;
            }
        }
        //кнопка очищення
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (dgvAddPassenger.Rows.Count > 0)
            {
                dgvAddPassenger.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця порожня", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //кнопка виходу
        private void Exitbutton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Зберегти зміни?", "Вихід з програми", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    SaveChangesAndExit();
                    break;
                case DialogResult.No:
                    Application.Exit();
                    break;
                case DialogResult.Cancel:
                    // Відміна виходу
                    break;
            }
        }
        //метод зберегти зміни та вийти для кнопки виходу
        private void SaveChangesAndExit()
        {
            DataSet ds = new DataSet();
            DataTable dt = CreateDataTable();
            ds.Tables.Add(dt);
            AddRowsToDataTable(dt);
            ds.WriteXml("Passengers.xml");
            Application.Exit();
        }
        //створюємо таблицю з потрібними значеннями
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Passenger";
            dt.Columns.Add("Прізвище");
            dt.Columns.Add("Ім'я");
            dt.Columns.Add("По-батькові");
            dt.Columns.Add("Станція призначення");
            dt.Columns.Add("№ вагона");
            dt.Columns.Add("№ місця");
            dt.Columns.Add("Кількість речей");
            dt.Columns.Add("Загальна вага речей");

            return dt;
        }
        //кнопка збереження
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataSet ds = CreateDataSet();
                DataTable dt = CreateDataTable();
                ds.Tables.Add(dt);
                AddRowsToDataTable(dt);
                ds.WriteXml(filePath);
                MessageBox.Show("XML файл успішно збережений", "Виконано!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void AddRowsToDataTable(DataTable dt)
        {
            foreach (DataGridViewRow r in dgvAddPassenger.Rows)
            {
                DataRow row = dt.NewRow();
                row["Прізвище"] = r.Cells[0].Value;
                row["Ім'я"] = r.Cells[1].Value;
                row["По-батькові"] = r.Cells[2].Value;
                row["Станція призначення"] = r.Cells[3].Value;
                row["№ вагона"] = r.Cells[4].Value;
                row["№ місця"] = r.Cells[5].Value;
                row["Кількість речей"] = r.Cells[6].Value;
                row["Загальна вага речей"] = r.Cells[7].Value;
                dt.Rows.Add(row);
            }
        }

        private SaveFileDialog CreateSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;

            return saveFileDialog;
        }

        private DataSet CreateDataSet()
        {
            return new DataSet();
        }
        private void loadButton_Click(object sender, EventArgs e)
        {
            if (dgvAddPassenger.Rows.Count > 0)
            {
                MessageBox.Show("Очистіть поле перед завантаженням нового файла", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OpenFileDialog openFileDialog = CreateOpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        LoadFile(filePath);
                    }
                    else
                    {
                        MessageBox.Show("XML файл не знайдено", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private OpenFileDialog CreateOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.DefaultExt = "xml";

            return openFileDialog;
        }

        private void LoadFile(string filePath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            if (ds.Tables.Count == 0 || ds.Tables["Passenger"].Rows.Count == 0)
            {
                MessageBox.Show("Файл порожній", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddRowsToDataGridView(ds);
            MessageBox.Show("Файл завантажено", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void AddRowsToDataGridView(DataSet ds)
        {
            foreach (DataRow item in ds.Tables["Passenger"].Rows)
            {
                int n = dgvAddPassenger.Rows.Add();
                dgvAddPassenger.Rows[n].Cells[0].Value = item["Прізвище"];
                dgvAddPassenger.Rows[n].Cells[1].Value = item["Ім'я"];
                dgvAddPassenger.Rows[n].Cells[2].Value = item["По-батькові"];
                dgvAddPassenger.Rows[n].Cells[3].Value = item["Станція призначення"];
                dgvAddPassenger.Rows[n].Cells[4].Value = item["№ вагона"];
                dgvAddPassenger.Rows[n].Cells[5].Value = item["№ місця"];
                dgvAddPassenger.Rows[n].Cells[6].Value = item["Кількість речей"];
                dgvAddPassenger.Rows[n].Cells[7].Value = item["Загальна вага речей"];
                Passenger passenger = new Passenger
                {
                    Surname = item["Прізвище"].ToString(),
                    Name = item["Ім'я"].ToString(),
                    Patronymic = item["По-батькові"].ToString(),
                    Destination = item["Станція призначення"].ToString(),
                    CarriageNumber = int.Parse(item["№ вагона"].ToString()),
                    SeatNumber = int.Parse(item["№ місця"].ToString()),
                    NumberOfItems = int.Parse(item["Кількість речей"].ToString()),
                    TotalWeight = double.Parse(item["Загальна вага речей"].ToString())
                };
                originalPassengers.Add(passenger);
            }
            passengers = new List<Passenger>(originalPassengers);
        }

        private void load2dgv_Click(object sender, EventArgs e)
        {
            if (dgvSearchPassenger.Rows.Count > 0)
            {
                MessageBox.Show("Очистіть поле перед завантаженням нового файла", "Увага!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OpenFileDialog openFileDialog = CreateOpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        LoadFileToSearchGrid(filePath);
                    }
                    else
                    {
                        MessageBox.Show("XML файл не знайдено", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void LoadFileToSearchGrid(string filePath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(filePath);
            if (ds.Tables.Count == 0 || ds.Tables["Passenger"].Rows.Count == 0)
            {
                MessageBox.Show("Файл порожній", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddRowsToSearchDataGridView(ds);
            MessageBox.Show("Файл завантажено", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Додаємо всіх пасажирів до списку originalPassengers щоб зберегти дані та мати змогу повернутися до них після певних дій
            originalPassengers.Clear();
            foreach (DataRow item in ds.Tables["Passenger"].Rows)
            {
                Passenger passenger = new Passenger
                {
                    Surname = item["Прізвище"].ToString(),
                    Name = item["Ім'я"].ToString(),
                    Patronymic = item["По-батькові"].ToString(),
                    Destination = item["Станція призначення"].ToString(),
                    CarriageNumber = int.Parse(item["№ вагона"].ToString()),
                    SeatNumber = int.Parse(item["№ місця"].ToString()),
                    NumberOfItems = int.Parse(item["Кількість речей"].ToString()),
                    TotalWeight = double.Parse(item["Загальна вага речей"].ToString())
                };
                originalPassengers.Add(passenger);
            }
        }

        private void AddRowsToSearchDataGridView(DataSet ds)
        {
            foreach (DataRow item in ds.Tables["Passenger"].Rows)
            {
                int n = dgvSearchPassenger.Rows.Add();
                dgvSearchPassenger.Rows[n].Cells[0].Value = item["Прізвище"];
                dgvSearchPassenger.Rows[n].Cells[1].Value = item["Ім'я"];
                dgvSearchPassenger.Rows[n].Cells[2].Value = item["По-батькові"];
                dgvSearchPassenger.Rows[n].Cells[3].Value = item["Станція призначення"];
                dgvSearchPassenger.Rows[n].Cells[4].Value = item["№ вагона"];
                dgvSearchPassenger.Rows[n].Cells[5].Value = item["№ місця"];
                dgvSearchPassenger.Rows[n].Cells[6].Value = item["Кількість речей"];
                dgvSearchPassenger.Rows[n].Cells[7].Value = item["Загальна вага речей"];
            }
        }
        //Пошук відомостей за 1 з 3 варіантів вибору
        private void Searchbtn_Click(object sender, EventArgs e)
        {
            // Перевіряємо, чи вибрано елемент у ComboBox
            if (cmbSearchField.SelectedItem == null)
            {
                MessageBox.Show("Оберіть спосіб пошуку", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string searchField = cmbSearchField.SelectedItem.ToString(); // ComboBox з опціями "Прізвище", "Ім'я", "Станція призначення"
            string searchValue = txtSearchValue.Text;
            // Перевіряємо, чи введене значення відповідає одному з дозволених варіантів
            if (searchField != "Прізвище" && searchField != "Ім'я" && searchField != "Станція призначення")
            {
                MessageBox.Show("Будь ласка, оберіть один із запропонованих способів пошуку: Прізвище, Ім'я, Станція призначення", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Отримуємо дані з DataGridView
            List<Passenger> dgvData = new List<Passenger>();
            foreach (DataGridViewRow row in dgvSearchPassenger.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    dgvData.Add(new Passenger
                    {
                        Surname = row.Cells[0].Value.ToString(),
                        Name = row.Cells[1].Value.ToString(),
                        Patronymic = row.Cells[2].Value.ToString(),
                        Destination = row.Cells[3].Value.ToString(),
                        CarriageNumber = int.Parse(row.Cells[4].Value.ToString()),
                        SeatNumber = int.Parse(row.Cells[5].Value.ToString()),
                        NumberOfItems = int.Parse(row.Cells[6].Value.ToString()),
                        TotalWeight = double.Parse(row.Cells[7].Value.ToString())
                    });
                }
            }
      

        List<Passenger> sortedPassengers;
            List<Passenger> foundPassengers;

            switch (searchField)
            {
                case "Прізвище":
                    sortedPassengers = InsertionSort(dgvData, p => p.Surname);
                    foundPassengers = BinarySearch(sortedPassengers, p => p.Surname, searchValue);
                    break;
                case "Ім'я":
                    sortedPassengers = InsertionSort(dgvData, p => p.Name);
                    foundPassengers = BinarySearch(sortedPassengers, p => p.Name, searchValue);
                    break;
                case "Станція призначення":
                    sortedPassengers = InsertionSort(dgvData, p => p.Destination);
                    foundPassengers = BinarySearch(sortedPassengers, p => p.Destination, searchValue);
                    break;
                default:
                    MessageBox.Show("Невідоме поле для пошуку", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            if (foundPassengers.Count > 0)
            {
                dgvSearchPassenger.Rows.Clear();
                foreach (Passenger passenger in foundPassengers)
                {
                    dgvSearchPassenger.Rows.Add(passenger.Surname, passenger.Name, passenger.Patronymic, passenger.Destination, passenger.CarriageNumber.ToString(), passenger.SeatNumber.ToString(), passenger.NumberOfItems.ToString(), passenger.TotalWeight.ToString());
                }
                MessageBox.Show("Пошук відомості успішний", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Пасажира або станцію призначення не знайдено \n Перевірте чи коректно ви ввели дані", "Пошук", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<Passenger> InsertionSort(List<Passenger> passengers, Func<Passenger, string> keySelector)
        {
            for (int i = 1; i < passengers.Count; i++)
            {
                Passenger key = passengers[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(keySelector(passengers[j]), keySelector(key)) > 0)
                {
                    passengers[j + 1] = passengers[j];
                    j = j - 1;
                }
                passengers[j + 1] = key;
            }

            return passengers;
        }

        private List<Passenger> BinarySearch(List<Passenger> passengers, Func<Passenger, string> keySelector, string searchValue)
        {
            List<Passenger> foundPassengers = new List<Passenger>();
            int l = 0, r = passengers.Count - 1;
            while (l <= r)
            {
                int m = l + (r - l) / 2;

                int comparison = string.Compare(keySelector(passengers[m]), searchValue);
                if (comparison == 0)
                {
                    // Якщо знайдено співпадіння, додаємо пасажира до списку знайдених пасажирів
                    foundPassengers.Add(passengers[m]);

                    // Перевіряємо наявність інших співпадінь в списку
                    int leftIndex = m - 1;
                    while (leftIndex >= 0 && string.Compare(keySelector(passengers[leftIndex]), searchValue) == 0)
                    {
                        foundPassengers.Add(passengers[leftIndex]);
                        leftIndex--;
                    }

                    int rightIndex = m + 1;
                    while (rightIndex < passengers.Count && string.Compare(keySelector(passengers[rightIndex]), searchValue) == 0)
                    {
                        foundPassengers.Add(passengers[rightIndex]);
                        rightIndex++;
                    }

                    break;
                }

                if (comparison < 0)
                    l = m + 1;
                else
                    r = m - 1;
            }

            return foundPassengers;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            dgvSearchPassenger.Rows.Clear();
            foreach (Passenger passenger in originalPassengers)
            {
                dgvSearchPassenger.Rows.Add(passenger.Surname, passenger.Name,
                    passenger.Patronymic, passenger.Destination, passenger.CarriageNumber.ToString(), passenger.SeatNumber.ToString(),
                    passenger.NumberOfItems.ToString(), passenger.TotalWeight.ToString());
            }
        }

        private void IndividualSearch_Click(object sender, EventArgs e)
        {
            if (!Individual1rb.Checked && !Individual2rb.Checked && !Individual3rb.Checked)
            {
                MessageBox.Show("Оберіть варіант пошуку", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Конвертуємо дані з dataGridView в список
            List<Passenger> passengers = ConvertDataGridViewToPassengerList(dgvSearchPassenger);
            if (Individual1rb.Checked)
            {
                // Знаходимо топ-5 пасажирів за загальною вагою речей
                passengers = SortByTotalWeightDescending(passengers);
                var top5Passengers = passengers.Take(5).ToList();
                // тут сортування на прізвищем пасажирів
                top5Passengers = SortByNameAscending(top5Passengers);

                UpdateDataGridView(dgvSearchPassenger, top5Passengers);
            }
            else if (Individual2rb.Checked)
            {
                if (!int.TryParse(Individual2.Text, out int enteredValue))
                {
                    MessageBox.Show("Некоректне значення, введіть число", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Знаходимо пасажирів, у яких кількість речей більше або дорівнює введеного значення
                var filteredPassengers = FilterByNumberOfItems(passengers, enteredValue);
                filteredPassengers = SortByNumberOfItemsDescending(filteredPassengers);

                UpdateDataGridView(dgvSearchPassenger, filteredPassengers);
            }
            else if (Individual3rb.Checked)
            {
                var maxWeightDestination = FindMaxWeightDestination(passengers);

                // Оновлюємо дані в dataGridView, виділяючи тільки станцію призначення з найбільшою загальною вагою речей
                UpdateDataGridViewWithMaxWeight(dgvSearchPassenger, passengers, maxWeightDestination);
            }
        }
        private List<Passenger> ConvertDataGridViewToPassengerList(DataGridView dataGridView)
        {
            List<Passenger> passengers = new List<Passenger>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue; // Пропускаємо нові ряди
                Passenger passenger = new Passenger
                {
                    Surname = row.Cells[0].Value?.ToString(),
                    Name = row.Cells[1].Value?.ToString(),
                    Patronymic = row.Cells[2].Value?.ToString(),
                    Destination = row.Cells[3].Value?.ToString(),
                    CarriageNumber = int.Parse(row.Cells[4].Value?.ToString() ?? "0"),
                    SeatNumber = int.Parse(row.Cells[5].Value?.ToString() ?? "0"),
                    NumberOfItems = int.Parse(row.Cells[6].Value?.ToString() ?? "0"),
                    TotalWeight = double.Parse(row.Cells[7].Value?.ToString() ?? "0")
                };
                passengers.Add(passenger);
            }
            return passengers;
        }
        private List<Passenger> SortByTotalWeightDescending(List<Passenger> passengers)
        {
            for (int i = 1; i < passengers.Count; i++)
            {
                Passenger key = passengers[i];
                int j = i - 1;

                while (j >= 0 && passengers[j].TotalWeight < key.TotalWeight)
                {
                    passengers[j + 1] = passengers[j];
                    j--;
                }
                passengers[j + 1] = key;
            }
            return passengers;
        }
        private List<Passenger> SortByNameAscending(List<Passenger> passengers)
        {
            for (int i = 1; i < passengers.Count; i++)
            {
                Passenger key = passengers[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(passengers[j].Surname, key.Surname) > 0)
                {
                    passengers[j + 1] = passengers[j];
                    j--;
                }
                passengers[j + 1] = key;
            }
            return passengers;
        }
        private void UpdateDataGridView(DataGridView dataGridView, List<Passenger> passengers)
        {
            dataGridView.Rows.Clear();
            foreach (Passenger passenger in passengers)
            {
                int n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = passenger.Surname;
                dataGridView.Rows[n].Cells[1].Value = passenger.Name;
                dataGridView.Rows[n].Cells[2].Value = passenger.Patronymic;
                dataGridView.Rows[n].Cells[3].Value = passenger.Destination;
                dataGridView.Rows[n].Cells[4].Value = passenger.CarriageNumber;
                dataGridView.Rows[n].Cells[5].Value = passenger.SeatNumber;
                dataGridView.Rows[n].Cells[6].Value = passenger.NumberOfItems;
                dataGridView.Rows[n].Cells[7].Value = passenger.TotalWeight;
            }
        }
        private List<Passenger> FilterByNumberOfItems(List<Passenger> passengers, int minNumberOfItems)
        {
            List<Passenger> filteredPassengers = new List<Passenger>();
            foreach (var passenger in passengers)
            {
                if (passenger.NumberOfItems >= minNumberOfItems)
                {
                    filteredPassengers.Add(passenger);
                }
            }
            return filteredPassengers;
        }
        private List<Passenger> SortByNumberOfItemsDescending(List<Passenger> passengers)
        {
            for (int i = 1; i < passengers.Count; i++)
            {
                Passenger key = passengers[i];
                int j = i - 1;

                while (j >= 0 && passengers[j].NumberOfItems < key.NumberOfItems)
                {
                    passengers[j + 1] = passengers[j];
                    j--;
                }
                passengers[j + 1] = key;
            }
            return passengers;
        }
        private string FindMaxWeightDestination(List<Passenger> passengers)
        {
            Dictionary<string, double> destinationWeights = new Dictionary<string, double>();

            foreach (var passenger in passengers)
            {
                if (destinationWeights.ContainsKey(passenger.Destination))
                {
                    destinationWeights[passenger.Destination] += passenger.TotalWeight;
                }
                else
                {
                    destinationWeights[passenger.Destination] = passenger.TotalWeight;
                }
            }

            string maxWeightDestination = null;
            double maxWeight = double.MinValue;

            foreach (var destination in destinationWeights)
            {
                if (destination.Value > maxWeight)
                {
                    maxWeightDestination = destination.Key;
                    maxWeight = destination.Value;
                }
            }

            return maxWeightDestination;
        }
        private void UpdateDataGridViewWithMaxWeight(DataGridView dataGridView, List<Passenger> passengers, string maxWeightDestination)
        {
            List<Passenger> filteredPassengers = new List<Passenger>();
            double totalWeight = 0;

            foreach (Passenger passenger in passengers)
            {
                if (passenger.Destination == maxWeightDestination)
                {
                    filteredPassengers.Add(passenger);
                    totalWeight += passenger.TotalWeight;
                }
            }

            dataGridView.Rows.Clear();
            foreach (Passenger passenger in filteredPassengers)
            {
                int n = dataGridView.Rows.Add();
                dataGridView.Rows[n].Cells[0].Value = passenger.Surname;
                dataGridView.Rows[n].Cells[1].Value = passenger.Name;
                dataGridView.Rows[n].Cells[2].Value = passenger.Patronymic;
                dataGridView.Rows[n].Cells[3].Value = passenger.Destination;
                dataGridView.Rows[n].Cells[4].Value = passenger.CarriageNumber;
                dataGridView.Rows[n].Cells[5].Value = passenger.SeatNumber;
                dataGridView.Rows[n].Cells[6].Value = passenger.NumberOfItems;
                dataGridView.Rows[n].Cells[7].Value = passenger.TotalWeight;
            }
        }
        //Очищення таблиці
        private void Clear2btn_Click(object sender, EventArgs e)
        {
            if (dgvSearchPassenger.Rows.Count > 0)
            {
                dgvSearchPassenger.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Таблиця порожня", "Помилка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Приховуємо текстове поле до активації радіокнопки
        private void Individual2rb_CheckedChanged(object sender, EventArgs e)
        {
            Individual2.Visible = Individual2rb.Checked;
        }
        //Інформація для 1 радіокнопки про 1-е індивідуальне завдання
        private void wit1_Click(object sender, EventArgs e)
        {
        MessageBox.Show("Видача відомостей про п'ятьох пасажирів потягу  загальна вага речей в багажі яких є найбільшою", "Інформація", MessageBoxButtons.OK);
        }
        //Інформація для 2 радіокнопки про 2-е індивідуальне завдання
        private void wit2_Click(object sender, EventArgs e)
        {
        MessageBox.Show("Видача відомостей про пасажирів потягу багаж яких містить кількість речей більше введеного значення", "Інформація", MessageBoxButtons.OK);
        }
        //Інформація для 3 радіокнопки про 3-є індивідуальне завдання
        private void wit3_Click(object sender, EventArgs e)
        {
        MessageBox.Show("Визначення станції призначення з найбільшою загальною вагою речей", "Інформація", MessageBoxButtons.OK);
        }

        private void Exit2btn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Зберегти зміни?", "Вихід з програми", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    SaveChangesAndExit();
                    break;
                case DialogResult.No:
                    Application.Exit();
                    break;
                case DialogResult.Cancel:
                    // Відміна виходу
                    break;
            }
        }

        private void save2btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = CreateSaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                DataSet ds = CreateDataSet();
                DataTable dt = CreateDataTable();
                ds.Tables.Add(dt);
                AddRowsToDataTable(dt);
                ds.WriteXml(filePath);
                MessageBox.Show("XML файл успішно збережений", "Виконано!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
                var result = MessageBox.Show("Зберегти зміни?", "Вихід з програми", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveChangesAndExit();
                        break;
                    case DialogResult.No:
                        Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        // Відміна виходу
                        break;
                }
        }
    }
}

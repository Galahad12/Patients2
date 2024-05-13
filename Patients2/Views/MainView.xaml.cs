using Microsoft.EntityFrameworkCore;
using Patients2.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Patients2.Views
{
    public partial class MainView : Window
    {
        private int activeTable = 0;
        PatientsContext context;
        List<Patient> patients;
        List<BasicInputPatient> basicInput;
        List<BasicInputCalculation> basicInputCalc;
        public MainView()
        {
            InitializeComponent();
            context = new PatientsContext();
            patientsButton_Click(this, new RoutedEventArgs());
            this.PreviewKeyDown += HandleEnterKeyPress;
            tableDataGrid.SelectedItem = null;
            tableDataGrid.CellEditEnding += tableDataGrid_SellEditEnding;
        }

        private void tableDataGrid_SellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            switch(activeTable)
            {
                case 0:
                    var editedPatient = tableDataGrid.SelectedItem as Patient; // Замените YourModel на тип вашей модели данных
                    var conn = context.Patients.Where(c => c.Id == editedPatient.Id).FirstOrDefault();
                    if (conn == null)
                        context.Patients.Add(editedPatient);
                    else
                    {
                        conn.MedicalHistory = editedPatient.MedicalHistory;
                        conn.Fullname = editedPatient.Fullname;
                        conn.Age = editedPatient.Age;

                        conn.Sex = editedPatient.Sex;
                        conn.SocialStatus = editedPatient.SocialStatus;
                        conn.Location = editedPatient.Location;
                        context.Entry(conn).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                    break;
                case 1:
                    var editedBasicInput = tableDataGrid.SelectedItem as BasicInputPatient; // Замените YourModel на тип вашей модели данных
                    context.Entry(editedBasicInput).State = EntityState.Modified;
                    context.SaveChanges();
                    break;
                case 2:
                    var editedBasicInputCalc = tableDataGrid.SelectedItem as BasicInputCalculation; // Замените YourModel на тип вашей модели данных

                    context.Entry(editedBasicInputCalc).State = EntityState.Modified;
                    context.SaveChanges();
                    break;
            }
        }

        private void patientsButton_Click(object sender, RoutedEventArgs e)
        {
            activeTable = 0;
            context.Patients.Load();
            patients = context.Patients.Include(o => o.SexNavigation).Include(o => o.SocialStatusNavigation).Include(o => o.LocationNavigation).ToList();
            tableDataGrid.Columns.Clear();
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "№ истории болезни",
                Binding = new Binding("MedicalHistory")
            }) ;
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "ФИО пациента",
                Binding = new Binding("Fullname")
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Возраст",
                Binding = new Binding("Age")
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Пол",
                SelectedItemBinding = new Binding("SexNavigation"),
                DisplayMemberPath = "Sex1",
                ItemsSource = context.Sexes.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Социальный статус",
                SelectedItemBinding = new Binding("SocialStatusNavigation"),
                DisplayMemberPath = "Status",
                ItemsSource = context.SocialStatuses.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Место жительства",
                SelectedItemBinding = new Binding("LocationNavigation"),
                DisplayMemberPath = "Location1",
                ItemsSource = context.Locations.ToList()
            });
            tableDataGrid.ItemsSource = patients;
        }

        private void basicInputButton_Click(object sender, RoutedEventArgs e)
        {
            activeTable = 1;
            context.BasicInputPatients.Load();
            basicInput = context.BasicInputPatients.Include(o => o.PatientNavigation).Include(o => o.ComplaintsNavigation).
                Include(o => o.AvailabilityNavigation).Include(o => o.AffectedArteryNavigation).Include(o => o.MainDiagnosisNavigation).ToList();
            tableDataGrid.Columns.Clear();
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Пациент",
                SelectedItemBinding = new Binding("PatientNavigation"),
                DisplayMemberPath = "Fullname",
                ItemsSource = context.Patients.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Длина тела, см.",
                Binding = new Binding("Height")
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Систолическое артериальное давление (САД) при поступлении, мм. рт. ст.",
                Binding = new Binding("Sad")
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Диастолическое артериальное давление (ДАД) при поступлении, мм. рт. ст.",
                Binding = new Binding("Dad")
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Пульсовое давление при поступлении, мм. рт. ст.",
                Binding = new Binding("Pd")
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Частота сердечных сокращений (ЧСС) при поступлении, уд. / мин.",
                Binding = new Binding("Pd")
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Жалобы",
                SelectedItemBinding = new Binding("ComplaintsNavigation"),
                DisplayMemberPath = "Complaint1",
                ItemsSource = context.Complaints.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Наличие атеросклероза",
                SelectedItemBinding = new Binding("AvailabilityNavigation"),
                DisplayMemberPath = "Availability1",
                ItemsSource = context.Availabilities.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Вид пораженной артерии",
                SelectedItemBinding = new Binding("AffectedArteryNavigation"),
                DisplayMemberPath = "Artery1",
                ItemsSource = context.Arteries.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Основной диагноз",
                SelectedItemBinding = new Binding("MainDiagnosisNavigation"),
                DisplayMemberPath = "Diagnosis1",
                ItemsSource = context.Diagnoses.ToList()
            });
            tableDataGrid.ItemsSource = basicInput;
        }

        private void basicInputCalcButton_Click(object sender, RoutedEventArgs e)
        {
            activeTable = 2;
            context.BasicInputCalculations.Load();
            tableDataGrid.Columns.Clear();
            basicInputCalc = context.BasicInputCalculations.Include(o => o.PatientNavigation).Include(o => o.ImtMeanNavigation).ToList();
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "Пациент",
                SelectedItemBinding = new Binding("PatientNavigation"),
                DisplayMemberPath = "Fullname",
                ItemsSource = context.Patients.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Индекс массы тела (ИМТ)",
                Binding = new Binding("Imt")
            });
            tableDataGrid.Columns.Add(new DataGridComboBoxColumn
            {
                Header = "ИМТ расшифровка",
                SelectedItemBinding = new Binding("ImtMeanNavigation"),
                DisplayMemberPath = "Mean",
                ItemsSource = context.ImtMeans.ToList()
            });
            tableDataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Среднее артериальное давление при поступлении, мм. рт. ст.",
                Binding = new Binding("Avad")
            });
            tableDataGrid.ItemsSource = basicInputCalc;
        }

        private void HandleEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    switch (activeTable)
                    {
                        case 0:
                            patientsButton_Click(this, new RoutedEventArgs());
                            DeleteSelectedPatient();
                            break;
                        case 1:
                            basicInputButton_Click(this, new RoutedEventArgs());
                            DeleteSelectedBasicInput();
                            break;
                        case 2:
                            basicInputCalcButton_Click(this, new RoutedEventArgs());
                            DeleteSelectedBasicInputCalculation();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void DeleteSelectedPatient()
        {
            var selectedPatient = tableDataGrid.SelectedItem as Patient;
            if (selectedPatient != null)
            {
                var relatedBasicInputs = context.BasicInputPatients.Where(b => b.Patient == selectedPatient.Id);
                foreach (var basicInput in relatedBasicInputs)
                {
                    var relatedBasicInputCalcs = context.BasicInputCalculations.Where(bic => bic.Id == basicInput.Id);
                    foreach (var basicInputCalc in relatedBasicInputCalcs)
                    {
                        context.BasicInputCalculations.Remove(basicInputCalc);
                    }
                    context.BasicInputPatients.Remove(basicInput);
                }

                context.Patients.Remove(selectedPatient);
                patients.Remove(selectedPatient);
                context.SaveChanges();
            }
        }

        private void DeleteSelectedBasicInput()
        {
            var selectedBasicInput = tableDataGrid.SelectedItem as BasicInputPatient;
            if (selectedBasicInput != null)
            {
                var relatedBasicInputCalcs = context.BasicInputCalculations.Where(bic => bic.Id == selectedBasicInput.Id);
                foreach (var basicInputCalc in relatedBasicInputCalcs)
                {
                    context.BasicInputCalculations.Remove(basicInputCalc);
                }

                context.BasicInputPatients.Remove(selectedBasicInput);
                basicInput.Remove(selectedBasicInput);
                context.SaveChanges();
            }
        }

        private void DeleteSelectedBasicInputCalculation()
        {
            var selectedBasicInputCalc = tableDataGrid.SelectedItem as BasicInputCalculation;
            if (selectedBasicInputCalc != null)
            {
                context.BasicInputCalculations.Remove(selectedBasicInputCalc);
                basicInputCalc.Remove(selectedBasicInputCalc);
                context.SaveChanges();
            }
        }
    }
}

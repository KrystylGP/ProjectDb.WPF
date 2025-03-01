using Data.Business.Services;
using Data.Entities;
using System.Windows;

namespace ProjectDb;

public partial class EditProjectWindow : Window
{
    private readonly ProjectService _projectService;
    private ProjectEntity _project;

    public EditProjectWindow(ProjectService projectService, ProjectEntity project)
    {
        InitializeComponent();
        _projectService = projectService;
        _project = project;
        
        ProjectNumberTextBox.Text = _project.ProjectNumber.ToString();
        ProjectNumberTextBox.IsReadOnly = true; // Förhindra ändring av projektnummer. Stoppas även i backend.

        ProjectNameTextBox.Text = _project.ProjectName;
        StartDateTextBox.Text = _project.StartDate.ToString("yyyy-MM-dd");
        EndDateTextBox.Text = _project.EndDate.ToString("yyyy-MM-dd");
        ProjectManagerTextBox.Text = _project.ProjectManager;
        ServiceTextBox.Text = _project.Service;
        TotalPriceTextBox.Text = _project.TotalPrice?.ToString() ?? "";
        ProjectStatusComboBox.Text = _project.ProjectStatus;

    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _project.ProjectName = ProjectNameTextBox.Text;
            _project.StartDate = DateTime.Parse(StartDateTextBox.Text);
            _project.EndDate = DateTime.Parse(EndDateTextBox.Text);
            _project.ProjectManager = ProjectManagerTextBox.Text;
            _project.Service = ServiceTextBox.Text;
            _project.TotalPrice = int.TryParse(TotalPriceTextBox.Text, out int price) ? price : (int?)null;
            _project.ProjectStatus = ProjectStatusComboBox.Text;

            if (DateTime.TryParse(StartDateTextBox.Text, out DateTime startDate))
                _project.StartDate = startDate;
            if (DateTime.TryParse(EndDateTextBox.Text, out DateTime endDate))
                _project.EndDate = endDate;

            bool updated = _projectService.UpdateProject(_project);
            MessageBox.Show(updated ? "Projektet har uppdaterats!" : "Uppdateringen misslyckades.");
            
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fel vid uppdatering: {ex.Message}");
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

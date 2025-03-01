using Data.Business.Services;
using Data.Contexts;
using Data.Entities;
using Data.Migrations;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace ProjectDb;

public partial class MainWindow : Window
{
    private readonly ProjectService _projectService;
    private readonly CustomerService _customerService;

    public MainWindow()
    {
        InitializeComponent();

        // Hämta DataContext från DI-container
        var context = App.ServiceProvider.GetRequiredService<DataContext>();

        // Skapa repositories
        var projectRepository = new ProjectRepository(context);
        var customerRepository = new CustomerRepository(context);

        _projectService = new ProjectService(projectRepository);
        _customerService = new CustomerService(customerRepository);

        // Starta menyloopen
        ShowMenu();
    }

    private void ShowMenu()
    {
        while (true)
        {
            string? choice = Microsoft.VisualBasic.Interaction.InputBox(
                "1. Skapa en kund och projekt\n" +
                "2. Lista alla projekt\n" +
                "3. Uppdatera projekt\n" +
                "4. Ta bort projekt\n" +
                "5. Lista alla kunder\n" +
                "6. Avsluta\n\n" +
                "Välj ett alternativ:",
                "Meny", "1");

            switch (choice)
            {
                case "1":
                    CreateCustomerAndProject();
                    break;
                case "2":
                    ListProjects();
                    break;
                case "3":
                    UpdateProject();
                    break;
                case "4":
                    DeleteProject();
                    break;
                case "5":
                    ListCustomers();
                    break;
                case "6":
                    MessageBox.Show("Programmet avslutas.");
                    Application.Current.Shutdown();
                    return;
                default:
                    MessageBox.Show("Ogiltigt val, försök igen.");
                    break;
            }
        }
    }

    private void CreateCustomerAndProject()
    {
        string customerName = Microsoft.VisualBasic.Interaction.InputBox("Ange kundnamn:", "Skapa Kund");
        if (string.IsNullOrWhiteSpace(customerName))
        {
            MessageBox.Show("Kundnamn får inte vara tomt.");
            return;
        }

        var customer = _customerService.CreateCustomer(customerName);
        if (customer == null)
        {
            MessageBox.Show("Kunden kunde inte skapas.");
            return;
        }

        string projectName = Microsoft.VisualBasic.Interaction.InputBox("Ange projektnamn:", "Skapa Projekt");
        string startDateInput = Microsoft.VisualBasic.Interaction.InputBox("Ange startdatum (yyyy-MM-dd):", "Skapa Projekt");
        string endDateInput = Microsoft.VisualBasic.Interaction.InputBox("Ange slutdatum (yyyy-MM-dd):", "Skapa Projekt");
        string projectManager = Microsoft.VisualBasic.Interaction.InputBox("Ange projektansvarig:", "Skapa Projekt");
        string service = Microsoft.VisualBasic.Interaction.InputBox("Ange tjänster:", "Skapa Projekt");
        string totalPriceInput = Microsoft.VisualBasic.Interaction.InputBox("Ange totalbelopp:", "Skapa Projekt");
        string projectStatus = Microsoft.VisualBasic.Interaction.InputBox("Ange projektstatus:", "Skapa Projekt");

        if (!DateTime.TryParse(startDateInput, out DateTime startDate) || !DateTime.TryParse(endDateInput, out DateTime endDate))
        {
            MessageBox.Show("Felaktigt datumformat.");
            return;
        }

        int? totalPrice = int.TryParse(totalPriceInput, out int price) ? price : null;

        var project = new ProjectEntity
        {
            ProjectName = projectName,
            StartDate = startDate,
            EndDate = endDate,
            ProjectManager = projectManager,
            Service = service,
            TotalPrice = totalPrice,
            ProjectStatus = projectStatus,
            CustomerId = customer.Id
        };

        bool result = _projectService.CreateProject(project);
        MessageBox.Show(result ? "Projektet och kunden har skapats!" : "Projektet kunde inte skapas.");
    }

    private void ListProjects()
    {
        var projects = _projectService.GetAllProjects();
        if (projects == null || projects.Count == 0)
        {
            MessageBox.Show("Inga projekt hittades.");
            return;
        }

        string projectList = "Lista över projekt:\n";
        foreach (var project in projects)
        {
            projectList += $"Projektnummer: {project.ProjectNumber}\nID: {project.Id}\nNamn: {project.ProjectName}\nStart: {project.StartDate:yyyy-MM-dd}\nSlut: {project.EndDate:yyyy-MM-dd}\n" +
                   $"Projektansvarig: {project.ProjectManager}\nTjänst: {project.Service}\nPris: {project.TotalPrice}\nStatus: {project.ProjectStatus}\n" +
                   $"Kund ID: {project.CustomerId}\n----------------\n";
        }

        MessageBox.Show(projectList, "Alla Projekt");
    }

    private void ListCustomers()
    {
        var customers = _customerService.GetAllCustomers();
        if (customers == null || customers.Count == 0)
        {
            MessageBox.Show("Inga kunder hittades.");
            return;
        }

        string customerList = "Lista över kunder:\n";
        foreach (var customer in customers)
        {
            customerList += $"ID: {customer.Id}\nNamn: {customer.Name}\n----------------\n";
        }

        MessageBox.Show(customerList, "Alla Kunder");
    }


    private void UpdateProject()
    {
        string projectIdInput = Microsoft.VisualBasic.Interaction.InputBox("Ange projekt-ID att uppdatera:", "Uppdatera Projekt");
        if (!int.TryParse(projectIdInput, out int projectId))
        {
            MessageBox.Show("Felaktigt projekt-ID.");
            return;
        }

        var project = _projectService.GetProjectById(projectId);
        if (project == null)
        {
            MessageBox.Show("Projektet hittades inte.");
            return;
        }

        // Öppna EditProjectWindow för redigering.
        EditProjectWindow editWindow = new EditProjectWindow(_projectService, project);
        editWindow.ShowDialog();

        // Uppdatera projektet i databasen efter redigering.
        _projectService.UpdateProject(project);

        LoadProjects(); // Uppdatera projektlistan efter ändring.
    }

    private void DeleteProject()
    {
        string projectIdInput = Microsoft.VisualBasic.Interaction.InputBox("Ange projekt-ID att ta bort:", "Ta Bort Projekt");
        if (!int.TryParse(projectIdInput, out int projectId))
        {
            MessageBox.Show("Felaktigt projekt-ID.");
            return;
        }

        bool deleted = _projectService.DeleteProject(projectId);
        MessageBox.Show(deleted ? "Projektet har raderats!" : "Projektet kunde inte raderas.");
    }

    private void LoadProjectsButton_Click(object sender, RoutedEventArgs e)
    {
        LoadProjects();
    }

    private void LoadProjects()
    {
        var projects = _projectService.GetAllProjects();
        if (projects == null || projects.Count == 0) 
        {
            MessageBox.Show("Inga projekt hittades.");
            return;
        }
        
        ProjectsListBox.ItemsSource = projects;
        ProjectsListBox.DisplayMemberPath = "ProjectName"; // Visar projektets namn i listan.
    }

    private void EditProjectButton_Click(object sender, RoutedEventArgs e)
    {
        UpdateProject(); // Anropar 'UpdateProject' som öppnar 'EditProjectWindow'.
    }
        
    private void ProjectsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (ProjectsListBox.SelectedItem is ProjectEntity selectedProject)
        {
            EditProjectWindow editWindow = new EditProjectWindow(_projectService, selectedProject);
            editWindow.ShowDialog(); // Öppna redigeringsfönstret.

            LoadProjects();
            MessageBox.Show($"Du klickade på: {selectedProject.ProjectName}");

        }
    }

}

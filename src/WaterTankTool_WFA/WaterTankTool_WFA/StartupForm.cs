using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WaterTankTool_WFA.Entity;

namespace WaterTankTool_WFA
{
    public partial class StartupForm : Form
    {
        private readonly DIContainer _diContainer;
        private List<string> recentProjects = new List<string>();

        public StartupForm(DIContainer diContainer)
        {
            InitializeComponent();
            _diContainer = diContainer;

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        


            this.BackColor = ColorTranslator.FromHtml("#F9F9F9");

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.Font = new Font("Segoe UI", 10);

            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.FromArgb(30, 30, 30)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            this.Controls.Add(mainLayout);

            FlowLayoutPanel recentProjectsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.FromArgb(40, 40, 42),
                WrapContents = false
            };
            mainLayout.Controls.Add(recentProjectsPanel, 0, 0);

            Label recentProjectsLabel = new Label
            {
                Text = "Open Recent",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 10)
            };
            recentProjectsPanel.Controls.Add(recentProjectsLabel);





            LoadRecentProjects();
            DisplayRecentProjects(recentProjectsPanel);


            Panel copyrightPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(40, 40, 42) // Match the panel color
            };

            Label copyrightText = new Label
            {
                Text = "© 2024 SDSU - Iron Maguire. All Rights Reserved.\n",
                //"This software and its associated materials are proprietary to Iron Maguire and are protected by applicable copyright and intellectual property laws.\n" +
                //"Unauthorized use, reproduction, or distribution of this software or any of its components is strictly prohibited.\n\n" +
                //"For licensing information, please contact: im@gmail.com\n\n" +
                //"Version: 1.1.0\n" +
                //"Developed by Nikhil Chaudhary, under the guidance of Dr. Akram Jwadhari at SDSU.",
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
    TextAlign = ContentAlignment.MiddleCenter,
    Dock = DockStyle.Fill


            };
            copyrightPanel.Controls.Add(copyrightText);
            recentProjectsPanel.Controls.Add(copyrightPanel);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                WrapContents = false
            };
            mainLayout.Controls.Add(buttonPanel, 1, 0);

            Label headerLabel = new Label
            {
                Text = "Get Started",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.White,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 10)
            };
            buttonPanel.Controls.Add(headerLabel);

            Button openProjectButton = CreateStyledButton("Open a Project");
            openProjectButton.Click += OpenProjectButton_Click;
            buttonPanel.Controls.Add(openProjectButton);

            Button newProjectButton = CreateStyledButton("Create New Project");
            newProjectButton.Click += NewProjectButton_Click;
            buttonPanel.Controls.Add(newProjectButton);
        }

        private Button CreateStyledButton(string text)
        {
            Button button = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 12),
                Size = new Size(570, 50),
                BackColor = ColorTranslator.FromHtml("#2A2D34"),
                FlatStyle = FlatStyle.Popup,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 5, 0, 5)
            };
            button.FlatAppearance.BorderSize = 0;
            button.MouseEnter += (s, e) => { button.BackColor = Color.FromArgb(65, 65, 70); };
            button.MouseLeave += (s, e) => { button.BackColor = Color.FromArgb(45, 45, 48); };
            return button;
        }

        private void LoadRecentProjects()
        {
            string recentProjectsFile = "recent_projects.txt";
            if (File.Exists(recentProjectsFile))
            {
                recentProjects = File.ReadAllLines(recentProjectsFile).ToList();
            }
        }

        private void DisplayRecentProjects(FlowLayoutPanel recentProjectsPanel)
        {
            foreach (var projectPath in recentProjects)
            {
                if (File.Exists(projectPath))
                {
                    Button projectButton = new Button
                    {
                        Text = Path.GetFileName(projectPath),
                        Font = new Font("Segoe UI", 10),
                        Width = 850,
                        Height = 50,
                        BackColor = ColorTranslator.FromHtml("#383D46"),
                        FlatStyle = FlatStyle.Popup,
                        ForeColor = Color.White,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(5),
                        Margin = new Padding(0, 5, 0, 5)
                    };
                    projectButton.FlatAppearance.BorderSize = 0;
                    projectButton.Click += (s, e) => { OpenProject(projectPath); };
                    recentProjectsPanel.Controls.Add(projectButton);
                }
            }
        }

        private void OpenProjectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Project Files (*.proj)|*.proj";
                openFileDialog.Title = "Open Existing Project";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string projectPath = openFileDialog.FileName;
                    OpenProject(projectPath);
                }
            }
        }

        private void NewProjectButton_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }

        private void CreateNewProject()
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                //folderDialog.Description = "Select a location to create the new project folder";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderDialog.SelectedPath;

                    bool projectExists = Directory.EnumerateFiles(selectedFolderPath, "*.proj").Any() ||
                                         File.Exists(Path.Combine(selectedFolderPath, "project_data.db"));

                    if (projectExists)
                    {
                        MessageBox.Show("A project already exists in the selected folder. Please choose a different location or folder.",
                                        "Project Exists",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }

                    string projectName = Prompt.ShowDialog("Enter Workspace Folder Name", "Create New Project");
                    if (string.IsNullOrEmpty(projectName))
                    {
                        MessageBox.Show("Project name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string projectFolderPath = Path.Combine(selectedFolderPath, projectName);
                    Directory.CreateDirectory(projectFolderPath);



                    string projectFilePath = Path.Combine(projectFolderPath, $"{projectName}.proj");
                    File.WriteAllText(projectFilePath, "Default project content or structure.");

                    InitializeProjectDatabase(projectFolderPath);

                    string dbFilePath = Path.Combine(projectFolderPath, "project_data.db");
                    string connectionString = $"Data Source={dbFilePath};";

                    var dbContext = new WaterTankDbContext(connectionString);
                    dbContext.EnsureDatabaseCreated();
                    _diContainer.Register<WaterTankDbContext>(dbContext);

                    var mainForm = new WaterTank();
                    this.Hide();
                    mainForm.Show();

                    AddToRecentProjects(projectFilePath);
                }
            }
        }

        private void AddToRecentProjects(string projectFilePath)
        {
            if (!recentProjects.Contains(projectFilePath))
            {
                recentProjects.Insert(0, projectFilePath);
                File.WriteAllLines("recent_projects.txt", recentProjects);
            }
        }

        private void OpenProject(string projectPath)
        {
            string dbFilePath = Path.Combine(Path.GetDirectoryName(projectPath), "project_data.db");
            string connectionString = $"Data Source={dbFilePath};";

            var dbContext = new WaterTankDbContext(connectionString);
            dbContext.EnsureDatabaseCreated();
            _diContainer.Register<WaterTankDbContext>(dbContext);

            var mainForm = new WaterTank();
            this.Hide();
            mainForm.Show();
        }

        private void InitializeProjectDatabase(string projectFolderPath)
        {
            string dbFilePath = Path.Combine(projectFolderPath, "project_data.db");

            if (File.Exists(dbFilePath))
            {
                File.Delete(dbFilePath);
            }

            using (var connection = new System.Data.SQLite.SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
            {
                connection.Open();

                string createTableSQL = @"
                    CREATE TABLE IF NOT EXISTS SegmentProperties (
                        SegmentNumber INTEGER PRIMARY KEY AUTOINCREMENT,
                        SegmentName TEXT NOT NULL,
                        SegmentType TEXT NOT NULL,
                        Diameter REAL CHECK(Diameter >= 0),
                        Thickness REAL CHECK(Thickness >= 0),
                        HeightInitial REAL CHECK(HeightInitial >= 0),
                        HeightFinal REAL CHECK(HeightFinal >= 0),
                        DiameterInitial REAL CHECK(DiameterInitial >= 0),
                        DiameterFinal REAL CHECK(DiameterFinal >= 0)
                    );

                    CREATE TABLE IF NOT EXISTS MaterialProperties (
                        MaterialNumber INTEGER PRIMARY KEY AUTOINCREMENT,
                        MaterialName TEXT NOT NULL,
                        MaterialType TEXT NOT NULL,
                        Density INTEGER NOT NULL CHECK(Density >= 0),
                        ModulusOfElasticity INTEGER NOT NULL CHECK(ModulusOfElasticity >= 0),
                        TensileYieldStress INTEGER NOT NULL CHECK(TensileYieldStress >= 0),
                        TensileUltimateStress INTEGER NOT NULL CHECK(TensileUltimateStress >= 0)
                    );

                    CREATE TABLE IF NOT EXISTS TankProperties (
                        TankNumber INTEGER PRIMARY KEY AUTOINCREMENT,
                        Capacity TEXT,
                        WeightOfWater TEXT,
                        WeightOfSteel TEXT,
                        TotalWeight TEXT,
                        ProjectedArea TEXT
                    );
                ";

                using (var command = new System.Data.SQLite.SQLiteCommand(createTableSQL, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }


        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 400,
                    Height = 220,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen,
                    BackColor = ColorTranslator.FromHtml("#F9F9F9"),
                    Font = new Font("Segoe UI", 10)
                };

                Label textLabel = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = text,
                    AutoSize = true,
                    ForeColor = ColorTranslator.FromHtml("#383D46")
                };

                TextBox inputBox = new TextBox()
                {
                    Left = 20,
                    Top = 60,
                    Width = 340,
                    Font = new Font("Segoe UI", 10),
                    BackColor = ColorTranslator.FromHtml("#E5E5E5"),
                    ForeColor = ColorTranslator.FromHtml("#383D46"),
                    BorderStyle = BorderStyle.FixedSingle
                };

                Button confirmation = new Button()
                {
                    Text = "OK",
                    Left = 280,
                    Width = 90,
                    Height = 40,
                    Top = 110,
                    DialogResult = DialogResult.OK,
                    BackColor = ColorTranslator.FromHtml("#2A2D34"),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                confirmation.FlatAppearance.BorderSize = 0;
                confirmation.Click += (sender, e) => { prompt.Close(); };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
            }
        }

            private void StartupForm_Load(object sender, EventArgs e)
        {
            // Any additional logic for when the StartupForm loads
        }
    }
}

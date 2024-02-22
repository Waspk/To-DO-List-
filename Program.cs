



namespace ToDoList
{
    class Task
    {
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public string? Project { get; set; }
    }

    class Program
    {
        static List<Task> tasks = new List<Task>();
        static string dataFilePath = "tasks.txt";
        private static DateTime newDueDate;

        static void Main(string[] args)
        {
            LoadTasks();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Welcome to ToDoList");
                int pendingTasks = tasks.Count(task => !task.IsDone);
                int completedTasks = tasks.Count - pendingTasks;
                Console.WriteLine($"* You have {pendingTasks} tasks todo and {completedTasks} tasks are done!");
                Console.WriteLine("* Pick an option:");
                Console.WriteLine("(1) Show Task List (by date or project)");
                Console.WriteLine("(2) Add New Task");
                Console.WriteLine("(3) Edit Task (update, mark as done, remove)");
                Console.WriteLine("(4) Save and Quit");

                Console.Write("> ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowTaskList();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        SaveAndQuit();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void SaveAndQuit()
        {
            throw new NotImplementedException();
        }

        static void LoadTasks()
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Task task = new Task
                    {
                        Title = parts[0],
                        DueDate = DateTime.Parse(parts[1]),
                        IsDone = bool.Parse(parts[2]),
                        Project = parts[3]
                    };
                    tasks.Add(task);
                }
            }
        }

        static void SaveTasks()
        {
            List<string> lines = new List<string>();
            foreach (Task task in tasks)
            {
                string line = $"{task.Title},{task.DueDate},{task.IsDone},{task.Project}";
                lines.Add(line);
            }
            File.WriteAllLines(dataFilePath, lines);
            Console.WriteLine("Tasks saved successfully.");
        }

        static void ShowTaskList()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("TASK LIST:");
                Console.WriteLine("Title\t\tDue Date\tProject\t\tStatus");
                Console.WriteLine("----------");
                foreach (Task task in tasks.OrderBy(t => t.DueDate))
                {
                    Console.WriteLine($"{task.Title}\t\t{task.DueDate.ToShortDateString()}\t{task.Project}\t\t{(task.IsDone ? "Done" : "Pending")}");
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Enter due date (YYYY-MM-DD): ");
            DateTime dueDate;
            if (!DateTime.TryParse(Console.ReadLine(), out dueDate))
            {
                Console.WriteLine("Invalid date format. Please enter date in YYYY-MM-DD format.");
                return;
            }

            Console.Write("Enter project: ");
            string project = Console.ReadLine() ?? "";

            Task newTask = new Task
            {
                Title = title,
                DueDate = dueDate,
                IsDone = false,
                Project = project
            };

            tasks.Add(newTask);
            Console.WriteLine("Task added successfully.");
        }

        static void EditTask()
        {
            ShowTaskList();
            Console.Write("Enter the title of the task to edit: ");
            string titleToEdit = Console.ReadLine() ?? "";

            Task? taskToEdit = tasks.Find(task => task.Title == titleToEdit);
            if (taskToEdit != null)
            {
                Console.Write("Enter new title: ");
                string newTitle = Console.ReadLine() ?? "";

                Console.Write("Enter new due date (YYYY-MM-DD): ");
                DateTime newDueDate;
                if (!DateTime.TryParse(Console.ReadLine(), out newDueDate))
                {
                    Console.WriteLine("Invalid date format. Please enter date in YYYY-MM-DD format.");
                    return;
                }

                Console.Write("Enter new project: ");
                string newProject = Console.ReadLine() ?? "";

                taskToEdit.Title = newTitle;

                taskToEdit.DueDate = newDueDate;




            }


        }

    }




}
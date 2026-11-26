using TaskMate.Utilities;
using TaskMate.ViewModel;

namespace TaskMate;

public partial class taskMateApp : Form
{
    private Button tasksButton = null!;
    private Button schedulesButton = null!;

    public taskMateApp()
    {
        InitializeComponent();
        Text = "TaskMate";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(850, 600);
        AddNavigationButtons();
        ShowTasks();
    }

    private void AddNavigationButtons()
    {
        tasksButton = new Button { Text = "Tasks", Location = new Point(0, 151), Size = new Size(143, 48), FlatStyle = FlatStyle.Standard };
        schedulesButton = new Button { Text = "Schedules", Location = new Point(0, 199), Size = new Size(143, 48), FlatStyle = FlatStyle.Standard };
        panel1.Controls.Add(tasksButton);
        panel1.Controls.Add(schedulesButton);
        tasksButton.Click += (_, _) => ShowTasks();
        schedulesButton.Click += (_, _) => ShowSchedules();
    }

    private void Display(Control view, bool showScheduleButton = false)
    {
        TaskPnl.Controls.Clear();
        view.Dock = DockStyle.Fill;
        TaskPnl.Controls.Add(view);
        AddScheduleBtn.Visible = showScheduleButton;
    }

    private void ShowTasks()
    {
        Display(new TaskView(), true);
    }

    private void ShowSchedules()
    {
        Display(new ScheduleView(), true);
    }

    private void Calendar_Click(object? sender, EventArgs e)
    {
        Display(new Calendar());
    }

    private void Announcement_Click(object? sender, EventArgs e)
    {
        Display(new Announcement());
    }

    private void Overview_Click(object? sender, EventArgs e)
    {
        Announcement_Click(sender, e);
    }

    private void Task_Click(object? sender, EventArgs e)
    {
        ShowTasks();
    }

    private void AddScheduleBtn_Click(object? sender, EventArgs e)
    {
        ShowSchedules();
    }
}

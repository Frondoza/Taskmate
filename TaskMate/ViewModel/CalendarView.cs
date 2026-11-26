using System.Data;
using TaskMate.Utilities;

namespace TaskMate.ViewModel;

public partial class Calendar : UserControl
{
    private DateTime shownMonth = new(DateTime.Today.Year, DateTime.Today.Month, 1);
    private readonly TableLayoutPanel days = new() { Dock = DockStyle.Fill, ColumnCount = 7, RowCount = 7, CellBorderStyle = TableLayoutPanelCellBorderStyle.Single };
    private readonly Label monthLabel = new() { AutoSize = true, Font = new Font("Segoe UI", 15, FontStyle.Bold) };

    public Calendar()
    {
        InitializeComponent();
        BuildUi();
        RenderMonth();
    }

    private void BuildUi()
    {
        Controls.Clear(); BackColor = Color.White;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(14), RowCount = 2 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var header = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = false, Padding = new Padding(0, 0, 0, 12) };
        var previous = UiHelpers.Button("Previous"); var next = UiHelpers.Button("Next"); var today = UiHelpers.Button("Today");
        header.Controls.Add(previous); header.Controls.Add(monthLabel); header.Controls.Add(next); header.Controls.Add(today);
        previous.Click += (_, _) => { shownMonth = shownMonth.AddMonths(-1); RenderMonth(); };
        next.Click += (_, _) => { shownMonth = shownMonth.AddMonths(1); RenderMonth(); };
        today.Click += (_, _) => { shownMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); RenderMonth(); };
        root.Controls.Add(header, 0, 0);
        for (int c = 0; c < 7; c++) days.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
        days.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        for (int r = 1; r < 7; r++) days.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));
        root.Controls.Add(days, 0, 1); Controls.Add(root);
    }

    private void RenderMonth()
    {
        monthLabel.Text = shownMonth.ToString("MMMM yyyy");
        days.SuspendLayout(); days.Controls.Clear();
        string[] headers = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        for (int c = 0; c < 7; c++)
            days.Controls.Add(new Label { Text = headers[c], Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = Color.Gainsboro }, c, 0);

        var entries = Database.Query("""
            SELECT DATE(start_time) AS event_day, title AS event_title, 'Schedule' AS event_type
            FROM schedules WHERE user_id=1 AND start_time >= @from AND start_time < @to
            UNION ALL
            SELECT DATE(due_date), title, CONCAT('Task - ',status)
            FROM tasks WHERE user_id=1 AND due_date IS NOT NULL AND due_date >= @from AND due_date < @to
            """, Database.P("@from", shownMonth), Database.P("@to", shownMonth.AddMonths(1)));
        var byDay = entries.AsEnumerable().GroupBy(r => Convert.ToDateTime(r["event_day"]).Day)
            .ToDictionary(g => g.Key, g => g.Select(r => $"{r["event_type"]}: {r["event_title"]}").ToList());

        int offset = (int)shownMonth.DayOfWeek;
        int totalDays = DateTime.DaysInMonth(shownMonth.Year, shownMonth.Month);
        for (int cell = 0; cell < 42; cell++)
        {
            int day = cell - offset + 1;
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4), BackColor = Color.White, AutoScroll = true };
            if (day > 0 && day <= totalDays)
            {
                var label = new Label { Text = day.ToString(), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), Dock = DockStyle.Top };
                panel.Controls.Add(label);
                if (byDay.TryGetValue(day, out var events))
                {
                    var eventLabel = new Label { Text = string.Join("\n", events), AutoSize = true, MaximumSize = new Size(130, 0), Font = new Font("Segoe UI", 8), Dock = DockStyle.Top };
                    panel.Controls.Add(eventLabel);
                }
                if (shownMonth.Year == DateTime.Today.Year && shownMonth.Month == DateTime.Today.Month && day == DateTime.Today.Day)
                    panel.BackColor = Color.FromArgb(240, 240, 240);
            }
            else panel.BackColor = Color.FromArgb(248, 248, 248);
            days.Controls.Add(panel, cell % 7, cell / 7 + 1);
        }
        days.ResumeLayout();
    }
}

using MySqlConnector;
using TaskMate.Utilities;

namespace TaskMate.ViewModel;

public partial class TaskView : UserControl
{
    private readonly DataGridView grid = new();
    private readonly Label emptyLabel = new() { Text = "No tasks yet. Select Add Task to get started.", AutoSize = true, Anchor = AnchorStyles.None };
    private readonly Button addButton = UiHelpers.Button("Add Task");
    private readonly Button editButton = UiHelpers.Button("Edit");
    private readonly Button deleteButton = UiHelpers.Button("Delete");
    private readonly Button doneButton = UiHelpers.Button("Mark Complete");
    private readonly Button categoriesButton = UiHelpers.Button("Manage Categories");

    public TaskView()
    {
        InitializeComponent();
        BuildUi();
        LoadTasks();
    }

    private void BuildUi()
    {
        Controls.Clear();
        BackColor = Color.White;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 3, ColumnCount = 1 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var heading = UiHelpers.Heading("My Tasks");
        root.Controls.Add(heading, 0, 0);
        var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, WrapContents = true, Padding = new Padding(0, 0, 0, 10) };
        actions.Controls.AddRange(new Control[] { addButton, editButton, deleteButton, doneButton, categoriesButton });
        root.Controls.Add(actions, 0, 1);
        UiHelpers.StyleGrid(grid);
        root.Controls.Add(grid, 0, 2);
        Controls.Add(root);
        addButton.Click += (_, _) => AddTask();
        editButton.Click += (_, _) => EditTask();
        deleteButton.Click += (_, _) => DeleteTask();
        doneButton.Click += (_, _) => MarkComplete();
        categoriesButton.Click += (_, _) => { using var f = new CategoryManagerForm(); f.ShowDialog(FindForm()); LoadTasks(); };
        grid.CellDoubleClick += (_, _) => EditTask();
    }

    private int? SelectedId()
    {
        if (grid.CurrentRow?.Cells["task_id"].Value is null) return null;
        return Convert.ToInt32(grid.CurrentRow.Cells["task_id"].Value);
    }

    private void LoadTasks()
    {
        try
        {
            grid.DataSource = Database.Query("""
                SELECT t.task_id, t.title AS Task, COALESCE(c.category_name, 'Uncategorized') AS Category,
                       t.priority AS Priority, t.status AS Status, t.due_date AS Due
                FROM tasks t LEFT JOIN categories c ON c.category_id=t.category_id
                WHERE t.user_id=1 ORDER BY
                  CASE t.status WHEN 'Pending' THEN 0 WHEN 'In Progress' THEN 1 ELSE 2 END,
                  t.due_date IS NULL, t.due_date;
                """);
            if (grid.Columns["task_id"] != null) grid.Columns["task_id"].Visible = false;
            emptyLabel.Visible = grid.Rows.Count == 0;
        }
        catch (Exception ex) { ShowDbError(ex); }
    }

    private void AddTask()
    {
        using var dialog = new TaskEditorForm();
        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
        try
        {
            Database.Execute("""
                INSERT INTO tasks(user_id,category_id,title,description,priority,status,start_date,due_date)
                VALUES(1,@category,@title,@description,@priority,@status,@start,@due)
                """,
                Database.P("@category", dialog.CategoryId), Database.P("@title", dialog.TaskTitle),
                Database.P("@description", dialog.Description), Database.P("@priority", dialog.Priority),
                Database.P("@status", dialog.Status), Database.P("@start", dialog.StartDate),
                Database.P("@due", dialog.DueDate));
            LoadTasks();
        }
        catch (Exception ex) { ShowDbError(ex); }
    }

    private void EditTask()
    {
        var id = SelectedId();
        if (id is null) { MessageBox.Show("Select a task first."); return; }
        var data = Database.Query("SELECT * FROM tasks WHERE task_id=@id AND user_id=1", Database.P("@id", id));
        if (data.Rows.Count == 0) return;
        var row = data.Rows[0];
        using var dialog = new TaskEditorForm(Convert.ToInt32(row["category_id"] is DBNull ? 0 : row["category_id"]),
            row["title"].ToString()!, row["description"] is DBNull ? "" : row["description"].ToString()!,
            row["priority"].ToString()!, row["status"].ToString()!,
            row["start_date"] is DBNull ? null : Convert.ToDateTime(row["start_date"]),
            row["due_date"] is DBNull ? null : Convert.ToDateTime(row["due_date"]));
        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
        Database.Execute("""
            UPDATE tasks SET category_id=@category,title=@title,description=@description,priority=@priority,
            status=@status,start_date=@start,due_date=@due WHERE task_id=@id AND user_id=1
            """, Database.P("@category", dialog.CategoryId), Database.P("@title", dialog.TaskTitle),
            Database.P("@description", dialog.Description), Database.P("@priority", dialog.Priority),
            Database.P("@status", dialog.Status), Database.P("@start", dialog.StartDate),
            Database.P("@due", dialog.DueDate), Database.P("@id", id));
        LoadTasks();
    }

    private void DeleteTask()
    {
        var id = SelectedId();
        if (id is null) { MessageBox.Show("Select a task first."); return; }
        if (MessageBox.Show("Delete this task?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        Database.Execute("DELETE FROM tasks WHERE task_id=@id AND user_id=1", Database.P("@id", id));
        LoadTasks();
    }

    private void MarkComplete()
    {
        var id = SelectedId();
        if (id is null) { MessageBox.Show("Select a task first."); return; }
        Database.Execute("UPDATE tasks SET status='Completed' WHERE task_id=@id AND user_id=1", Database.P("@id", id));
        LoadTasks();
    }

    private void label2_Click(object? sender, EventArgs e) { }
    private void AddScheduleBtn_Click(object? sender, EventArgs e)
    {
        var schedule = new ScheduleView { Dock = DockStyle.Fill };
        Parent?.Controls.Add(schedule);
        schedule.BringToFront();
    }

    private static void ShowDbError(Exception ex) =>
        MessageBox.Show("Could not complete the database operation.\n\n" + ex.Message, "TaskMate", MessageBoxButtons.OK, MessageBoxIcon.Error);
}

internal sealed class TaskEditorForm : Form
{
    private readonly TextBox title = new() { Dock = DockStyle.Fill };
    private readonly TextBox description = new() { Dock = DockStyle.Fill, Multiline = true, Height = 70, ScrollBars = ScrollBars.Vertical };
    private readonly ComboBox category = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox priority = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox status = new() { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly DateTimePicker start = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm", ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill };
    private readonly DateTimePicker due = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm", ShowCheckBox = true, Checked = false, Dock = DockStyle.Fill };

    public string TaskTitle => title.Text.Trim();
    public string Description => description.Text.Trim();
    public int? CategoryId => category.SelectedValue is int id && id > 0 ? id : null;
    public string Priority => priority.Text;
    public string Status => status.Text;
    public DateTime? StartDate => start.Checked ? start.Value : null;
    public DateTime? DueDate => due.Checked ? due.Value : null;

    public TaskEditorForm(int categoryId = 0, string taskTitle = "", string desc = "", string prio = "Medium",
        string state = "Pending", DateTime? startDate = null, DateTime? dueDate = null)
    {
        Text = string.IsNullOrEmpty(taskTitle) ? "Add Task" : "Edit Task";
        Width = 520; Height = 570; StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false; MinimizeBox = false;
        title.Text = taskTitle; description.Text = desc;
        priority.Items.AddRange(new object[] { "Low", "Medium", "High" }); priority.SelectedItem = prio;
        status.Items.AddRange(new object[] { "Pending", "In Progress", "Completed" }); status.SelectedItem = state;
        if (startDate.HasValue) { start.Value = startDate.Value; start.Checked = true; }
        if (dueDate.HasValue) { due.Value = dueDate.Value; due.Checked = true; }
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 9 };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        AddRow(layout, 0, "Title", title, 36);
        AddRow(layout, 1, "Description", description, 85);
        AddRow(layout, 2, "Category", category, 36);
        AddRow(layout, 3, "Priority", priority, 36);
        AddRow(layout, 4, "Status", status, 36);
        AddRow(layout, 5, "Start date", start, 36);
        AddRow(layout, 6, "Due date", due, 36);
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, AutoSize = true };
        var save = UiHelpers.Button("Save"); var cancel = UiHelpers.Button("Cancel");
        save.Click += (_, _) => { if (string.IsNullOrWhiteSpace(title.Text)) { MessageBox.Show("Enter a task title."); return; } if (DueDate.HasValue && StartDate.HasValue && DueDate < StartDate) { MessageBox.Show("Due date must be after the start date."); return; } DialogResult = DialogResult.OK; Close(); };
        cancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        buttons.Controls.Add(save); buttons.Controls.Add(cancel);
        layout.Controls.Add(buttons, 1, 7);
        Controls.Add(layout);
        LoadCategories(categoryId);
    }

    private static void AddRow(TableLayoutPanel layout, int row, string label, Control control, int height)
    {
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, height));
        layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, TextAlign = ContentAlignment.MiddleLeft }, 0, row);
        layout.Controls.Add(control, 1, row);
    }

    private void LoadCategories(int selected)
    {
        var table = Database.Query("SELECT category_id, category_name FROM categories WHERE user_id=1 ORDER BY category_name");
        var source = table.Copy();
        var uncategorized = source.NewRow(); uncategorized["category_id"] = 0; uncategorized["category_name"] = "Uncategorized"; source.Rows.InsertAt(uncategorized, 0);
        category.DataSource = source; category.DisplayMember = "category_name"; category.ValueMember = "category_id";
        category.SelectedValue = selected;
    }
}

internal sealed class CategoryManagerForm : Form
{
    private readonly DataGridView grid = new();
    public CategoryManagerForm()
    {
        Text = "Manage Categories"; Width = 620; Height = 420; StartPosition = FormStartPosition.CenterParent;
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 2 };
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var actions = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill };
        var add = UiHelpers.Button("Add"); var edit = UiHelpers.Button("Edit"); var delete = UiHelpers.Button("Delete");
        actions.Controls.AddRange(new Control[] { add, edit, delete });
        UiHelpers.StyleGrid(grid); root.Controls.Add(actions, 0, 0); root.Controls.Add(grid, 0, 1); Controls.Add(root);
        add.Click += (_, _) => ChangeCategory(false); edit.Click += (_, _) => ChangeCategory(true); delete.Click += (_, _) => DeleteCategory();
        LoadCategories();
    }
    private int? Id => grid.CurrentRow?.Cells["category_id"].Value is null ? null : Convert.ToInt32(grid.CurrentRow.Cells["category_id"].Value);
    private void LoadCategories() { grid.DataSource = Database.Query("SELECT category_id,category_name AS Category,description AS Description FROM categories WHERE user_id=1 ORDER BY category_name"); grid.Columns["category_id"].Visible = false; }
    private void ChangeCategory(bool edit)
    {
        var id = edit ? Id : null;
        if (edit && id is null) { MessageBox.Show("Select a category."); return; }
        string name = "", desc = "";
        if (id.HasValue) { var r = Database.Query("SELECT category_name,description FROM categories WHERE category_id=@id", Database.P("@id", id)).Rows[0]; name = r["category_name"].ToString()!; desc = r["description"] is DBNull ? "" : r["description"].ToString()!; }
        using var f = new CategoryEditorForm(name, desc);
        if (f.ShowDialog(this) != DialogResult.OK) return;
        if (id.HasValue) Database.Execute("UPDATE categories SET category_name=@n,description=@d WHERE category_id=@id AND user_id=1", Database.P("@n", f.NameValue), Database.P("@d", f.DescriptionValue), Database.P("@id", id));
        else Database.Execute("INSERT INTO categories(user_id,category_name,description) VALUES(1,@n,@d)", Database.P("@n", f.NameValue), Database.P("@d", f.DescriptionValue));
        LoadCategories();
    }
    private void DeleteCategory()
    {
        if (Id is not int id) { MessageBox.Show("Select a category."); return; }
        if (MessageBox.Show("Delete this category? Tasks in it will become uncategorized.", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        Database.Execute("DELETE FROM categories WHERE category_id=@id AND user_id=1", Database.P("@id", id)); LoadCategories();
    }
}

internal sealed class CategoryEditorForm : Form
{
    private readonly TextBox name = new() { Dock = DockStyle.Fill };
    private readonly TextBox desc = new() { Dock = DockStyle.Fill, Multiline = true };
    public string NameValue => name.Text.Trim();
    public string DescriptionValue => desc.Text.Trim();
    public CategoryEditorForm(string n, string d)
    {
        Text = "Category"; Width = 400; Height = 240; StartPosition = FormStartPosition.CenterParent;
        name.Text = n; desc.Text = d;
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 2, RowCount = 3 };
        panel.Controls.Add(new Label { Text = "Name", AutoSize = true }, 0, 0); panel.Controls.Add(name, 1, 0);
        panel.Controls.Add(new Label { Text = "Description", AutoSize = true }, 0, 1); panel.Controls.Add(desc, 1, 1);
        var save = UiHelpers.Button("Save"); var cancel = UiHelpers.Button("Cancel");
        save.Click += (_, _) => { if (string.IsNullOrWhiteSpace(name.Text)) { MessageBox.Show("Enter a category name."); return; } DialogResult = DialogResult.OK; Close(); };
        cancel.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft }; buttons.Controls.Add(save); buttons.Controls.Add(cancel);
        panel.Controls.Add(buttons, 1, 2); Controls.Add(panel);
    }
}

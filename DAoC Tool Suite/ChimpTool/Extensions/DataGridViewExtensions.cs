using System.Data;
using DAoCToolSuite.ChimpTool.Json;
using Logger;
using DAoCToolSuite.ChimpTool.Settings;

namespace DAoCToolSuite.ChimpTool.Extensions
{
    public static class DataGridViewExtensions
    {
        internal static LogManager Logger => LogManager.Instance;

        public static SettingsManager? _settings = null;
        public static SettingsManager Settings
        {
            get
            {
                _settings ??= new SettingsManager();

                return _settings;

            }
            set => _settings = value;
        }

        public static void FastAutoSizeColumns(this DataGridView targetGrid)
        {
            // Cast out a DataTable from the target grid datasource.
            // We need to iterate through all the data in the grid and a DataTable supports enumeration.
            try
            {
                if (targetGrid.DataSource is null)
                {
                    return;
                }

                BindingSource? source = targetGrid.DataSource as BindingSource;
                //(DataTable)targetGrid.DataSource;
                if ((source?.DataSource as List<ChimpJson>)?.ToDataTable() is not DataTable gridTable)
                {
                    return;
                }
                // Create a graphics object from the target grid. Used for measuring text size.
                using Graphics gfx = targetGrid.CreateGraphics();
                if (gridTable?.Columns is null)
                {
                    return;
                }
                int gridTableCount = gridTable.Columns.Count;
                // Iterate through the columns.
                for (int i = 0; i < gridTableCount; i++)
                {
                    // Leverage Linq enumerator to rapidly collect all the rows into a string array, making sure to exclude null values.
                    string?[] colStringCollection = gridTable?.AsEnumerable()?.Where(r => r.Field<object>(i) != null)?.Select(r => r.Field<object>(i)?.ToString())?.ToArray() ?? new string[] { };
                    if (colStringCollection is null)
                    {
                        continue;
                    }
                    // Sort the string array by string lengths.
                    colStringCollection = colStringCollection.OrderBy((x) => x?.Length ?? 0).ToArray();
                    if (colStringCollection is null)
                    {
                        continue;
                    }
                    // Get the last and longest string in the array.
                    string? longestColString = colStringCollection.Last();
                    if (longestColString is null)
                    {
                        continue;
                    }
                    // Use the graphics object to measure the string size.
                    SizeF colWidth = gfx.MeasureString(longestColString, targetGrid.Font);

                    // If the calculated width is larger than the column header width, set the new column width.
                    targetGrid.Columns[i].Width = colWidth.Width > targetGrid.Columns[i].HeaderCell.Size.Width
                        ? (int)colWidth.Width
                        : targetGrid.Columns[i].HeaderCell.Size.Width;
                }
            }
            catch (System.Exception ex)
            {
                Logger.Debug(ex);
            }
        }

        public static void FormatTable(this DataGridView dataGridView, TextProgressBar? progressBar = null)
        {
            List<string> visibleColumns = Settings.DisplayedDatabaseColumnNames;
            List<string> visibleColumnHeaderNames = Settings.DisplayedDataGridViewHeaderNames;
            int rowCount = dataGridView.Rows.Count;
            int columnCount = dataGridView.Columns.Count;
            int nonVisibleIndex = visibleColumns.Count;
            if (progressBar is not null)
            {
                progressBar.MarqueeAnimationSpeed = 0;
                progressBar.Maximum = columnCount + rowCount;
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                progressBar.CustomText = "Formatting Table";
                progressBar.VisualMode = ProgressBarDisplayMode.TextAndPercentage;
                progressBar.Visible = true;
                progressBar.Refresh();
            }

            //Sets the column names, order, and what data from the DB to display.
            //Values pulled from the config file.

            if (columnCount > 0)
            {
                for (int index = 0; index < columnCount; index++)
                {
                    if (progressBar is not null)
                    {
                        progressBar.Value++;
                    }

                    DataGridViewColumn column = dataGridView.Columns[index];
                    if (!visibleColumns.Contains(column.Name))
                    {
                        column.Visible = false;
                        column.DisplayIndex = nonVisibleIndex;
                        nonVisibleIndex++;
                    }
                    else
                    {
                        column.DisplayIndex = visibleColumns.IndexOf(column.Name);
                        column.HeaderText = visibleColumnHeaderNames[visibleColumns.IndexOf(column.Name)];
                        column.ValueType = typeof(string);
                        //Logger.Debug($"{column.Name},{index},{column.DisplayIndex},{column.Visible}");
                        column.AutoSizeMode = column.Name switch
                        {
                            "MasterLevel_Name" => DataGridViewAutoSizeColumnMode.Fill,
                            _ => DataGridViewAutoSizeColumnMode.AllCells,
                        };
                    }

                }
            }

            ////Formats the row color by Realm
            if (rowCount > 0)
            {
                for (int index = 0; index < rowCount; index++)
                {
                    if (progressBar is not null)
                    {
                        progressBar.Value++;
                    }

                    DataGridViewRow? row = dataGridView.Rows[index];
                    string? realm = row?.Cells["Realm"]?.Value?.ToString();
                    if (row is null || realm is null)
                    {
                        continue;
                    }

                    switch (realm)
                    {
                        case "Albion":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkRed;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkRed;
                            break;
                        case "Hibernia":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkGreen;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkGreen;
                            break;
                        case "Midgard":
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.DarkBlue;
                            row.DefaultCellStyle.SelectionBackColor = Color.White;
                            row.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (progressBar is not null)
            {
                progressBar.Visible = false;
                progressBar.CustomText = "";
                progressBar.VisualMode = ProgressBarDisplayMode.Percentage;
                progressBar.Value = 0;
                progressBar.Refresh();
            }
        }
    }
}

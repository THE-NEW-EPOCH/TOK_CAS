using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MySpringbrook.Droid
{
    [Activity(Label = "Planner", MainLauncher = true)]
    public class Planner : Activity
    {
        ListView taskList;
        List<string> tasks = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Planner);

            Button newTask = FindViewById<Button>(Resource.Id.AddTasks);
            Button delete = FindViewById<Button>(Resource.Id.deleteButton);
            taskList = FindViewById<ListView>(Resource.Id.taskListView);

            //Populate task list
            updateTaskList();

            //Bring up add new task screen, passing null values
            newTask.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(AddTask));
                intent.PutStringArrayListExtra("taskData", null);
                StartActivity(intent);
            };

            //When an item in the task list is clicked, open up dialog for editing
            taskList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                //Get the task name
                string taskName = tasks[(int)e.Id];

                //Pull up add new task screen, passing values that are currently set
                var intent = new Intent(this, typeof(AddTask));
                intent.PutStringArrayListExtra("taskData", new string[] { taskName.Substring(0, taskName.IndexOf("due on:") - 1), taskName.Substring(taskName.IndexOf(":") + 1) });
                StartActivity(intent);
            };

            delete.Click += (object sender, EventArgs e) =>
            {
                clearFile();
            };
        }

        public static ISharedPreferencesEditor editTasks()
        {
            ISharedPreferences file = Application.Context.GetSharedPreferences("tasks", FileCreationMode.Private);
            return file.Edit();
        }

        public void updateTaskList()
        {
            ISharedPreferences file = Application.Context.GetSharedPreferences("tasks", FileCreationMode.Private);

            //Convert values from string to DateTime
            Dictionary<string, DateTime> data = new Dictionary<string, DateTime>();
            foreach (string s in file.All.Keys)
            {
                data.Add(s, DateTime.Parse(file.All[s].ToString()));
            }
            
            //Sort data according to date
            var sortedData = from entry in data orderby entry.Value ascending select entry;

            //Add data to task list
            tasks.Clear();
            foreach (KeyValuePair<string, DateTime> entry in sortedData)
            {
                string date = entry.Value.ToString();
                tasks.Add(entry.Key + " due on: " + date.Substring(0, date.IndexOf(" ")));
            }

            //Find and implement list of tasks
            taskList.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, tasks);

            Console.WriteLine("updated, " + tasks.Count);
        }

        private void clearFile()
        {
            ISharedPreferencesEditor edit = Planner.editTasks();
            edit.Clear();
            edit.Commit();
            updateTaskList();
        }
    }
}
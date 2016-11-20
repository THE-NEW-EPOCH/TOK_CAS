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
    [Activity(Label = "AddTasks", MainLauncher = true)]
    public class AddTasks : Activity
    {
        ListView assignmentList;
        List<string> assignments = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Planner);

            // Create your application here

            //clearFile();

            Button newTask = FindViewById<Button>(Resource.Id.AddTasks);
            Button delete = FindViewById<Button>(Resource.Id.deleteButton);
            assignmentList = FindViewById<ListView>(Resource.Id.assignmentListView);

            updateAssignmentList();

            newTask.Click += (object sender, EventArgs e) =>
            {
                AddTaskDialog dialog = new AddTaskDialog();
                dialog.Show(FragmentManager, "dialog");
            };

            assignmentList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                string listedAs = assignments[(int)e.Id];
                Console.WriteLine(listedAs +", "+ listedAs.Substring(listedAs.IndexOf(":") + 1));
                AddTaskDialog dialog = new AddTaskDialog();

                dialog.assignmentDueDate = DateTime.Parse(listedAs.Substring(listedAs.IndexOf(":") + 1));
                dialog.assignmentName = listedAs.Substring(0, listedAs.IndexOf("due on:") - 1);

                dialog.Show(FragmentManager, "dialog");
            };

            delete.Click += (object sender, EventArgs e) =>
            {
                clearFile();
            };
        }

        public void updateAssignmentList()
        {
            ISharedPreferences file = Application.Context.GetSharedPreferences("assignments", FileCreationMode.Private);

            //Convert values from string to DateTime
            Dictionary<string, DateTime> data = new Dictionary<string, DateTime>();
            foreach (string s in file.All.Keys)
            {
                data.Add(s, DateTime.Parse(file.All[s].ToString()));
            }
            
            //Sort data according to date
            var sortedData = from entry in data orderby entry.Value ascending select entry;

            //Add data to assignment list
            assignments.Clear();
            foreach (KeyValuePair<string, DateTime> entry in sortedData)
            {
                string date = entry.Value.ToString();
                assignments.Add(entry.Key + " due on: " + date.Substring(0, date.IndexOf(" ")));
            }

            //Find and implement list of assignments
            assignmentList.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, assignments);

            Console.WriteLine("updated");
        }

        private void clearFile()
        {
            ISharedPreferences file = Application.Context.GetSharedPreferences("assignments", FileCreationMode.Private);
            ISharedPreferencesEditor edit = file.Edit();
            edit.Clear();
            edit.Commit();
            updateAssignmentList();
        }
    }
}
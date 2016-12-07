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
    [Activity(Label = "Add Assignment")]
    public class AddTask : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddTask);

            EditText name = FindViewById<EditText>(Resource.Id.taskNameField);
            DatePicker datepicker = FindViewById<DatePicker>(Resource.Id.dueDate);
            Button addButton = FindViewById<Button>(Resource.Id.addButton);
            Button complete = FindViewById<Button>(Resource.Id.button1);
            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);

            //Get and assign name and date data passed from planner page
            IList<string> temp = Intent.Extras.GetStringArrayList("taskData") ?? new List<string>() { "", DateTime.Now.ToString() };
            string taskName = temp[0];
            DateTime taskDueDate = DateTime.Parse(temp[1]);

            name.Text = taskName;

            //Set add button text depending on whether task is new or being edited
            addButton.Text = (taskName == "") ? "Add" : "Update";

            //If this task is not being added for the first time, enable option to mark as done
            if (taskName != "")
                complete.Visibility = ViewStates.Visible;

            //If marked as completed, remove from stored tasks
            complete.Click += (object sender, EventArgs e) =>
            {
                ISharedPreferencesEditor edit = Planner.editTasks();

                edit.Remove(taskName);
                edit.Commit();

                //Switch back to planner
                var intent = new Intent(this, typeof(Planner));
                StartActivity(intent);
            };

            //datepicker.MinDate = Java.Lang.JavaSystem.CurrentTimeMillis() - 100000;
            datepicker.DateTime = taskDueDate;

            addButton.Click += (object sender, EventArgs e) =>
            {
                ISharedPreferences file = Application.Context.GetSharedPreferences("tasks", FileCreationMode.Private);

                //Check to make sure there is not already an assignment with the name entered
                if (taskName != name.Text && file.Contains(name.Text))
                {
                    //If there is, display an error
                    AlertDialog.Builder error = new AlertDialog.Builder(this);
                    error.SetTitle("Oops!");
                    error.SetMessage("You already have an task with that name.");
                    error.SetPositiveButton("OK", (senderAlert, args) => { });

                    Dialog dialog = error.Create();
                    dialog.Show();
                }
                else
                {
                    //Otherwise, save the new data
                    ISharedPreferencesEditor edit = file.Edit();

                    //If the name is being changed, remove the old name
                    if (taskName != "")
                        edit.Remove(taskName);

                    edit.PutString(name.Text, datepicker.DateTime.ToString());
                    edit.Commit();

                    //Switch back to planner
                    var intent = new Intent(this, typeof(Planner));
                    StartActivity(intent);
                }
            };
        }
    }
}
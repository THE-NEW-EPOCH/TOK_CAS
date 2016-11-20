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
    public class AddTaskDialog : DialogFragment
    {
        public string assignmentName = "";
        public DateTime assignmentDueDate = DateTime.Now;

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);

            View view = Activity.LayoutInflater.Inflate(Resource.Layout.AddTaskDialog, null);
            EditText name = view.FindViewById<EditText>(Resource.Id.assignment);
            DatePicker datepicker = view.FindViewById<DatePicker>(Resource.Id.dueDate);
            Button complete = view.FindViewById<Button>(Resource.Id.button1);

            name.Text = assignmentName;
            string originalName = name.Text;

            if (assignmentName != "")
                complete.Visibility = Android.Views.ViewStates.Visible;

            complete.Click += (object sender, EventArgs e) =>
            {
                ISharedPreferences file = Application.Context.GetSharedPreferences("assignments", FileCreationMode.Private);
                ISharedPreferencesEditor edit = file.Edit();

                edit.Remove(originalName);
                edit.Commit();

                ((AddTasks)Activity).updateAssignmentList();
                this.Dismiss();
            };

            //datepicker.MinDate = Java.Lang.JavaSystem.CurrentTimeMillis() - 100000;
            datepicker.DateTime = assignmentDueDate;

            string neutralButton;
            if (assignmentName == "")
                neutralButton = "Add";
            else
                neutralButton = "Update";

            alert.SetView(view);
            alert.SetMessage("Add Homework");
            alert.SetNeutralButton(neutralButton, delegate {
                ISharedPreferences file = Application.Context.GetSharedPreferences("assignments", FileCreationMode.Private);
                ISharedPreferencesEditor edit = file.Edit();

                Console.WriteLine(assignmentName +", "+ name.Text + ", " + originalName);
                if (assignmentName != "")
                    edit.Remove(originalName);

                edit.PutString(name.Text, datepicker.DateTime.ToString());
                edit.Commit();

                ((AddTasks)Activity).updateAssignmentList();
            });
            alert.SetNegativeButton("Cancel", delegate { });
            
            return alert.Create();
        }
    }
}
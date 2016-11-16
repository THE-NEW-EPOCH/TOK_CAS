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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Planner);

            // Create your application here

            Button newTask = FindViewById<Button>(Resource.Id.AddTasks);

            newTask.Click += (object sender, EventArgs e) =>
            {
                AddTaskDialog dialog = new AddTaskDialog();
                dialog.Show(FragmentManager, "dialog");
            };

            /*    var taskDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage("Call " + translatedNumber + "?");
                callDialog.SetNeutralButton("Call", delegate {
                    // Create intent to dial phone
                    var callIntent = new Intent(Intent.ActionCall);
                    callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                    StartActivity(callIntent);
                });
                callDialog.SetNegativeButton("Cancel", delegate { });

                // Show the alert dialog to the user and wait for response.
                callDialog.Show();
            };*/
        }
    }
}
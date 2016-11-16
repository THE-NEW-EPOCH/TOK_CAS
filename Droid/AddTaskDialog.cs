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
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);

            LayoutInflater inflater = Activity.LayoutInflater; //getActivity().getLayoutInflater();

            alert.SetView(inflater.Inflate(Resource.Layout.AddTaskDialog, null));
            alert.SetMessage("Add Homework");
            alert.SetNeutralButton("Add", delegate {
                Console.WriteLine("added");
            });
            alert.SetNegativeButton("Cancel", delegate {
                Console.WriteLine("canceled");
            });
            
            return alert.Create();
        }
    }
}
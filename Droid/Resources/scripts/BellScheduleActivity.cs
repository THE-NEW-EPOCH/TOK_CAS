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
using Android.Graphics;

namespace MySpringbrook.Droid.Resources.scripts
{
    [Activity(Label = "BellSchedule", MainLauncher = true)]
    public class BellScheduleActivity : Activity
    {
        public TableLayout table;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BellSchedule);

            Spinner daySpinner = FindViewById<Spinner>(Resource.Id.daySpinner);
            table = FindViewById<TableLayout>(Resource.Id.scheduleTable);

            //Set up spacing and alignment for text in table
            for (int i = 0; i < table.ChildCount; i++)
            {
                TableRow row = (TableRow)table.GetChildAt(i);
                //15 px above and below each row
                row.SetPadding(0, 15, 0, 15);

                //Center everything in the row
                for (int j = 0; j < 3; j++)
                    ((TextView)row.GetChildAt(j)).Gravity = GravityFlags.CenterHorizontal;
            }

            daySpinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, BellSchedule.days.Keys.ToArray());

            //Set advisory for Wednesday
            if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                daySpinner.SetSelection(1);
            else
                daySpinner.SetSelection(0);

            daySpinner.ItemSelected += (object sender, Spinner.ItemSelectedEventArgs e) => 
            {
                FindViewById<TextView>(Resource.Id.titleText).Text = "Today is " + MainActivity.aOrAn(daySpinner.SelectedItem.ToString());

                //Get formated list of period start and end times
                List<Period> periods = BellSchedule.getSchedule(daySpinner.SelectedItem.ToString());

                for (int i = 0; i < table.ChildCount - 1; i++)
                {
                    TableRow row = (TableRow)table.GetChildAt(i + 1);

                    //Set text to times, or clear if extra rows are not needed
                    if (i < periods.Count)
                    {
                        ((TextView)row.GetChildAt(0)).Text = periods[i].Name;
                        ((TextView)row.GetChildAt(1)).Text = periods[i].startTime;
                        ((TextView)row.GetChildAt(2)).Text = periods[i].endTime;

                        //Highlight current period
                        if (DateTime.Now > DateTime.Parse(periods[i].startTime) && DateTime.Now < DateTime.Parse(periods[i].endTime))
                            row.SetBackgroundColor(Color.Navy);
                        else
                            row.SetBackgroundColor(Color.Transparent);
                    }
                    else
                    {
                        for (int j = 0; j < 3; j++)
                            ((TextView)row.GetChildAt(j)).Text = "";
                    }
                }
            };
        }
    }
}
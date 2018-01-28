using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Convert_Stuff
{
    public class Time_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Unsigned_conversion_layout, container, false);

            EditText inText = view.FindViewById<EditText>(Resource.Id.input);
            TextView outText = view.FindViewById<TextView>(Resource.Id.outputText);
            Spinner inSpinner = view.FindViewById<Spinner>(Resource.Id.inSpinner);
            Spinner outSpinner = view.FindViewById<Spinner>(Resource.Id.outSpinner);

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.time_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.time_units, Resource.Layout.my_spinner);
            outSpinner.Adapter = outAdapter;
            outAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            outText.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Output");
                builder.SetMessage(Convert.ToString(outText.Text));
                builder.SetPositiveButton("Done", delegate { });
                builder.Show();
            };

            outSpinner.ItemSelected += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    outText.Text = "";
                }
            };

            var convert = view.FindViewById(Resource.Id.convert_btn);
            convert.Click += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                }
                else
                {
                    Toast.MakeText(this.Context, "Enter decimal value", ToastLength.Long).Show();
                }
            };

            return view;
        }

        private string Convert_units(double input, long input_id, long output_id)
        {
            double output = 0;
            double temp = 0;
            switch (input_id)
            {
                case 0:   //Nanoseconds
                    temp = input / 3600000000000;
                    break;
                case 1:   //Microseconds
                    temp = input / 3600000000;
                    break;
                case 2:   //Milliseconds
                    temp = input / 3600000;
                    break;
                case 3:   //Seconds
                    temp = input / 3600;
                    break;
                case 4:   //Minutes
                    temp = input / 60;
                    break;
                case 5:   //Hours
                    temp = input;
                    break;
                case 6:   //Days
                    temp = input * 24;
                    break;
                case 7:   //Weeks
                    temp = input * 168;
                    break;
                case 8:   //Years
                    temp = input * 8766;
                    break;
                case 9:  //Decades
                    temp = input * 87660;
                    break;
                case 10:  //Centuries
                    temp = input * 876600;
                    break;
                case 11:  //Millennia
                    temp = input * 8766000;
                    break;
            }
            switch (output_id)
            {
                case 0:   //Nanoseconds
                    output = temp * 3600000000000;
                    break;
                case 1:   //Microseconds
                    output = temp * 3600000000;
                    break;
                case 2:   //Milliseconds
                    output = temp * 3600000;
                    break;
                case 3:   //Seconds
                    output = temp * 3600;
                    break;
                case 4:   //Minutes
                    output = temp * 60;
                    break;
                case 5:   //Hours
                    output = temp;
                    break;
                case 6:   //Days
                    output = temp / 24;
                    break;
                case 7:   //Weeks
                    output = temp / 168;
                    break;
                case 8:   //Years
                    output = temp / 8766;
                    break;
                case 9:  //Decades
                    output = temp / 87660;
                    break;
                case 10:  //Centuries
                    output = temp / 876600;
                    break;
                case 11:  //Millennia
                    output = temp / 8766000;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}
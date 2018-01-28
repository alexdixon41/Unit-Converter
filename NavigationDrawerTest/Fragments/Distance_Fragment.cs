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
using Android.Support.V4.Widget;

namespace Convert_Stuff
{
    public class Distance_Fragment : Android.Support.V4.App.Fragment
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

            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.distance_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.distance_units, Resource.Layout.my_spinner);
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
                case 0:   //Inches
                    temp = input / 12;
                    break;
                case 1:   //Feet
                    temp = input;
                    break;
                case 2:   //Yards
                    temp = input * 3;
                    break;
                case 3:   //Miles
                    temp = input * 5280;
                    break;
                case 4:   //Nanometers
                    temp = input / 304800000;
                    break;
                case 5:   //Micrometers
                    temp = input / 304800;
                    break;
                case 6:   //Millimeters
                    temp = input / 304.8;
                    break;
                case 7:   //Centimeters
                    temp = input / 30.48;
                    break;
                case 8:   //Meters
                    temp = input / 0.3048;
                    break;
                case 9:   //Kilometers
                    temp = input * 3280.84;
                    break;
            }
            switch (output_id)
            {
                case 0:   //Inches
                    output = temp * 12;
                    break;
                case 1:   //Feet
                    output = temp;
                    break;
                case 2:   //Yards
                    output = temp / 3;
                    break;
                case 3:   //Miles
                    output = temp / 5280;
                    break;
                case 4:   //Nanometers
                    output = temp * 304800000;
                    break;
                case 5:   //Micrometers
                    output = temp * 304800;
                    break;
                case 6:   //Millimeters
                    output = temp * 304.8;
                    break;
                case 7:   //Centimeters
                    output = temp * 30.48;
                    break;
                case 8:   //Meters
                    output = temp * 0.3048;
                    break;
                case 9:   //Kilometers
                    output = temp / 3280.84;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}
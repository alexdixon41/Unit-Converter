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
    public class Temperature_Fragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Signed_conversion_layout, container, false);

            EditText inText = view.FindViewById<EditText>(Resource.Id.input);
            TextView outText = view.FindViewById<TextView>(Resource.Id.outputText);
            Spinner inSpinner = view.FindViewById<Spinner>(Resource.Id.inSpinner);
            Spinner outSpinner = view.FindViewById<Spinner>(Resource.Id.outSpinner);
           
            var inAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.temp_units, Resource.Layout.my_spinner);
            inSpinner.Adapter = inAdapter;
            inAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            var outAdapter = ArrayAdapter.CreateFromResource(container.Context, Resource.Array.temp_units, Resource.Layout.my_spinner);
            outSpinner.Adapter = outAdapter;
            outAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            outSpinner.ItemSelected += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {
                    
                    if (input < 0 && inSpinner.SelectedItemId == 2)
                    {
                        Toast.MakeText(this.Context, "Kelvin value must be positive", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else if (input < -273.15 && inSpinner.SelectedItemId == 1)
                    {
                        Toast.MakeText(this.Context, "Celsius value must be greater than -273.15", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else if (input < -459.67 && inSpinner.SelectedItemId == 0)
                    {
                        Toast.MakeText(this.Context, "Fahrenheit value must be greater than -459.67", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else
                    {
                        outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                    }
                }
                else
                {
                    outText.Text = "";
                }
            };

            outText.Click += delegate
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this.Context);
                builder.SetTitle("Output");
                builder.SetMessage(Convert.ToString(outText.Text));
                builder.SetPositiveButton("Done", delegate { });
                builder.Show();
            };

            var convert = view.FindViewById(Resource.Id.convert_btn);
            convert.Click += delegate
            {
                double input;
                bool validInput = Double.TryParse(inText.Text, out input);
                if (validInput)
                {                 
                    if (input < 0 && inSpinner.SelectedItemId == 2)
                    {
                        Toast.MakeText(this.Context, "Kelvin value must be positive", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else if (input < -273.15 && inSpinner.SelectedItemId == 1)
                    {
                        Toast.MakeText(this.Context, "Celsius value must be greater than -273.15", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else if (input < -459.67 && inSpinner.SelectedItemId == 0)
                    {
                        Toast.MakeText(this.Context, "Fahrenheit value must be greater than -459.67", ToastLength.Long).Show();
                        outText.Text = "Error";
                    }
                    else
                    {
                        outText.Text = Convert_units(input, inSpinner.SelectedItemId, outSpinner.SelectedItemId);
                    }                   
                }
                else
                {
                    Toast.MakeText(this.Context, "Enter decimal value", ToastLength.Long).Show();
                    outText.Text = "";
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
                case 0:   // Fahrenheit
                    temp = (input - 32) / 1.8;
                    break;
                case 1:   // Celsius
                    temp = input;
                    break;
                case 2:   // Kelvin
                    temp = input - 273.15;
                    break;
            }
            switch (output_id)
            {
                case 0:   // Fahrenheit
                    output = (temp * 1.8) + 32;
                    break;
                case 1:   // Celsius
                    output = temp;
                    break;
                case 2:   // Kelvin
                    output = temp + 273.15;
                    break;
            }
            return Convert.ToString(Math.Round(output, 10, MidpointRounding.AwayFromZero));
        }
    }
}